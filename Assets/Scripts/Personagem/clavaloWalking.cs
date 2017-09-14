using UnityEngine;
using System.Collections;

public class clavaloWalking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log ("Clavalo walking");
		if (gameObject.GetComponent<Animation>() != null)
			gameObject.GetComponent<Animation>().Play("clavaloWalking");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
