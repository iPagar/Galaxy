using UnityEngine;
using System.Collections;
using RTS;

public class Planet : Planets {

	private Moon moon;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		drawOrbit ();
		setupMoon ();
	}

	private void setupMoon(){	
		moon = ObjectFactory.CreateMoon (gameObject);
	}

	protected void drawOrbit(){
		float radius = Vector3.Distance(transform.position, transform.parent.position);
		int vertexCount = 361;
		
		gameObject.AddComponent("LineRenderer");
		LineRenderer lineRenderer = GetComponent<LineRenderer> ();
	    Color c1 = new Color(0.5f, 0.5f, 0.5f, 1f);
		//lineRenderer.material = new Material();
		lineRenderer.SetColors(c1, c1);
		lineRenderer.SetWidth(0.05f, 0.05f);
		lineRenderer.SetVertexCount(vertexCount);
		
		int i = 0;
		
		for(float angle = 0.0f; angle <= 360; angle++)
		{
			float x = transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
			float y = transform.position.y + radius * Mathf.Sin(Mathf.Deg2Rad * angle);
			float z = transform.position.z;
			Vector3 pos = new Vector3(x, 0, y);
			
			lineRenderer.SetPosition(i, pos);
			i += 1;
		}
	}

	public override void Initialize(int orbit = 1){
		speed = ResourceManager.CoffOrbitDeegres / (orbit + 1);
		name = nameGeneration ();
		renderer.material.color = colorGeneration();
		scale = Random.Range (ResourceManager.MinScale, ResourceManager.MaxScale);
		transform.localScale = new Vector3 (scale, scale, scale);
	}
}
