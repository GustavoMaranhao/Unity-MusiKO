using UnityEngine;
using System.Collections;

public class moverBarcaca : MonoBehaviour {

/// <summary>
	/// COLOQUEI O OBJETO DE TESTE DA BARCACA NA POSICAO (120, 15, 100) E ESCALA (2, 2, 2)
	/// 
/// Este script deve ser associado ao objeto da Barcaca Voadora (airShip).
/// 
/// circleMe - deve ser criado na cena como um objeto vazio, ao redor do qual a barcaca voadora
///            ficara girando
/// anguloDeRotacao - e o valor usado para dar movimento a barcaca. Pode ser modificado no 
///                   inspector ou ter seu valor modificado aqui mesmo neste codigo
/// </summary>
	public Transform circleMe;

	public float anguloDeRotacao = 15.0f;

	void Start()
	{
		//ContainerSons.ControladorSons.PararLoopsMusicas();
		//ContainerSons.ControladorSons.TocarMusicaLoop(10);
	}

	void Update () {
		transform.RotateAround (circleMe.position, Vector3.up, anguloDeRotacao * Time.deltaTime);
	}

//	public Vector3 pointB;
	
//	IEnumerator Start () {
//		Vector3 pointA = transform.position;
//		while (true) {
//			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 10.0f));
//			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 10.0f));
//		}
//	}
	
//	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
//		float i = 0.0f;
//		float rate = 1.0f / time;
//		while (i < 1.0f) {
//			i += Time.deltaTime * rate;
//			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
//			yield return null; 
//		}
//	}
}
