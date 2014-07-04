using System; 
using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Twitter : MonoBehaviour {
	
	#region Attributes
    public string screenName;
    private string url;
    //private float time = 24;
    private bool reading;
	public int nQ = 100;
    private List<string> question;

    public List<string> Question {
        get { return question; }
    }
	#endregion
/*	
	#region Read Twitter
    IEnumerator Start() {

        question = new List<string>();
        if (!(Application.platform == RuntimePlatform.Android))
        {
            string[] info;
            url = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=" + 
				PlayerPrefs.GetString("Name") +"&count=150";

            WWW www = new WWW(url);
            yield return www;

            info = www.text.Split('{', '}', '[', ']');

            for (int i = 0; i < info.Length; i++)
                foreach (string s in info[i].Split(','))
                    if (s.Contains("\"text\":"))
                    {
                        string temp = s.Replace("\"text\":", "");
                        temp = s.Replace("\"", "");
                        if (!question.Contains(temp))
                        {
                            question.Add(temp);
                            //print(temp);
                        }
                    }
        }
    }
	#endregion
	*/
	void Start(){
		question = new List<string>();
		
		for (int i= 0; i < nQ; i++){
			question.Add("Question "+(i+1));
		}
	}
	
}

