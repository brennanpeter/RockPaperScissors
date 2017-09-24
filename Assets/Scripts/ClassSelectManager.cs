using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelectManager : MonoBehaviour
{
	PlayerClass SelectedClass = PlayerClass.NULL;
	Button PlayButton;

	void Start ()
	{
		PlayButton = GameObject.Find ("PlayButton").GetComponent<Button> ();
		//TODO: Set image to blank
	}

	void Update ()
	{
		if (SelectedClass != PlayerClass.NULL) {
			PlayButton.interactable = true;
		} else {
			PlayButton.interactable = false;
		}
	}

	public void SelectClass (int selectedClass)
	{
		//TODO: Change class image
		SelectedClass = (PlayerClass)selectedClass;
		Debug.Log ("Class Selected: " + SelectedClass.ToString ());
	}

	public void SelectRandomClass ()
	{
		PlayerClass prevClass = SelectedClass;
		do {
			SelectedClass = (PlayerClass)Random.Range (0, 3);
		} while(SelectedClass == prevClass);

		Debug.Log ("Class Selected: " + SelectedClass.ToString ());
	}

	public void Spawn ()
	{
		Destroy (GameObject.Find ("ClassSelect"));
        GameObject.Find("_Scripts").GetComponent<NetworkManager>().SpawnMyPlayer((int)SelectedClass);

        Destroy(this.gameObject);
	}
}
