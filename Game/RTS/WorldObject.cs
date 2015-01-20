using UnityEngine;
using System.Collections;
using RTS;
using BM;

public class WorldObject : MonoBehaviour {

	protected Player player;
	protected UserInput userInput;

	protected virtual void Awake() {
		
	}
	
	protected virtual void Start () {
		player = transform.root.GetComponentInChildren< Player >();
	}
	
	protected virtual void Update () {

	}

	protected virtual void OnGUI() {

	}
}
