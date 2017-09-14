using UnityEngine;
using System.Collections;

public class ClicarFotoBarra : MonoBehaviour {

	private GameObject  Mestre;
	
	private GameMasterCombate AjudanteMestre;
	
	void Start(){
		Mestre = GameObject.Find("ControladorJogo");
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
	}
	
	public void ClicarNaFoto(){
		AjudanteMestre.ClicouNaFotoDopersonagemEspecifico(int.Parse(gameObject.name));
	}
}
