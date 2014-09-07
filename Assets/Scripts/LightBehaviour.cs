using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {

	void Update () {
		transform.rotation = Camera.main.transform.rotation;
	}
}
