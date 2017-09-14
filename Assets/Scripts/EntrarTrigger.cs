using UnityEngine;
using System.Collections;

public class EntrarTrigger : MonoBehaviour {

	public GameObject Mestre;
	public GameMasterCombate AjudanteMestre;
	
	
	public void Start(){
		Mestre = GameObject.Find("ControladorJogo");
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
	}

	void OnTriggerEnter(Collider other) {
		AjudanteMestre.DesligarCorrida(int.Parse(other.name),
		                               gameObject.transform.position.x,
		                               gameObject.transform.position.z);
	}

}
