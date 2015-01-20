using UnityEngine;
using System.Collections;

namespace RTS {
	public static class ResourceManager { //константы для всего
		public static float ScrollSpeed { get { return 10; } }
		public static float PerspectiveViewSpeed { get { return 0.5f; } }
		public static float ScrollArea { get { return 25; } }
		public static float DragSpeed { get { return 100.0f; } }
		public static float MinCameraHeight { get { return 10; } }
		public static float MaxCameraHeight { get { return 25; } }
		public static int MaxNumberOfPlanets { get { return 8; } }
		public static int MinNumberOfPlanets { get { return 2; } }
		public static int MinNumberOfPlanetarySystems { get { return 20; } }
		public static int MaxNumberOfPlanetarySystems { get { return 5; } }
		public static int MinStats { get { return 0; } }
		public static int MaxStats { get { return 100; } }
		public static int CoffOrbitDeegres { get { return 100; } }
		public static float MinScale { get { return 0.5f; } }
		public static float MaxScale { get { return 1.5f; } }
		public static float MinScaleMoon { get { return 0.1f; } }
		public static float MaxScaleMoon { get { return 0.3f; } }
		public static int MaxNumberOfMoons { get { return 0; } }
		public static int MinNumberOfMoons { get { return 2; } }
		public static int ZoomPlanetarySystemZ { get { return 10; } }
		public static int ZoomGalaxyZ { get { return 20; } }
		private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
		public static Vector3 InvalidPosition { get { return invalidPosition; } }
		public static int yMaxLimit { get { return 80; } }
		public static int yMinLimit { get { return 20; } }
		public static int xDefaultCam { get { return 80; } }
		public static int yDefaultCam { get { return 180; } }
	}
}