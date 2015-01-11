using UnityEngine;
using System.Collections;
using RTS;

public class WorldObject : MonoBehaviour {

	protected Player player;
	protected string[] actions = {};
	protected bool currentlySelected = false;

	protected virtual void Awake() {
		
	}
	
	protected virtual void Start () {
		player = transform.root.GetComponentInChildren< Player >();
	}
	
	protected virtual void Update () {
		
	}
	
	protected virtual void OnGUI() {

	}

	public string[] GetActions() {
		return actions;
	}
	
	public virtual void PerformAction(string actionToPerform) {
		//it is up to children with specific actions to determine what to do with each of those actions
	}

	public void SetSelection(bool selected) {
		currentlySelected = selected;
	}

	public void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller) {
		clickOnObject (hitObject, hitPoint, controller);
	}

	protected virtual void clickOnObject(GameObject hitObject, Vector3 hitPoint, Player controller) {
		//only handle input if currently selected
		if(currentlySelected && hitObject && controller) {
			WorldObject worldObject = hitObject.transform.GetComponent<WorldObject>();
			//clicked on another selectable object
			if(worldObject) ChangeSelection(worldObject, controller);
		} else if(!currentlySelected && hitObject && controller){
			WorldObject worldObject = hitObject.transform.GetComponent<WorldObject>();
			if(worldObject) {
				//we already know the player has no selected object
				controller.SelectedObject = worldObject;
				worldObject.SetSelection(true);
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, hitObject.transform.position + new Vector3(0, 0, 0), Time.deltaTime);
				Camera.main.transform.parent = worldObject.transform;
				Camera.main.transform.LookAt(worldObject.transform.GetComponentInParent<PlanetarySystem>().star.transform);
			}
		}
	}
	
	protected virtual void ChangeSelection(WorldObject worldObject, Player controller) {
		//this should be called by the following line, but there is an outside chance it will not
		SetSelection(false);
		if(controller.SelectedObject) controller.SelectedObject.SetSelection(false);
		controller.SelectedObject = worldObject;
		worldObject.SetSelection(true);
	}
}
