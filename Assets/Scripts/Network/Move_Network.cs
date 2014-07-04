using UnityEngine;
using System.Collections;

public class Move_Network : MonoBehaviour {
	Vector3 lastPosition;
	float minimumMovement = .5f;
	
	// Update is called once per frame
	void Update () {
		if (Network.isServer)
            networkView.RPC("SetPosition", RPCMode.Others, transform.position);
	}
	
	#region RPC FUNC
	[RPC]
	void SetPosition(Vector3 newPosition){
	    transform.position = newPosition;
	}
	
	//Network View
	/*void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
	    if (stream.isWriting){
	        Vector3 pos = transform.position;
	        stream.Serialize(ref pos);
		}
		else{
	        Vector3 receivedPosition = Vector3.zero;
	        stream.Serialize(ref receivedPosition);
	        transform.position = receivedPosition;
		}
	}*/
	
	#endregion
}
