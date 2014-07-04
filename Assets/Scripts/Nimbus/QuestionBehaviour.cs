using UnityEngine;
using System.Collections;

public class QuestionBehaviour : MonoBehaviour {

    public GameObject[] satellites;

    void OnMouseDown() {
        for (int i = 0; i < satellites.Length; i++)
        {
            if (!satellites[i].activeInHierarchy)
            {
                satellites[i].SetActive(true);
                break;
            }
        }
        networkView.RPC("activateSatellite", RPCMode.Server, name);
    }

    [RPC]
    void activateSatellite(string name) {
        print("Satelite de " + name);
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
