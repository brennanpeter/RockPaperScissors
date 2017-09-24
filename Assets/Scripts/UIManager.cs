using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	GameObject HUD;
	GameObject ClassSelect;
	bool HUDOpen;

	void Start()
	{
		HUDOpen = false;
		DisplayClassSelect ();
	}

	public void DisplayHUD(PlayerClass pclass)
	{
		HUD = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/PlayerHUD"));
		HUDOpen = true;

		//TODO: Load in class image.
	}

	public void DisplayClassSelect()
	{
		if (HUDOpen) {
			Destroy (GameObject.Find ("PlayerHUD"));
			HUDOpen = false;
		}

		ClassSelect = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/ClassSelect"));
		ClassSelect.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, false);
	}
}
