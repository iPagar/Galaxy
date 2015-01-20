using UnityEngine;
using System.Collections;
using RTS;
using BM;

public class Star : Planets {
	public override void Initialize(int orbit){
		name = nameGeneration ();
		renderer.material.color = colorGeneration();
		scale = Random.Range (ResourceManager.MinScale, ResourceManager.MaxScale);
		transform.localScale = new Vector3 (scale, scale, scale);
	}

	public void renderStar() {
		if(BoolManager.isZoomedToGalaxy){
			renderer.enabled = true;
		} else if(BoolManager.isZoomedToPlanetarySystem){
			if(GetComponent<Star>() == player.SelectedPlanetarySystem.star){
				renderer.enabled = true;
			} else {
				renderer.enabled = false;
			}
		}
	}
}
