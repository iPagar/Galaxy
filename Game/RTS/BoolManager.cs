using UnityEngine;
using System.Collections;

namespace BM {
	public static class BoolManager { 
		public static bool isZooming { get; set; }
		public static bool isZoomedToPlanet { get; set; }
		public static bool isZoomedToPlanetarySystem { get; set; }
		public static bool isZoomedToGalaxy { get; set; }
		public static bool isStepping { get; set; }
	}
}