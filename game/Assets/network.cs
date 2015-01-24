using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";

		// Use this for initialization
		void Start ()
		{
		
				//		MasterServer.ipAddress="192.168.59.3";
				//		MasterServer.port = 23466;
				//		Network.natFacilitatorIP = "192.168.59.3";
				//		Network.natFacilitatorPort=50005;
				if (Network.peerType == NetworkPeerType.Disconnected) {
						Debug.Log ("in connect");
						MasterServer.RequestHostList ("BlindFlier");
						HostData[] hostDataArray = MasterServer.PollHostList ();
						Debug.Log (hostDataArray.ToString ());
				}
		}
	
		[RPC]
		public void startGame ()
		{
				Debug.Log ("startgame");
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
}
