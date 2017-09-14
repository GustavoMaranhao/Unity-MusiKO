using UnityEngine;
using System.Collections;

public class iniciarSegundaBatalha : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") 
		{
			DadosCombate.ControleDadosCombate.NumeroDeInimigos[0] = 1; // quantidades de inimigos do id dessa posicao
			DadosCombate.ControleDadosCombate.NumeroDeInimigos[1] = 1;
			DadosCombate.ControleDadosCombate.QuaisInimigos[0] = 0;   // ID do inimigo
			DadosCombate.ControleDadosCombate.QuaisInimigos[1] = 1;

			DadosIntroducao.ControleDadosDaIntroducao.ProximaCena = "TesteMecanicaCombate";
			DadosIntroducao.ControleDadosDaIntroducao.IdFalas = 4;
			Application.LoadLevel("CenaTransicao");

			//Application.LoadLevel("TesteMecanicaCombate");
		}
	}
}
