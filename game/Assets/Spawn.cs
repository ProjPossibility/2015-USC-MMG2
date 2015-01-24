using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
		private float elapsed = 0f;
		public  float SPAWN_FREQ = 1f;
		public  float UP_OFFSET = 5f;
		public GameObject prefab;
		public GameObject parent;
		public float x;
	private float leftEdge, rightEdge;
		// Use this for initialization
		void Start ()
		{
				x = 2;
		leftEdge = camera.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		rightEdge = camera.ScreenToWorldPoint (new Vector3 (camera.pixelWidth, 0, 0));
		}
	
		// Update is called once per frame
		void Update ()
		{
				GameObject leftObject, rightObject;
				elapsed += Time.deltaTime;
				if (elapsed > SPAWN_FREQ) 
				{
						elapsed = 0;
						leftObject=this.spawn ();
						rightObject=this.spawn();
				}
		float lengthOfLeft = x - leftEdge;

		}

		private GameObject spawn ()
		{
		Vector3 cameraPos = new Vector3(0,0,parent.transform.position.z);
		Vector3 offset = parent.transform.up * UP_OFFSET;
		offset.y = 0;
				GameObject capsule = GameObject.Instantiate (prefab, cameraPos+offset, Quaternion.identity) as GameObject;
				capsule.transform.rotation = Quaternion.Euler (0, 0, 90);
				Vector3 vel =new Vector3(0,0,-4);
				capsule.rigidbody.velocity = vel;
		return capsule;
		}
}
