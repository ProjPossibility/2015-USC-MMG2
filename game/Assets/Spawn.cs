using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
		private float elapsed = 0f;
		public  float SPAWN_FREQ = 1f;
		public  float UP_OFFSET = 5f;
		public GameObject prefab;
	public GameObject parent;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				elapsed += Time.deltaTime;
				if (elapsed > SPAWN_FREQ) {
						elapsed = 0;
						this.spawn ();
				}
		}

		private void spawn ()
		{
		Vector3 cameraPos = new Vector3(0,0,parent.transform.position.z);
		Vector3 offset = parent.transform.up * UP_OFFSET;
		offset.y = 0;
				GameObject capsule = GameObject.Instantiate (prefab, cameraPos+offset, Quaternion.identity) as GameObject;
				capsule.transform.rotation = Quaternion.Euler (0, 0, 90);
				Vector3 vel =new Vector3(0,0,-4);
				capsule.rigidbody.velocity = vel;
		}
}
