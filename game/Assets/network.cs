using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
		public string MASTERSERVER_ID = "2015uscmmg2";
		public float CLIENT_CONNECT_TIMEOUT = 5.0f;
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";
		public NetworkView netview;
		bool started = false;
		float elapsed = 0;
		enum nstate
		{
				DISCONNECTED ,
				CONNECTING_AS_CLIENT,
				SERVER,
				IN_GAME,
		}
		nstate m_state = nstate.DISCONNECTED;
		// Use this for initialization
		void Start ()
		{
				//		MasterServer.ipAddress="192.168.59.3";
				//		MasterServer.port = 23466;
				//		Network.natFacilitatorIP = "192.168.59.3";
				//		Network.natFacilitatorPort=50005;
				if (Network.peerType == NetworkPeerType.Disconnected) {
						MasterServer.ClearHostList ();
						MasterServer.RequestHostList (MASTERSERVER_ID);
				}
		}
	
		[RPC]
		public void startGame ()
		{
				Debug.Log ("startgame");
				this.m_state = nstate.IN_GAME;
		}
		[RPC]
		public void endGame (bool win)
		{
				MasterServer.UnregisterHost ();
				Network.Disconnect ();
				this.started = false;
				this.m_state = nstate.DISCONNECTED;
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
				if (Network.peerType == NetworkPeerType.Disconnected && this.m_state == nstate.DISCONNECTED) {
						HostData[] hostDataArray = MasterServer.PollHostList ();
						if (hostDataArray.Length != 0) {
								Debug.Log (hostDataArray.Length + " servers found, joining server id " + hostDataArray [0].gameName);
								NetworkConnectionError error = Network.Connect (hostDataArray [0]);
								if (error != NetworkConnectionError.NoError) {
										m_state = nstate.DISCONNECTED;
										Debug.Log ("Error connected to server: " + error.ToString ());
								} else {
										m_state = nstate.CONNECTING_AS_CLIENT;
								}
						} else {
								this.startAsServer ();
						}
				}
		}
		void OnConnectedToClient ()
		{
				Debug.Log ("Connected to client");
				this.m_state = nstate.IN_GAME;
		}
		void OnConnectedToServer ()
		{
				Debug.Log ("Connected to server");
				this.netview.RPC ("startGame", RPCMode.All);
				this.m_state = nstate.IN_GAME;
		}
		// Update is called once per frame
		void Update ()
		{
				if (this.m_state == nstate.CONNECTING_AS_CLIENT) {
						this.elapsed++;
						if (this.elapsed > CLIENT_CONNECT_TIMEOUT) {
								this.m_state = nstate.DISCONNECTED;
								this.elapsed = 0;
						}
				}
				//Debug.Log (Network.peerType + " " + Network.connections.Length);
				if (this.m_state != nstate.IN_GAME) {
						if (Network.peerType == NetworkPeerType.Client && Network.connections.Length != 0) {
								this.OnConnectedToServer ();
								this.started = true;
								return;
						}
						if (Network.peerType == NetworkPeerType.Server && Network.connections.Length != 0) {
								this.OnConnectedToClient ();
								this.started = true;
								return;
						}
				}
		}
}
