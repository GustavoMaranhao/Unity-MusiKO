using UnityEngine;
using System.Collections;

public class ClicarMinimapa : MonoBehaviour {

	private GameObject  Mestre;
	
	private GameMasterCombate AjudanteMestre;

	private RectTransform MeuRect;
	
	void Awake(){
		Mestre = GameObject.Find("ControladorJogo");
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
		MeuRect = gameObject.GetComponent<RectTransform>();
	}
	
	public void ClicarCedulaMinimapa(){
		AjudanteMestre.ClicouTerrenoMinimapa((int)MeuRect.anchoredPosition.x/6,(int)MeuRect.anchoredPosition.y/6);
	}
}
