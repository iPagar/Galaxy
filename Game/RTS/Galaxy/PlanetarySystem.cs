using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS; 

public class PlanetarySystem : WorldObject {
	
	private Planets[] planets; //массив из рандомного числа планет;
	public Planets star;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		setupPlanets ();
	}

	private void setupPlanets(){
		planets = new Planets[Random.Range (ResourceManager.MinNumberOfPlanets, ResourceManager.MaxNumberOfPlanets)]; //массив из рандомного числа планет
		star = ObjectFactory.CreateStar(gameObject, 0);
		for (int y = 0; y < planets.Length; y++) {
			planets[y] = ObjectFactory.CreatePlanet(star.gameObject, y);
		}
	}
}
