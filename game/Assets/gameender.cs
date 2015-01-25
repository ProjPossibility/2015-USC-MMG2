using UnityEngine;
using System.Collections;

public class gameender : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
		public void end ()
		{
		
				MasterServer.ClearHostList ();
				MasterServer.UnregisterHost ();
				Network.Disconnect ();
				Application.LoadLevel ("mainmenu");
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
}
