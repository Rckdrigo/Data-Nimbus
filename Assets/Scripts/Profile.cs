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
        public string id;
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
		StartCoroutine(LoadProfile());
        //printProfile();
        
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
                data.question[i].id = "Q" + i;
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
	}

    void printProfile() {
        for (int i = 0; i < data.question.Length; i++)
        {
            print(data.question[i].text);
            for (int j = 0; j < data.question[i].answer.Length; j++)
                print(data.question[i].answer[j].text);
        }
    }

}


