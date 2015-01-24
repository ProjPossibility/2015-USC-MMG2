﻿using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
		public string MASTERSERVER_ID = "2015uscmmg2";
		public float HOST_POLL_TIMEOUT = 5.0f;
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";
		public NetworkView netview;
	public GUI.Button startBtn;
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
		private void startAsServer ()
		{
				string id = System.Guid.NewGuid ().ToString ();
				Network.InitializeServer (4, portNum, !Network.HavePublicAddress ());
				MasterServer.RegisterHost (MASTERSERVER_ID, id);
				Debug.Log ("No servers found, created new server with id " + id);
		}
		void OnGUI ()
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
		void OnConnectedToServer ()
		{
				Debug.Log ("Connected to server");
		}
		// Update is called once per frame
		void Update ()
		{

				if (Network.peerType != NetworkPeerType.Disconnected) {
						Debug.Log (Network.peerType.ToString ());
				}
		}

}
