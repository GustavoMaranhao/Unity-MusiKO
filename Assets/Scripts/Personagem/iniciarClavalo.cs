using UnityEngine;
using System.Collections;

public class iniciarClavalo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log ("Clavalo feeding");
		gameObject.GetComponent<Animation>().Play("clavaloFeeding");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
