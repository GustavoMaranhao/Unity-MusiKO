using UnityEngine;
using System.Collections;


public class DadosIntroducao : MonoBehaviour {
	
	public static DadosIntroducao ControleDadosDaIntroducao;
	
	public int IdFalas;
	public string ProximaCena;
	
	
	// Use this for initialization
	void Awake () {
		
		if(ControleDadosDaIntroducao == null){
			
			DontDestroyOnLoad(gameObject);
			ControleDadosDaIntroducao = this;
			
		}else if(ControleDadosDaIntroducao != this){
			
			Destroy(gameObject);
		}
		
		
	}//awake
	
	
}