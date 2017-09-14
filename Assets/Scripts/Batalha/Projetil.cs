using UnityEngine;
using System.Collections;

public class Projetil : MonoBehaviour {

	public float Speed = 1.0f;
	public Transform target;
	public GameObject MeuFilho;
	

	public GameObject MeuPai;
	public ControladorTurno AjudanteMeuPai;
	
	public void ColocarMeupai(GameObject ajudante){
		MeuPai = ajudante;
		AjudanteMeuPai = MeuPai.GetComponent<ControladorTurno>();
	}

	void Start(){
		//target.position = new Vector3(MeuFilho.transform.position.x+10,MeuFilho.transform.position.y+10,MeuFilho.transform.position.z+10);
	}
	void Update(){

		float step = Speed * Time.deltaTime;
		MeuFilho.transform.position = Vector3.MoveTowards(MeuFilho.transform.position, target.position, step);

		if(MeuFilho.transform.position == target.position){
			AjudanteMeuPai.AtivarDano();
			Destroy(gameObject);
		}

	}

}
