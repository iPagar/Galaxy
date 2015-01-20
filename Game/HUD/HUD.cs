using UnityEngine;
using System.Collections;
using BM;

public class HUD : MonoBehaviour {

	private Player player;

	public void OnClick(){
		player.nextStep ();
	}

	void Start () {
		player = transform.root.GetComponent< Player >();
	}

	/*public GUISkin resourceSkin, ordersSkin, encounterSkin;
	private const int ORDERS_BAR_WIDTH = 150, RESOURCE_BAR_HEIGHT = 40, ENCOUNTER_BAR_HEIGHT = 40;
	private const int SELECTION_NAME_HEIGHT = 25;
	private GUIStyle style;

	// Use this for initialization


	void OnGUI() {
		if(player && player.human) {
			style = new GUIStyle();
			#if UNITY_EDITOR || UNITY_STANDALONE
			style.fontSize = 14;
			#elif UNITY_IOS
			style.fontSize = 30;
			#endif
			style.normal.textColor = Color.white;
			//drawOrdersBar();
			drawResourcesBar();
			drawStepsEncounter();
			drawTimeBar();
			drawPlanetPanel();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Rect screenRelativeRect(float left, float top, float width, float height) {
		Rect r = new Rect(Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height);
		return r;
	}

	private void drawOrdersBar() {
		GUI.skin = ordersSkin;
		string selectionName = "";
		if(player.SelectedPlanet) {
			selectionName = player.SelectedPlanet.name;
		}
		if(!selectionName.Equals("")) {
			GUI.Label(new Rect(0,10,ORDERS_BAR_WIDTH,SELECTION_NAME_HEIGHT), selectionName);
		}
		GUI.BeginGroup(new Rect(Screen.width-ORDERS_BAR_WIDTH,RESOURCE_BAR_HEIGHT,ORDERS_BAR_WIDTH,Screen.height-RESOURCE_BAR_HEIGHT));
		GUI.Box(new Rect(0,0,ORDERS_BAR_WIDTH,Screen.height-RESOURCE_BAR_HEIGHT),"");
		GUI.EndGroup();
	}

	private void drawResourcesBar() {
		GUI.skin = resourceSkin;
		GUI.BeginGroup(screenRelativeRect(0, 0, 1, 0.1f), "");
		GUI.Box(screenRelativeRect(0, 0, 1, 0.1f), "");
		GUI.Label(screenRelativeRect(0, 0, 1, 0.1f), player.name, style);
		GUI.EndGroup();
	}

	private void drawTimeBar() {
		GUI.skin = resourceSkin;
		GUI.BeginGroup(screenRelativeRect(0, 0, 1, 0.1f), "");
		GUI.Label(screenRelativeRect(0.93f, 0, 1, 0.1f), player.Month.ToString(), style);
		GUI.Label(screenRelativeRect(0.96f, 0, 1, 0.1f), player.year.ToString(), style);
		GUI.EndGroup();
	}

	private void drawPlanetPanel() {
		if(BoolManager.isZoomedToPlanet){
			GUI.skin = resourceSkin;
			GUI.BeginGroup(screenRelativeRect(0, 0.8f, 0.5f, 0.2f), "");
			GUI.Box(screenRelativeRect(0, 0, 0.5f, 0.2f), "");
			GUI.Label(screenRelativeRect(0, 0, 0.4f, 0.1f), player.SelectedPlanetarySystem.name, style);
			GUI.Label(screenRelativeRect(0, 0.05f, 0.4f, 0.1f), player.SelectedPlanet.name, style);
			GUI.Label(screenRelativeRect(0, 0.08f, 0.4f, 0.1f), "Население: ", style);
			GUI.Label(screenRelativeRect(0.1f, 0.08f, 0.4f, 0.1f), player.SelectedPlanet.population.ToString(), style);
			GUI.EndGroup();
		}
	}

	private void drawStepsEncounter() {
		GUI.skin = encounterSkin;
		if()){
			player.nextStep();
		}
	}
	
	//мышка в игровой области
	public bool MouseInBounds() {
		//Screen coordinates start in the lower-left corner of the screen
		//not the top-left of the screen like the drawing coordinates do
		#if UNITY_EDITOR || UNITY_STANDALONE
		Vector3 mousePos = Input.mousePosition;
		#elif UNITY_IOS
		Vector3 mousePos = Input.GetTouch(0).position;
		#endif
		bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.height - Screen.height * 0.1f;
		bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.width - Screen.width * 0.9f;
		return insideWidth && insideHeight;
	}
	*/
}
