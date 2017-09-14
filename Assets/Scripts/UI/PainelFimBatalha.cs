using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PainelFimBatalha : MonoBehaviour {
	
	public bool Tutorial = true;
	public float posicaoOriginal;
	public float LimiteInferior;
	public float Velocidade = 0;
	public bool Subir = true;
	public bool Vitoria = true;
	public bool Meio = true;

	public Text MeuTexto;
	
	public RectTransform meuTranform;
	// Update is called once per frame
	void Start (){
		meuTranform = GetComponent<RectTransform>();
		if(Meio==true){
			posicaoOriginal = 50*Screen.height/566;
		}
	}
	void Update () {
		
		if(Subir==true){
			
			if(meuTranform.anchoredPosition.y>=posicaoOriginal){
				
			}else{
				meuTranform.anchoredPosition = new Vector2(meuTranform.anchoredPosition.x,meuTranform.anchoredPosition.y+Velocidade);
			}

			if(Input.GetKeyDown("space")){
				if(Tutorial==true){
					if(Vitoria==true){
						ContainerSons.ControladorSons.PararLoopsMusicas();
						ContainerSons.ControladorSons.PararTodosOsSons();
						
						DadosIntroducao.ControleDadosDaIntroducao.ProximaCena = "CenaVilaAposAtaque2";
						DadosIntroducao.ControleDadosDaIntroducao.IdFalas = 2;
						Application.LoadLevel("CenaTransicao");
						
						//Application.LoadLevel("Loot");
					}else{
						ContainerSons.ControladorSons.PararLoopsMusicas();
						ContainerSons.ControladorSons.PararTodosOsSons();
						
						DadosIntroducao.ControleDadosDaIntroducao.ProximaCena = "CenaVilaAposAtaque2";
						DadosIntroducao.ControleDadosDaIntroducao.IdFalas = 3;
						Application.LoadLevel("CenaTransicao");
						
						//Application.LoadLevel("GameOver");
					}
				}else{
					if(Vitoria==true){
						ContainerSons.ControladorSons.PararLoopsMusicas();
						ContainerSons.ControladorSons.PararTodosOsSons();
						

						Application.LoadLevel("FimDemo");
						
						//Application.LoadLevel("Loot");
					}else{
						ContainerSons.ControladorSons.PararLoopsMusicas();
						ContainerSons.ControladorSons.PararTodosOsSons();
						

						Application.LoadLevel("GameOver");
						
						//Application.LoadLevel("GameOver");
					}
				}


			}
			
		}else{
			if(meuTranform.anchoredPosition.y<=LimiteInferior){
				
			}else{
				meuTranform.anchoredPosition = new Vector2(meuTranform.anchoredPosition.x,meuTranform.anchoredPosition.y-Velocidade);
			}
		}
		
	}

	public void MudarTexto(string Teste){
		MeuTexto.text = Teste;
		FazerSubir();
	}

	public void FazerSubir(){
		Subir = true;
	}
	public void FazerDescer(){
		Subir = false;
	}
	
	
}
