using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour 
{
	protected static ObjectFactory instance; // Needed
	
	void Start()
	{
		instance = this;
	}
	
	public static Planet CreatePlanet(GameObject parent, int orbit)
	{
		GameObject planet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		planet.AddComponent ("Planet");
		planet.transform.parent = parent.transform;
		planet.transform.position = new Vector3 (planet.transform.parent.position.x, 0, orbit * 2 + 3);
		Debug.Log(new Vector3 (planet.transform.parent.position.x, 0, orbit * 2 + 3));
		planet.GetComponent<Planet>().Initialize(orbit);
		return planet.GetComponent<Planet>();
	}

	public static Star CreateStar(GameObject parent, int orbit)
	{
		GameObject star = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		star.AddComponent ("Star");
		star.transform.parent = parent.transform;
		star.transform.position = new Vector3 (star.transform.parent.position.x, 0, star.transform.parent.position.y);
		star.GetComponent<Star>().Initialize(orbit);
		return star.GetComponent<Star>();
	}

	public static Moon CreateMoon(GameObject parent, int orbit = 1)
	{
		GameObject moon = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		moon.AddComponent ("Moon");
		moon.transform.parent = parent.transform;
		moon.transform.position = new Vector3 (moon.transform.parent.position.x, 0, moon.transform.parent.position.y);
		Debug.Log (new Vector3 (moon.transform.parent.position.x, 0, moon.transform.parent.position.y));
		moon.GetComponent<Moon>().Initialize(orbit);
		return moon.GetComponent<Moon>();
	}
	
}