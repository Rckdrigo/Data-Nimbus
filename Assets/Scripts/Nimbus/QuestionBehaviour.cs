using UnityEngine;
using System.Collections;

public class QuestionBehaviour : MonoBehaviour {

    public GameData.Question question;
	[HideInInspector()]
	public int count = 0;
#if UNITY_ANDROID
    void OnMouseDown() {
        if (!GameObject.Find("ButtonAdd").GetComponent<AddButton>().addingQuestion)
        {
            GameObject.Find("ButtonAdd").GetComponent<AddButton>().addingQuestion = true;
			GameObject.Find("ButtonAdd").GetComponent<AddButton>().currentQ = int.Parse(name);
			GameObject.Find("ButtonAdd").GetComponent<AddButton>().newSphere = false;
            GameObject.Find("ButtonAdd").GetComponent<AddButton>().networkView.RPC("AskQuestion", RPCMode.Server, int.Parse(name));
        }
    }
	

#endif
	public void increaseSize(float scale) {
		if(transform.localScale.x < 3*5)
			transform.localScale += new Vector3(scale,scale,scale);
		networkView.RPC("sameSize",RPCMode.Server,transform.localScale);
	}
	
	[RPC]
	void sameSize(Vector3 size){
		transform.localScale = size;
	}

}
