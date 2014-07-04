using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ForceField : MonoBehaviour {
	
	public enum TYPE{Atractor, Question};
	public TYPE type;
	private Vector3 force;
	private GameObject[] atractor;
	
	public float minDistance = 10, maxDistance = 20;
	public float speed = 5.0f;
	
	
	// Use this for initialization
	void Start () {
		force = Vector3.zero;	
		atractor = GameObject.FindGameObjectsWithTag("Atractor");
	}
	
	// Update is called once per frame
	void Update () {
		force = Vector3.zero;
		if(Vector3.Magnitude(transform.position-transform.parent.position) < minDistance){
			force += (transform.position-transform.parent.position).normalized;
			//Debug.DrawLine(transform.position,transform.parent.position,Color.red);
		}
		else if(Vector3.Magnitude(transform.position-transform.parent.position) > maxDistance){
			force -= (transform.position-transform.parent.position).normalized;
			//Debug.DrawLine(transform.position,transform.parent.position,Color.green);
		}
		
		
		if(type == TYPE.Atractor){
			foreach(GameObject i in atractor){
				if(Vector3.Magnitude( i.transform.position - transform.position) < minDistance){
					force -= (i.transform.position - transform.position).normalized;
					//Debug.DrawLine(transform.position,i.transform.position,Color.blue);
				}
			}
		}
			
			
		if(type == TYPE.Question){
			foreach(GameObject j in GameObject.FindGameObjectsWithTag(gameObject.tag)){
				if(Vector3.Magnitude( j.transform.position - transform.position) < minDistance){
					force -= (j.transform.position - transform.position).normalized;
					//Debug.DrawLine(transform.position,j.transform.position,Color.blue);
				}
			}
		}
	
		if(transform.position.y <0)
			force.y = 1;
		force = force.normalized * speed;
		rigidbody.AddForce(force,ForceMode.Acceleration);
	}
}
