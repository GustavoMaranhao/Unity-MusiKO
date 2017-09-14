using UnityEngine;
using System.Collections;

public class ClicarFoto : MonoBehaviour {

	public GameObject  Mestre;

	public GameMasterCombate AjudanteMestre;

	void Start(){
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
	}

	public void ClicarNaFoto(){
		AjudanteMestre.ClicouNaFotoDoPersonagem();
	}
}
