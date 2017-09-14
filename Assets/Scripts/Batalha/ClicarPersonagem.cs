using UnityEngine;
using System.Collections;

public class ClicarPersonagem : MonoBehaviour {

	public ControladorTurno MeuPai;
	void OnMouseDown(){
		print ("Clicou Personagem");
		if((GameMasterCombate.Rodando==false)&&(GameMasterCombate.ExecutandoClique==false)){
			print ("Clicou Personagem");

		}
		
	}
}
