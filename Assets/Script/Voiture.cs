﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Voiture : MonoBehaviour {

	//end

	bool ending = false;

	Rigidbody rb;
	public GameObject cameravoiture;

	bool Drifting = true;
	bool VaporWave = false;

	bool lacherdroit = false;

	bool lachergauche = false;

	public GameObject VoitureFBX;

	public Image fadeImage;

	float  multiplicateurvitesse;
	float ralenti = 1;

	// Use this for initialization
	void Start () {


		multiplicateurvitesse = 1;
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (VaporWave == true) {
			transform.position += new Vector3 (0, 0, 0.5f * multiplicateurvitesse * ralenti);
		}
			

		if (ending == true) {
			if (VoitureFBX.transform.position.z < 900) {
				transform.position += new Vector3 (0, 0, 0.5f * multiplicateurvitesse * ralenti);
				VoitureFBX.transform.Rotate (0, 4f, 0);
			}

			if (VoitureFBX.transform.position.z >= 900) {
				fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + 0.01f);

				if (fadeImage.color.a >= 1)
				{
					ChallengerManager.Instance.currentChallenger?.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
					MovementCharacter.Instance.StopSoundQuestion();
					SceneManager.LoadScene (2);
				}
			}
		}







		if (fadeImage.color.a > 0 && VaporWave == false) {
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - 0.01f);
		}


		if (Input.GetKey (KeyCode.Z) && VaporWave  == false) {
		transform.position += new Vector3 (0, 0, 1f*multiplicateurvitesse * ralenti);
		}


		if (Input.GetKey (KeyCode.Q) && VaporWave  == false) {
			VoitureFBX.transform.Rotate (0, 2.5f, 0);
			lachergauche = false;
			transform.position += new Vector3 (-1f*ralenti, 0, 0);
		}
		if (Input.GetKey (KeyCode.D)&& VaporWave  == false) {
			VoitureFBX.transform.Rotate (0, -2.5f, 0);
			lacherdroit = false;
			transform.position += new Vector3 (1f*ralenti, 0, 0);
		}


		if (Input.GetKeyUp (KeyCode.D)&& VaporWave  == false) {
			lacherdroit = true;
		}
		if (Input.GetKeyUp (KeyCode.Q)&& VaporWave  == false) {
			lachergauche = true;
		}



		if (lacherdroit == true && VaporWave == false) {
			VoitureFBX.transform.Rotate (0, -VoitureFBX.transform.rotation.y*6, 0);
		}
		if (lachergauche == true && VaporWave  == false) {
			VoitureFBX.transform.Rotate (0, -VoitureFBX.transform.rotation.y*6, 0);
		}

	}


	void OnTriggerEnter (Collider Col) {
		if (Col.tag == "Phase2") {
			Drifting = false;
			multiplicateurvitesse = 1.5f;

		}

		if (Col.tag == "Phase3") {
			VaporWave = true;
			multiplicateurvitesse = 1;
			ralenti = 0.5f;


		}

		if (Col.tag == "end") {
			ending = true;
		}
	}
}
