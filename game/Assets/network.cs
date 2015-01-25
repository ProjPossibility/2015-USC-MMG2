using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class network : MonoBehaviour
{
		public string MASTERSERVER_ID = "2015uscmmg2";
		public float CLIENT_CONNECT_TIMEOUT = 5.0f;
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";
		public NetworkView netview;
		public Text text;
		float elapsed = 0;
		enum nstate
		{
				DISCONNECTED ,
				CONNECTING_AS_CLIENT,
				SERVER,
				IN_GAME,
		}
		nstate m_state = nstate.DISCONNECTED;

		private void chState (nstate st)
		{
				this.m_state = st;
				this.text.text = st.ToString ();
		}

		// Use this for initialization
		void Start ()
		{
				if (Network.peerType == NetworkPeerType.Disconnected) {
						MasterServer.ClearHostList ();
						MasterServer.RequestHostList (MASTERSERVER_ID);
						this.chState (nstate.DISCONNECTED);
				}
		}
	
		[RPC]
		public void startGame ()
		{
				Debug.Log ("startgame");
				this.chState (nstate.IN_GAME);
		Application.LoadLevel ("gyroScene");
		}
		[RPC]
		public void endGame (bool win)
		{
				MasterServer.UnregisterHost ();
				Network.Disconnect ();
				this.chState (nstate.DISCONNECTED);
				//todo reset the map and display end game message
		}

		private void startAsServer ()
		{
				string id = System.Guid.NewGuid ().ToString ();
				Network.InitializeServer (32, portNum, !Network.HavePublicAddress ());
				MasterServer.RegisterHost (MASTERSERVER_ID, id);
				this.chState (nstate.SERVER);
				Debug.Log ("No servers found, created new server with id " + id);
		}

		public void OnClick ()
		{
				if (Network.peerType == NetworkPeerType.Disconnected && this.m_state == nstate.DISCONNECTED) {
						HostData[] hostDataArray = MasterServer.PollHostList ();
						if (hostDataArray.Length != 0) {
								Debug.Log (hostDataArray.Length + " servers found, joining server id " + hostDataArray [0].gameName);
								NetworkConnectionError error = Network.Connect (hostDataArray [0].guid);
								if (error != NetworkConnectionError.NoError) {
										this.chState (nstate.DISCONNECTED);
										Debug.Log ("Error connected to server: " + error.ToString ());
								} else {
										this.chState (nstate.CONNECTING_AS_CLIENT);
								}
						} else {
								this.startAsServer ();
						}
				}
		}
		void OnConnectedToClient ()
		{
				Debug.Log ("Connected to client");
				this.chState (nstate.IN_GAME);
		}
		void OnConnectedToServer ()
		{
				Debug.Log ("Connected to server");
				this.netview.RPC ("startGame", RPCMode.All);
				this.chState (nstate.IN_GAME);
		}
		// Update is called once per frame
		void Update ()
		{
				if (this.m_state == nstate.CONNECTING_AS_CLIENT) {
						this.elapsed++;
						if (this.elapsed > CLIENT_CONNECT_TIMEOUT) {
								this.chState (nstate.DISCONNECTED);
								this.elapsed = 0;
						}
				}
				//Debug.Log (Network.peerType + " " + Network.connections.Length);
				if (this.m_state != nstate.IN_GAME) {
						if (Network.peerType == NetworkPeerType.Client && Network.connections.Length != 0) {
								this.OnConnectedToServer ();
								return;
						}
						if (Network.peerType == NetworkPeerType.Server && Network.connections.Length != 0) {
								this.OnConnectedToClient ();
								return;
						}
				}
		}
}
