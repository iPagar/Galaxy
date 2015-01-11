using UnityEngine;
using System.Collections;
using RTS;

public class Player : MonoBehaviour {

	public string username;
	public bool human;
	public HUD hud;
	public WorldObject SelectedObject { get; set; }

	// Use this for initialization
	void Start () {
		hud = GetComponentInChildren<HUD>();
		Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
