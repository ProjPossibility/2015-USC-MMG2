﻿using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
		public string MASTERSERVER_ID = "2015uscmmg2";
		public float HOST_POLL_TIMEOUT = 5.0f;
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";
		private float elapsed;
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
				if (Network.peerType == NetworkPeerType.Disconnected && Network.connections.Length == 0) {
						this.elapsed += Time.deltaTime;

						HostData[] hostDataArray = MasterServer.PollHostList ();
						if (hostDataArray.Length != 0) {
								NetworkConnectionError error = Network.Connect (hostDataArray [0]);
								Debug.Log (hostDataArray.Length + " servers found, joining server id " + hostDataArray [0].gameName + " and error " + error.ToString ());
						}
						if (elapsed > HOST_POLL_TIMEOUT) {
								this.startAsServer ();
						}
				} else if (!this.started) {
						this.netview.RPC ("startGame", RPCMode.All);
						this.started = true;
				}
		}
}
