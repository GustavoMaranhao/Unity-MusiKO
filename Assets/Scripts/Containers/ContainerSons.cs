using UnityEngine;
using System.Collections;

public class ContainerSons : MonoBehaviour {

	public static ContainerSons ControladorSons;

	public AudioClip[] ListaMusicas;
	public AudioClip[] EfeitosSonors;

	public GameObject[] PaisdosCanaisM;
	public AudioSource[] CanaisM;

	public GameObject[] PaisdosCanaisE;
	public AudioSource[] CanaisE;


	void Awake () {
		
		if(ControladorSons == null){
			
			DontDestroyOnLoad(gameObject);
			ControladorSons = this;
			
		}else if(ControladorSons != this){
			
			Destroy(gameObject);
		}
		
		
	}//awake

	void Start(){
		CanaisM = new AudioSource[PaisdosCanaisM.Length];
		for(int x = 0;x<PaisdosCanaisM.Length;x++){
			CanaisM[x] = PaisdosCanaisM[x].GetComponent<AudioSource>();
		}

		CanaisE = new AudioSource[PaisdosCanaisE.Length];
		for(int x = 0;x<PaisdosCanaisE.Length;x++){
			CanaisE[x] = PaisdosCanaisE[x].GetComponent<AudioSource>();
		}
	}
	public void PararTodosOsSons(){
		for(int x = 0;x<CanaisM.Length;x++){
			CanaisM[x].Stop();
		}
	}

	public void TocarMusica(int posicao){
		for(int x = 0;x<CanaisM.Length;x++){
			if(CanaisM[x].isPlaying==false){
				CanaisM[x].loop = false;
				CanaisM[x].clip = ListaMusicas[posicao];
				CanaisM[x].Play();
				break;
			}
		}
	}

	public void TocarMusicaLoop(int posicao){
		for(int x = 0;x<CanaisM.Length;x++){
			if(CanaisM[x].isPlaying==false){
				CanaisM[x].loop = true;
				CanaisM[x].clip = ListaMusicas[posicao];
				CanaisM[x].Play();
				break;
			}
		}
	}

	public void PararLoopsMusicas(){
		for(int x = 0;x<CanaisM.Length;x++){

			CanaisM[x].loop = false;
				
			
		}
	}

	public void TocarSons(int posicao){
		for(int x = 0;x<CanaisE.Length;x++){
			if(CanaisE[x].isPlaying==false){
				CanaisE[x].loop = false;
				CanaisE[x].clip = EfeitosSonors[posicao];
				CanaisE[x].Play();
				break;
			}
		}
	}

	public void TocarSonsLoop(int posicao){
		for(int x = 0;x<CanaisE.Length;x++){
			if(CanaisE[x].isPlaying==false){
				CanaisE[x].loop = true;
				CanaisE[x].clip = EfeitosSonors[posicao];
				CanaisE[x].Play();
				break;
			}
		}
	}



}
