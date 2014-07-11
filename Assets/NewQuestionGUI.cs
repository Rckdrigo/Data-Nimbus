using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IntroductionGUI))]
public class NewQuestionGUI : MonoBehaviour {

	public Texture addButton;
	public Texture blackWallpaper;
	IntroductionGUI intro;
	public GUIStyle questionGUI;

	bool showQuestion;
	
	public GameObject[] atractors;

	void Awake() {
		#if !UNITY_ANDROID
		Destroy(this);
		#endif
	}
	void OnEnable(){
		showQuestion = false;
		intro = GetComponent<IntroductionGUI> ();
	}

	void Update(){
		if (Input.GetMouseButtonDown (0) && !intro.onPause 
		    && new Rect(0,0,Screen.width,addButton.height).Contains(Input.mousePosition)
		    && !showQuestion) {
			showQuestion = true;
			intro.CancelInvoke("reactivate");
			intro.Invoke ("reactivate", 10.0f);

			int randomAtractor = Random.Range(0, 9);
			GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor);
			networkView.RPC("CreateSphere", RPCMode.Server, temp.transform.position, randomAtractor);
		}
	}

	void OnGUI(){
		if (!intro.onPause && !showQuestion) 
			GUI.DrawTexture (new Rect (0, Screen.height - (Screen.height * addButton.height / addButton.width),
                         Screen.width, Screen.height * addButton.height / addButton.width), addButton);
		/*else if (!intro.onPause && showQuestion) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), blackWallpaper);	
			GUILayout.Label("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc faucibus quam sit amet fermentum suscipit. Ut tristique, arcu non facilisis adipiscing, metus mauris scelerisque est, ac aliquam nisi risus ut eros. Praesent nec mi vitae lectus elementum varius. Curabitur at adipiscing diam. ",questionGUI);
		}*/

	}

	[RPC]
	void CreateSphere(Vector3 pos, int randomAtractor)
	{
		GameObject temp = GameObject.FindWithTag("GameController").GetComponent<QuestionPool>().CreateSphere(randomAtractor);
		temp.transform.position = pos;
	}
}
