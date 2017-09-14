using UnityEngine;
using System.Collections;

public class animarGuitarra : MonoBehaviour {

	public GameObject personagem;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Animation animacao = personagem.GetComponent<Animation>();
		animacao.Play( "Take 001"); 
		Debug.Log (	"Take 001"); 
	}
}
