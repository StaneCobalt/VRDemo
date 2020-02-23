using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StaneCobalt
{
	public class Spawner : MonoBehaviour
	{
		void Spawn(Color color)
		{
			GameObject sphere = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
				sphere.transform.localScale *= 0.05f;
				sphere.transform.position = transform.position + transform.forward * 0.3f;
				sphere.GetComponent<Renderer>().material.color = color;
		}

		public void SpawnRed()
		{
			Spawn(Color.red);
		}

		public void SpawnGreen()
		{
			Spawn(Color.green);
		}

		public void SpawnBlue()
		{
			Spawn(Color.blue);
		}

		public void SpawnYellow()
		{
			Spawn(Color.yellow);
		}
	}
}
