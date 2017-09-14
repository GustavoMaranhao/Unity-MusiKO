using UnityEngine;
using System.Collections;

public class AnimatorAtacarArma2H : StateMachineBehaviour {

	public GameObject Mestre;
	public ControladorAnimacao AjudanteMestre;
	
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		Mestre = animator.gameObject;
		AjudanteMestre = Mestre.GetComponent<ControladorAnimacao>();


		animator.SetBool ("AtaqueArma2H", false);
		animator.SetBool ("AtacarMagia2H", false);
		Debug.Log ("Enter Atacar");

		if(AjudanteMestre!=null){
			AjudanteMestre.Acabou();
			
		}
	
	}

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
		//Mestre = animator.gameObject;
		//AjudanteMestre = Mestre.GetComponent<ControladorAnimacao>();
		
		
		animator.SetBool ("AtaqueArma2H", false);
		animator.SetBool ("AtacarMagia2H", false);
		Debug.Log ("Enter Atacar");
		
		//AjudanteMestre.Acabou();
		
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineExit e chamado no last frame da transicao
//	override public void OnStateMachineExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//		animator.SetBool ("AtaqueArma2H", false);
//		animator.SetBool ("AtacarMagia2H", false);
//	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
