using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	private int count;
	public collision collisionScript;
	public Control controlScript;
	public NetworkView network;
	public GameObject victory_gui;
	public GameObject defeat_gui;

	bool lost = false;
	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	[RPC]
	void endGame(){
		Debug.Log ("in end game");
		GameObject.Instantiate (this.lost ? this.defeat_gui : this.victory_gui);
	}
	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("in on collision");
		if (col.gameObject.name == "Cube")
						Debug.Log ("collision with cube " + this.count++);
		this.lost = true;
		this.network.RPC ("endGame", RPCMode.All);
		this.collider.isTrigger = true;
		controlScript = this.GetComponent<Control>();
		controlScript.Stop();
		controlScript.enabled = false;
	}
	void OnGUI()
	{

	}
}
