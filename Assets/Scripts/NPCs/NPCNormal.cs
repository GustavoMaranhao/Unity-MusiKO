using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCNormal : MonoBehaviour {
	public bool SubRotinas = true;
	public UnityEngine.AI.NavMeshAgent AuxiliarNavMesh;
	public int Comportamento = 0;
	public Text NomePersonagem;
	public Text AjudaTexto;
	public Image Moldura;
	public string[] Falas;
	public string[] FalasSecundarias;
	public string Nome;
	public int indivTimer = 0;

	public bool SouAdagio = false;
	public GameObject Intrumentos;
	public bool jaDestrui  = false;
	
	public float Timer = 0.0f;
	
	public bool questGiver;
	public string chainToStart = "";
	public int questNumber;
	public bool follower;
	
	public GameObject personagem = null;  // CHICO
	
	[HideInInspector]
	public Transform actionTarget;
	
	
	private GameMasterFora gameMaster;
	private ControladorQuests contQuests;
	private int ContadorFalas = 0;
	private Vector3 PosicaoInicial;
	private bool followingEvent = false;
	private Transform targetPosition;
	private float targetDistance = 2f;
	private float sprintSpeed = 6f;
	private float defaultSpeed = 3.5f;
	public Sprite MinhaFoto;
	
	private Animation animacao = null;  // CHICO

	void Start(){
		PosicaoInicial = gameObject.transform.position;
		gameMaster = FindObjectOfType<GameMasterFora> ();
		contQuests = gameMaster.GetComponent<ControladorQuests> ();

		Debug.Log ("NpcNormal.cs buscando quest de " + gameObject.name);
		if (gameObject.transform.Find ("Quest") != null) 
		{
			gameObject.transform.Find ("Quest").gameObject.SetActive (questGiver);
			gameObject.transform.Find ("SubQuest").gameObject.SetActive (false);
			gameObject.transform.Find ("CompleteQuest").gameObject.SetActive (false);
		}
		
		//// CHICO
		
		if (personagem != null)
		{
			animacao = personagem.GetComponent<Animation>();
			if (animacao == null)
				Debug.Log ( personagem.name+" animacao nao encontrada ");
		}
		/// CHICO
	}
	void Update(){
		
		if (gameMaster.Livre == true) {
			if(SubRotinas==true){
				Timer += Time.deltaTime;
				
				if (Timer > (Random.Range(1,7)+indivTimer)) {
					switch(Comportamento){
					case 0:
						if(Vector3.Distance(PosicaoInicial,gameObject.transform.position)>2){
							AuxiliarNavMesh.destination = PosicaoInicial;
							Timer = 0;
						}
						break;
					case 1:
						Debug.Log ("SetDestination "+gameObject.name);
 						AuxiliarNavMesh.destination = new Vector3((Random.Range(-5,5)+gameObject.transform.position.x),gameObject.transform.position.y,(Random.Range(-5,5)+gameObject.transform.position.z));
						Timer = 0;
						if (animacao != null)
						{
							Debug.Log (" personagem vai andar "+personagem.name);
							animacao.CrossFade ("Walk");	
						}
						break;
					}//fim switch
				}//fim if

				float distancia = Vector3.Magnitude(AuxiliarNavMesh.destination - transform.position);
				if (animacao != null) 
				{
					Debug.Log("personagem sem animaçao?"+gameObject.name);
				   if ((distancia <= 1.0f) && (!animacao["Idle"].enabled))
					  animacao.CrossFade("Idle");	
				}
			}//subrotinas
			

			
		}//livre

		if (followingEvent){
			AuxiliarNavMesh.destination = targetPosition.position;
			
			float distancia = Vector3.Magnitude(AuxiliarNavMesh.destination - transform.position);
			if (distancia < targetDistance) 
				animacao.CrossFade ("Idle");
			else{
				if(distancia >= targetDistance*4f){
					AuxiliarNavMesh.speed = sprintSpeed;	
					animacao.CrossFade ("Run");
				} else {
					if(animacao["Run"].enabled == true){
						if(distancia <= targetDistance*2f){
							AuxiliarNavMesh.speed = defaultSpeed;
							Debug.Log (" personagem vai andar apos correr "+personagem.name);
							animacao.CrossFade ("Walk");	
						}
					} else {
						AuxiliarNavMesh.speed = defaultSpeed;
						Debug.Log (" personagem vai andar 2  "+personagem.name);
						animacao.CrossFade ("Walk");							
					}
				}
			}
		}
		
	}
	
	public void Evento(string tipoEvento, Transform targetEvento){
		switch(tipoEvento){
		case "Move":
			//AuxiliarNavMesh.destination = targetEvento.position - targetEvento.forward*targetDistance;
			AuxiliarNavMesh.destination = targetEvento.position;
			followingEvent = true;
			SubRotinas = false;
			targetPosition = targetEvento;
			Comportamento = 0;
			AuxiliarNavMesh.stoppingDistance = targetDistance;
			break;
		}//fim switch
	}//fim evento
	
	public IEnumerator Falar(){
		gameMaster.bWriting = true;
		gameMaster.Livre = false;
		gameMaster.ImagemPersonagem.sprite = MinhaFoto;
		NomePersonagem.text = Nome;
		
		string[] usarFalas = new string[0];
		if (FalasSecundarias.Length > 0)
			if ((gameObject.transform.Find ("Quest").gameObject.activeInHierarchy) ||
			    (gameObject.transform.Find ("SubQuest").gameObject.activeInHierarchy) ||
			    (gameObject.transform.Find ("CompleteQuest").gameObject.activeInHierarchy))
				usarFalas = Falas;
			else
				usarFalas = FalasSecundarias;
		else
			usarFalas = Falas;
		
		int NumeroDeFalas = usarFalas.Length;
		if (ContadorFalas < NumeroDeFalas) {
			int Comprimento = usarFalas[ContadorFalas].Length;
			AjudaTexto.text = "";
			for (int x = 0; x<Comprimento; x++) {
				AjudaTexto.text = AjudaTexto.text + usarFalas[ContadorFalas] [x];
				yield return new WaitForSeconds (0.01f);
			}
			
			print (usarFalas[ContadorFalas]);
			ContadorFalas++;
			gameMaster.bWriting = false;
			
		} else {
			gameMaster.Livre = true;
			gameMaster.Painel.SetActive(false);
			ContadorFalas = 0;
			gameMaster.resetEngagedNPC();
			
			if (questGiver)
				questGiver = !questGiver;
			
			
			if((gameObject.transform.Find ("Quest").gameObject.activeInHierarchy) ||
			   (gameObject.transform.Find ("SubQuest").gameObject.activeInHierarchy) ||
			   (gameObject.transform.Find ("CompleteQuest").gameObject.activeInHierarchy)){
				
				for(int i=0;i<gameMaster.presentChains.Length;i++){
					if(gameMaster.presentChains[i].chainName != chainToStart){
						if((SouAdagio==true)&&(jaDestrui==false)){
							jaDestrui=true;
							Destroy(Intrumentos);
						}

						continue;
					}
					if(gameMaster.presentChains[i].chainSteps[gameMaster.presentChains[i].chainStep].actionTarget != null && follower){
						Evento("Move",gameMaster.presentChains[i].chainSteps[gameMaster.presentChains[i].chainStep].actionTarget);
					}
					
					gameMaster.presentChains[i].chainActive = true;
				}
				
				gameObject.GetComponent<Quests>().toggleQuestMesh("None");
				contQuests.advanceQuest(chainToStart,"");
			}
			
		}
	}//falar
}
