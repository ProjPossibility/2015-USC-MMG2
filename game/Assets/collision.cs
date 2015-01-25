using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour
{

		private int count;
		public collision collisionScript;
		public NetworkView network;
		public GameObject victory_gui;
		public GameObject defeat_gui;
		public Spawn spawn;
		public AudioSource gameover;
		public AudioSource victory;
		private bool end = false;
		bool lost = false;
		// Use this for initialization
		void Start ()
		{
				count = 0;
				end = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
		}
		[RPC]
		void endGame ()
		{
				end = true;
				Debug.Log ("in end game");
				this.spawn.End ();
				(this.lost ? gameover : victory).audio.Play ();
				GameObject.Instantiate (this.lost ? this.defeat_gui : this.victory_gui);
		}
		void OnCollisionEnter (Collision col)
		{
				if (end)
						return;
				Debug.Log ("in on collision");
				if (col.gameObject.name == "Cube")
						Debug.Log ("collision with cube " + this.count++);
				this.lost = true;
				Control controlScript = this.GetComponent<Control> ();
				controlScript.Stop ();
				controlScript.enabled = false;
				this.network.RPC ("endGame", RPCMode.All);
				this.collider.isTrigger = true;
		}
		void OnGUI ()
		{

		}
}
