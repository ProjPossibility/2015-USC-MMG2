using UnityEngine;
using System.Collections;

public class audiopitch : MonoBehaviour {
		//Arrange the minimum and maximum pitches
		public static float maxPitch = 8; //Right Turn
	public static float minPitch = 3; //Left Turn
		
		
		// Decreases the pitch in the given seconds
	public float startingPitch = 5;
	public float target;
	public float timeToDecrease = 5;


	
	public void setTarget(float t) {
		target = t;
		
	}
	// Use this for initialization
	void Start () {
		audio.pitch = startingPitch;
		this.target= startingPitch;
	}
	
	// Update is called once per frame
	void Update () {
		//Implement this Weird Stuff Just so that stuff works ok?
		if(!this.audio.isPlaying){
			this.audio.Play();
		}
//		Debug.Log(audio.pitch);
		
		//Let's compare audio.pitch to target
		if(audio.pitch > target)
		{
			audio.pitch -= ((Time.deltaTime * startingPitch)/timeToDecrease);
			if(audio.pitch < target){
				audio.pitch = target;
			}
		}
		else if(audio.pitch <= target)
		{
			audio.pitch += ((Time.deltaTime * startingPitch)/timeToDecrease);
			if(audio.pitch > target){
				audio.pitch = target;
			}
		}
		
		//if(move){
		//audio.pitch += ((Time.deltaTime * startingPitch)/timeToDecrease);
		//if(audio.pitch >= maxPitch){
		//move = !move;
		//}
		//}
		//else{
		//audio.pitch -= ((Time.deltaTime * startingPitch)/timeToDecrease);
		//if(audio.pitch <= minPitch){
		//move = !move;	
		//}
	}
}
