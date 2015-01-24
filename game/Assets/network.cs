using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
		public string MASTERSERVER_ID = "2015uscmmg2";
		public float HOST_POLL_TIMEOUT = 5.0f;
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";
		private float elapsed;
		// Use this for initialization
		void Start ()
		{
				//		MasterServer.ipAddress="192.168.59.3";
				//		MasterServer.port = 23466;
				//		Network.natFacilitatorIP = "192.168.59.3";
				//		Network.natFacilitatorPort=50005;
				MasterServer.UnregisterHost ();
				Network.Disconnect ();
				if (Network.peerType == NetworkPeerType.Disconnected) {
						Debug.Log ("in connect");
						MasterServer.ClearHostList ();
						MasterServer.RequestHostList (MASTERSERVER_ID);
				}
				this.elapsed = 0;
		}
	
		[RPC]
		public void startGame ()
		{
				Debug.Log ("startgame");
		}
		private void startAsServer ()
		{
				string id = System.Guid.NewGuid ().ToString ();
				Network.InitializeServer (4, portNum, !Network.HavePublicAddress ());
				MasterServer.RegisterHost (MASTERSERVER_ID, id);
				Debug.Log ("No servers found, created new server with id " + id);
		}
		// Update is called once per frame
		void Update ()
		{
				if (Network.peerType == NetworkPeerType.Disconnected) {
						this.elapsed += Time.deltaTime;

						HostData[] hostDataArray = MasterServer.PollHostList ();
						if (hostDataArray.Length != 0) {
								Network.Connect (hostDataArray [0]);
								Debug.Log (hostDataArray.Length + " servers found, joining server id " + hostDataArray [0].guid);
						}
						if (elapsed > HOST_POLL_TIMEOUT) {
								this.startAsServer ();
						}
				}
		}
}
