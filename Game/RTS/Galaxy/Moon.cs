using UnityEngine;
using System.Collections;
using RTS;

public class Moon : Planets {

	public GameObject parentL;

	// Use this for initialization
	protected override void Start () {
		parentL = transform.parent.gameObject;
	}
	
	public override void Initialize(int orbit = 1){
		speed = ResourceManager.CoffOrbitDeegres / orbit;
		name = nameGeneration ();
		renderer.material.color = colorGeneration();
		scale = Random.Range (ResourceManager.MinScaleMoon, ResourceManager.MaxScaleMoon);
		transform.localScale = new Vector3 (scale, scale, scale);
	}
}
