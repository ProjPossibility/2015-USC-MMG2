using UnityEngine;
using System.Collections;

public class guiscale : MonoBehaviour {
	private float sWidth;
	private float sHeight; 
	private float guiRatioX;
	private float guiRatioY;
	int sizegui;
	//A vector3 that will be created using the scale ratio
	private Vector3 GUIsF;
	// Use this for initialization
	void Start () {
		sWidth = Screen.width;
		sHeight = Screen.height;
		//calculate the rescale ratio
		guiRatioX = sWidth/1920 * sizegui;
		guiRatioY = sHeight/1080* sizegui;
		//create a rescale Vector3 with the above ratio
		GUIsF = new Vector3(guiRatioX,guiRatioY,1);
		GUI.matrix = Matrix4x4.TRS (new Vector3 (GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
	}
	void OnGui(){
				GUI.matrix = Matrix4x4.TRS (new Vector3 (GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
		Debug.Log ("GUIO");
		}
	// Update is called once per frame
	void Update () {
	
	}
}
