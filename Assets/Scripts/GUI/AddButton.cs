using UnityEngine;
using System.Collections;

public class AddButton : MonoBehaviour {

	public Camera cam2d;
    public SplashScreen ss;
    public GameObject[] atractors;
    public Texture bg;

    [HideInInspector()]
    public int currentQ;
	[HideInInspector()]
	public bool newSphere;
    [HideInInspector()]
    public bool addingQuestion = false;

    public Texture addAnswer;
	public Texture otherAnswers;
	public Texture returnApp;

	bool showingOtherAnswers;
    public GUISkin skin;
    string actualAnswer = "";
    string question = "";
	string[] answers;

    public QuestionPool pool;

	void Start(){
		newSphere = true;
		answers = new string[5];
#if !UNITY_ANDROID
        renderer.enabled = false;
        collider2D.enabled = false;
#endif
		transform.position = cam2d.ViewportToWorldPoint(new Vector3(0.5f,0.025f,Mathf.Abs(transform.position.z - cam2d.transform.position.z)));
    }

	void OnMouseDown(){
        if (Network.isClient && !addingQuestion)
        {
            addingQuestion = true;
			currentQ = Random.Range(0,pool.maxQuestions);
			if(pool.questions[currentQ].activeInHierarchy)
				newSphere = false;
            networkView.RPC("AskQuestion", RPCMode.Server, currentQ);
        }
	}

    void Update() {
        collider2D.enabled = !ss.onPause & !addingQuestion;
    }

    [RPC]
    void CreateSphere(Vector3 pos, int randomAtractor, int name)
    {
        GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor,name);
        temp.transform.position = pos;
    }

    [RPC]
    public void AskQuestion(int qId)
    {
        string temp = pool.questions[qId].GetComponent<QuestionBehaviour>().question.text;
		string[] aws = new string[5];
		for(int i = 0; i < 5; i++)
			aws[i] = pool.questions[qId].GetComponent<QuestionBehaviour>().question.answer[i].text;
		
		networkView.RPC("ReturnQuestion", RPCMode.Others, temp, aws[0],aws[1],aws[2],aws[3],aws[4]);
	}
	
    [RPC]
	void ReturnQuestion(string q, string a1, string a2, string a3, string a4, string a5)
    {
        question = q;
		answers[0] = a1;
		answers[1] = a2;
		answers[2] = a3;
		answers[3] = a4;
		answers[4] = a5;
    }

	[RPC]
	void saveStats(int qId, string aanswer){
		if(!Profile.data.question[qId].active)
			Profile.data.question[qId].active = true;

		for(int i = 0; i < 5; i++){
			if(Profile.data.question[qId].answer[i].text == "parangaricutirumicuaro"){
				Profile.data.question[qId].answer[i].text = aanswer;
				break;
			}
		}
		Profile.SaveProfile();
	}

	#if UNITY_ANDROID

	bool hasOtherAnswers(){
		foreach(string a in answers)
			if(a == null)
				return false;
		return true;
	}

    void OnGUI()
    {
		GUI.skin = skin;
		if (!ss.onPause && addingQuestion ){
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bg);
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			if(!showingOtherAnswers)
	        {
	            GUILayout.Label(question);
				actualAnswer = GUILayout.TextArea(actualAnswer,140);
				if (GUILayout.Button(addAnswer))
				{
					if(!actualAnswer.Equals("")){
						if(!newSphere){
							GameObject.Find(""+currentQ).GetComponent<QuestionBehaviour>().increaseSize(0.6f);
							newSphere = true;
						}
						else{
							int randomAtractor = Random.Range(0, 9);
							GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor,currentQ);
							temp.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
							networkView.RPC("CreateSphere", RPCMode.Server, temp.transform.position, randomAtractor,currentQ);
						}
						networkView.RPC("saveStats",RPCMode.Server,currentQ,actualAnswer);
						actualAnswer="";
						addingQuestion = false;
					}
				}
				if (GUILayout.Button(otherAnswers)){
					showingOtherAnswers=true;
				}
				if (GUILayout.Button(returnApp) || Input.GetKeyDown(KeyCode.Return)){
					actualAnswer="";
	                addingQuestion = false;
				}
	            
	        }
			else{
				GUILayout.Label("Respuestas:");
				for(int i = 0; i < 5; i++)
					if(answers[i] != "parangaricutirumicuaro")
						GUILayout.Label("R: "+answers[i]);
				if (GUILayout.Button(returnApp) || Input.GetKeyDown(KeyCode.Return)){
					showingOtherAnswers = false;
				}
			}
			
			GUILayout.EndArea();
		}
    }
#endif
}






























































































































