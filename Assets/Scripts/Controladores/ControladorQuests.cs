using UnityEngine;
using System.Collections;

public class ControladorQuests : MonoBehaviour {

	private GameMasterFora gameMaster;
	private ControladorQuests.questChain[] presentChains;

	[System.Serializable]
	public struct questChain{
		public string chainName;
		public questSteps[] chainSteps;

		[HideInInspector]
		public bool chainComplete;
		[HideInInspector]
		public bool chainActive;
		[HideInInspector]
		public int chainStep;

		public questChain(int chainStepNumber){
			chainName = "";
			chainSteps = new questSteps[chainStepNumber];
			chainComplete = false;
			chainActive = false;
			chainStep = 0;
		}
	}

	[System.Serializable]
	public struct questSteps{
		public string stepName;
		public int stepQtdeDeliver;
		public GameObject[] stepTarget;
		public string actionTriggered;
		public Transform actionTarget;

		[HideInInspector]
		public bool stepComplete;
		[HideInInspector]
		public bool stepActive;

		public questSteps(string stepCurrName){
			stepName = stepCurrName;
			stepQtdeDeliver = 0;
			stepTarget = null;
			stepComplete = false;
			stepActive = false;
			actionTriggered = "";
			actionTarget = null;
		}
	}

	// Use this for initialization
	void Start () {
		gameMaster = FindObjectOfType<GameMasterFora> ();
		presentChains = gameMaster.presentChains;
	}

	public void advanceQuest(string chainName, string newMapName){
		for (int i=0; i<gameMaster.presentChains.Length; i++) {
			if (gameMaster.presentChains[i].chainName != chainName)
				continue;

			if(presentChains[i].chainStep > 0)
				for (int j=0; j<gameMaster.presentChains[i].chainSteps[presentChains[i].chainStep - 1].stepTarget.Length; j++){
					bool subquestActive = false;
					subquestActive = gameMaster.presentChains[i].chainSteps[presentChains[i].chainStep - 1].stepTarget[j].transform.Find ("SubQuest").gameObject.activeInHierarchy;
					if (subquestActive)
						return;
			}

			int chain = i;
			int step = presentChains[chain].chainStep;
			presentChains [chain].chainSteps [step].stepComplete = true;
			presentChains [chain].chainSteps [step].stepActive = false;
			if (step < presentChains [chain].chainSteps.Length - 1) {
				for (int j=0; j<gameMaster.presentChains[i].chainSteps[presentChains[i].chainStep].stepTarget.Length; j++){
					if(step < presentChains [chain].chainSteps.Length - 2)
						presentChains [chain].chainSteps[step].stepTarget[j].GetComponent<Quests>().toggleQuestMesh("Subquest");
					else
						presentChains [chain].chainSteps[step].stepTarget[j].GetComponent<Quests>().toggleQuestMesh("Complete");
				}
				presentChains [chain].chainSteps [step + 1].stepActive = true;
				presentChains [chain].chainStep++;
				print ("Quest Step Complete");
			} else {
				presentChains [chain].chainComplete = true;
				presentChains [chain].chainActive = false;
				presentChains [chain].chainSteps[presentChains.Length-1].stepActive = false;
				print ("Quest Chain Complete");
			}
			
			if (newMapName != "") {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<ForaCombate> ().saveStatus ();
				Application.LoadLevel (newMapName); 
			}
		}
	}

}
