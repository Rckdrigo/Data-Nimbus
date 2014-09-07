using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    [HideInInspector()]
    public bool onPause;

    void Awake() {
#if !UNITY_ANDROID
        Destroy(gameObject);
#else
        onPause = true;
#endif
    }

	void OnMouseDown () {
        if (Network.peerType == NetworkPeerType.Client){
            collider2D.enabled = false;
            renderer.enabled = false;
            Invoke("reactivate",300.0f);
        }
	}

	void OnDisconnectedFromServer(){
		reactivate();
	}

    void reactivate() {
        collider2D.enabled = true;
        renderer.enabled = true;
        onPause = true;
    }
}
