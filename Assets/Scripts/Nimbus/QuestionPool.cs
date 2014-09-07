using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionPool : MonoBehaviour {

	public GameObject question;
	public int maxQuestions;
	public Transform[] atractors;

	public List<GameObject> questions;


    void Start() {
#if UNITY_ANDROID
        GetComponent<QuestionPool>().CreatePool();
#endif
    }

	// Use this for initialization
	public void CreatePool () {
		questions = new List<GameObject>();
		for(int i = 0; i < maxQuestions; i++){
			GameObject temp = (GameObject) Instantiate(question);
#if !UNITY_ANDROID
            temp.GetComponent<QuestionBehaviour>().question = Profile.data.question[i];	

            temp.name = ""+temp.GetComponent<QuestionBehaviour>().question.id;
            if (temp.GetComponent<QuestionBehaviour>().question.active)
            {
                int randomAtractor = Random.Range(0, atractors.Length);
                temp.transform.position = atractors[randomAtractor].transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
                temp.transform.rotation =  atractors[randomAtractor].transform.rotation;
                temp.transform.parent = atractors[randomAtractor];
				for (int j = 0; j < 5; j++)
					if (temp.GetComponent<QuestionBehaviour>().question.answer[j].text != "parangaricutirumicuaro")
						temp.GetComponent<QuestionBehaviour>().increaseSize(0.6f);

            }

            temp.SetActive(temp.GetComponent<QuestionBehaviour>().question.active);
#else
            temp.name = ""+i;
            temp.SetActive(false);
#endif
            questions.Add(temp);

		}
		StartCoroutine(sendPhp());
	}

	public int activeLength(){
		int cont = 0;
		for(int i = 0; i < maxQuestions; i++)
			if(questions[i].activeInHierarchy)
				cont++;
		return cont;
	}

	IEnumerator sendPhp(){
		WWWForm form = new WWWForm ();
		form.AddField ("name", "1");
		
		WWW www = new WWW ("http://annigarzalau.com/datanimbus/variables.php?nodo="+activeLength(),form);
		yield return www;
		
		if (www.error != null) {
			print(www.error); //if there is an error, tell us
		} else {
			print("Test ok");
			www.Dispose(); //clear our form in game
		}
	}

    public GameObject CreateSphere(int randomAtractor,int i)
    {
        //int i = Random.Range(0, questions.Count);
        if (!questions[i].activeInHierarchy)
        {
            questions[i].SetActive(true);
            questions[i].transform.position = atractors[randomAtractor].transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
            questions[i].transform.rotation = atractors[randomAtractor].transform.rotation;
            questions[i].transform.parent = atractors[randomAtractor];
        }
		StartCoroutine(sendPhp());
		return questions[i];
    }

}