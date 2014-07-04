using UnityEngine;
using System.Collections;

public class InstructionScreen : MonoBehaviour {

    public SplashScreen ss;

	// Use this for initialization
	void Awake () {
#if !UNITY_ANDROID
        Destroy(gameObject);
#endif
	}

    void OnMouseDown()
    {
        if (Network.peerType == NetworkPeerType.Client){
            collider2D.enabled = false;
            renderer.enabled = false;
            ss.onPause = false;
        }
    }

    void Update() { 

        if (ss.onPause) {
            collider2D.enabled = true;
            renderer.enabled = true;
        }
    }
}
