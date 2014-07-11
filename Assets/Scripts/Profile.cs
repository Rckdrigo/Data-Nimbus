using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// 
/// </summary>
public class GameData{

    public struct Answer
    {
        public string text;
    };

    public struct Question
    {
        [XmlAttribute("id")]
        public int id;
        [XmlAttribute("active")]
        public bool active;
        public string text;
        public Answer[] answer;
    };

    public Question[] question;

	public GameData(){
        question = new Question[150];
        for (int i = 0; i < question.Length; i++)
            question[i].answer = new Answer[5];
	}
}

/// <summary>
/// Profile.
/// </summary>
public class Profile : MonoBehaviour{
	public static GameData data;

	void Awake(){
		data = new GameData();
		DontDestroyOnLoad (gameObject);
    }

#if !UNITY_ANDROID
    void Start() {
        StartCoroutine(LoadProfile());
    }

	void Update(){
		if(Input.GetKeyDown(KeyCode.S))
			SaveProfile();

        if (Input.GetKeyDown(KeyCode.P))
            printProfile();

        if(Input.GetKeyDown(KeyCode.L))
            StartCoroutine(LoadProfile());

        if(Input.GetKeyDown(KeyCode.C)){
            for (int i = 0; i < data.question.Length; i++ ){
                data.question[i].id = i;
                data.question[i].text = "Question "+i;
                data.question[i].active = false;
                for (int j = 0; j < data.question[i].answer.Length; j++)
                    data.question[i].answer[j].text = "A" + j;
            }
        }
	}
	
	public void SaveProfile(){
        XMLManager.CreateXML("profile.xml", XMLManager.SerializeObject(data, "GameData"));
	}
	

	public IEnumerator LoadProfile(){
        print("Reading");
        yield return data = XMLManager.DeserializeObject<GameData>(XMLManager.LoadXML("profile.xml"));
        print("Finished reading");
        GetComponent<QuestionPool>().CreatePool();
	}

    void OnPlayerConnected(NetworkPlayer player){
        print("Conectado");
        QuestionPool pool = GetComponent<QuestionPool>();
        foreach (GameObject q in pool.questions)
        {
            if (q.activeInHierarchy)
            {
                print( int.Parse(q.name)+"\t"+int.Parse(q.transform.parent.name.Replace("Atractor ", ""))+"\t");
                networkView.RPC("sendGameDataToPlayer", player, int.Parse(q.name), int.Parse(q.transform.parent.name.Replace("Atractor ", "")), q.transform.position);
            }
        }
    }

    void printProfile() {
        for (int i = 0; i < data.question.Length; i++)
        {
            print(data.question[i].text + "\t" + data.question[i].active);
            for (int j = 0; j < data.question[i].answer.Length; j++)
                print(data.question[i].answer[j].text);
        }
    }

#endif
    [RPC]
    void sendGameDataToPlayer(int id, int atractor, Vector3 position) {
        QuestionPool pool = GetComponent<QuestionPool>();
        pool.questions[id].SetActive(true);
        pool.questions[id].transform.parent = GameObject.Find("Atractor "+atractor).transform;
        pool.questions[id].transform.position = position;
    }
}


