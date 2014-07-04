using UnityEngine;
using System.Collections;

public class AddButton : MonoBehaviour {

	public Camera cam2d;
    public SplashScreen ss;
    public GameObject[] atractors;

	void Start(){
#if UNITY_ANDROID
		transform.position = cam2d.ViewportToWorldPoint(new Vector3(0.5f,0.015f,Mathf.Abs(transform.position.z - cam2d.transform.position.z)));
        collider2D.enabled = false;
#else
        collider2D.enabled = false;
        renderer.enabled = false;
#endif
    }

	void OnMouseDown(){
        if (Network.isClient){
            int randomAtractor = Random.Range(0, 9);
            GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor);
            networkView.RPC("CreateSphere", RPCMode.Server, temp.transform.position, randomAtractor);
        }
	}

    void Update() { 
        if(!ss.onPause)
            collider2D.enabled = true;
    }

    [RPC]
    void CreateSphere(Vector3 pos, int randomAtractor)
    {
        GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor);
        temp.transform.position = pos;
    }

}
