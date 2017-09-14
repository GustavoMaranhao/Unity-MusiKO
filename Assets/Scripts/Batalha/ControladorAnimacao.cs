using UnityEngine;
using System.Collections;

public class ControladorAnimacao : MonoBehaviour {

	public Animator MeuAnimator;
	public bool morto = false;

	public GameObject Mestre;
	public GameMasterCombate AjudanteMestre;

	public int IDInimigo;
	public int MeuId;
	public float Dano;
	public int IDTipoAtaque;
	public int Xzao;
	public int Zzao;


	// Use this for initialization
	void Awake () {
		MeuAnimator = gameObject.GetComponent<Animator>();

	}

	public void IniciarMestre(GameObject MeuMestre){
		Mestre = MeuMestre;
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
	}

	public void FicaPronto(int Inimigos,int eu, float meudano,int meuataque,int xDele,int zDele){
		IDInimigo = Inimigos;
		MeuId = eu;
		Dano = meudano;
		IDTipoAtaque = meuataque;
		Xzao = xDele;
		Zzao = zDele;

	}

	public void Acabou(){
		AjudanteMestre.AnimacaoAcabou(MeuId,IDInimigo,Dano,IDTipoAtaque,Xzao,Zzao);
	}

	void Update(){
		if(morto==false){
			gameObject.transform.localPosition = new Vector3(0f,-0.5f,0f);
		}

	}
	public void AtaqueArma1H(bool Parametro){
		MeuAnimator.SetBool("AtaqueArma1H",Parametro);
	}

	public void AtaqueArma2H(bool Parametro){
		MeuAnimator.SetBool("AtaqueArma2H",Parametro);
	}

	public void AtaqueArmaArco(bool Parametro){
		MeuAnimator.SetBool("AtaqueArmaArco",Parametro);
	}

	public void Andar(bool Parametro){
		MeuAnimator.SetBool("Andar",Parametro);
	}

	public void Correr(bool Parametro){
		MeuAnimator.SetBool("Correr",Parametro);
	}

	public void Apanhar(bool Parametro){
		MeuAnimator.SetBool("Apanhar",Parametro);
	}

	public void AtacarMagia1H(bool Parametro){
		MeuAnimator.SetBool("AtacarMagia1H",Parametro);
	}

	public void AtacarMagia2H(bool Parametro){
		MeuAnimator.SetBool("AtacarMagia2H",Parametro);
	}

	public void SoltarMagiaProjetil(bool Parametro){
		MeuAnimator.SetBool("SoltarMagiaProjetil",Parametro);
	}

	public void AtacarMagiaArco(bool Parametro){
		MeuAnimator.SetBool("AtacarMagiaArco",Parametro);
	}

	public void Morrer(bool Parametro){
		morto = Parametro;
		MeuAnimator.SetBool("Morrer",Parametro);
	}

}
