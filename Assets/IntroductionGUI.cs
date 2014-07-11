using UnityEngine;
using System.Collections;

public class IntroductionGUI : MonoBehaviour {

	public Texture intro;
	public Texture instructions;
	
	private bool showInstructions;
	[HideInInspector()]
	public bool onPause;

	void Awake() {
#if !UNITY_ANDROID
		Destroy(this);
#else
		onPause = true;
		showInstructions = false;
#endif
	}

	void Update(){
        print(Network.peerType);
		if (onPause){// && Network.peerType == NetworkPeerType.Client) {
			if (Input.GetMouseButtonDown (0) && !showInstructions){ 
				CancelInvoke("reactivate");
				Invoke ("reactivate", 10.0f);
				showInstructions = true;

			}
			else if (Input.GetMouseButtonDown (0)&& Input.mousePosition.y < Screen.height/4 && showInstructions){
				showInstructions = false;
				onPause = false;
				GetComponent<NewQuestionGUI>().enabled=true;
				CancelInvoke("reactivate");
				Invoke ("reactivate", 10.0f);
			}
		}
	}

	void OnGUI(){
		if (onPause) {
			if (!showInstructions)
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), intro, ScaleMode.StretchToFill,false, intro.height / intro.width);

			else
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), instructions, ScaleMode.StretchToFill,false, instructions.height / instructions.width);
			
		}
	}

	public void reactivate() {
		onPause = true;
		GetComponent<NewQuestionGUI>().enabled=false;
	}
}
