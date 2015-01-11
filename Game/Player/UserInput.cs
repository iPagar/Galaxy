using UnityEngine;
using System.Collections;
using RTS;

public class UserInput : MonoBehaviour {

	private Player player;
	private bool drag = false;
	private bool zoom = false;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent< Player >();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.human) {
			moveCamera();
			depthCamera();
			mouseActivity();
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

	private void rightMouseClick() {
		if(player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject) {
			player.SelectedObject.SetSelection(false);
			player.SelectedObject = null;
		}
	}

	private void leftMouseClick() {
		if(player.hud.MouseInBounds()) {
			GameObject hitObject = FindHitObject();
			Vector3 hitPoint = FindHitPoint();
			if(hitObject && hitPoint != ResourceManager.InvalidPosition) {
				if(hitObject.transform.GetComponent<WorldObject>()){
					hitObject.transform.GetComponent<WorldObject>().MouseClick(hitObject, hitPoint, player);
				}
			}
		}
	}

	private void mouseActivity() {
		if(Input.GetMouseButtonDown(0)) leftMouseClick();
		else if(Input.GetMouseButtonDown(1)) rightMouseClick();
	}

	private void moveCamera (){
		if ((Input.GetMouseButton(0)) || (Input.touchCount == 1)) { //0 - ЛКМ
			#if UNITY_EDITOR || UNITY_STANDALONE
			Vector3 positionA = new Vector3(Input.GetAxis("Mouse X"), 0, 
			                                Input.GetAxis("Mouse Y"));

			#elif UNITY_IOS
			Vector3 positionA = new Vector3(Input.touches[0].deltaPosition.x, 0, 
			                                Input.touches[0].deltaPosition.y);
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + positionA, 0.0f);
			#endif

			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + positionA * ResourceManager.DragSpeed, Time.deltaTime);
		}
	}

	private void depthCamera (){
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = new Vector3(0, 0, 0);

		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			destination = new Vector3 (origin.x, ResourceManager.MaxCameraHeight, origin.z);
			Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
			//Debug.Log (Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed));
		} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			destination = new Vector3 (origin.x, ResourceManager.MinCameraHeight, origin.z);
			Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
			//Debug.Log (Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed));
		}
	}
}