using UnityEngine;
using System.Collections;

public class randomGenerate : MonoBehaviour {

	public GameObject prefab;
	// Use this for initialization
	void Start () {
		CreateRandom ();

	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	void CreateRandom()
	{
			StartCoroutine(delay (10));
			Create();
	}
	void Create()
	{
		Vector3 pos = new Vector3 (1, 0, 3);
		GameObject capsule = GameObject.Instantiate (prefab,pos,Quaternion.identity) as GameObject;
		capsule.transform.rotation = Quaternion.Euler (0, 0, 90);
		Debug.Log ("in generate");
		Vector3 vel = new Vector3 (0, 0, -10);
		capsule.rigidbody.velocity = vel;
	}
	IEnumerator delay(float seconds)
	{
		yield return new WaitForSeconds (seconds);
	}
}
