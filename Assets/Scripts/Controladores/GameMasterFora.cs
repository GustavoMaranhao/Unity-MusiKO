using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterFora : MonoBehaviour {
	
	public ControladorQuests.questChain[] presentChains;

	private int offsetToNPC = 2;
	private GameObject Personagem;
	private int numeroNpcs = 0;
	private GameObject [] Npcs;
	private NPCNormal[] AjudanteNpcs;
	private float Timer = 0;

	[HideInInspector]
	public GameObject Painel;
	public Image ImagemPersonagem;
	[HideInInspector]
	public bool bWriting = false;
	[HideInInspector]
	public bool Livre = true;
	[HideInInspector]
	public NPCNormal EngagedNPC;
	
	void Start(){
		Painel = GameObject.Find ("ChatPanel");

		Personagem = GameObject.FindGameObjectWithTag ("Player");
		Npcs = GameObject.FindGameObjectsWithTag ("NPC");
		numeroNpcs = Npcs.Length;

		Painel.SetActive(false);
		EngagedNPC = null;
	}

	void Update(){

		Timer += Time.deltaTime;
		if (Input.GetKeyUp ("t") && Livre) {
			if(Timer>0.5f){

				if(Livre){
					
					for(int x = 0;x<numeroNpcs;x++){
						//Personagem tem que estar a uma certa distancia do NPC
						if(Vector3.Distance(Personagem.transform.position, Npcs[x].transform.position)<offsetToNPC){
							Vector3 forward = Personagem.transform.TransformDirection(Vector3.forward);
							Vector3 toOther = Npcs[x].transform.position - Personagem.transform.position;
							float dotProduct = Vector3.Dot(Vector3.Normalize(forward), Vector3.Normalize(toOther));

							//Personagem tem que estar Livre (nao estar falando ja) e dentro de um 
							//cone de abertura com angulo de 60 graus na sua frente
							if(Livre && (dotProduct >= 0.5f)){
								Painel.SetActive(true);
								StartCoroutine(Npcs[x].GetComponent<NPCNormal>().Falar());
								EngagedNPC = Npcs[x].GetComponent<NPCNormal>();
								Livre = false;
							}
						}
					}
					
				}else{
					Painel.SetActive(false);
					Livre = true;
				}

			}




			Timer  =0.0f;
		}
		if (Input.GetKey ("e")) {
			//Npcs[0].GetComponent<NPCNormal>().Evento(0);
		}

		if ((Input.GetMouseButtonUp (0)|| Input.GetKeyDown("space")) && !Livre && EngagedNPC != null && !bWriting) {
			StartCoroutine(EngagedNPC.Falar());
		}

	}

	public void resetEngagedNPC(){
		EngagedNPC = null;
	}

}
