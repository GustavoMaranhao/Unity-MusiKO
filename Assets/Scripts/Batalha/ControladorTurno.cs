using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorTurno : MonoBehaviour {

	public int ID;
	public bool SouEu;
	public bool EstouVivo;
	public string Nome;
	public float Agilidade;
	public float HPMax;
	public float HPAtual;
	public float MPMax;
	public float MPAtual;
	public float Stamina;
	public float Forca;
	public float Inteligencia;
	public float Defesa;
	public Sprite MinhaFoto;
	public int QuantidadeMovimento = 2;
	public int QuantidadeAtaque = 1;
	public float Dano;
	public int posicaoX = 0;
	public int posicaoY = 0;
	public int TipoArma = 0;
	public int[] ListaMagias;
	public Slider ExemploSlider;
	public Slider ControladorVida;
	public GameObject AjudanteCanvas;
	public int MeuModelo = 0;

	public Text ExemploTextoCombate;

	public GameObject Mestre;
	public GameMasterCombate AjudanteMestre;

	//Variaveis Referentes a IA Do Jogo

	public int ChanceAtacar = 0;
	public int ChanceMoverDirecaoPersonagem = 0;
	public int ChanceCorrer = 0;
	public int ChanceAgrupar = 0;
	public int ChanceSoltarMagia = 0;

	public bool Atacar = true;
	public bool MoverDirecaoPersonagem = true;
	public bool Correr = true;
	public bool Agrupar = true;
	public bool SoltarMagia = true;

	public int IDDoInimigo;
	public float DanodoInimigo;

	public ParticleSystem MeuSangue;
	public ParticleSystem TiroDeFogo;
	public ParticleSystem ApanharMelee;

	public GameObject ExemploBolaFogo;
	public GameObject ExemploBolaFogo2;
	public GameObject ExemploBolaFogo3;
	public GameObject ExemploFlechaFogo;

	void Start(){
		EstouVivo = true;
		AjudanteCanvas = GameObject.Find("MeuPreciosoCanvas");
		Mestre = GameObject.Find("ControladorJogo");

		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();

		ControladorVida = Instantiate(ExemploSlider) as Slider;
		ControladorVida.transform.SetParent(AjudanteCanvas.transform);
		ControladorVida.transform.localScale = new Vector3(0.015f,0.015f,1);
		ControladorVida.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+4,gameObject.transform.position.z);
		ControladorVida.name = Nome.ToString();

	}


	void OnMouseDown(){
		print ("Clicou Personagem");
		if((GameMasterCombate.Rodando==false)&&(GameMasterCombate.ExecutandoClique==false)){
			print ("Clicou Personagem");
			AjudanteMestre.ClicarTerreno(posicaoX*5,posicaoY*5);
		}
		
	}

	void Update(){

		ControladorVida.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+4,gameObject.transform.position.z);
	}
	
	public void AtivarDano(){
		AjudanteMestre.AtivarDanoAnim(IDDoInimigo,DanodoInimigo,ID);
	}

	public void TacarBolaFogo(int Mx,int Mz,int id,float dano){

		IDDoInimigo = id;
		DanodoInimigo = dano;
		GameObject temp = Instantiate(ExemploBolaFogo) as GameObject;
		Projetil AjudTemp = temp.GetComponent<Projetil>();
		AjudTemp.ColocarMeupai(gameObject);
		temp.transform.SetParent(Mestre.transform);
		temp.transform.localScale = new Vector3(1,1,1);
		temp.transform.position = new Vector3(0,0,0);
		
		temp.name = Nome.ToString();
		AjudTemp.target.position = new Vector3(Mx,gameObject.transform.position.y+2,Mz);
		AjudTemp.MeuFilho.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+2,gameObject.transform.position.z);




	}

	public void TacarBolaFogo2(int Mx,int Mz,int id,float dano){
		
		IDDoInimigo = id;
		DanodoInimigo = dano;
		GameObject temp = Instantiate(ExemploBolaFogo2) as GameObject;
		Projetil AjudTemp = temp.GetComponent<Projetil>();
		AjudTemp.ColocarMeupai(gameObject);
		temp.transform.SetParent(Mestre.transform);
		temp.transform.localScale = new Vector3(1,1,1);
		temp.transform.position = new Vector3(0,0,0);
		
		temp.name = Nome.ToString();

		AjudTemp.target.position = new Vector3(Mx,gameObject.transform.position.y+2,Mz);
		AjudTemp.MeuFilho.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+2,gameObject.transform.position.z);
		
		
		
		
	}

	public void TacarBolaFogo3(int Mx,int Mz,int id,float dano){
		
		IDDoInimigo = id;
		DanodoInimigo = dano;
		GameObject temp = Instantiate(ExemploBolaFogo3) as GameObject;
		Projetil AjudTemp = temp.GetComponent<Projetil>();
		AjudTemp.ColocarMeupai(gameObject);
		temp.transform.SetParent(Mestre.transform);
		temp.transform.localScale = new Vector3(1,1,1);
		temp.transform.position = new Vector3(0,0,0);
		
		temp.name = Nome.ToString();
	
		AjudTemp.target.position = new Vector3(Mx,gameObject.transform.position.y+2,Mz);
		AjudTemp.MeuFilho.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+2,gameObject.transform.position.z);
		
		
		
		
	}

	public void ApanharMeleee(int IdInimigo,float dano){
		IDDoInimigo = IdInimigo;
		DanodoInimigo = dano;
		ApanharMelee.Play();
		AtivarDano();
	}
	public void AtirarTiroFogo(int Mx,int Mz,int id,float dano){
		IDDoInimigo = id;
		DanodoInimigo = dano;
		GameObject temp = Instantiate(ExemploFlechaFogo) as GameObject;
		Projetil AjudTemp = temp.GetComponent<Projetil>();
		AjudTemp.ColocarMeupai(gameObject);
		temp.transform.SetParent(Mestre.transform);
		temp.transform.localScale = new Vector3(1,1,1);
		temp.transform.position = new Vector3(0,0,0);
		
		temp.name = Nome.ToString();
		
		AjudTemp.target.position = new Vector3(Mx,gameObject.transform.position.y+2,Mz);
		AjudTemp.MeuFilho.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+2,gameObject.transform.position.z);
	}

	public bool TirarVida(float quantidade,float AlvoX,float AlvoZ){

		if((HPAtual-quantidade)<=0){

			TextoDeCombate("Morto");

			EstouVivo = false;

			ControladorVida.value = 0;

			gameObject.transform.LookAt(new Vector3(AlvoX,gameObject.transform.position.y,AlvoZ));

			MeuSangue.Play();

			return false;
			//Morrer
		}else{
			HPAtual = HPAtual-quantidade;

			float porcentagem  = 100*HPAtual/HPMax;

			TextoDeCombate(quantidade.ToString());

			ControladorVida.value = porcentagem;

			gameObject.transform.LookAt(new Vector3(AlvoX,gameObject.transform.position.y,AlvoZ));

			MeuSangue.Play();

			return true;



			//// Colocar Codigo Dano ////////
		}

	}

	public void TextoDeCombate(string quantidade){
		Text temp = Instantiate(ExemploTextoCombate) as Text;
		temp.transform.SetParent(AjudanteCanvas.transform);
		temp.transform.localScale = new Vector3(0.08f,0.08f,1);
		temp.rectTransform.localPosition = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+50.0f,gameObject.transform.position.z);
		//temp.transform.position = 
		temp.name = Nome.ToString();

		temp.text = quantidade;
		temp.GetComponent<Animator>().SetTrigger("Hit");
		Destroy(temp.gameObject,2);

	}


	//Funçoes Referentes a IA do Jogo

	public void ChamarIA(){

		//Resetar as Variaveis
		ChanceAtacar = 10;
		ChanceMoverDirecaoPersonagem = 0;
		ChanceCorrer = 0;
		ChanceAgrupar = 0;
		ChanceSoltarMagia = 10;
		
		Atacar = false;
		MoverDirecaoPersonagem = true;
		Correr = true;
		Agrupar = true;
		SoltarMagia = false;



		//Inicio das Condiçoes

		//--------VerificacaoPossibilidadeAtaque-----

		for(int ajud = 0;ajud<AjudanteMestre.RetornarQuantidadeJogadores();ajud++){

			if((AjudanteMestre.AjudantePersonagens[ajud].SouEu==true)&&(AjudanteMestre.AjudantePersonagens[ajud].EstouVivo==true)&&((Mathf.Abs(AjudanteMestre.RetornarPlayerX(ajud)-posicaoX)<=QuantidadeAtaque)&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(ajud)-posicaoY)<=QuantidadeAtaque))){
				Atacar = true;
			}

		}

		//VerificarPossibilidadeMagias
		
		bool[] PossibilidadeMagia = new bool[4];

		for(int ajud = 0;ajud<4;ajud++){
			PossibilidadeMagia[ajud] = false;
			int custo = AjudanteMestre.RetornarCustoMana(ListaMagias[ajud]);

			for(int ajud2 = 0;ajud2<AjudanteMestre.RetornarQuantidadeJogadores();ajud2++){
				
				if((custo<=MPAtual)&&(AjudanteMestre.AjudantePersonagens[ajud2].SouEu==true)&&(AjudanteMestre.AjudantePersonagens[ajud2].EstouVivo==true)&&((Mathf.Abs(AjudanteMestre.RetornarPlayerX(ajud2)-posicaoX)<=AjudanteMestre.RetornarAlcance(ListaMagias[ajud]))&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(ajud2)-posicaoY)<=AjudanteMestre.RetornarAlcance(ListaMagias[ajud])))){
					//print ("SoltarMagia");
					SoltarMagia = true;
					PossibilidadeMagia[ajud] = true;
				}
				
			}

		}


		//------------Maquina de Estados--------------------------

		float porcentagem2  = 100*HPAtual/HPMax;


		if(porcentagem2>=70){
			ChanceAtacar = ChanceAtacar+40;
			ChanceSoltarMagia = ChanceSoltarMagia+40;
			ChanceMoverDirecaoPersonagem = ChanceMoverDirecaoPersonagem+15;
			ChanceAgrupar = ChanceAgrupar+5;
		}else if(porcentagem2>=30){
			ChanceAtacar = ChanceAtacar+30;
			ChanceSoltarMagia = ChanceSoltarMagia+30;
			ChanceMoverDirecaoPersonagem = ChanceMoverDirecaoPersonagem+20;
			ChanceAgrupar = ChanceAgrupar+10;
			ChanceCorrer = ChanceCorrer+10;
		}else{
			ChanceAtacar = ChanceAtacar+20;
			ChanceSoltarMagia = ChanceSoltarMagia+20;
			ChanceMoverDirecaoPersonagem = ChanceMoverDirecaoPersonagem+10;
			ChanceAgrupar = ChanceAgrupar+25;
			ChanceCorrer = ChanceCorrer+25;
		}


		//-------------Ajustes Finais-----------------------------
		if(Atacar==false){
			//ChanceMoverDirecaoPersonagem = ChanceMoverDirecaoPersonagem +ChanceAtacar/4 ;
			//ChanceCorrer = ChanceCorrer + ChanceAtacar/4;
			//ChanceAgrupar = ChanceAgrupar + ChanceAtacar/4;
			ChanceSoltarMagia = ChanceSoltarMagia + ChanceAtacar/4;
			ChanceAtacar = 0;
		}
		if(SoltarMagia==false){

			//ChanceMoverDirecaoPersonagem = ChanceMoverDirecaoPersonagem +ChanceSoltarMagia/4 ;
			//ChanceCorrer = ChanceCorrer + ChanceSoltarMagia/4;
			//ChanceAgrupar = ChanceAgrupar + ChanceSoltarMagia/4;
			ChanceAtacar = ChanceAtacar + ChanceSoltarMagia/4;

			if(Atacar==false){
				ChanceAtacar = 0;
			}
			ChanceSoltarMagia  = 0;
		}
		//-------------Codigo Chance Randomica e Execucao-------------------


		int chance = Random.Range(1,(ChanceAtacar+ChanceMoverDirecaoPersonagem+ChanceCorrer+ChanceAgrupar+ChanceSoltarMagia));

		//print ("Sorteado: "+chance.ToString());
		if((chance<=ChanceAtacar)&&(Atacar==true)){
			//Codigo para Atacar
			//print ("atacou");
			int melhorAlvo = -1;
			float porcentagemVida = 110;
			for(int ajud = 0;ajud<AjudanteMestre.RetornarQuantidadeJogadores();ajud++){
				
				if((AjudanteMestre.AjudantePersonagens[ajud].SouEu==true)&&(AjudanteMestre.AjudantePersonagens[ajud].EstouVivo==true)&&((Mathf.Abs(AjudanteMestre.RetornarPlayerX(ajud)-posicaoX)<=QuantidadeAtaque)&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(ajud)-posicaoY)<=QuantidadeAtaque))){
					if(porcentagemVida>AjudanteMestre.RetornarPorcentagemPlayer(ajud)){
						melhorAlvo = ajud;
						porcentagemVida = AjudanteMestre.RetornarPorcentagemPlayer(ajud);
					}
				}
				
			}

			AjudanteMestre.AtacarAlvo(ID,melhorAlvo);

		}else if(chance<=ChanceAtacar+ChanceMoverDirecaoPersonagem){
			//print ("Moveu Direcao Personagem");
			int melhorAlvo = -1;
			float porcentagemVida = 110;
			for(int ajud = 0;ajud<AjudanteMestre.RetornarQuantidadeJogadores();ajud++){
				
				if((AjudanteMestre.AjudantePersonagens[ajud].SouEu==true)&&(AjudanteMestre.AjudantePersonagens[ajud].EstouVivo==true)&&((Mathf.Abs(AjudanteMestre.RetornarPlayerX(ajud)-posicaoX)<=QuantidadeAtaque+QuantidadeMovimento)&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(ajud)-posicaoY)<=QuantidadeAtaque+QuantidadeMovimento))){
					if(porcentagemVida>AjudanteMestre.RetornarPorcentagemPlayer(ajud)){
						melhorAlvo = ajud;
						porcentagemVida = AjudanteMestre.RetornarPorcentagemPlayer(ajud);
					}
				}
				
			}

			if(melhorAlvo!=-1){
				//print ("Opcao 1");
				int andarX = posicaoX;
				int andarY = posicaoY;
				for(int ajudX = posicaoX-QuantidadeMovimento;ajudX<=posicaoX+QuantidadeMovimento;ajudX++){

					for(int ajudY = posicaoY-QuantidadeMovimento;ajudY<=posicaoY+QuantidadeMovimento;ajudY++){
						if((ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())&&(ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())){
							if(GerarTerrenos.MatrizTerrenosAndaveis[ajudX,ajudY]==true){

								if((Mathf.Abs(AjudanteMestre.RetornarPlayerX(melhorAlvo)-ajudX)<=QuantidadeAtaque)&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(melhorAlvo)-ajudY)<=QuantidadeAtaque)){

									andarX = ajudX;
									andarY = ajudY;
								}
							}


							
							
							
						}
					}

				}

				AjudanteMestre.MoverPersonagem(andarX,andarY,ID);

			}else{
				//print ("Opcao 2");
				int melhorX = 100;
				int melhorY = 100;
				int novaPosicaoX = posicaoX;
				int novaPosicaoY = posicaoY;

				for(int ajudX = posicaoX-QuantidadeMovimento;ajudX<=posicaoX+QuantidadeMovimento;ajudX++){
					for(int ajudY = posicaoY-QuantidadeMovimento;ajudY<=posicaoY+QuantidadeMovimento;ajudY++){
						if((ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())&&(ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())){
							if(GerarTerrenos.MatrizTerrenosAndaveis[ajudX,ajudY]==true){
								if((ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())&&(ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())&&(Mathf.Abs(novaPosicaoX-posicaoX)<=QuantidadeMovimento)&&(Mathf.Abs(novaPosicaoY-posicaoY)<=QuantidadeMovimento)){
									
									int tempX = Mathf.Abs(ajudX-AjudanteMestre.MediaPersonagensX());
									int tempY = Mathf.Abs(ajudY-AjudanteMestre.MediaPersonagensY());
									
									if((melhorX+melhorY)>(tempX+tempY)){
										melhorX = tempX;
										melhorY = tempY;
										novaPosicaoX = ajudX;
										novaPosicaoY = ajudY;
									}
									
								}
								
							}
						}

					}
				}
			

				AjudanteMestre.MoverPersonagem(novaPosicaoX,novaPosicaoY,ID);

			}


			//Codigo Mover Direcao Personagem
		}else if(chance<=ChanceAtacar+ChanceMoverDirecaoPersonagem+ChanceCorrer){
			//print ("Correu");
			int melhorAlvo = -1;
			float porcentagemVida = 110;
			for(int ajud = 0;ajud<AjudanteMestre.RetornarQuantidadeJogadores();ajud++){
				
				if((AjudanteMestre.AjudantePersonagens[ajud].SouEu==true)&&(AjudanteMestre.AjudantePersonagens[ajud].EstouVivo==true)&&((Mathf.Abs(AjudanteMestre.RetornarPlayerX(ajud)-posicaoX)<=QuantidadeAtaque+QuantidadeMovimento)&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(ajud)-posicaoY)<=QuantidadeAtaque+QuantidadeMovimento))){
					if(porcentagemVida>AjudanteMestre.RetornarPorcentagemPlayer(ajud)){
						melhorAlvo = ajud;
						porcentagemVida = AjudanteMestre.RetornarPorcentagemPlayer(ajud);
					}
				}
				
			}

			if(melhorAlvo!=-1){
				//print ("opcao 1");
				int melhorX = -100;
				int melhorY = -100;
				int novaPosicaoX = posicaoX;
				int novaPosicaoY = posicaoY;
				
				for(int ajudX = posicaoX-QuantidadeMovimento;ajudX<=posicaoX+QuantidadeMovimento;ajudX++){
					for(int ajudY = posicaoY-QuantidadeMovimento;ajudY<=posicaoY+QuantidadeMovimento;ajudY++){
						if((ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())&&(ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())){

							if(GerarTerrenos.MatrizTerrenosAndaveis[ajudX,ajudY]==true){
								if((ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())&&(ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())&&(Mathf.Abs(novaPosicaoX-posicaoX)<=QuantidadeMovimento)&&(Mathf.Abs(novaPosicaoY-posicaoY)<=QuantidadeMovimento)){
									
									int tempX = Mathf.Abs(ajudX-AjudanteMestre.RetornarPlayerX(melhorAlvo));
									int tempY = Mathf.Abs(ajudY-AjudanteMestre.RetornarPlayerY(melhorAlvo));
									
									if((melhorX+melhorY)<(tempX+tempY)){
										melhorX = tempX;
										melhorY = tempY;
										novaPosicaoX = ajudX;
										novaPosicaoY = ajudY;
									}
									
								}
								
							}

						}

					}
				}
				
				
				AjudanteMestre.MoverPersonagem(novaPosicaoX,novaPosicaoY,ID);



			}else{
				//print ("Opcao2");
				int novaPosicaoX = 0;
				int novaPosicaoY = 0;
				bool verificacao = true;
				while(verificacao==true){

					int numero = (int)Random.Range(1,9);

					switch(numero){
					case 1:

						novaPosicaoX = -1;
						novaPosicaoY = 1;
						break;
					case 2:
						novaPosicaoX = 0;
						novaPosicaoY = 1;
						break;
					case 3:
						novaPosicaoX = 1;
						novaPosicaoY = 1;
						break;
					case 4:
						novaPosicaoX = -1;
						novaPosicaoY = 0;
						break;
					case 5:
						novaPosicaoX = 1;
						novaPosicaoY = 0;
						break;
					case 6:
						novaPosicaoX = -1;
						novaPosicaoY = -1;
						break;
					case 7:
						novaPosicaoX = 0;
						novaPosicaoY = -1;
						break;
					case 8:
						novaPosicaoX = 1;
						novaPosicaoY = -1;
						break;
					}

					if(((posicaoX+novaPosicaoX)>=0)&&((posicaoX+novaPosicaoX)<AjudanteMestre.RetornarXTerreno())&&((posicaoY+novaPosicaoY)>=0)&&((posicaoY+novaPosicaoY)<AjudanteMestre.RetornarYTerreno())){

						if(GerarTerrenos.MatrizTerrenosAndaveis[(posicaoX+novaPosicaoX),(posicaoY+novaPosicaoY)]==true){
							novaPosicaoX = (posicaoX+novaPosicaoX);
							novaPosicaoY = (posicaoY+novaPosicaoY);
							verificacao=false;
						}
					}

				}
				AjudanteMestre.MoverPersonagem(novaPosicaoX,novaPosicaoY,ID);

			}

			//AjudanteMestre.mediaX;
			//AjudanteMestre.mediaY;
			//Codigo Correr
		}else if(chance<=ChanceAtacar+ChanceMoverDirecaoPersonagem+ChanceCorrer+ChanceAgrupar){
			//print ("agrupou");
			int melhorAlvo = -1;
			float porcentagemVida = 110;
			for(int ajud = 0;ajud<AjudanteMestre.RetornarQuantidadeJogadores();ajud++){
				
				if((AjudanteMestre.AjudantePersonagens[ajud].SouEu==false)&&(AjudanteMestre.AjudantePersonagens[ajud].EstouVivo==true)&&(ID!=ajud)&&((Mathf.Abs(AjudanteMestre.RetornarPlayerX(ajud)-posicaoX)<=QuantidadeMovimento)&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(ajud)-posicaoY)<=QuantidadeMovimento))){
					if(porcentagemVida>AjudanteMestre.RetornarPorcentagemPlayer(ajud)){
						melhorAlvo = ajud;
						porcentagemVida = AjudanteMestre.RetornarPorcentagemPlayer(ajud);
					}
				}
				
			}
			
			if(melhorAlvo!=-1){
				//print ("Opcao 1");
				int andarX = posicaoX;
				int andarY = posicaoY;
				for(int ajudX = posicaoX-QuantidadeMovimento;ajudX<=posicaoX+QuantidadeMovimento;ajudX++){
					
					for(int ajudY = posicaoY-QuantidadeMovimento;ajudY<=posicaoY+QuantidadeMovimento;ajudY++){
						if((ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())&&(ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())){
							if(GerarTerrenos.MatrizTerrenosAndaveis[ajudX,ajudY]==true){
								
								if((Mathf.Abs(AjudanteMestre.RetornarPlayerX(melhorAlvo)-ajudX)<=QuantidadeAtaque)&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(melhorAlvo)-ajudY)<=QuantidadeAtaque)){
									
									andarX = ajudX;
									andarY = ajudY;
								}
							}
							
							
							
							
							
						}
					}
					
				}
				
				AjudanteMestre.MoverPersonagem(andarX,andarY,ID);

			}else{
				//print ("Opcao2");
				int melhorX = 100;
				int melhorY = 100;
				int novaPosicaoX = posicaoX;
				int novaPosicaoY = posicaoY;
				
				for(int ajudX = posicaoX-QuantidadeMovimento;ajudX<=posicaoX+QuantidadeMovimento;ajudX++){
					for(int ajudY = posicaoY-QuantidadeMovimento;ajudY<=posicaoY+QuantidadeMovimento;ajudY++){
						if((ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())&&(ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())){
							if(GerarTerrenos.MatrizTerrenosAndaveis[ajudX,ajudY]==true){
								if((ajudX>=0)&&(ajudX<AjudanteMestre.RetornarXTerreno())&&(ajudY>=0)&&(ajudY<AjudanteMestre.RetornarYTerreno())&&(Mathf.Abs(novaPosicaoX-posicaoX)<=QuantidadeMovimento)&&(Mathf.Abs(novaPosicaoY-posicaoY)<=QuantidadeMovimento)){
									
									int tempX = Mathf.Abs(ajudX-AjudanteMestre.MediaInimigosX());
									int tempY = Mathf.Abs(ajudY-AjudanteMestre.MediaInimigosY());
									
									if((melhorX+melhorY)>(tempX+tempY)){
										melhorX = tempX;
										melhorY = tempY;
										novaPosicaoX = ajudX;
										novaPosicaoY = ajudY;
									}
									
								}
								
							}
						}

					}
				}
				
				
				AjudanteMestre.MoverPersonagem(novaPosicaoX,novaPosicaoY,ID);
				
			}
			
			
			//Codigo Mover Direcao Personagem
			//CodigoAgrupar
		}else if ((chance<=ChanceAtacar+ChanceMoverDirecaoPersonagem+ChanceCorrer+ChanceAgrupar+ChanceSoltarMagia)&&(SoltarMagia==true)){
			//print ("Soltou magia");
			int melhorMagiaDisponivel = 0;
			int quantidade = 0;
			for(int ajud = 0;ajud<4;ajud++){
				if(PossibilidadeMagia[ajud]==true){
					switch(AjudanteMestre.RetornarAtributoModificador(ajud)){
					case 1:
						if(Inteligencia>quantidade){
							melhorMagiaDisponivel = ajud;
							quantidade = (int)Inteligencia;
						}
						break;
					case 2:
						if(Forca>quantidade){
							melhorMagiaDisponivel = ajud;
							quantidade = (int)Forca;
						}
						break;
					case 3:
						if(Agilidade>quantidade){
							melhorMagiaDisponivel = ajud;
							quantidade = (int)Agilidade;
						}
						break;
					case 4:
						float porcentagem  = 100*HPAtual/HPMax;
						if(porcentagem<=15){
							melhorMagiaDisponivel = ajud;
							quantidade = 999;
						}
						break;

					}
	
				}
			}

			int melhorAlvo = -1;
			float porcentagemVida = 110;
			for(int ajud = 0;ajud<AjudanteMestre.RetornarQuantidadeJogadores();ajud++){
				
				if((AjudanteMestre.AjudantePersonagens[ajud].SouEu==true)&&(AjudanteMestre.AjudantePersonagens[ajud].EstouVivo==true)&&((Mathf.Abs(AjudanteMestre.RetornarPlayerX(ajud)-posicaoX)<=AjudanteMestre.RetornarAlcance(ListaMagias[melhorMagiaDisponivel]))&&(Mathf.Abs(AjudanteMestre.RetornarPlayerY(ajud)-posicaoY)<=AjudanteMestre.RetornarAlcance(ListaMagias[melhorMagiaDisponivel])))){
					if(porcentagemVida>AjudanteMestre.RetornarPorcentagemPlayer(ajud)){
						melhorAlvo = ajud;
						porcentagemVida = AjudanteMestre.RetornarPorcentagemPlayer(ajud);
					}
				}
				
			}

			//print("MelhorAlvo Magia: "+melhorAlvo);
			//print("Eu ID: "+ID);
			//print("MinhaMagia: "+ListaMagias[melhorMagiaDisponivel]);
			AjudanteMestre.SoltarMagia(ID,melhorAlvo,melhorMagiaDisponivel);

			//Codigo SoltarMagia
		}

	}

}
