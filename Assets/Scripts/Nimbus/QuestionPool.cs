using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionPool : MonoBehaviour {

	public GameObject question;
	public int maxQuestions;
	public Transform[] atractors;

	List<GameObject> questions;

	// Use this for initialization
	void Start () {
		questions = new List<GameObject>();
		for(int i = 0; i < maxQuestions; i++){
			GameObject temp = (GameObject) Instantiate(question);
			temp.SetActive(false);
            temp.name = "Pregunta "+i;
			questions.Add(temp);	
		}
	}

    public GameObject CreateSphere(int randomAtractor)
    {
		for(int i = 0; i < questions.Count; i++){
			if(!questions[i].activeInHierarchy){
				questions[i].SetActive(true);
                questions[i].transform.position = atractors[randomAtractor].transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
				questions[i].transform.rotation = atractors[randomAtractor].transform.rotation;
				questions[i].transform.parent = atractors[randomAtractor];
                questions[i].GetComponent<QuestionBehaviour>().question = Profile.data.question[i];
				return questions[i];
			}
		}
        return null;
    }

}