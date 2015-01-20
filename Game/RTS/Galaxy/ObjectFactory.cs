using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour 
{
	protected static ObjectFactory instance; // Needed
	
	void Start()
	{
		instance = this;
	}
	
	public static Planet CreatePlanet(GameObject parentObject, int orbit)
	{
		GameObject planet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		planet.transform.parent = parentObject.transform;
		planet.AddComponent ("Planet");
		planet.GetComponent<Planet>().Initialize(orbit);
		return planet.GetComponent<Planet>();
	}

	public static Star CreateStar(GameObject parentObject, int orbit)
	{
		GameObject star = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		star.transform.parent = parentObject.transform;
		star.transform.position = new Vector3 (star.transform.parent.position.x, 0, star.transform.parent.position.y);
		star.AddComponent ("Star");
		star.GetComponent<Star>().Initialize(orbit);
		return star.GetComponent<Star>();
	}

	public static Moon CreateMoon(GameObject parentObject, int orbit = 1)
	{
		GameObject moon = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		moon.transform.parent = parentObject.transform;
		moon.transform.localPosition = new Vector3 (moon.transform.parent.localScale.x / 2 , 0, moon.transform.parent.localScale.y / 2 );
		moon.AddComponent ("Moon");
		moon.GetComponent<Moon>().Initialize(orbit);
		return moon.GetComponent<Moon>();
	}

	public static PlanetarySystem CreatePlanetarySystem(GameObject parentObject, int position)
	{
		GameObject planetarySystem = new GameObject();
		planetarySystem.transform.parent = parentObject.transform;
		planetarySystem.transform.localPosition = new Vector3 (position * Random.Range(0.1f, 1.0f), position * Random.Range(0.1f, 1.0f), 0);
		planetarySystem.AddComponent ("PlanetarySystem");
		planetarySystem.GetComponent<PlanetarySystem>().Initialize(position);
		return planetarySystem.GetComponent<PlanetarySystem>();
	}
}