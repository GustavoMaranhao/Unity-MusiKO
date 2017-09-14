using UnityEngine;
using System.Collections;


/// <summary>
/// Associar este script ao modelo de Barcaca voadora, no componente interno  [pas]
/// A velocidade padrao (150.0f) pode ser mudada no inspector ou mudada para outro valor
 // neste script.
/// </summary>
/// 
public class girarPasBarcaca : MonoBehaviour {

	public float velocidadePas = 150.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 giro = new Vector3 (0, 0, velocidadePas*Time.deltaTime);
		gameObject.transform.Rotate (giro);
	}
}
