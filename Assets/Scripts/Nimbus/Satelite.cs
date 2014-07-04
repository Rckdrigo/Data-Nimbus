using UnityEngine;
using System.Collections;

public class Satelite : MonoBehaviour {
	
	public float velAngular;
	float randX,randY,randZ;
	
	void Start(){
		randX = Random.Range(-1.0f,1.0f);
		randY = Random.Range(-1.0f,1.0f);
		randZ = Random.Range(-1.0f,1.0f);	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.parent.position,new Vector3(randX,randY,randZ), Mathf.PI/2.0f * Time.deltaTime * velAngular);
	}
}
