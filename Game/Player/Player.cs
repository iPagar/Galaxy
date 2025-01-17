using UnityEngine;
using System.Collections;
using RTS;
using BM;

public class Player : MonoBehaviour {

	public string username;
	public bool human;
//	public HUD hud;
	public Galaxy galaxy;
	public Planet SelectedPlanet { get; set; }
	public PlanetarySystem SelectedPlanetarySystem { get; set; }
	public PlanetarySystem[] territory;
	public static int countSteps = 0;
	public Vector3 oldPosCam { get; set; }
	public PlanetarySystem oldSelectedPlanetarySystem { get; set; }
	public enum Months : int{ Jan = 1, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec }; 
	public Months Month;
	public int year = 2256;

	// Use this for initialization
	void Start () {
//		hud = GetComponentInChildren<HUD>();
		territory = new PlanetarySystem[galaxy.systems.Length];
		territory.SetValue (galaxy.systems [Random.Range (0, galaxy.systems.Length - 1)], 0);
		SelectedPlanetarySystem = territory [0];
		oldSelectedPlanetarySystem = SelectedPlanetarySystem;
		oldPosCam = Camera.main.transform.position;
	}

	void Awake(){
		name = "Название страны";
		Month = Months.Jan;
		Application.targetFrameRate = 60;
		BoolManager.isZoomedToGalaxy = true;
		galaxy = GetComponentInChildren<Galaxy>();
		BoolManager.isStepping = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void nextStep(){
		if(!BoolManager.isStepping && !BoolManager.isZooming){
			BoolManager.isStepping = true;
			nextMonthAndYear ();
			movePlanetsAndOther ();
		}
	}

	private void nextMonthAndYear(){
		Month++;
		if(Month == Months.Dec + 1)
			Month = Months.Jan;
		if(Month == Months.Jan)
			year++;
	}

	private void movePlanetsAndOther(){
		for(int y = 0; y < territory.Length; y++){
			if(territory[y] != null){
				Planet[] planets = territory[y].planets;
				
				for(int i = 0; i < planets.Length; i++){
					planets[i].movePlanets();
				}
			}
		}
	}
}