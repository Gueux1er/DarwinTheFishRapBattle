using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Deplacement : MonoBehaviour {


    


	//Fadeaunoir
	bool contactvoiture = false;





	public GameObject Floor;
	public GameObject Runner;
	public GameObject monopedeCanvas;
	public GameObject bipedeCanvas;
	public GameObject TripedeCanvas;
	public GameObject QuadripedeCanvas;
	public GameObject ChangementAvatar;
	public GameObject Avataranimation;
	public GameObject cameraAvatar;
	public GameObject Blocus;

	public Image fadeImage;
	public Image fondcolorer;




	Animator animator;


	public Text Longeur;

	Rigidbody rb;

	public int Character = 1;
	int distance = 0;
	int nbinput = 0;


	float Movement = 0;
	float velocityX;

	bool StopInstantiate = false;
	bool oneframe = false;
	bool changementcamera = false;

	float timer = 0;
	float pute = 1f;

	bool enculer = false;


	bool un = false;
	bool deux = false;
	bool trois = false;
	bool quatre = false;
	bool cinq	= false;


	// Use this for initialization
	void Start () {



		rb = Runner.GetComponent<Rigidbody>();
		animator = Avataranimation.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		timer += Time.deltaTime;

		if (distance < 100 && enculer == false) {
			enculer = true;
		}
		if (contactvoiture == true) {
			
			pute -= 0.1f;
		}

		if (timer > 5) {
			timer = 0;
		}

		Avataranimation.transform.position = new Vector3 (Avataranimation.transform.position.x, -3.3f,Avataranimation.transform.position.z);

		if (contactvoiture == true) {
			Avataranimation.transform.localScale = new Vector3 (Avataranimation.transform.localScale.x - 0.01f, Avataranimation.transform.localScale.y - 0.01f, Avataranimation.transform.localScale.z - 0.01f);
//			fadeImage.DOFade(1, fadeDurationSeconds).SetDelay(delayBeforeFadingIn).SetEase(fadeEase);
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + 0.01f * Time.deltaTime);
			if (fadeImage.color.a >= 1)
			{
				ChallengerManager.Instance.currentChallenger?.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				MovementCharacter.Instance.StopSoundQuestion();
				SceneManager.LoadScene (1);
			}
		}

		velocityX = rb.velocity.x;

		animator.speed = velocityX * 0.1f;

		if (Input.GetKeyDown (KeyCode.R))
		{
			ChallengerManager.Instance.currentChallenger?.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			MovementCharacter.Instance.StopSoundQuestion();
			SceneManager.LoadScene (0);
		}

		oneframe = false;

		distance = (int)Runner.transform.position.x;

		animator.SetFloat("distance",distance);


			




		if (Movement < 0) {

			Movement = 0;
		}

		if (Movement != 0 && Movement > 0) {

			run ();
		}


		if (distance <300) {
			monopedeCanvas.SetActive (true);
			CulDeJatte ();
		}


		if (distance > 300 && distance < 600) {
			monopedeCanvas.SetActive (false);
			bipedeCanvas.SetActive (true);
			biped ();
		}

		if (distance > 300 && un == false) {
			un = true;
		}

		if (distance > 600 && deux == false) {
			deux = true;
		}



		if (distance > 600 && distance < 1200) {
			bipedeCanvas.SetActive (false);
			QuadripedeCanvas.SetActive (true);
			bipedbras ();
		}

		if (distance > 1200 && StopInstantiate == false) {
			Instantiate (Blocus, new Vector3 (Runner.transform.position.x +25, Runner.transform.position.y, Runner.transform.position.z), new Quaternion (0,0,0,0));
			StopInstantiate = true;
		}

		//		if (Character == 3) {
//			TripedeCanvas.SetActive (true);
//			triped ();
//		}
//		if (Character == 4) {
//			QuadripedeCanvas.SetActive (true);
//			quadripede ();
//		}
	}


	public void OnTriggerEnter (Collider col) {
		if (col.tag == "Floor") {
			Instantiate (Floor, new Vector3 (col.transform.position.x + 25f, col.transform.position.y, 0), new Quaternion (0,0,0,0));
		}
	}

	public void OnTriggerExit (Collider col) {
		if (col.tag == "Floor") {
			Destroy (col.gameObject);
		}
	}

	public void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "ChangementAvatar") {
			Character += 1;
			StopInstantiate = false;
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "changementniveau") {
			contactvoiture = true;
			QuadripedeCanvas.SetActive (false);
		}
	}


	void CulDeJatte () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			
			Movement += 15;
		}
	}

	void biped () {
		

		if (Input.GetKeyDown (KeyCode.Q) && nbinput == 0 && oneframe == false) {
			
			nbinput = 1;
			oneframe = true;
		}
		if (Input.GetKeyDown (KeyCode.Q) && nbinput != 0 && oneframe == false) {
			Movement -= Movement * 0.5f;
			oneframe = true;
		}





		if (Input.GetKeyDown (KeyCode.D) && nbinput == 1 && oneframe == false) {
			Movement += 15;
			nbinput =0;
			oneframe = true;

		}
		if (Input.GetKeyDown (KeyCode.D) && nbinput != 1 && oneframe == false) {
			Movement -= Movement * 0.5f;
			oneframe = true;

		}

	}

	void bipedbras () {
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.K) && nbinput == 0 && oneframe == false) {
			nbinput =1;
			oneframe = true;
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.M) && nbinput == 1 && oneframe == false) {
			nbinput =0;
			oneframe = true;
			Movement += 40;
		}
	}

















//
//	void triped () {
//		
//		if (Input.GetKeyDown (KeyCode.Z) && nbinput == 0 && oneframe == false) {
//
//			nbinput = 1;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.Z) && nbinput != 0 && oneframe == false) {
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput == 1 && oneframe == false) {
//
//			nbinput = 2;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput != 1 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.D) && nbinput == 2 && oneframe == false) {
//
//			nbinput = 0;
//			oneframe = true;
//			Movement += 50;
//		}
//		if (Input.GetKeyDown (KeyCode.D) && nbinput != 2 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//	}
//
//
//	void quadripede () {
//
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput == 0 && oneframe == false) {
//
//			nbinput = 1;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput != 0 && oneframe == false) {
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//		if (Input.GetKeyDown (KeyCode.S) && nbinput == 1 && oneframe == false) {
//
//			nbinput = 2;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.S) && nbinput != 1 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.D) && nbinput == 2 && oneframe == false) {
//
//			nbinput = 3;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.D) && nbinput != 2 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.F) && nbinput == 3 && oneframe == false) {
//			Movement += 80;
//			nbinput = 1;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.F) && nbinput != 3 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//	}
//




	void run () {
		rb.AddForce (new Vector3 (Movement * 0.5f, 0, 0));
		Movement -= Movement * 0.05f + 0.25f;
		Debug.Log (Movement);
	}
}
