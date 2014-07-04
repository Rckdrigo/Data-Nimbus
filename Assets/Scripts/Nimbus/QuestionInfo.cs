using UnityEngine;
using System.Collections;

public class QuestionInfo : MonoBehaviour {

    string text;
	int nAnswers;
	

    public string Text {
        set { text = value; }
        get { return text; }
    }
	public int NAnswers{
		get{return nAnswers;}
	}
	
	public void addAnswer(){
		nAnswers++;
		if(nAnswers>4)
			rigidbody.useGravity = true;
	}
}
