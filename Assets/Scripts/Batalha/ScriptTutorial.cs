using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScriptTutorial : MonoBehaviour {

	public int NumeroAtual = 0;
	public int NumeroTotal = 2;

	public Text TextoJogo;
	public Text TextoNome;
	public Image MinhaFoto;
	public Image FotoAjudante;

	public string[] MinhasFalas;
	public string[] MeusNomes;
	public Sprite[] MinhasImagens;
	public Sprite[] MinhasImagens2;

	public bool PodeIr = true;
	// Update is called once per frame
	void Start(){
		PodeIr = false;
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
			ContainerSons.ControladorSons.TocarMusicaLoop(0);
			GameMasterCombate.Rodando=true;
			Destroy(gameObject);
		}else{
			TextoNome.text = MeusNomes[NumeroAtual];
			MinhaFoto.sprite = MinhasImagens2[NumeroAtual];
			FotoAjudante.sprite = MinhasImagens[NumeroAtual];
			
			if (NumeroAtual < NumeroTotal) {
				int Comprimento = MinhasFalas[NumeroAtual].Length;
				TextoJogo.text = "";
				for(int x = 0;x<Comprimento;x++){
					TextoJogo.text =TextoJogo.text+MinhasFalas[NumeroAtual][x];
					yield return new WaitForSeconds(0.01f);
				}
				
				NumeroAtual = NumeroAtual+1;
				PodeIr = true;
				
			}
		}

	}//falar
}
