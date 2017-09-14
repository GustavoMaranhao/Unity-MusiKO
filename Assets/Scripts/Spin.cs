using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public float spinSpeed = 50f;
	public float translateSpeed = 0.5f;
	public float translateTresholds = 3f;
	public string axisChoice = "y";

	private float actualTresholds;

	void Start(){
		actualTresholds = transform.position.y + translateTresholds;
	}

	// Update is called once per frame
	void Update () {
		switch (axisChoice) {
		case "x":
			transform.Rotate (spinSpeed * Time.deltaTime, 0, 0);
			break;
		case"y":
			transform.Rotate (0, spinSpeed * Time.deltaTime, 0);
			break;
		case"z":
			transform.Rotate (0, 0, spinSpeed * Time.deltaTime);
			break;
		default:
			transform.Rotate (0, spinSpeed * Time.deltaTime, 0);
			break;
		}
		/*int dir = 1;
		if (transform.localPosition.y < actualTresholds)
			dir = 1;
		else 
			dir = -1;

		transform.Translate (0, dir * translateSpeed * Time.deltaTime, 0);*/
	}
}
