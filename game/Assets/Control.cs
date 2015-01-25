using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour
{
		private int shift;
		public int speed;
		// Use this for initialization
		void Start ()
		{
				shift = 0;
				speed = 600;
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (Input.acceleration.x > 0) {
						if (shift > 0) {
								rigidbody.velocity = Vector3.zero;
								rigidbody.angularVelocity = Vector3.zero;
						}
						Vector3 movement = new Vector3 ((speed * Time.deltaTime), 0, 0);
						rigidbody.velocity = movement;
						shift = -1;
				} else if (Input.acceleration.x < 0) {
						if (shift < 0) {
								rigidbody.velocity = Vector3.zero;
								rigidbody.a ngularVelocity = Vector3.zero;
						}
						Vector3 movement = new Vector3 ((-1 * speed * Time.deltaTime), 0, 0);
						rigidbody.velocity = movement;
						shift = 1;
				} else {
						rigidbody.velocity = Vector3.zero;
						rigidbody.angularVelocity = Vector3.zero;
				}
		}
}
