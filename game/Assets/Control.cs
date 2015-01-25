using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour
{
		public float deadzone_width = .2f;
		public float x_limit = 5;
		public int speed;
		private float deadzone;
		// Use this for initialization
		void Start ()
		{
				speed = 600;
		} 
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKey (KeyCode.D) || Input.acceleration.x > deadzone_width / 2) {
						if (rigidbody.transform.position.x > x_limit) {
								rigidbody.velocity = Vector3.zero;
								return;
						}
						Vector3 movement = new Vector3 ((speed * Time.deltaTime), 0, 0);
						rigidbody.velocity = movement;
		} else if (Input.GetKey (KeyCode.A) || Input.acceleration.x < -deadzone_width / 2) {
						if (rigidbody.transform.position.x < -x_limit) {
								rigidbody.velocity = Vector3.zero;
								return;
						}
						Vector3 movement = new Vector3 ((-1 * speed * Time.deltaTime), 0, 0);
						rigidbody.velocity = movement;
				} else {
						rigidbody.velocity = Vector3.zero;
						rigidbody.angularVelocity = Vector3.zero;
				}
		}
}
