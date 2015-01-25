using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	private int count;
	public bool showMsg;
	public collision collisionScript;
	public Control controlScript;
	// Use this for initialization
	void Start () {
		count = 0;
		showMsg = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision col)
	{
		showMsg = true;
		Debug.Log ("in on collision");
		if (col.gameObject.name == "Cube")
						Debug.Log ("collision with cube " + this.count++);
	}
	void OnGUI()
	{
		GUIStyle msgStlye = new GUIStyle();
		msgStlye.normal.textColor=Color.black;
		if(showMsg)
		{
			GUI.Label (new Rect (0,0,Screen.width / 2,Screen.height/2), "YOU LOSE",msgStlye);
			GameObject 	player= GameObject.Find("player");
			player.collider.isTrigger = true;
			controlScript = player.GetComponent<Control>();
			controlScript.enabled = false;
		}


	}
}
