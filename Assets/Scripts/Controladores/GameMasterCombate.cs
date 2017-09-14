
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterCombate : MonoBehaviour {

	//Variaveis Referentes a geraçao automatica de jogadores
	public GameObject ModeloPlayerExemplo;
	public ControladorTurno AjudanteModeloPlayerExemplo;

	public GameObject ContainerDeInimigos;
	public Inimigos[] AjudanteContainerInimigos;

	public GameObject ContainerDePersonagens;
	public Personagens[] AjudanteContainerDePersonagens;

	public GameObject[] ModelosGuitarraMachado;
	public GameObject[] ModelosArpa;
	public GameObject[] ModelosPandeiro;
	public GameObject[] ModelosTimpano;
	public GameObject[] ModelosTrompete;

	public GameObject[] ModelosPersonagens;
	public ControladorAnimacao[] AjudanteAnimaçao;

	public int TamanhoTurno = 1000;
	public float LimiteInferior = -429.0f;
	public float LimiteSuperior = 171.0f;
	public float LimiteFianl = 301;
	public RectTransform ReferenciaLimiteInferior;
	public RectTransform ReferenciaLimiteSuperior;
	public RectTransform ReferenciaLimiteFinal;
	public float RateDaBarra = 0;

	public GameObject MestreMagias;
	public ContainerMagias AjudanteMestreMagias;

	public GameObject[] TodasMagias;
	public int ContadorMagias;
	public Magias[] AjudanteMagias;
	public static int NumeroMagia = 0;
	public bool prontoMagia = false;
	//public Image[] ImagensDasMagias;

	public GameObject ContainerMagias;
	public Image[] ContainerIcones;

	public Vector3 AjudanteDaCamera;
	public float VelocidadeCamera;
	public Camera MinhaCamera;
	public static int Opcao = 0;
	public bool prontoMover = false;
	public bool prontoAtacar = false;
	public Canvas MeuCanvas;


	public GameObject Painel;
	public static bool Rodando = true;
	public static bool ExecutandoClique = false;
	public static bool ChaveDeSeguraca = true;

	public Image Exemplo;
	public int ContadorPersonagens;
	public Image[] PersonagensImg;
	public ControladorTurno[] AjudantePersonagens;
	public float[] AjudanteTurno;
	public GameObject [] Personagens;
    UnityEngine.AI.NavMeshAgent[] AjudanteNavMeshPersonagens;

	public static int Turno = 0;
	public float Timer = 0;
	public GameObject CirculoAcao;

	public GameObject PainelModelo;
	public PainelPersonagem ajudanteainelModelo;

	public Text Errinho;
	public GameObject TextoErro;
	Animator AjudanteTextoErro;

	public GameObject PainelFimdeJogo;
	PainelFimBatalha AjudantePainelFimDeJogo;

	private float theScreenWidth;
	private float theScreenHeight;

	public int Distancia = 10;
	public int velocidadeDoAjudanteCamera = 1;
	public bool LigarCamera = true;

	public int mediaX = 0;
	public int mediaY = 0;
	public int mediaXI = 0;
	public int mediaYI = 0;
	public int contadorInimigos = 0;


	public ControladorMagnetude MagntudeCalculoDano;


	private GerarTerrenos gerenciadorTerrenos;
	private LimitesMapa limitesMapa;
	private struct LimitesMapa{
		public float minX;
		public float maxX;
		public float minY;
		public float maxY;
	}
	public float cameraMinXOffset;
	public float cameraMaxXOffset;
	public float cameraMinYOffset;
	public float cameraMaxYOffset;

	public GameObject MeuTutorial;

	// Use this for initialization
	void Start () {
		AjudantePainelFimDeJogo = PainelFimdeJogo.GetComponent<PainelFimBatalha>();

		ContainerSons.ControladorSons.PararLoopsMusicas();
		ContainerSons.ControladorSons.PararTodosOsSons();
		if(DadosPersonagens.ControleDadosPersonagem.Tutorial==true){
			AjudantePainelFimDeJogo.Tutorial = true;
			ContainerSons.ControladorSons.TocarMusicaLoop(3);
			MeuTutorial.SetActive(true);
			Rodando = false;
			DadosPersonagens.ControleDadosPersonagem.Tutorial = false;
		}else{
			AjudantePainelFimDeJogo.Tutorial = false;
			ContainerSons.ControladorSons.TocarMusicaLoop(0);
			Rodando = true;
			MeuTutorial.SetActive(false);
		}

		LimiteSuperior = ReferenciaLimiteSuperior.localPosition.x;
		LimiteInferior = ReferenciaLimiteInferior.localPosition.x;
		LimiteFianl = ReferenciaLimiteFinal.localPosition.x;

		theScreenWidth = Screen.width;
		theScreenHeight = Screen.height;

		//MinhaCamera.aspect = 1345/566;

		//print (theScreenHeight);
		//print (theScreenWidth);

		ChaveDeSeguraca = true;



		ContainerInimigos AjudanteTemporarioInimigos = ContainerDeInimigos.GetComponent<ContainerInimigos>();

		AjudanteContainerInimigos = new Inimigos[AjudanteTemporarioInimigos.ListaInimigos.Length];
		for(int x = 0;x<AjudanteTemporarioInimigos.ListaInimigos.Length;x++){
			AjudanteContainerInimigos[x] = AjudanteTemporarioInimigos.ListaInimigos[x].GetComponent<Inimigos>();
		}

		ContainerPersonagens AjudanteTemporarioPersonagens = ContainerDePersonagens.GetComponent<ContainerPersonagens>();
		
		AjudanteContainerDePersonagens = new Personagens[AjudanteTemporarioPersonagens.ListaPersonagens.Length];
		for(int x = 0;x<AjudanteTemporarioPersonagens.ListaPersonagens.Length;x++){
			AjudanteContainerDePersonagens[x] = AjudanteTemporarioPersonagens.ListaPersonagens[x].GetComponent<Personagens>();
		}

		IniciarGeracaoRandomica();



		PersonagensImg = new Image[ContadorPersonagens];

		AjudanteMestreMagias = MestreMagias.GetComponent<ContainerMagias>();

		ContadorMagias = AjudanteMestreMagias.ListaMagias.Length;
		AjudanteTextoErro = TextoErro.GetComponent<Animator>();

		RateDaBarra = (Mathf.Abs(LimiteSuperior) + Mathf.Abs(LimiteInferior))/TamanhoTurno;

		AjudanteDaCamera = new Vector3(MinhaCamera.transform.position.x,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z);
		AjudanteTurno = new float[ContadorPersonagens];
		AjudanteNavMeshPersonagens = new UnityEngine.AI.NavMeshAgent[ContadorPersonagens];
		AjudanteMagias = new Magias[ContadorMagias];
		//ContainerIcones = new Image[ContadorMagias];


		//ContadorMagias = AjudanteMestreMagias.Tamanho;
		TodasMagias = AjudanteMestreMagias.ListaMagias;

		ajudanteainelModelo = PainelModelo.GetComponent<PainelPersonagem>();

		AjudanteAnimaçao = new ControladorAnimacao[ContadorPersonagens];
		ModelosPersonagens = new GameObject[ContadorPersonagens];

		for (int x = 0; x<ContadorPersonagens; x++) {
			AjudanteTurno[x] = 0; 
			AjudantePersonagens[x] = Personagens[x].GetComponent<ControladorTurno>();
			GerarTerrenos.MudarPosicao(AjudantePersonagens[x].posicaoX,AjudantePersonagens[x].posicaoY,AjudantePersonagens[x].SouEu);
			AjudanteNavMeshPersonagens[x] = Personagens[x].GetComponent<UnityEngine.AI.NavMeshAgent>();
			AjudanteNavMeshPersonagens[x].destination = new Vector3(Personagens[x].transform.position.x,1,Personagens[x].transform.position.z);
			PersonagensImg[x] = Instantiate(Exemplo) as Image;
			PersonagensImg[x].transform.SetParent(MeuCanvas.transform);
			PersonagensImg[x].transform.localScale = new Vector3(1,1,1);
			//PersonagensImg[x].rectTransform.anchoredPosition = new Vector2 (1,5);
			PersonagensImg[x].rectTransform.offsetMin = new Vector2(ReferenciaLimiteInferior.offsetMin.x, ReferenciaLimiteInferior.anchoredPosition.x);
			PersonagensImg[x].rectTransform.offsetMax = new Vector2(ReferenciaLimiteInferior.offsetMax.x, ReferenciaLimiteInferior.anchoredPosition.y);

			PersonagensImg[x].rectTransform.localPosition = ReferenciaLimiteInferior.localPosition; // Inicio -429 Final x 171
			PersonagensImg[x].name = x.ToString();
		

			PersonagensImg[x].sprite = AjudantePersonagens[x].MinhaFoto;

			/*Tipo arma 
			:1 Guitarra Machado
			:2 Arpa
			:3 Pandeiro
			:4 Timpano
			:5 Trompete
			*/
			switch(AjudantePersonagens[x].TipoArma){
			case 1:
				ModelosPersonagens[x] = Instantiate(ModelosGuitarraMachado[AjudantePersonagens[x].MeuModelo]) as GameObject;
				break;
			case 2:
				ModelosPersonagens[x] = Instantiate(ModelosArpa[AjudantePersonagens[x].MeuModelo]) as GameObject;
				break;
			case 3:
				ModelosPersonagens[x] = Instantiate(ModelosPandeiro[AjudantePersonagens[x].MeuModelo]) as GameObject;
				break;
			case 4:
				ModelosPersonagens[x] = Instantiate(ModelosTimpano[AjudantePersonagens[x].MeuModelo]) as GameObject;
				break;
			case 5:
				ModelosPersonagens[x] = Instantiate(ModelosTrompete[AjudantePersonagens[x].MeuModelo]) as GameObject;
				break;
			}


			ModelosPersonagens[x].transform.SetParent(Personagens[x].transform);
			ModelosPersonagens[x].transform.localPosition = new Vector3(0,-0.5f,0);
			ModelosPersonagens[x].transform.localScale = new Vector3(0.02f,0.02f,0.02f);
			ModelosPersonagens[x].name = AjudantePersonagens[x].Nome+" Modelo";

			AjudanteAnimaçao[x] = ModelosPersonagens[x].GetComponent<ControladorAnimacao>();
			AjudanteAnimaçao[x].IniciarMestre(gameObject);



		}
		AjudanteDaCamera = new Vector3(Personagens[0].transform.position.x,MinhaCamera.transform.position.y,Personagens[0].transform.position.z);
		for(int x = 0;x<ContadorMagias;x++){
			AjudanteMagias[x] = TodasMagias[x].GetComponent<Magias>();
			//ImagensDasMagias[x].sprite = AjudanteMagias[x].Icone.sprite;

		}

		Invoke("SetBoundaries", 1);



	}

	void SetBoundaries()
	{
		gerenciadorTerrenos = GameObject.Find("GerenciadorTerrenos").GetComponent<GerarTerrenos>();
		limitesMapa.minX = gerenciadorTerrenos.TerrenosContruidos[0,0].transform.position.x - cameraMinXOffset;
		limitesMapa.maxX = gerenciadorTerrenos.TerrenosContruidos[gerenciadorTerrenos.xTerrenos-1,0].transform.position.x + cameraMaxXOffset;
		limitesMapa.minY = gerenciadorTerrenos.TerrenosContruidos[0,0].transform.position.z - cameraMinYOffset;
		limitesMapa.maxY = gerenciadorTerrenos.TerrenosContruidos[0,gerenciadorTerrenos.yTerrenos-1].transform.position.z + cameraMaxYOffset;
	}
	
	// Update is called once per frame
	void Update () {

		if ((Rodando == true)&&(ChaveDeSeguraca==true)) {


			for (int x = 0; x<ContadorPersonagens; x++) {
				if(AjudantePersonagens[x].EstouVivo==true){
					AjudanteTurno [x] = AjudanteTurno [x] + (AjudantePersonagens [x].Agilidade) / 10;
					PersonagensImg [x].rectTransform.localPosition = new Vector2 ((AjudanteTurno [x]*RateDaBarra+LimiteInferior), PersonagensImg [x].rectTransform.localPosition.y);
					if (AjudanteTurno [x] >= TamanhoTurno) {

						if(AjudantePersonagens[x].SouEu==true){
							AjudanteTextoErro.SetBool("Hit",false);
							
							CirculoAcao.SetActive(true);
							CirculoAcao.transform.position = new Vector3(Personagens[x].transform.position.x+0.4f,4,Personagens[x].transform.position.z-1);
							AjudanteDaCamera = new Vector3(0+Personagens[x].transform.position.x,10,-10+Personagens[x].transform.position.z);
							Rodando = false;
							Turno = x;
							PersonagensImg [x].rectTransform.localPosition = new Vector2 (LimiteFianl, PersonagensImg [x].rectTransform.localPosition.y);
							FazerPainel(AjudantePersonagens[x].posicaoX,AjudantePersonagens[x].posicaoY);
						}else{
							AjudanteTextoErro.SetBool("Hit",false);
							
							//CirculoAcao.SetActive(true);
							//CirculoAcao.transform.position = new Vector3(Personagens[x].transform.position.x+0.4f,2,Personagens[x].transform.position.z-1);
							AjudanteDaCamera = new Vector3(0+Personagens[x].transform.position.x,10,-10+Personagens[x].transform.position.z);
							Rodando = false;
							Turno = x;
							PersonagensImg [x].rectTransform.localPosition = new Vector2 (LimiteFianl, PersonagensImg [x].rectTransform.localPosition.y);
							AjudantePersonagens[Turno].ChamarIA();
							//FazerPainel(AjudantePersonagens[x].posicaoX,AjudantePersonagens[x].posicaoY);
						}

						
					}
				}

			}//fim for
		} else {

			if(Input.GetKeyDown("q")){
				
				
				ClicarMover();
				
			}
			
			if(Input.GetKeyDown("w")){
				
				
				ClicarAtaque();
				
			}
			
			if(Input.GetKeyDown("e")){
				
				
				ClicarMagia();
				
			}
			
			if(Input.GetKeyDown("1")){
				
				NumeroMagia = 0;
				ClicarMagiaEspecifica();
				
			}
			if(Input.GetKeyDown("2")){
				
				NumeroMagia = 1;
				ClicarMagiaEspecifica();
				
			}
			if(Input.GetKeyDown("3")){
				
				NumeroMagia = 2;
				ClicarMagiaEspecifica();
				
			}
			if(Input.GetKeyDown("4")){
				
				NumeroMagia = 3;
				ClicarMagiaEspecifica();
				
			}

		}

		if(Input.GetKeyDown("l")){

			LigarCamera = !LigarCamera;
			if(LigarCamera==true){
				Errinho.text = "Camera Ligada";
				AjudanteTextoErro.SetBool("Hit",true);
			}else{
				Errinho.text = "Camera Desligada";
				AjudanteTextoErro.SetBool("Hit",true);
			}


		}





		if(LigarCamera==true){
			int AjudanteX = 0;
			int AjudanteZ = 0;
			if ((Input.mousePosition.x > theScreenWidth - Distancia) && (MinhaCamera.transform.position.x <= limitesMapa.maxX)){
				
				AjudanteX = AjudanteX+velocidadeDoAjudanteCamera;
				AjudanteDaCamera = new Vector3(MinhaCamera.transform.position.x+velocidadeDoAjudanteCamera,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z + AjudanteZ);// move on +X axis
				
			}
			
			
			if ((Input.mousePosition.x < 0 + Distancia) && (MinhaCamera.transform.position.x >= limitesMapa.minX)){
				
				AjudanteX = AjudanteX-velocidadeDoAjudanteCamera;
				AjudanteDaCamera = new Vector3(MinhaCamera.transform.position.x-velocidadeDoAjudanteCamera,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z + AjudanteZ);// move on -X axis
				
			}
			
			if ((Input.mousePosition.y > theScreenHeight - Distancia) && (MinhaCamera.transform.position.z <= limitesMapa.maxY)){
				
				AjudanteZ = AjudanteZ +velocidadeDoAjudanteCamera;
				AjudanteDaCamera = new Vector3(MinhaCamera.transform.position.x + AjudanteX,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z+velocidadeDoAjudanteCamera);
				
				// move on +Z axis
				
			}
			
			if ((Input.mousePosition.y < 0 + Distancia) && (MinhaCamera.transform.position.z >= limitesMapa.minY)){
				
				AjudanteZ = AjudanteZ -velocidadeDoAjudanteCamera;
				AjudanteDaCamera = new Vector3(MinhaCamera.transform.position.x + AjudanteX,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z-velocidadeDoAjudanteCamera);// move on -Z axis
				
			}
		}



		if(MinhaCamera.transform.position!=AjudanteDaCamera){
			float diferencaX = (MinhaCamera.transform.position.x - AjudanteDaCamera.x)* Time.deltaTime * VelocidadeCamera;
			float diferencaZ = (MinhaCamera.transform.position.z - AjudanteDaCamera.z)* Time.deltaTime * VelocidadeCamera;
			MinhaCamera.transform.position = new Vector3(MinhaCamera.transform.position.x - diferencaX,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z - diferencaZ);

		}
	}//fim Update


	void IniciarGeracaoRandomica(){

		//ContarElementos
		int contador = 0;
		for(int x = 0;x<DadosPersonagens.ControleDadosPersonagem.EstouNaParty.Length;x++){
			if(DadosPersonagens.ControleDadosPersonagem.EstouNaParty[x]==true){
				contador = contador+1;
			}
		}

		for(int x = 0;x<DadosCombate.ControleDadosCombate.NumeroDeInimigos.Length;x++){
			contador = contador + DadosCombate.ControleDadosCombate.NumeroDeInimigos[x];
		}

		ContadorPersonagens = contador;
		Personagens = new GameObject[ContadorPersonagens];
		AjudantePersonagens = new ControladorTurno[ContadorPersonagens];
		contador = 0;

		for(int x = 0;x<DadosPersonagens.ControleDadosPersonagem.EstouNaParty.Length;x++){
			if(DadosPersonagens.ControleDadosPersonagem.EstouNaParty[x]==true){

				Personagens[contador] = Instantiate(ModeloPlayerExemplo) as GameObject;
				Personagens[contador].transform.SetParent(gameObject.transform);
				Personagens[contador].transform.localScale = new Vector3(1,1,1);
				Personagens[contador].transform.position = new Vector3(0,1,0);
				Personagens[contador].name = contador.ToString();


				AjudantePersonagens[contador] = Personagens[contador].GetComponent<ControladorTurno>();

				//-------------------Detalhe Mudar a Fonte Assim que For Terminado o Inventario -----------------------------

				AjudantePersonagens[contador].ID = contador;
				AjudantePersonagens[contador].SouEu = true;
				AjudantePersonagens[contador].Nome = AjudanteContainerDePersonagens[x].Nome;
				AjudantePersonagens[contador].Agilidade = (float)AjudanteContainerDePersonagens[x].Agilidade;
				AjudantePersonagens[contador].HPMax = AjudanteContainerDePersonagens[x].HP;
				AjudantePersonagens[contador].MeuModelo = AjudanteContainerDePersonagens[x].MeuModelo;
				AjudantePersonagens[contador].HPAtual = AjudanteContainerDePersonagens[x].HP;
				AjudantePersonagens[contador].MPMax = AjudanteContainerDePersonagens[x].MP;
				AjudantePersonagens[contador].MPAtual = AjudanteContainerDePersonagens[x].MP;
				AjudantePersonagens[contador].Stamina = AjudanteContainerDePersonagens[x].Stamina;
				AjudantePersonagens[contador].Forca = AjudanteContainerDePersonagens[x].Forca;
				AjudantePersonagens[contador].Inteligencia = AjudanteContainerDePersonagens[x].Inteligencia;
				AjudantePersonagens[contador].Defesa = AjudanteContainerDePersonagens[x].Defesa;
				AjudantePersonagens[contador].MinhaFoto = AjudanteContainerDePersonagens[x].MinhaFoto;
				AjudantePersonagens[contador].QuantidadeMovimento = AjudanteContainerDePersonagens[x].QuantidadeMovimento;
				AjudantePersonagens[contador].QuantidadeAtaque = AjudanteContainerDePersonagens[x].QuantidadeAtaque;
				AjudantePersonagens[contador].TipoArma = AjudanteContainerDePersonagens[x].TipoArma;
				
				AjudantePersonagens[contador].ListaMagias = AjudanteContainerDePersonagens[x].Magias;
				
				AjudantePersonagens[contador].Dano = 20;
				
				
				AjudantePersonagens[contador].posicaoX = 0;
				AjudantePersonagens[contador].posicaoY = 0;

				switch(contador){

				case 0: 
					AjudantePersonagens[contador].posicaoX = 10;
					AjudantePersonagens[contador].posicaoY = 0;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 1: 
					AjudantePersonagens[contador].posicaoX = 11;
					AjudantePersonagens[contador].posicaoY = 0;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 2: 
					AjudantePersonagens[contador].posicaoX = 12;
					AjudantePersonagens[contador].posicaoY = 0;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 3: 
					AjudantePersonagens[contador].posicaoX = 10;
					AjudantePersonagens[contador].posicaoY = 1;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 4: 
					AjudantePersonagens[contador].posicaoX = 11;
					AjudantePersonagens[contador].posicaoY = 1;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 5: 
					AjudantePersonagens[contador].posicaoX = 12;
					AjudantePersonagens[contador].posicaoY = 1;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				}
				//GerarTerrenos.MatrizImagensMinimapa[10,10].color = Color.green;
				mediaX = AjudantePersonagens[contador].posicaoX + mediaX;
				mediaY = AjudantePersonagens[contador].posicaoY + mediaY;

				GerarTerrenos.MatrizTerrenosAndaveis[AjudantePersonagens[contador].posicaoX,AjudantePersonagens[contador].posicaoY] = false;

				contador = contador+1;


			}
		}
		int zona1 = 0;
		int zona2 = 0;
		int zona3 = 0;

		for(int x = 0;x<DadosCombate.ControleDadosCombate.NumeroDeInimigos.Length;x++){
			for(int y =0;y<DadosCombate.ControleDadosCombate.NumeroDeInimigos[x];y++){

				Personagens[contador] = Instantiate(ModeloPlayerExemplo) as GameObject;
				Personagens[contador].transform.SetParent(gameObject.transform);
				Personagens[contador].transform.localScale = new Vector3(1,1,1);
				Personagens[contador].transform.position = new Vector3(0,1,0);
				Personagens[contador].name = contador.ToString();

				AjudantePersonagens[contador] = Personagens[contador].GetComponent<ControladorTurno>();

				AjudantePersonagens[contador].SouEu = false;
				AjudantePersonagens[contador].ID = contador;
				AjudantePersonagens[contador].Nome = AjudanteContainerInimigos[x].Nome;
				AjudantePersonagens[contador].Agilidade = (float)AjudanteContainerInimigos[x].Agilidade;
				AjudantePersonagens[contador].HPMax = AjudanteContainerInimigos[x].HP;
				AjudantePersonagens[contador].HPAtual = AjudanteContainerInimigos[x].HP;
				AjudantePersonagens[contador].MPMax = AjudanteContainerInimigos[x].MP;
				AjudantePersonagens[contador].MPAtual = AjudanteContainerInimigos[x].MP;
				AjudantePersonagens[contador].MeuModelo = AjudanteContainerInimigos[x].MeuModelo;
				AjudantePersonagens[contador].Stamina = AjudanteContainerInimigos[x].Stamina;
				AjudantePersonagens[contador].Forca = AjudanteContainerInimigos[x].Forca;
				AjudantePersonagens[contador].Inteligencia = AjudanteContainerInimigos[x].Inteligencia;
				AjudantePersonagens[contador].Defesa = AjudanteContainerInimigos[x].Defesa;
				AjudantePersonagens[contador].MinhaFoto = AjudanteContainerInimigos[x].MinhaFoto;
				AjudantePersonagens[contador].QuantidadeMovimento = AjudanteContainerInimigos[x].QuantidadeMovimento;
				AjudantePersonagens[contador].QuantidadeAtaque = AjudanteContainerInimigos[x].QuantidadeAtaque;
				AjudantePersonagens[contador].TipoArma = AjudanteContainerInimigos[x].TipoArma;

				AjudantePersonagens[contador].ListaMagias = AjudanteContainerInimigos[x].Magias;

				AjudantePersonagens[contador].Dano = Random.Range((float)AjudanteContainerInimigos[x].DanoMin,(float)AjudanteContainerInimigos[x].DanoMax);
				contadorInimigos = contadorInimigos+1;

				bool continuar = true;
				int ZonaRandom = 0;
				while(continuar==true){
					ZonaRandom = Random.Range(1,4);

					if((ZonaRandom==1)&&(zona1<6)){
						continuar = false;
					}else if((ZonaRandom==2)&&(zona2<6)){
						continuar = false;
					}else if((ZonaRandom==3)&&(zona3<6)){
						continuar = false;
					}

				}

				switch(ZonaRandom){
					
				case 1: 

					switch(zona1){


					case 0:

						AjudantePersonagens[contador].posicaoX = 10;
						AjudantePersonagens[contador].posicaoY = 23;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 1:

						AjudantePersonagens[contador].posicaoX = 11;
						AjudantePersonagens[contador].posicaoY = 23;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 2:

						AjudantePersonagens[contador].posicaoX = 12;
						AjudantePersonagens[contador].posicaoY = 23;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 3:

						AjudantePersonagens[contador].posicaoX = 10;
						AjudantePersonagens[contador].posicaoY = 22;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 4:

						AjudantePersonagens[contador].posicaoX = 11;
						AjudantePersonagens[contador].posicaoY = 22;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 5:

						AjudantePersonagens[contador].posicaoX = 12;
						AjudantePersonagens[contador].posicaoY = 22;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;
						

					}//fim swich interno
					zona1 = zona1+1;
					break;//fim case 1
				case 2: 

					switch(zona2){
						
						
					case 0:
						
						AjudantePersonagens[contador].posicaoX = 0;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 1:
						
						AjudantePersonagens[contador].posicaoX = 1;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 2:
						
						AjudantePersonagens[contador].posicaoX = 0;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 3:
						
						AjudantePersonagens[contador].posicaoX = 1;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 4:
						
						AjudantePersonagens[contador].posicaoX = 0;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 5:
						
						AjudantePersonagens[contador].posicaoX = 1;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
						
					}//fim swich interno
					zona2 = zona2+1;
					break;
				case 3: 


					switch(zona3){
						
						
					case 0:
						
						AjudantePersonagens[contador].posicaoX = 23;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 1:
						
						AjudantePersonagens[contador].posicaoX = 22;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 2:
						
						AjudantePersonagens[contador].posicaoX = 23;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 3:
						
						AjudantePersonagens[contador].posicaoX = 22;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 4:
						
						AjudantePersonagens[contador].posicaoX = 23;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 5:
						
						AjudantePersonagens[contador].posicaoX = 22;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
						
					}//fim swich interno
					zona3 = zona3+1;
					break;
				}



				//GerarTerrenos.MatrizImagensMinimapa[20,20].color = Color.red;

				mediaXI = AjudantePersonagens[contador].posicaoX + mediaXI;
				mediaYI = AjudantePersonagens[contador].posicaoY + mediaYI;

				GerarTerrenos.MatrizTerrenosAndaveis[AjudantePersonagens[contador].posicaoX,AjudantePersonagens[contador].posicaoY] = false;

				contador = contador+1;
			}


		}


	}

	public void ClicarMagia(){
		ContainerSons.ControladorSons.TocarSons(0);
		AjudanteTextoErro.SetBool("Hit",false);
		ContainerMagias.SetActive(true);

		for(int x = 0;x<4;x++){

			ContainerIcones[x].sprite = AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[x]].Icone;
		}
	}
	public void ClicarMagiaEspecifica(){
		ContainerSons.ControladorSons.TocarSons(0);
		AjudanteTextoErro.SetBool("Hit",false);
		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
				if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-ajud)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-ajud1)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
						
						GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = true;
						if((AjudantePersonagens[Turno].posicaoX==ajud)&&(AjudantePersonagens[Turno].posicaoY==ajud1)){
							GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
						}else{
							GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.yellow;
						}
						for(int ajud2 =0;ajud2<ContadorPersonagens;ajud2++){
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==false)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.red;
							}
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==true)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.blue;
							}
						}
						
					}
				}
				
			}
		}//fimfor
		prontoMagia = true;
	}

	public void ClicarAtaque(){
		ContainerSons.ControladorSons.TocarSons(0);
		AjudanteTextoErro.SetBool("Hit",false);
		ContainerMagias.SetActive(false);
		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
				if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-ajud)<=AjudantePersonagens[Turno].QuantidadeAtaque){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-ajud1)<=AjudantePersonagens[Turno].QuantidadeAtaque){
						
						GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = true;
						if((AjudantePersonagens[Turno].posicaoX==ajud)&&(AjudantePersonagens[Turno].posicaoY==ajud1)){
							GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
						}else{
							GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.yellow;
						}
						for(int ajud2 =0;ajud2<ContadorPersonagens;ajud2++){
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==false)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.red;
							}
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==true)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.blue;
							}
						}
						
					}
				}
				
			}
		}//fimfor
		prontoAtacar = true;


	}
	public void ClicarMover(){
		ContainerSons.ControladorSons.TocarSons(0);
		AjudanteTextoErro.SetBool("Hit",false);
		ContainerMagias.SetActive(false);
		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;



				if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-ajud)<=AjudantePersonagens[Turno].QuantidadeMovimento){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-ajud1)<=AjudantePersonagens[Turno].QuantidadeMovimento){
						
						GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = true;
						if((AjudantePersonagens[Turno].posicaoX==ajud)&&(AjudantePersonagens[Turno].posicaoY==ajud1)){
							GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
						}else{
							GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.green;
						}
						for(int ajud2 =0;ajud2<ContadorPersonagens;ajud2++){
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
							}
						}
						
					}
				}
				if(GerarTerrenos.MatrizTerrenosAndaveis[ajud,ajud1]==false){
					GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
				}
				
			}
		}//fimfor
		prontoMover = true;



	}//clicarMover

	public void FazerPainel(float xTerr,float zTerr){
		ajudanteainelModelo.FazerDescer();
		int indicePersonagem = -1;

		for(int x = 0;x<ContadorPersonagens;x++){

			if((AjudantePersonagens[x].posicaoX==xTerr)&&(AjudantePersonagens[x].posicaoY==zTerr)){
				indicePersonagem = x;
			}

		}
		ajudanteainelModelo.AtualizarInformaçoes(AjudantePersonagens[indicePersonagem].Nome,AjudantePersonagens[indicePersonagem].HPMax,AjudantePersonagens[indicePersonagem].HPAtual,AjudantePersonagens[indicePersonagem].MPMax,AjudantePersonagens[indicePersonagem].MPAtual,AjudantePersonagens[indicePersonagem].MinhaFoto);

		ajudanteainelModelo.FazerSubir();

		//ClicarTerreno(xTerr,zTerr);
	}

	public void ClicarTerreno(float xTerr,float zTerr){

		if(Opcao==0){

		}else if(Opcao==1){
			if(prontoAtacar==true){

				float x = xTerr/5;
				float z = zTerr/5;
				bool podeIr = false;
				int posicaoAtacado = 0;
				for(int ajud = 0;ajud<ContadorPersonagens;ajud++){
					if((ajud!=Turno)&&(AjudantePersonagens[ajud].posicaoX==x)&&(AjudantePersonagens[ajud].posicaoY==z)){
						podeIr = true;
						posicaoAtacado = ajud;
					}
				}//fimfor
				if(podeIr==true){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-x)<=AjudantePersonagens[Turno].QuantidadeAtaque){
						if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-z)<=AjudantePersonagens[Turno].QuantidadeAtaque){
							
							
							if((AjudantePersonagens[Turno].posicaoX==x)&&(AjudantePersonagens[Turno].posicaoY==z)){


								Errinho.text = "Nao Posso me Atacar";
								AjudanteTextoErro.SetBool("Hit",true);
				
								//Nao Faz Nada
							}else if(AjudantePersonagens[posicaoAtacado].EstouVivo==true){
								ExecutandoClique = true;
								//codigo ataque
								//inserir formula ataque
								//inserir Todos as outras coisas para


								Personagens[Turno].transform.LookAt(Personagens[posicaoAtacado].transform);
								int DanoRecebido = (int)((AjudantePersonagens[posicaoAtacado].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[posicaoAtacado].Stamina))-(AjudantePersonagens[Turno].Dano + (MagntudeCalculoDano.MagnetudeForca*AjudantePersonagens[Turno].Forca)));

								if(DanoRecebido>=0){
									DanoRecebido = 0;
								}

								DanoRecebido = Mathf.Abs(DanoRecebido);
								Personagens[Turno].transform.LookAt(Personagens[posicaoAtacado].transform);
								Personagens[posicaoAtacado].transform.LookAt(Personagens[Turno].transform);

								/*
								 * TIPOATAQE
								1 = ApanharMelee
								2 = AtirarTiroFogo
								3 = TacarBolaFogo
								4 = TacarBolaFogo2
								5 = TacarBolaFogo3
								*/

								/*Tipo arma 
								:1 Guitarra Machado
								:2 Arpa
								:3 Pandeiro
								:4 Timpano
								:5 Trompete
								*/
								switch(AjudantePersonagens[Turno].TipoArma){
								case 1:

									//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);

									AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,1,(int)xTerr,(int)zTerr);
									AjudanteAnimaçao[Turno].AtaqueArma2H(true);
									AtivarSon(Turno);
									//ContainerSons.ControladorSons.TocarSons(2);

									break;
								case 2:

									//AjudantePersonagens[Turno].AtirarTiroFogo((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);

									AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,2,(int)xTerr,(int)zTerr);
									AjudanteAnimaçao[Turno].AtaqueArma2H(true);
									AtivarSon(Turno);
									//ContainerSons.ControladorSons.TocarSons(3);
									break;
								case 3:
			
									//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);

									AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,1,(int)xTerr,(int)zTerr);
									AjudanteAnimaçao[Turno].AtaqueArma2H(true);
									AtivarSon(Turno);
									//ContainerSons.ControladorSons.TocarSons(4);

									break;
								case 4:

									//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);

									AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,1,(int)xTerr,(int)zTerr);
									AjudanteAnimaçao[Turno].AtaqueArma2H(true);
									AtivarSon(Turno);
									//ContainerSons.ControladorSons.TocarSons(5);
									break;
								case 5:

									//AjudantePersonagens[Turno].AtirarTiroFogo((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);

									AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,2,(int)xTerr,(int)zTerr);
									AjudanteAnimaçao[Turno].AtaqueArma2H(true);
									AtivarSon(Turno);
									//ContainerSons.ControladorSons.TocarSons(6);
									break;
								}
								for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
									for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
										GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
									}
									
								}//fimfor
								CirculoAcao.SetActive(false);
								ContainerMagias.SetActive(false);
								ajudanteainelModelo.FazerDescer();


							}
							
						}else{
							Errinho.text = "Alvo Invalido";
							AjudanteTextoErro.SetBool("Hit",true);
						}
					}else{
						Errinho.text = "Alvo Invalido";
						AjudanteTextoErro.SetBool("Hit",true);
					}
				}

			}//fim pronto atacar
		}else if(Opcao==2){
			if(prontoMover==true){

				float x = xTerr/5;
				float z = zTerr/5;
				bool podeIr = true;
				for(int ajud = 0;ajud<ContadorPersonagens;ajud++){
					if((ajud!=Turno)&&(AjudantePersonagens[ajud].posicaoX==x)&&(AjudantePersonagens[ajud].posicaoY==z)){
						podeIr = false;
					}
				}//fimfor

				if(podeIr==true){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-x)<=AjudantePersonagens[Turno].QuantidadeMovimento){
						if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-z)<=AjudantePersonagens[Turno].QuantidadeMovimento){
							
							
							if((AjudantePersonagens[Turno].posicaoX==x)&&(AjudantePersonagens[Turno].posicaoY==z)){

								//AjudanteTextoErro.SetBool("Hit",false);
								Errinho.text = "Ja Estou Nesse Lugar";
								AjudanteTextoErro.SetBool("Hit",true);
							

							}else{

								if(GerarTerrenos.MatrizTerrenosAndaveis[(int)x,(int)z]==true){

									ExecutandoClique = true;

									GerarTerrenos.voltarBranco(AjudantePersonagens[Turno].posicaoX,AjudantePersonagens[Turno].posicaoY);

									mediaX = mediaX - AjudantePersonagens[Turno].posicaoX;
									mediaY = mediaY - AjudantePersonagens[Turno].posicaoY;

									GerarTerrenos.MatrizTerrenosAndaveis[AjudantePersonagens[Turno].posicaoX,AjudantePersonagens[Turno].posicaoY] = true;

									AjudantePersonagens[Turno].posicaoX = (int)x;
									AjudantePersonagens[Turno].posicaoY = (int)z;

									GerarTerrenos.MatrizTerrenosAndaveis[AjudantePersonagens[Turno].posicaoX,AjudantePersonagens[Turno].posicaoY] = true;

									mediaX = mediaX + AjudantePersonagens[Turno].posicaoX;
									mediaY = mediaY + AjudantePersonagens[Turno].posicaoY;

									GerarTerrenos.MudarPosicao(AjudantePersonagens[Turno].posicaoX,AjudantePersonagens[Turno].posicaoY,AjudantePersonagens[Turno].SouEu);

									AjudanteNavMeshPersonagens[Turno].destination = new Vector3(x*5,1,z*5);
									AjudanteDaCamera = new Vector3(x*5,10,z*5 - 10);
									
									
									for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
										for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
											GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
										}
										
									}//fimfor

									AjudanteAnimaçao[Turno].Andar(true);

									AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPAtual +20;
									if(AjudantePersonagens[Turno].MPAtual>AjudantePersonagens[Turno].MPMax){
										AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPMax;
									}
									prontoMover = false;
									prontoMagia = false;
									prontoAtacar = false;
									Rodando = true;
									AjudanteTurno[Turno] = 0;
									print(AjudantePersonagens[Turno].Nome+" Moveu-se");
									CirculoAcao.SetActive(false);
									ContainerMagias.SetActive(false);
									ajudanteainelModelo.FazerDescer();
									ExecutandoClique = false;
									
									//Anda

								}else{
									Errinho.text = "Local Invalido";
									AjudanteTextoErro.SetBool("Hit",true);
								}


							}
							
						}else{
							Errinho.text = "Local Invalido";
							AjudanteTextoErro.SetBool("Hit",true);
						}
					}else{
						Errinho.text = "Local Invalido";
						AjudanteTextoErro.SetBool("Hit",true);
					}
				}

			}
		}else if(Opcao==3){

			if(prontoMagia==true){

				float x = xTerr/5;
				float z = zTerr/5;
				bool podeIr = false;
				int posicaoAtacado = 0;
				for(int ajud = 0;ajud<ContadorPersonagens;ajud++){
					if((ajud!=Turno)&&(AjudantePersonagens[ajud].posicaoX==x)&&(AjudantePersonagens[ajud].posicaoY==z)){
						podeIr = true;
						posicaoAtacado = ajud;
					}
				}//fimfor
				if(podeIr==true){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-x)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
						if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-z)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
							
							
							if((AjudantePersonagens[Turno].posicaoX==x)&&(AjudantePersonagens[Turno].posicaoY==z)){


								Errinho.text = "Nao Posso me Atacar";
								AjudanteTextoErro.SetBool("Hit",true);


							}else{
								if(AjudantePersonagens[Turno].MPAtual<AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].CustoMP){
									//Nao Faz Nada
									// Toca som  de Erro
									Errinho.text = "Preciso de Mais Mana";
									AjudanteTextoErro.SetBool("Hit",true);

								}else{
									ExecutandoClique = true;
									AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPAtual - AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].CustoMP;
									//codigo ataque Magico, Todas as verificaçoes de dano e diversas formulas de dano entram aqui
									//inserir formula ataque
									//inserir Todos as outras coisas para



									//AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]]

									int quantidadeRecurso = 0 ;


									// Case do Atributo Modificador da Magia

									switch(AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].AtributoModificador){

									case 1:
										quantidadeRecurso = (int)AjudantePersonagens[Turno].Inteligencia;
										break;
									case 2:
										quantidadeRecurso = (int)AjudantePersonagens[Turno].Forca;
										break;
									case 3:
										quantidadeRecurso = (int)AjudantePersonagens[Turno].Agilidade;
										break;
									case 4:
										quantidadeRecurso = (int)((100*AjudantePersonagens[Turno].HPAtual)/AjudantePersonagens[Turno].HPMax);
										break;

									}

									//print (quantidadeRecurso);
									int DanoRecebido = 0;
									

									switch(AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].FormulaDano){

									case 1:
										DanoRecebido = (int)((AjudantePersonagens[posicaoAtacado].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[posicaoAtacado].Stamina))-(AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].DanoBase + (MagntudeCalculoDano.Formula1*quantidadeRecurso)));
										break;
									case 2:
										DanoRecebido = (int)((MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[posicaoAtacado].Stamina)-(AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].DanoBase*MagntudeCalculoDano.Formula2*MagntudeCalculoDano.Formula2*quantidadeRecurso));
										break;
									case 3:
										DanoRecebido = (int)((AjudantePersonagens[posicaoAtacado].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[posicaoAtacado].Stamina))-(AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].DanoBase + (MagntudeCalculoDano.Formula3*quantidadeRecurso)));
										break;
									case 4:
										DanoRecebido = (int)(((AjudantePersonagens[posicaoAtacado].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[posicaoAtacado].Stamina))*(quantidadeRecurso/100))-((MagntudeCalculoDano.Formula4+(1 - (quantidadeRecurso/100)))*AjudantePersonagens[Turno].Dano+AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].DanoBase));
										break;


									}
									//print ((MagntudeCalculoDano.Formula4+(1 - (quantidadeRecurso/100)))*AjudantePersonagens[Turno].Dano+AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].DanoBase);
									if(DanoRecebido>=0){
										DanoRecebido = 0;
									}
									DanoRecebido = Mathf.Abs(DanoRecebido);
									Personagens[Turno].transform.LookAt(Personagens[posicaoAtacado].transform);
									Personagens[posicaoAtacado].transform.LookAt(Personagens[Turno].transform);

									/*
									 * TIPOATAQE
									1 = ApanharMelee
									2 = AtirarTiroFogo
									3 = TacarBolaFogo
									4 = TacarBolaFogo2
									5 = TacarBolaFogo3
									*/

									switch(AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].ID){
										
									case 0:
										AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,4,(int)xTerr,(int)zTerr);
										AjudanteAnimaçao[Turno].AtacarMagia2H(true);
										AtivarSon(Turno);
										//ContainerSons.ControladorSons.TocarSons(2);

										//AjudantePersonagens[Turno].TacarBolaFogo2((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);

										break;
									case 1:
										AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,3,(int)xTerr,(int)zTerr);
										AjudanteAnimaçao[Turno].AtacarMagia2H(true);
										AtivarSon(Turno);
										//ContainerSons.ControladorSons.TocarSons(2);

										//AjudantePersonagens[Turno].TacarBolaFogo((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
										break;
									case 2:
										AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,1,(int)xTerr,(int)zTerr);
										AjudanteAnimaçao[Turno].AtacarMagia2H(true);
										AtivarSon(Turno);
										//ContainerSons.ControladorSons.TocarSons(2);

										//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);
										break;
									case 3:
										AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,5,(int)xTerr,(int)zTerr);
										AjudanteAnimaçao[Turno].AtacarMagia2H(true);
										AtivarSon(Turno);
										//ContainerSons.ControladorSons.TocarSons(2);

										//AjudantePersonagens[Turno].TacarBolaFogo3((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
										break;
									case 4:
										AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,5,(int)xTerr,(int)zTerr);
										AjudanteAnimaçao[Turno].AtacarMagia2H(true);
										AtivarSon(Turno);
										//ContainerSons.ControladorSons.TocarSons(2);

										//AjudantePersonagens[Turno].TacarBolaFogo3((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
										break;
									case 5:
										AjudanteAnimaçao[Turno].FicaPronto(posicaoAtacado,Turno,DanoRecebido,1,(int)xTerr,(int)zTerr);
										AjudanteAnimaçao[Turno].AtacarMagia2H(true);
										AtivarSon(Turno);
										//ContainerSons.ControladorSons.TocarSons(2);

										//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);
										break;
										
									}
									for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
										for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
											GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
										}
										
									}//fimfor

									CirculoAcao.SetActive(false);
									ContainerMagias.SetActive(false);
									ajudanteainelModelo.FazerDescer();
								}
							}
						}

							}else{
								Errinho.text = "Alvo Invalido";
								AjudanteTextoErro.SetBool("Hit",true);
							}
						}else{
							Errinho.text = "Alvo Invalido";
							AjudanteTextoErro.SetBool("Hit",true);
						}
					}


					


				
			}//fim pronto Magia

	}//fim clicar terreno

	public void AtivarDanoAnim(int IdAlvo,float quantidadeDano,int meuId){

		bool EstouVivo = AjudantePersonagens[IdAlvo].TirarVida(quantidadeDano,Personagens[meuId].transform.position.x,Personagens[meuId].transform.position.z);
		AjudanteAnimaçao[IdAlvo].Apanhar(true);

						
						
		if(EstouVivo==false){
			AjudanteAnimaçao[IdAlvo].Morrer(true);
			PersonagensImg[IdAlvo].enabled = false;
			int contadorAliados = 0;
			int contadorInimigos = 0;
							
			for(int zeta = 0;zeta<ContadorPersonagens;zeta++){
				if((AjudantePersonagens[zeta].SouEu==true)&&(AjudantePersonagens[zeta].EstouVivo==true)){
					contadorAliados = contadorAliados+1;
				}else if((AjudantePersonagens[zeta].SouEu==false)&&(AjudantePersonagens[zeta].EstouVivo==true)){
					contadorInimigos = contadorInimigos+1;
				}
			}
							//print (contadorAliados);
							//print (contadorInimigos);
							
			if((contadorAliados<=0)&&(contadorInimigos>0)){
				ContainerSons.ControladorSons.PararTodosOsSons();
				ContainerSons.ControladorSons.TocarMusica(1);
				ChaveDeSeguraca = false;
				AjudantePainelFimDeJogo.Vitoria = false;
				AjudantePainelFimDeJogo.MudarTexto("Derrota");
				AjudantePainelFimDeJogo.FazerSubir();
			}
			if((contadorInimigos<=0)&&(contadorAliados>0)){
				ContainerSons.ControladorSons.PararTodosOsSons();
				ContainerSons.ControladorSons.TocarMusica(2);
				ChaveDeSeguraca = false;
				AjudantePainelFimDeJogo.Vitoria = true;
				AjudantePainelFimDeJogo.MudarTexto("Vitoria");
				AjudantePainelFimDeJogo.FazerSubir();
			}
							
							
							
		}
		ContainerSons.ControladorSons.TocarSons(Random.Range(16,17));				
		print(AjudantePersonagens[Turno].Nome+" Soltou a Magia "+AjudanteMagias[AjudantePersonagens[meuId].ListaMagias[NumeroMagia]].Nome+" em "+AjudantePersonagens[IdAlvo].Nome);
						
		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
			}
							
		}//fimfor
						
		prontoMover = false;
		prontoMagia = false;
		prontoAtacar = false;
		Rodando = true;
		AjudanteTurno[Turno] = 0;
		CirculoAcao.SetActive(false);
		ContainerMagias.SetActive(false);
		ajudanteainelModelo.FazerDescer();
		ExecutandoClique = false;
						//Anda
						
	}
					
				
	public void AtivarSon(int eu){
		/*Tipo arma 
								:1 Guitarra Machado
								:2 Arpa
								:3 Pandeiro
								:4 Timpano
								:5 Trompete
								*/
		switch(AjudantePersonagens[eu].TipoArma){
		case 1:
			
			ContainerSons.ControladorSons.TocarSons(11);
			
			break;
		case 2:
			
			ContainerSons.ControladorSons.TocarSons(12);
			
			break;
		case 3:
			
			ContainerSons.ControladorSons.TocarSons(13);
			
			break;
		case 4:
			
			ContainerSons.ControladorSons.TocarSons(14);
			
			break;
		case 5:
			
			ContainerSons.ControladorSons.TocarSons(15);
			
			break;
		}
	}
			

	

	public void AnimacaoAcabou(int eu, int inimigo,float dano,int Tipoataque,int Xzao,int Zzao){


		/*
		1 = ApanharMelee
		2 = AtirarTiroFogo
		3 = TacarBolaFogo
		4 = TacarBolaFogo2
		5 = TacarBolaFogo3
		*/

		switch(Tipoataque){
		case 1:

			AjudantePersonagens[inimigo].ApanharMeleee(inimigo,dano);
			//ContainerSons.ControladorSons.TocarSons(2);
			break;
		case 2:

			AjudantePersonagens[eu].AtirarTiroFogo(Xzao,Zzao,inimigo,dano);
			//ContainerSons.ControladorSons.TocarSons(3);
			break;
		case 3:

			AjudantePersonagens[inimigo].TacarBolaFogo(Xzao,Zzao,inimigo,dano);
			//ContainerSons.ControladorSons.TocarSons(4);
			break;
		case 4:

			AjudantePersonagens[inimigo].TacarBolaFogo2(Xzao,Zzao,inimigo,dano);
			//ContainerSons.ControladorSons.TocarSons(5);
			break;
		case 5:

			AjudantePersonagens[eu].TacarBolaFogo3(Xzao,Zzao,inimigo,dano);
			//ContainerSons.ControladorSons.TocarSons(6);
			break;
		}

	}

	public void DesligarCorrida(int posicao,float xTerr, float zTerr){
		
		int x = (int)xTerr/5;
		int z = (int)zTerr/5;
		
		if((AjudantePersonagens[posicao].posicaoX==x)&&(AjudantePersonagens[posicao].posicaoY==z)){
			AjudanteAnimaçao[posicao].Correr(false);
			AjudanteAnimaçao[posicao].Andar(false);
			//print ("Desligar corrida funcionou");

		}
		
	}

	public void ClicouNaFotoDoPersonagem(){
		AjudanteDaCamera = new Vector3(0+Personagens[Turno].transform.position.x,10,-10+Personagens[Turno].transform.position.z);
	}

	public void ClicouNaFotoDopersonagemEspecifico(int indicie){
		AjudanteDaCamera = new Vector3(0+Personagens[indicie].transform.position.x,10,-10+Personagens[indicie].transform.position.z);
	}

	public void ClicouTerrenoMinimapa(int x,int y){
		AjudanteDaCamera = new Vector3(x*5,10,-10 + y*5);
	}

	//Funçoes Referentes a IA DO JOGO

	public int RetornarXTerreno(){
		return gerenciadorTerrenos.xTerrenos;
	}
	public int RetornarYTerreno(){
		return gerenciadorTerrenos.yTerrenos;
	}

	public int RetornarPlayerX(int posicao){
		return AjudantePersonagens[posicao].posicaoX;
	}

	public int RetornarPlayerY(int posicao){
		return AjudantePersonagens[posicao].posicaoY;
	}

	public float RetornarPorcentagemPlayer(int posicao){ //porcentagem de vida 
		float porcentagem = (AjudantePersonagens[posicao].HPAtual*100)/AjudantePersonagens[posicao].HPMax;
		return porcentagem;
	}

	public int RetornarQuantidadeJogadores(){
		return ContadorPersonagens;
	}

	public int RetornarCustoMana(int IDMagia){
		return AjudanteMagias[IDMagia].CustoMP;
	}

	public int RetornarAlcance(int IDMagia){
		return AjudanteMagias[IDMagia].Alcance;
	}

	public int RetornarAtributoModificador(int IDMagia){
		return AjudanteMagias[IDMagia].AtributoModificador;
	}

	public int MediaPersonagensX(){
		return mediaX/DadosPersonagens.ControleDadosPersonagem.EstouNaParty.Length;
	}

	public int MediaPersonagensY(){
		return mediaY/DadosPersonagens.ControleDadosPersonagem.EstouNaParty.Length;
	}

	public int MediaInimigosX(){
		return mediaXI/contadorInimigos;
	}
	
	public int MediaInimigosY(){
		return mediaYI/contadorInimigos;
	}

	public void MoverPersonagem(int AjudX, int AjudY,int posicao){


		
		GerarTerrenos.voltarBranco(AjudantePersonagens[posicao].posicaoX,AjudantePersonagens[posicao].posicaoY);


		GerarTerrenos.MatrizTerrenosAndaveis[AjudantePersonagens[posicao].posicaoX,AjudantePersonagens[posicao].posicaoY] = true;

		GerarTerrenos.MatrizTerrenosAndaveis[AjudX,AjudY] = false;

		mediaXI = mediaXI - AjudantePersonagens[posicao].posicaoX;
		mediaYI = mediaYI - AjudantePersonagens[posicao].posicaoY;

		AjudantePersonagens[posicao].posicaoX = (int)AjudX;
		AjudantePersonagens[posicao].posicaoY = (int)AjudY;

		mediaXI = mediaXI + AjudantePersonagens[posicao].posicaoX;
		mediaYI = mediaYI + AjudantePersonagens[posicao].posicaoY;
		
		GerarTerrenos.MudarPosicao(AjudantePersonagens[posicao].posicaoX,AjudantePersonagens[posicao].posicaoY,AjudantePersonagens[posicao].SouEu);
		
		AjudanteNavMeshPersonagens[posicao].destination = new Vector3(AjudX*5,1,AjudY*5);
		AjudanteDaCamera = new Vector3(AjudX*5,10,AjudY*5 - 10);
		
		
		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
			}
			
		}//fimfor

		AjudanteAnimaçao[Turno].Andar(true);
		AjudantePersonagens[posicao].MPAtual = AjudantePersonagens[Turno].MPAtual +20;
		if(AjudantePersonagens[posicao].MPAtual>AjudantePersonagens[Turno].MPMax){
			AjudantePersonagens[posicao].MPAtual = AjudantePersonagens[Turno].MPMax;
		}
		prontoMover = false;
		prontoMagia = false;
		prontoAtacar = false;
		Rodando = true;
		AjudanteTurno[posicao] = 0;
		print(AjudantePersonagens[posicao].Nome+" Moveu-se");
		CirculoAcao.SetActive(false);
		ContainerMagias.SetActive(false);
		ajudanteainelModelo.FazerDescer();
		
		//Anda


	}

	public void AtacarAlvo(int eu,int alvo){

		//codigo ataque
		//inserir formula ataque
		//inserir Todos as outras coisas para
		/*Tipo arma 
		:1 Guitarra Machado
		:2 Arpa
		:3 Pandeiro
		:4 Timpano
		:5 Trompete
		*/

		Personagens[eu].transform.LookAt(Personagens[alvo].transform);
		int DanoRecebido = (int)((AjudantePersonagens[alvo].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[alvo].Stamina))-(AjudantePersonagens[eu].Dano + (MagntudeCalculoDano.MagnetudeForca*AjudantePersonagens[eu].Forca)));
		
		if(DanoRecebido>=0){
			DanoRecebido = 0;
		}
		
		DanoRecebido = Mathf.Abs(DanoRecebido);
		Personagens[eu].transform.LookAt(Personagens[alvo].transform);
		Personagens[alvo].transform.LookAt(Personagens[eu].transform);
		AjudanteDaCamera = new Vector3(0+Personagens[alvo].transform.position.x,10,-10+Personagens[alvo].transform.position.z);

		/*
								 * TIPOATAQE
								1 = ApanharMelee
								2 = AtirarTiroFogo
								3 = TacarBolaFogo
								4 = TacarBolaFogo2
								5 = TacarBolaFogo3
								*/
		
		/*Tipo arma 
								:1 Guitarra Machado
								:2 Arpa
								:3 Pandeiro
								:4 Timpano
								:5 Trompete
								*/
		switch(AjudantePersonagens[eu].TipoArma){
		case 1:
			
			//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);
			
			AjudanteAnimaçao[Turno].FicaPronto(alvo,eu,DanoRecebido,1,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[Turno].AtaqueArma2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(2);
			
			break;
		case 2:
			
			//AjudantePersonagens[Turno].AtirarTiroFogo((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
			
			AjudanteAnimaçao[Turno].FicaPronto(alvo,eu,DanoRecebido,2,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[Turno].AtaqueArma2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(3);
			break;
		case 3:
			
			//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);
			
			AjudanteAnimaçao[Turno].FicaPronto(alvo,eu,DanoRecebido,1,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[Turno].AtaqueArma2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(4);
			
			break;
		case 4:
			
			//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);
			
			AjudanteAnimaçao[Turno].FicaPronto(alvo,eu,DanoRecebido,1,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[Turno].AtaqueArma2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(5);
			break;
		case 5:
			
			//AjudantePersonagens[Turno].AtirarTiroFogo((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
			
			AjudanteAnimaçao[Turno].FicaPronto(alvo,eu,DanoRecebido,2,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[Turno].AtaqueArma2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(6);
			break;
		}

	


	}

	public void SoltarMagia(int eu, int alvo, int minhaMagia){

		AjudantePersonagens[eu].MPAtual = AjudantePersonagens[eu].MPAtual - AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].CustoMP;
		//codigo ataque Magico, Todas as verificaçoes de dano e diversas formulas de dano entram aqui
		//inserir formula ataque
		//inserir Todos as outras coisas para
		
		Personagens[eu].transform.LookAt(Personagens[alvo].transform);
		
		//AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]]
		
		int quantidadeRecurso = 0 ;
		



		// Case do Atributo Modificador da Magia
		
		switch(AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].AtributoModificador){
			
		case 1:
			quantidadeRecurso = (int)AjudantePersonagens[eu].Inteligencia;
			break;
		case 2:
			quantidadeRecurso = (int)AjudantePersonagens[eu].Forca;
			break;
		case 3:
			quantidadeRecurso = (int)AjudantePersonagens[eu].Agilidade;
			break;
		case 4:
			quantidadeRecurso = (int)((100*AjudantePersonagens[eu].HPAtual)/AjudantePersonagens[eu].HPMax);
			break;
			
		}
		
		//print (quantidadeRecurso);
		int DanoRecebido = 0;
		
		
		switch(AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].FormulaDano){
			
		case 1:
			DanoRecebido = (int)((AjudantePersonagens[alvo].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[alvo].Stamina))-(AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].DanoBase + (MagntudeCalculoDano.Formula1*quantidadeRecurso)));
			break;
		case 2:
			DanoRecebido = (int)((MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[alvo].Stamina)-(AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].DanoBase*MagntudeCalculoDano.Formula2*MagntudeCalculoDano.Formula2*quantidadeRecurso));
			break;
		case 3:
			DanoRecebido = (int)((AjudantePersonagens[alvo].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[alvo].Stamina))-(AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].DanoBase + (MagntudeCalculoDano.Formula3*quantidadeRecurso)));
			break;
		case 4:
			DanoRecebido = (int)(((AjudantePersonagens[alvo].Defesa + (MagntudeCalculoDano.MagnetudeStamina*AjudantePersonagens[alvo].Stamina))*(quantidadeRecurso/100))-((MagntudeCalculoDano.Formula4+(1 - (quantidadeRecurso/100)))*AjudantePersonagens[eu].Dano+AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].DanoBase));
			break;
			
			
		}
		//print ((MagntudeCalculoDano.Formula4+(1 - (quantidadeRecurso/100)))*AjudantePersonagens[Turno].Dano+AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].DanoBase);
		if(DanoRecebido>=0){
			DanoRecebido = 0;
		}
		DanoRecebido = Mathf.Abs(DanoRecebido);
		Personagens[eu].transform.LookAt(Personagens[alvo].transform);
		Personagens[alvo].transform.LookAt(Personagens[eu].transform);

		AjudanteDaCamera = new Vector3(0+Personagens[alvo].transform.position.x,10,-10+Personagens[alvo].transform.position.z);


		/*
									 * TIPOATAQE
									1 = ApanharMelee
									2 = AtirarTiroFogo
									3 = TacarBolaFogo
									4 = TacarBolaFogo2
									5 = TacarBolaFogo3
									*/
		
		switch(AjudanteMagias[AjudantePersonagens[eu].ListaMagias[minhaMagia]].ID){
			
		case 0:
			AjudanteAnimaçao[eu].FicaPronto(alvo,eu,DanoRecebido,4,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(2);
			
			//AjudantePersonagens[Turno].TacarBolaFogo2((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
			
			break;
		case 1:
			AjudanteAnimaçao[eu].FicaPronto(alvo,eu,DanoRecebido,3,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(2);
			
			//AjudantePersonagens[Turno].TacarBolaFogo((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
			break;
		case 2:
			AjudanteAnimaçao[eu].FicaPronto(alvo,eu,DanoRecebido,1,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(2);
			
			//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);
			break;
		case 3:
			AjudanteAnimaçao[eu].FicaPronto(alvo,eu,DanoRecebido,5,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(2);
			
			//AjudantePersonagens[Turno].TacarBolaFogo3((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
			break;
		case 4:
			AjudanteAnimaçao[eu].FicaPronto(alvo,eu,DanoRecebido,5,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(2);
			
			//AjudantePersonagens[Turno].TacarBolaFogo3((int)xTerr,(int)zTerr,posicaoAtacado,DanoRecebido);
			break;
		case 5:
			AjudanteAnimaçao[eu].FicaPronto(alvo,eu,DanoRecebido,1,AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5);
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AtivarSon(eu);
			//ContainerSons.ControladorSons.TocarSons(2);
			
			//AjudantePersonagens[posicaoAtacado].ApanharMeleee(posicaoAtacado,DanoRecebido);
			break;
			
		}
		/*
		switch(AjudanteMagias[AjudantePersonagens[eu].ListaMagias[NumeroMagia]].ID){
			
			case 0:
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AjudantePersonagens[Turno].TacarBolaFogo2(AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5,alvo,DanoRecebido);
			break;
			case 1:
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AjudantePersonagens[Turno].TacarBolaFogo(AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5,alvo,DanoRecebido);
			break;
			case 2:
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AjudantePersonagens[alvo].ApanharMeleee(alvo,DanoRecebido);
			break;
			case 3:
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AjudantePersonagens[Turno].TacarBolaFogo3(AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5,alvo,DanoRecebido);
			break;
			case 4:
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AjudantePersonagens[Turno].TacarBolaFogo3(AjudantePersonagens[alvo].posicaoX*5,AjudantePersonagens[alvo].posicaoY*5,alvo,DanoRecebido);
			break;
			case 5:
			AjudanteAnimaçao[eu].AtacarMagia2H(true);
			AjudantePersonagens[alvo].ApanharMeleee(alvo,DanoRecebido);
			break;
			
		}
		*/



	}
}
