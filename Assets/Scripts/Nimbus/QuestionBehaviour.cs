using UnityEngine;
using System.Collections;

public class QuestionBehaviour : MonoBehaviour {

    public GameObject[] satellites;
    public GameData.Question question;

    void OnMouseDown() {
        if (!GameObject.Find("ButtonAdd").GetComponent<AddButton>().addingQuestion)
        {
            for (int i = 0; i < satellites.Length; i++)
            {
                if (!satellites[i].activeInHierarchy)
                {
                    satellites[i].SetActive(true);
                   break;
                }
            }
            GameObject.Find("ButtonAdd").GetComponent<AddButton>().addingQuestion = true;
            GameObject.Find("ButtonAdd").GetComponent<AddButton>().networkView.RPC("AskQuestion", RPCMode.Server, int.Parse(name));
            networkView.RPC("activateSatellite", RPCMode.Server, name);
        }
    }

    [RPC]
    void activateSatellite(string name) {
        QuestionBehaviour reference = GameObject.Find(name).GetComponent<QuestionBehaviour>();
        for (int i = 0; i < reference.satellites.Length; i++)
        {
            if (!reference.satellites[i].activeInHierarchy)
            {
                reference.satellites[i].SetActive(true);
                break;
            }
        }
    }

}
