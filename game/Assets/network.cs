using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
		public string MASTERSERVER_ID = "2015uscmmg2";
		public float HOST_POLL_TIMEOUT = 5.0f;
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";
		public NetworkView netview;
		bool started = false;
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
						MasterServer.ClearHostList ();
						MasterServer.RequestHostList (MASTERSERVER_ID);
				}
		}
	
		[RPC]
		public void startGame ()
		{
				Debug.Log ("startgame");
		}
		[RPC]
		public void endGame (bool win)
		{
				this.started = false;
				//todo reset the map and display end game message
		}

		private void startAsServer ()
		{
				string id = System.Guid.NewGuid ().ToString ();
				Network.InitializeServer (4, portNum, !Network.HavePublicAddress ());
				MasterServer.RegisterHost (MASTERSERVER_ID, id);
				Debug.Log ("No servers found, created new server with id " + id);
		}

		public void OnClick ()
		{
				if (Network.peerType == NetworkPeerType.Disconnected) {
						HostData[] hostDataArray = MasterServer.PollHostList ();
						if (hostDataArray.Length != 0) {
								NetworkConnectionError error = Network.Connect (hostDataArray [0]);
								Debug.Log (hostDataArray.Length + " servers found, joining server id " + hostDataArray [0].gameName + " and error " + error.ToString ());
						} else {
								this.startAsServer ();
						}
				} else if (Network.peerType == NetworkPeerType.Client) {
						this.netview.RPC ("startGame", RPCMode.All);
						Debug.Log ("Connected as client");

				}
		}
		void OnConnectedToClient ()
		{
				Debug.Log ("Connected to client");
		}
		void OnConnectedToServer ()
		{
				Debug.Log ("Connected to server");
				this.netview.RPC ("startGame", RPCMode.All);
				this.startGame ();
		}
		// Update is called once per frame
		void Update ()
		{
				//Debug.Log (Network.peerType + " " + Network.connections.Length);
				if (!this.started) {
						if (Network.peerType == NetworkPeerType.Client && Network.connections.Length != 0) {
								this.OnConnectedToServer ();
								this.started = true;
						}
						if (Network.peerType == NetworkPeerType.Server && Network.connections.Length != 0) {
								this.OnConnectedToServer ();
								this.started = true;
						}
				}
		}
}
