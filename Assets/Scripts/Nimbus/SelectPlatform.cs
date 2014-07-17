using UnityEngine;
using System.Collections;


public class SelectPlatform : MonoBehaviour {

    public string ip;//= "192.168.1.98";

    public GameObject dataNimbus;
    public GameObject imgTarget, arCam;
    public GameObject pcCam;
    public int connectionPort = 25000;
    bool refreshing;
    private HostData[] hostData;

	// Use this for initialization
	void Start () {
        MasterServer.ipAddress = ip;

#if UNITY_ANDROID
            Destroy(pcCam);
            arCam.SetActive(true);
            imgTarget.SetActive(true);
            StartCoroutine(seekConnection());
#else
            pcCam.SetActive(true);
            Destroy(imgTarget);
            Destroy(arCam);
            dataNimbus.transform.parent = null;
            Network.InitializeServer(5, connectionPort, false);
            MasterServer.RegisterHost("DataNimbus", "DataNimbus");
#endif                      
	}


    IEnumerator seekConnection() {
        HostData[] hostData;

        refreshHost();
        yield return new WaitForSeconds(0.1f);
        if (MasterServer.PollHostList().Length > 0)
        {
            print("Found");
            hostData = MasterServer.PollHostList();
            Network.Connect(hostData[0].ip, connectionPort);
            yield break;
        }
        else
        {
            print("Still seeking");
            StartCoroutine(seekConnection());
        }

    }

    void OnDisconnectedFromServer(NetworkDisconnection info){
        Application.Quit();
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
            print("Server Registered");
    }

    void refreshHost(){
        MasterServer.RequestHostList("DataNimbus");
	}
}
