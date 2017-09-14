using UnityEngine;
using System.Collections;

public class InicioDeJogo : MonoBehaviour {

	public int numeroDaMusica = 0;

	public void Iniciar(){



		ContainerSons.ControladorSons.PararLoopsMusicas();
		ContainerSons.ControladorSons.PararTodosOsSons();

		DadosIntroducao.ControleDadosDaIntroducao.ProximaCena = "CenaTesteVila";
		DadosIntroducao.ControleDadosDaIntroducao.IdFalas = 0;
		Application.LoadLevel("CenaTransicao");
	}
	public void Carregar(){

	}

	void Start(){

		ContainerSons.ControladorSons.PararLoopsMusicas();
		ContainerSons.ControladorSons.TocarMusicaLoop(numeroDaMusica);
	}

	public void VoltarMenu(){

		ContainerSons.ControladorSons.PararLoopsMusicas();
		ContainerSons.ControladorSons.PararTodosOsSons();

		Application.LoadLevel("MenuPrincipal");
	}
}
