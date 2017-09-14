using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TelaTransicao : MonoBehaviour {

	public int NumeroAtual = 0;
	public int NumeroTotal = 2;
	
	public Text TextoTitulo;
	public Text Texto;
	public Image MeuPlanoFundo;
	
	public string[] MeuTexto;
	public string[] MeuTextoTitulo;

	public GameObject[] TodasTransicoes;
	public ContainerTransicao[] AjudanteTodasTransicoes;
	
	public bool PodeIr = true;
	// Update is called once per frame
	void Start(){
		PodeIr = false;
		ContainerSons.ControladorSons.PararLoopsMusicas();
		ContainerSons.ControladorSons.PararTodosOsSons();
		ContainerSons.ControladorSons.TocarMusicaLoop (3);

		NumeroAtual = 0;
		AjudanteTodasTransicoes = new ContainerTransicao[TodasTransicoes.Length];
		for(int x = 0;x<TodasTransicoes.Length;x++){
			AjudanteTodasTransicoes[x] = TodasTransicoes[x].GetComponent<ContainerTransicao>();
		}
		NumeroTotal = AjudanteTodasTransicoes[DadosIntroducao.ControleDadosDaIntroducao.IdFalas].MeuTexto.Length;
		MeuTexto = AjudanteTodasTransicoes[DadosIntroducao.ControleDadosDaIntroducao.IdFalas].MeuTexto;
		MeuTextoTitulo = AjudanteTodasTransicoes[DadosIntroducao.ControleDadosDaIntroducao.IdFalas].MeuTextoTitulo;
		MeuPlanoFundo.sprite = AjudanteTodasTransicoes[DadosIntroducao.ControleDadosDaIntroducao.IdFalas].PlanoDeFundo;


		StartCoroutine(Falar());
	}
	void Update () {
		
		if((Input.GetKey("space"))||(Input.GetMouseButtonUp(0))){
			
			if(PodeIr==true){
				
				PodeIr = false;
				StartCoroutine(Falar());
			}
		}
		
	}
	
	public IEnumerator Falar(){
		
		if(NumeroTotal==NumeroAtual){
			ContainerSons.ControladorSons.PararLoopsMusicas();
			ContainerSons.ControladorSons.PararTodosOsSons();

			Application.LoadLevel(DadosIntroducao.ControleDadosDaIntroducao.ProximaCena);

		}else{

			
			if (NumeroAtual < NumeroTotal) {
				int Comprimento = MeuTexto[NumeroAtual].Length;
				Texto.text = "";
				for(int x = 0;x<Comprimento;x++){
					Texto.text =Texto.text+MeuTexto[NumeroAtual][x];
					yield return new WaitForSeconds(0.01f);
				}

				int Comprimento2 = MeuTextoTitulo[NumeroAtual].Length;
				TextoTitulo.text = "";
				for(int x = 0;x<Comprimento2;x++){
					TextoTitulo.text =TextoTitulo.text+MeuTextoTitulo[NumeroAtual][x];
					yield return new WaitForSeconds(0.01f);
				}

				NumeroAtual = NumeroAtual+1;
				PodeIr = true;
				
			}
		}
		
	}//falar

	public void ClicarSkip(){
		ContainerSons.ControladorSons.PararLoopsMusicas();
		ContainerSons.ControladorSons.PararTodosOsSons();
		
		Application.LoadLevel(DadosIntroducao.ControleDadosDaIntroducao.ProximaCena);
	}
}
