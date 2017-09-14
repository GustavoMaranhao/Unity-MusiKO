using UnityEngine;
using System.Collections;

public class updown : MonoBehaviour {

	private Vector3 startPos;

	public float maxUpAndDown = 1;             // amount of meters going up and down
	public float speed = 200;            // up and down speed
	protected float angle = 0;            // angle to determin the height by using the sinus
	protected float toDegrees = Mathf.PI/180;    // radians to degrees

	float Step; //A variable we increment
	float Offset; //How far to offset the object upwards
	public float adjustment = 1;

	void Start(){
		//Store where we were placed in the editor
		var InitialPosition = transform.position;
		//Create an offset based on our height
		Offset = transform.position.y + transform.localScale.y;
	}

	void FixedUpdate () {		
		Step +=0.01f*speed;
		//Make sure Steps value never gets too out of hand 
		if(Step > 999999){Step = 1f;}
		
		//Float up and down along the y axis, 
		Vector3 tempPos;
		tempPos.y = Mathf.Sin(Step)/adjustment+Offset/maxUpAndDown;
		transform.position = new Vector3 (transform.position.x, tempPos.y, transform.position.z);
	}
}
