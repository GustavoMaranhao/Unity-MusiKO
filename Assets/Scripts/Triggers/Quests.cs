using UnityEngine;
using System.Collections;

public class Quests : MonoBehaviour {

	private GameMasterFora gameMaster;
	private ControladorQuests.questChain[] presentChains;

	public string loadNewMapName;
	public bool questGiver;
	public string belongToChain;

	// Use this for initialization
	void Start () {
		DadosIntroducao.ControleDadosDaIntroducao.ProximaCena ="TesteMecanicaCombate";
		DadosIntroducao.ControleDadosDaIntroducao.IdFalas = 1;

		ContainerSons.ControladorSons.PararTodosOsSons();
		ContainerSons.ControladorSons.PararLoopsMusicas();
		Debug.Log ("  vai tocar som 7 ");


		ContainerSons.ControladorSons.TocarMusicaLoop(3);

		gameMaster = FindObjectOfType<GameMasterFora> ();
		presentChains = gameMaster.presentChains;
		Debug.Log ("Tentara acessar quest do " + gameObject.name);

		if (gameObject.transform.Find ("Quest") != null) {
			gameObject.transform.Find ("Quest").gameObject.SetActive (questGiver);
			gameObject.transform.Find ("SubQuest").gameObject.SetActive (false);
			gameObject.transform.Find ("CompleteQuest").gameObject.SetActive (false);
		}
	}

	public void toggleQuestMesh(string switchTo){
		bool auxState;
		if (switchTo == "Start") {
			auxState = !gameObject.transform.Find ("Quest").gameObject.activeInHierarchy;
			gameObject.transform.Find ("Quest").gameObject.SetActive (auxState);
			gameObject.transform.Find ("SubQuest").gameObject.SetActive (false);
			gameObject.transform.Find ("CompleteQuest").gameObject.SetActive (false);
		} 
		else if (switchTo == "Subquest") {
			auxState = !gameObject.transform.Find ("SubQuest").gameObject.activeInHierarchy;
			gameObject.transform.Find ("Quest").gameObject.SetActive (false);
			gameObject.transform.Find ("SubQuest").gameObject.SetActive (auxState);
			gameObject.transform.Find ("CompleteQuest").gameObject.SetActive (false);
		} 
		else if (switchTo == "Complete") {
			auxState = !gameObject.transform.Find ("CompleteQuest").gameObject.activeInHierarchy;
			gameObject.transform.Find ("Quest").gameObject.SetActive (false);
			gameObject.transform.Find ("SubQuest").gameObject.SetActive (false);
			gameObject.transform.Find ("CompleteQuest").gameObject.SetActive (auxState);
		} 
		else if (switchTo == "None") {
			gameObject.transform.Find ("Quest").gameObject.SetActive (false);
			gameObject.transform.Find ("SubQuest").gameObject.SetActive (false);
			gameObject.transform.Find ("CompleteQuest").gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			for (int i=0;i<presentChains.Length;i++){
				if(presentChains[i].chainName != belongToChain)
					continue;

				for(int j=0;j<presentChains[i].chainSteps.Length;j++){
					if(!presentChains[i].chainSteps[j].stepActive)
						continue;

					/*if (--pseudo codigo--player.numeroDeItensQuest--final pseudo codigo-- >= presentChains[i].chainSteps[j].stepQtdeDeliver){
						advanceQuest(i,j,loadNewMap);
					}*/

					/*if (presentChains[i].chainSteps[j].actionTriggered != "")
						gameMaster.EngagedNPC.Evento(presentChains[i].chainSteps[j].actionTriggered, presentChains[i].chainSteps[j].actionTarget.transform);
*/
					if (presentChains[i].chainSteps[j].stepTarget!= null){// && (transform.position == presentChains[i].chainSteps[j-1].stepTarget[0].transform.position)){
						//toggleQuestMesh("None");
						gameMaster.GetComponent<ControladorQuests>().advanceQuest(presentChains[i].chainName,loadNewMapName);
					}

				}
			}
		}
	}

}