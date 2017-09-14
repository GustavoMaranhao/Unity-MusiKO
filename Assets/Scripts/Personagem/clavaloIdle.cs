using UnityEngine;
using System.Collections;

public class clavaloIdle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log ("Clavalo idle");
		gameObject.GetComponent<Animation>().Play("clavaloIdle");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
