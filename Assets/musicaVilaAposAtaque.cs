using UnityEngine;
using System.Collections;

public class musicaVilaAposAtaque : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ContainerSons.ControladorSons.PararTodosOsSons();
		ContainerSons.ControladorSons.PararLoopsMusicas();
		Debug.Log ("  vai tocar som 7 ");
		ContainerSons.ControladorSons.TocarMusicaLoop(0);
	}
}
