using UnityEngine;
using System.Collections;
using RTS;
using UnityEngine.UI;
using BM;

public class Planet : Planets {

	private Moon moon;
	public float radius;
	private LineRenderer lineRenderer;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		drawOrbit ();
		renderPlanet();
		//setupMoon ();
		//renderName ();
	}

	private void renderName(){

	}

	private void setupMoon(){	
		//moon = ObjectFactory.CreateMoon (gameObject);
	}

	protected void drawOrbit(){
		int vertexCount = 361;
		
		gameObject.AddComponent("LineRenderer");
		lineRenderer = GetComponent<LineRenderer> ();
	    Color c1 = new Color(0.5f, 0.5f, 0.5f, 1f);
		//lineRenderer.material = new Material();
		lineRenderer.SetColors(c1, c1);
		lineRenderer.SetWidth(0.05f, 0.05f);
		lineRenderer.SetVertexCount(vertexCount);
		
		int i = 0;
		
		for(float angle = 0.0f; angle <= 360; angle++)
		{
			float x = transform.TransformDirection(transform.parent.position).x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
			float y = transform.TransformDirection(transform.parent.position).z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);
			float z = 0;
			Vector3 pos = new Vector3(x, z, y);
			
			lineRenderer.SetPosition(i, pos);
			i += 1;
		}
	}

	public override void Initialize(int orbit){
		speed = ResourceManager.CoffOrbitDeegres / (orbit + 1);
		name = nameGeneration ();
		Color f = colorGeneration ();
		f.a = 0.0f;
		renderer.material.color = f;
		scale = Random.Range (ResourceManager.MinScale, ResourceManager.MaxScale);
		transform.localScale = new Vector3 (scale, scale, scale);
		radius = orbit;
		int angle = Random.Range (0, 360);
		float x = transform.parent.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
		float y = transform.parent.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);
		float z = 0;
		transform.position = new Vector3 (x, z, y);
	}

	public void renderPlanet() {
		if(BoolManager.isZoomedToGalaxy){
			renderer.enabled = false;
			transform.GetComponent<LineRenderer>().enabled = false;
		} else if(BoolManager.isZoomedToPlanetarySystem){
			renderer.enabled = true;
			transform.GetComponent<LineRenderer>().enabled = true;
		}
	}
}
