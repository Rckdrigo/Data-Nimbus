using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	
 	//Local Server
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
	public GUIStyle style;
	public GUIStyle button;
	
 	private HostData[] hostData;
	bool refreshing;
	
	void Start(){
        Network.InitializeServer(32, connectionPort, false);
        MasterServer.RegisterHost("NetworkTutorialUNAM", "Vuforia network", "un servidor");

        refreshHost();

        Network.Disconnect(200);

        if (hostData != null)
        {
            for (int i = 0; i < hostData.Length; i++)
            {
                if (GUI.Button(new Rect(480, 50 + i * 20, 420, 40), hostData[i].gameName, button))
                {
                    Network.Connect(hostData[i].ip, connectionPort);
                }
            }
        }
	}
	
	void Update(){
		if(refreshing){
			if(MasterServer.PollHostList().Length >0){
				refreshing = false;
				hostData = MasterServer.PollHostList();
				print (hostData);
			}
		}
	}
	
	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded)
			print ("Server Registered");
	}
	
	void refreshHost(){
		refreshing = true;
		MasterServer.RequestHostList("NetworkTutorialUNAM");
	}
}