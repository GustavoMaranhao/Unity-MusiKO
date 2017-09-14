using UnityEngine;
using System.Collections;

public class AjusteNavMesh : MonoBehaviour {

	public GameObject ObjetoEspelhado;

	// Update is called once per frame
	void Update () {


		gameObject.transform.position = new Vector3(ObjetoEspelhado.transform.position.x,gameObject.transform.position.y,ObjetoEspelhado.transform.position.z);
		gameObject.transform.rotation = ObjetoEspelhado.transform.rotation;

	}
}
