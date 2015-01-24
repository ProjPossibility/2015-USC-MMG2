using UnityEngine;
using System.Collections;

public class ConnectionTutorial : MonoBehaviour
{
		public int portNum = 25001;
		public string ipAdd = "127.0.0.1";
		private GameObject player;
		public int fontSize = 30;
	
		void OnGUI ()
		{
				GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
				GUIStyle myButtonStyle1 = new GUIStyle (GUI.skin.button);
				myButtonStyle.fontSize = 70;
				myButtonStyle1.fontSize = 30;
				GUI.depth = 0;
				MasterServer.ipAddress = "192.168.59.3";
				MasterServer.port = 23466;
				Network.natFacilitatorIP = "192.168.59.3";
				Network.natFacilitatorPort = 50005;
				if (Network.peerType == NetworkPeerType.Disconnected) {
						//GUI.Label(new Rect(320,15,480,60),"Status:Disconnected",myButtonStyle1);
						//GUI.Label(new Rect(25,15,280,60),Network.player.ipAddress,myButtonStyle1);
						//ipAdd=GUI.TextField(new Rect(112,480,800,130),ipAdd,myButtonStyle);
						Debug.Log ("in connect");
						MasterServer.RequestHostList ("MuseumHeist");
						HostData[] hostDataArray = MasterServer.PollHostList ();
						foreach (var elem in  hostDataArray) {
								GUILayout.BeginHorizontal ();
								Rect buttonSize = new Rect (Screen.width * 0.75f, Screen.height - 80f, 100, 50);
								Rect connectButton = new Rect (Screen.width, Screen.height - 100f, 300, 100);
								buttonSize.x = Screen.width * 0.75f - connectButton.size.x * 0.5f;
								connectButton.x = connectButton.x - connectButton.size.x;
								var name = elem.gameName;
								//GUI.Label(buttonSize,name,myButtonStyle1);
								GUILayout.Space (5);
								string hostInfo;
								hostInfo = "[";
								foreach (var host in elem.ip)
										hostInfo = hostInfo + host + ":" + elem.port + " ";
								hostInfo = hostInfo + "]";
								//GUILayout.Label(hostInfo);
								GUILayout.Space (5);
								//GUILayout.Label(elem.comment);
								GUILayout.Space (5);
								GUILayout.FlexibleSpace ();
								if (GUI.Button (connectButton, "As Thief", myButtonStyle)) {
										// Connect to HostData struct, internally the correct method is used (GUID when using NAT).
										Network.Connect (elem);
								}
								GUILayout.EndHorizontal (); 
				
						}
						//				ConnectionTesterStatus ct = Network.TestConnection();
						//				while(ct.Equals(ConnectionTesterStatus.Undetermined))
						//				Debug.Log(ct);
						//				Debug.Log(ct);
						Rect guardButton = new Rect (0, Screen.height - 100f, 300, 100);
						//guardButton.x=guardButton.size.x/2;
						if (GUI.Button (guardButton, "As Guard", myButtonStyle)) {
								Network.InitializeServer (8, portNum, !Network.HavePublicAddress ());
								MasterServer.RegisterHost ("MuseumHeist", "As Thief", "come join");
								//MasterServer.RegisterHost("MuseumHeist","Guard1");
								//networkView.RPC("startGame",RPCMode.All,role);
						}
			
				} else if (Network.peerType == NetworkPeerType.Client) {
						//				GUI.Label(new Rect(100,100,100,100),"Connected as client");
						//				if(GUI.Button(new Rect(112,240,100,100),"Disconnect"))
						//					Network.Disconnect(200);
				} else if (Network.peerType == NetworkPeerType.Server) {
						Rect waitingMessageRectangle = new Rect (Screen.width / 2 - 125, Screen.height / 4, 250, 60);
						Rect disconnectButtonRectangle = new Rect (0, Screen.height - 70, 370, 70);
						GUI.Label (waitingMessageRectangle, "Waiting for a thief", myButtonStyle1);
						if (GUI.Button (disconnectButtonRectangle, "Disconnect", myButtonStyle))
								Network.Disconnect (200);
				}
		}
		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
		void OnConnectedToServer ()
		{
				Debug.Log ("in onconnected");
				networkView.RPC ("startGame", RPCMode.All);
		}
		[RPC]
		public void startGame ()
		{
				Application.LoadLevel ("MuseumScene");
				Debug.Log ("startgame");
		}
}


