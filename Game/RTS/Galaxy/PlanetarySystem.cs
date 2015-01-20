using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS; 
using BM;

public class PlanetarySystem : WorldObject {
	
	public Planet[] planets; //массив из рандомного числа планет;
	public Star star;

	// Use this for initialization
	protected override void Awake () {
		base.Start();
		setupPlanets ();
	}

	private void setupPlanets(){
		planets = new Planet[Random.Range (ResourceManager.MinNumberOfPlanets, ResourceManager.MaxNumberOfPlanets)]; //массив из рандомного числа планет
		star = ObjectFactory.CreateStar(gameObject, 0);
		for (int y = 0; y < planets.Length; y++) {
			//float d = 5 * Mathf.Pow(2.0f, (float)y); //Правило Тициуса — Боде
			planets[y] = ObjectFactory.CreatePlanet(star.gameObject, y * 2 + 3);
		}
	}

	public void renderPlanets(){
		for (int y = 0; y < planets.Length; y++) {
			planets[y].renderPlanet();
		}
	}

	protected static string nameGeneration() {
		string[] planetNames = new string[]
		{
			"Bak'hur",   "Etonia",      "Laurellan",
			"Ragki",     "Metiope",     "Defel", 
			"Liehne",    "Rykhan",      "Heito",
			"Makha",     "Cerbi",       "Helios",
			"Luruguan",  "Chawnos",     "T'hig",
			"Blana",     "Pergate V",   "Shemon III",
			"Yokteth",   "Letry",       "Last Besgino",
			"Ayus",      "Alterus",     "Qurenos"
			
		};
		
		return planetNames[Random.Range(0, planetNames.Length)];
	}

	public virtual void Initialize(int position){
		name = nameGeneration ();
	}
}
