using UnityEngine;
using System.Collections;

public class WebCamController : MonoBehaviour {

    IEnumerator Start()
    {
		foreach(WebCamDevice d in WebCamTexture.devices)
			print (d.isFrontFacing);

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone))
        {
			WebCamTexture webcamTexture = new WebCamTexture(5000,5000);
			//webcamTexture.requestedHeight = 2000;
			//webcamTexture.requestedWidth = 20;
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
        else
        {
            print("No permisions");
        }
    }
}
