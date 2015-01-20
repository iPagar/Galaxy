using UnityEngine;
using System.Collections;
using RTS;
using BM;

public class UserInput : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent< Player >();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(player.human) {
			depthCamera();
			mouseActivity();
			moveCamera();
		}
	}

	private Vector3 FindHitPoint() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)) return hit.point;
		return ResourceManager.InvalidPosition;
	}

	private GameObject FindHitObject() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)) return hit.collider.gameObject;
		return null;
	}

	private void leftMouseClick() {
		//if(player.hud.MouseInBounds()) {
			GameObject hitObject = FindHitObject();
			Vector3 hitPoint = FindHitPoint();
			if(hitObject && hitPoint != ResourceManager.InvalidPosition) {
				if(BoolManager.isZoomedToGalaxy){
					if(hitObject.transform.GetComponent<Star>()){
						player.SelectedPlanetarySystem = hitObject.transform.parent.GetComponentInChildren<PlanetarySystem> ();
						StartCoroutine(MoveCameraToPlanetarySystem());
					}
				} else if(BoolManager.isZoomedToPlanetarySystem){
					if(hitObject.transform.GetComponent<Planet>()){
						player.SelectedPlanet = hitObject.transform.GetComponent<Planet>();
						StartCoroutine(MoveCameraToPlanet());
					}
				}
			}
		//} 
	}

	private void mouseActivity() {
		if(Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
			leftMouseClick();
		}
	}

	private void moveCamera (){
		if ((Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)) && BoolManager.isZoomedToGalaxy && !BoolManager.isZooming == false) { //0 - ЛКМ
			#if UNITY_EDITOR || UNITY_STANDALONE
			Vector3 positionA = new Vector3(Input.GetAxis("Mouse X"), 0, 
			                                Input.GetAxis("Mouse Y"));
			
			#elif UNITY_IOS
			Vector3 positionA = new Vector3(Input.touches[0].deltaPosition.x, 0, 
			                                Input.touches[0].deltaPosition.y);
			#endif
			
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + positionA, Time.deltaTime);
		}

		if (!(BoolManager.isStepping || BoolManager.isZooming) && (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)) && BoolManager.isZoomedToPlanetarySystem) { //0 - ЛКМ
			Vector3 angles = Camera.main.transform.eulerAngles;
			float x = angles.y;
			float y = angles.x;
			
			#if UNITY_EDITOR || UNITY_STANDALONE
			x += Input.GetAxis("Mouse X") * ResourceManager.DragSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ResourceManager.DragSpeed * 0.02f;
			
			#elif UNITY_IOS
			x += Input.touches[0].deltaPosition.x * ResourceManager.ScrollSpeed * 0.02f;
			y -= Input.touches[0].deltaPosition.y * ResourceManager.ScrollSpeed * 0.02f;
			
			#endif
			
			y = ClampAngle(y, ResourceManager.yMinLimit, ResourceManager.yMaxLimit);
			Debug.Log(y);
			Quaternion rotation = Quaternion.Euler(y, 180, 0);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -Vector3.Distance(player.SelectedPlanetarySystem.planets[player.SelectedPlanetarySystem.planets.Length - 1].transform.position * 2, player.SelectedPlanetarySystem.star.transform.position)) + player.SelectedPlanetarySystem.star.transform.position;
			Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, rotation, ResourceManager.DragSpeed * Time.deltaTime);
			Camera.main.transform.position = Vector3.Slerp (Camera.main.transform.position, position, ResourceManager.DragSpeed * Time.deltaTime);
		}

	}

	private static float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}

	private void depthCamera (){
		#if UNITY_EDITOR || UNITY_STANDALONE
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			if(BoolManager.isZoomedToPlanet && !BoolManager.isZooming){
				StartCoroutine(MoveCameraToPlanetarySystem());
			} else if(BoolManager.isZoomedToPlanetarySystem && !BoolManager.isZooming){
				StartCoroutine(MoveCameraToGalaxy());
			}
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			if(BoolManager.isZoomedToGalaxy && !BoolManager.isZooming){
				StartCoroutine(MoveCameraToPlanetarySystem());
			}
		}

		#elif UNITY_IOS
		if((Input.touchCount == 2) && (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)){
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
		
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if(deltaMagnitudeDiff > 0){
				if(BoolManager.isZoomedToPlanet && !BoolManager.isZooming){
					StartCoroutine(MoveCameraToPlanetarySystem());
				} else if(BoolManager.isZoomedToPlanetarySystem && !BoolManager.isZooming){
					StartCoroutine(MoveCameraToGalaxy());
				}
			} 

			if (deltaMagnitudeDiff < 0) {
				if(BoolManager.isZoomedToGalaxy && !BoolManager.isZooming){
					StartCoroutine(MoveCameraToPlanetarySystem());
				}
			}
		}
		#endif
	}
	
	protected IEnumerator MoveCameraToGalaxy(float overTime = 1.0f)
	{
		BoolManager.isZooming = true;
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, player.oldPosCam, (Time.time - startTime)/overTime);
			Quaternion targetRotation = Quaternion.Euler(90, 180, 0);
			Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, targetRotation, (Time.time - startTime)/overTime);
			yield return null;
		}
		BoolManager.isZooming = false;
		BoolManager.isZoomedToPlanetarySystem = false;
		BoolManager.isZoomedToGalaxy = true;
		player.galaxy.renderStars();
		player.SelectedPlanetarySystem.renderPlanets ();
	}
	
	protected IEnumerator MoveCameraToPlanetarySystem(float overTime = 1.0f)
	{
		if(BoolManager.isZoomedToGalaxy)
			player.oldPosCam = Camera.main.transform.position;
		
		player.oldSelectedPlanetarySystem = player.SelectedPlanetarySystem;
		BoolManager.isZooming = true;
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			Quaternion rotation = Quaternion.Euler(80, 180, 0);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -Vector3.Distance(player.SelectedPlanetarySystem.planets[player.SelectedPlanetarySystem.planets.Length - 1].transform.position * 2, player.SelectedPlanetarySystem.star.transform.position)) + player.SelectedPlanetarySystem.star.transform.position;
			Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, rotation, (Time.time - startTime)/overTime);
			Camera.main.transform.position = Vector3.Slerp (Camera.main.transform.position, position, (Time.time - startTime)/overTime);
			yield return null;
		}
		BoolManager.isZooming = false;
		BoolManager.isZoomedToPlanet = false;
		BoolManager.isZoomedToPlanetarySystem = true;
		BoolManager.isZoomedToGalaxy = false;
		player.SelectedPlanetarySystem.renderPlanets();
		player.galaxy.renderStars();
	}

	float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		
		// angle in [-179,180]
		float signed_angle = angle * sign;
		
		// angle in [0,360] (not used but included here for completeness)
		//float angle360 =  (signed_angle + 180) % 360;
		
		return signed_angle;
	}

	protected IEnumerator MoveCameraToPlanet(float overTime = 1.0f)
	{
		BoolManager.isZooming = true;
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			/*float x = player.SelectedPlanet.transform.position.x;
			float y = player.SelectedPlanet.transform.position.z;
			Vector3 relativePos = player.SelectedPlanetarySystem.star.transform.position - Camera.main.transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, rotation, (Time.time - startTime)/overTime);
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(x, 1, y), (Time.time - startTime)/overTime);*/
			yield return null;
		}
		BoolManager.isZooming = false;
		BoolManager.isZoomedToPlanetarySystem = false;
		BoolManager.isZoomedToPlanet = true;
	}
}
