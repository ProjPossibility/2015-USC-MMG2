using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour
{
		private Animator anim;
		public float deadzone_width = .2f;
		public float x_limit = 5;
		public int speed = 500;
		private float deadzone;
		private bool stop = false;
		// Use this for initialization
		void Start ()
		{
				this.stop = false;
				this.anim = this.GetComponent<Animator> ();
		} 
		public void Stop ()
		{
				this.stop = true;
				rigidbody.velocity = Vector3.zero;
		}
		// Update is called once per frame
		void Update ()
		{
				if (this.stop)
						return;
				if (Input.GetKey (KeyCode.D) || Input.acceleration.x > deadzone_width / 2) {
						if (rigidbody.transform.position.x > x_limit) {
								rigidbody.velocity = Vector3.zero;
								this.anim.SetInteger ("lir", 1);
								return;
						}
						Vector3 movement = new Vector3 ((speed * Time.deltaTime), 0, 0);
						rigidbody.velocity = movement;
						this.anim.SetInteger ("lir", 2);
				} else if (Input.GetKey (KeyCode.A) || Input.acceleration.x < -deadzone_width / 2) {
						if (rigidbody.transform.position.x < -x_limit) {
								rigidbody.velocity = Vector3.zero;
								this.anim.SetInteger ("lir", 1);
								return;
						}
						Vector3 movement = new Vector3 ((-1 * speed * Time.deltaTime), 0, 0);
						rigidbody.velocity = movement;
						this.anim.SetInteger ("lir", 0);
				} else {
						rigidbody.velocity = Vector3.zero;
						rigidbody.angularVelocity = Vector3.zero;
						this.anim.SetInteger ("lir", 1);
				}
		}
}
