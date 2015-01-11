using UnityEngine;
using System.Collections;
using RTS;

public class Planets : WorldObject {

	public Texture2D myTexture;
	
	protected int science = 0;
	protected int military = 0;
	protected int indusrty = 0;

	protected float speed = 0;
	protected float scale;

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		movePlanet ();
	}

	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * ( point - pivot) + pivot;
	}
	
	protected void movePlanet(){
		transform.position = 
			RotatePointAroundPivot (transform.position,
			                        transform.parent.position,
			                        Quaternion.Euler (0, speed * Time.deltaTime, 0));
	}

	public virtual void Initialize(int orbit){

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

	protected static Color colorGeneration() {
		Color[] planetColors = new Color[]
		{
			Color.red,  
			Color.blue,    
			Color.black,   
			Color.cyan,    
			Color.green, 
			Color.yellow,  
			Color.white,   
			Color.magenta    
			
		};
		
		return planetColors[Random.Range(0, planetColors.Length)];
	}


}
