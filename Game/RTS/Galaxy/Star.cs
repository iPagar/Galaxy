using UnityEngine;
using System.Collections;
using RTS;

public class Star : Planets {
	public override void Initialize(int orbit){
		name = nameGeneration ();
		renderer.material.color = colorGeneration();
		scale = Random.Range (ResourceManager.MinScale, ResourceManager.MaxScale);
		transform.localScale = new Vector3 (scale, scale, scale);
	}
}
