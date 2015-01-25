#pragma strict

public class audioPitch extends MonoBehaviour
{
	//Arrange the minimum and maximum pitches
	var maxPitch = 7; //Right Turn
	var minPitch = 2; //Left Turn


	// Decreases the pitch in the given seconds
	var startingPitch = 4;
	var target = startingPitch;
	var timeToDecrease = 5;
	/*function RightTurn(){
	timeToDecrease -=Time.deltaTime;
	while (timeToDecrease != 0)
		{
			audio.pitch += ((Time.deltaTime * startingPitch) /timeToDecrease);
		
		}
	}*/
	
	function setInt(t) {
		target = t;
	
	}
	
	
	function Start() {
		audio.pitch = startingPitch;
	}

var move = false;
	function Update() {
	//Implement this Weird Stuff Just so that stuff works ok?
	if(!audio.isPlaying){
	audio.Play();
	}
	Debug.Log(audio.pitch);
	
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