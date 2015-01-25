using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public  float SPAWN_FREQ = 1f;
	public float CHANGE_X_FREQ = 3f;
	public float X_CH_SPEED = 1;
	public  float UP_OFFSET = 5f;
	public float Y_OFFSET =0f;
	public float MIN_DELTA_TARGET = 2;
	public float MIN_GAP = 2;
	public float MAX_GAP = 4;
	public float GAME_SPEED = 4;
	
	
	public GameObject prefab;
	public Camera cameraObj;
	private Vector3 leftEdge, rightEdge;
	public audiopitch pitch;
	
	
	public float target_x = 0;//where we wanna go
	public float curr_x;//where we are right now
	
	private float spawn_timer = 0;//used with SPAWN_FREQ to determine whether or not to spawn a new object
	
	enum spwstate{
		MOVE_LEFT,MOVE_RIGHT, IDLE
	}
	spwstate m_state;
	void chstate(spwstate s){
		Debug.Log (this.m_state + " going to " + s);	this.m_state = s;
	}

	// Use this for initialization
	void Start ()
	{
		m_state = spwstate.IDLE;
		this.curr_x = this.target_x;

		//try to get camera to right position
		/*
		 * Screen.SetResolution (640, 480, true);
		this.cameraObj.aspect = (Screen.currentResolution.width / Screen.currentResolution.height);

		float fT = 180 / Screen.width * Screen.height;
		fT = fT / (2.0f * Mathf.Tan (0.5f * this.cameraObj.fieldOfView * Mathf.Deg2Rad));
		Vector3 v3T = this.cameraObj.transform.position;
		v3T.z = -fT;
		transform.position = v3T;
*/

		//set the left and right edges
		leftEdge = cameraObj.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		rightEdge = cameraObj.ScreenToWorldPoint (new Vector3 (cameraObj.pixelWidth, 0, 0));
		//Debug.Log (x);



		this.updateX ();
	}
	
	private GameObject spawn (float x, bool isLeft)
	{
		float block_length = Mathf.Abs (isLeft ? x - leftEdge.x : rightEdge.x - x);
		block_length -= Random.Range (MIN_GAP, MAX_GAP);
		Vector3 block_scale = new Vector3 (block_length, 1, 1);
		Vector3 block_position = new Vector3 ((isLeft ? leftEdge.x + block_length / 2 : rightEdge.x - block_length / 2), Y_OFFSET, UP_OFFSET);
		
		GameObject capsule = GameObject.Instantiate (prefab, block_position, Quaternion.identity) as GameObject;
		
		capsule.transform.localScale = block_scale;

		Vector3 vel = new Vector3 (0, 0, -GAME_SPEED);
		capsule.rigidbody.velocity = vel;
		capsule.rigidbody.useGravity = false;
		//Debug.Log (this.leftEdge + " " + this.rightEdge);
		return capsule;
	}
	private void trySpawn(){
		this.spawn_timer+=Time.deltaTime;
		if(this.spawn_timer > SPAWN_FREQ){
			this.spawn_timer = 0;
			this.spawn(this.curr_x,true);
			this.spawn(this.curr_x,false);
		}
	}
	/**
        This function will move curr_x to the left and update the state if appropriate.
        */
	private void updateLeft(){
		
		curr_x-=Time.deltaTime*X_CH_SPEED; 
		if(curr_x < this.target_x){
			this.chstate (spwstate.IDLE);
		}
		this.changePitch();
	}
	private void updateRight(){
		//write herete here
		
		curr_x+=Time.deltaTime*X_CH_SPEED;
	if(curr_x > this.target_x){
		this.chstate (spwstate.IDLE);
	} 
	this.changePitch();
}
private void changePitch()
{
	float pitchDiff=audiopitch.maxPitch-audiopitch.minPitch;        
	float w = Mathf.Abs(leftEdge.x);
	float pitchValue = (curr_x+w)/(rightEdge.x+w)* pitchDiff;
		if(this.pitch != null)
	this.pitch.setTarget(pitchValue+audiopitch.minPitch);
}
// Update is called once per frame
void Update ()
{
	this.trySpawn();
	switch(this.m_state){
	case spwstate.MOVE_LEFT:
		this.updateLeft();
		break;
	case spwstate.MOVE_RIGHT:
		this.updateRight();
		break;
	case spwstate.IDLE:
		this.updateX();
		break;
	}
	
}
private void updateX ()
{
	int randomNum;
	do{
		randomNum = Random.Range ((int)this.leftEdge.x+1, (int)this.rightEdge.x-1);
	}     while(Mathf.Abs(randomNum-this.target_x) < MIN_DELTA_TARGET);
	this.target_x = randomNum;
	if(this.target_x > this.curr_x){
		this.chstate(spwstate.MOVE_RIGHT);
	}else{
		this.chstate (spwstate.MOVE_LEFT);                
	}    }

}