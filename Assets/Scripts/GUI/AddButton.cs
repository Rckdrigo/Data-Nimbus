using UnityEngine;
using System.Collections;

public class AddButton : MonoBehaviour {

	public Camera cam2d;
    public SplashScreen ss;
    public GameObject[] atractors;
    public Texture bg;
    int currentQ;

    bool addingQuestion = false;
    public Texture addAnswer;
    public GUISkin skin;
    string answer = "Lorem ipsum";
    string question = "";

    public QuestionPool pool;

	void Start(){
#if !UNITY_ANDROID
        renderer.enabled = false;
        collider2D.enabled = false;
#endif
		transform.position = cam2d.ViewportToWorldPoint(new Vector3(0.5f,0.02f,Mathf.Abs(transform.position.z - cam2d.transform.position.z)));
    }

	void OnMouseDown(){
        if (Network.isClient && !addingQuestion)
        {
            addingQuestion = true;
            int randomAtractor = Random.Range(0, 9);
            GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor);
            currentQ = int.Parse(temp.name);
            networkView.RPC("AskQuestion", RPCMode.Server, currentQ);
            networkView.RPC("CreateSphere", RPCMode.Server, temp.transform.position, randomAtractor);
        }
	}

    void Update() {
        collider2D.enabled = !ss.onPause & !addingQuestion;
    }

    [RPC]
    void CreateSphere(Vector3 pos, int randomAtractor)
    {
        GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor);
        temp.transform.position = pos;
    }

    [RPC]
    void AskQuestion(int qId)
    {
        string temp = pool.questions[qId].GetComponent<QuestionBehaviour>().question.text;
        networkView.RPC("ReturnQuestion", RPCMode.Others, temp);
    }

    [RPC]
    void ReturnQuestion(string q)
    {
        question = q;
    }

#if UNITY_ANDROID
    void OnGUI()
    {
        GUI.skin = skin;
        if (!ss.onPause && addingQuestion)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bg);
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.Label(question);//Profile.data.question[currentQ].text);
            answer = GUILayout.TextArea(answer,140);//Profile.data.question[currentQ].text);
            if (GUILayout.Button(addAnswer))
            {
                addingQuestion = false;
            }
            GUILayout.EndArea();
        }
    }
#endif
}
