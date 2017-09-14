using UnityEngine;
using System.Collections;

public class ItensAppear : MonoBehaviour {

	public GameObject[] itensToToggle;

	private bool toggleOnce = true;

	void OnTriggerEnter(Collider other) {
		if ((other.tag == "Player")) {
			if(((gameObject.transform.Find ("SubQuest").gameObject.activeInHierarchy) ||
			   (gameObject.transform.Find ("CompleteQuest").gameObject.activeInHierarchy))
				&& toggleOnce){
				for(int i=0; i<itensToToggle.Length; i++){
					bool aux = !itensToToggle[i].activeInHierarchy;
					itensToToggle[i].gameObject.SetActive(aux);
				}
				toggleOnce = !toggleOnce;
				gameObject.GetComponent<Quests>().toggleQuestMesh("None");
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if ((other.tag == "Player") && !toggleOnce) {
			toggleOnce = !toggleOnce;
		}
	}

}
