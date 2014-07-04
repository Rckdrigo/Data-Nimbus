using UnityEngine;
using System.Collections;

public class WebCamController : MonoBehaviour {

    IEnumerator Start()
    {

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone))
        {
            WebCamTexture webcamTexture = new WebCamTexture();
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
        else
        {
            print("No permisions");
        }
    }
}
