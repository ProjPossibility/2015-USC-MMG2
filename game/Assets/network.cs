using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
		public static string MASTERSERVER_ID = "2015uscmmg2";
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
						Network.Disconnect ();
						MasterServer.UnregisterHost ();
						MasterServer.RequestHostList (MASTERSERVER_ID);
						HostData[] hostDataArray = MasterServer.PollHostList ();
						Debug.Log (hostDataArray.ToString ());
						if (hostDataArray.Length == 0) {
								string id = System.Guid.NewGuid ().ToString ();
								Network.InitializeServer (8, portNum, !Network.HavePublicAddress ());
								MasterServer.RegisterHost (MASTERSERVER_ID, id);
								Debug.Log ("No servers found, created new server with id " + id);
						} else {
								Network.Connect (hostDataArray [0]);
								Debug.Log (hostDataArray.Length + " servers found, joining server id " + hostDataArray [0]);
						}
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
