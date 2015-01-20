using UnityEngine;
using System.Collections;
using RTS;
using BM;

public class Galaxy : WorldObject {

	public PlanetarySystem[] systems;

	// Use this for initialization
	protected override void Awake () {
		base.Start ();
		name = "Galaxy";
		setupPlanetarySystems ();
	}

	protected override void Start(){
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public void setupPlanetarySystems(){
		systems = new PlanetarySystem[Random.Range (ResourceManager.MinNumberOfPlanetarySystems, ResourceManager.MaxNumberOfPlanetarySystems)]; //массив из рандомного числа планетарных систем
		for (int y = 0; y < systems.Length; y++) {
			//float d = 5 * Mathf.Pow(2.0f, (float)y); //Правило Тициуса — Боде
			systems[y] = ObjectFactory.CreatePlanetarySystem(gameObject, y * 5 + 3);
		}
	}

	public void renderStars(){
		for (int y = 0; y < systems.Length; y++) {
			systems[y].star.renderStar();
		}
	}
}
