using UnityEngine;
using System.Collections;

public class destroyObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < -7)
						Destroy (this.gameObject);
	}
}
