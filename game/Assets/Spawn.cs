using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
		private float elapsed = 0f;
		private float elapsedForX = 0f;
		public  float SPAWN_FREQ = 1f;
		public float CHANGE_X_FREQ = 3f;
		public  float UP_OFFSET = 5f;
		public float Y_OFFSET = 1;
		public GameObject prefab;
		public GameObject parent;
		public Camera cameraObj;
		public float x;
		private Vector3 leftEdge, rightEdge;
		// Use this for initialization
		void Start ()
		{
				leftEdge = cameraObj.ScreenToWorldPoint (new Vector3 (0, 0, 0));
				rightEdge = cameraObj.ScreenToWorldPoint (new Vector3 (cameraObj.pixelWidth, 0, 0));
				x = -8;
				Debug.Log (x);
		}
	
		// Update is called once per frame
		void Update ()
		{
				elapsed += Time.deltaTime;
				if (elapsed > SPAWN_FREQ) {
						elapsed = 0;
						this.spawn (x, true);
						this.spawn (x, false);
				
						//rightObject=this.spawn(x);
						/*float lengthOfRight=(float)rightEdge.x-x-obstacleOffset;
						Vector3 rightScale=new Vector3(lengthOfRight,0,0);
						rightObject.transform.localScale=rightScale;
						Vector3 rightPosition = new Vector3(rightEdge.x - lengthOfRight/2,0,0);
						rightObject.transform.position=rightPosition;*///
				}
				elapsedForX += Time.deltaTime;
				if (elapsedForX > CHANGE_X_FREQ) {
						this.changeX ();
						elapsedForX=0;
				}


		}

		private void changeX ()
		{
		float up = (rightEdge.x - x )/ 8;
		float down = (leftEdge.x - x )/ 8;
				this.x += Random.Range (up,down);
		if (this.x < leftEdge.x+1) {
			this.x = leftEdge.x+1;
				}
		if (this.x > rightEdge.x - 1) {
						this.x = rightEdge.x - 1;
				}
		}

		private GameObject spawn (float x, bool isLeft)
		{
				float block_length = Mathf.Abs(isLeft ? x - leftEdge.x : rightEdge.x - x);
		block_length -= Random.Range (1, 3);
				Vector3 block_scale = new Vector3 (block_length, 1, 1);
				Vector3 block_position = new Vector3 ((isLeft ? leftEdge.x + block_length / 2 : rightEdge.x - block_length / 2), Y_OFFSET, UP_OFFSET);

				GameObject capsule = GameObject.Instantiate (prefab, block_position, Quaternion.identity) as GameObject;

				capsule.transform.localScale = block_scale;
				capsule.transform.rotation = Quaternion.Euler (0, 0, 0);
				Vector3 vel = new Vector3 (0, 0, -4);
				capsule.rigidbody.velocity = vel;
				capsule.rigidbody.useGravity = false;
				Debug.Log (this.leftEdge + " " + this.rightEdge);
				return capsule;
		}
}

