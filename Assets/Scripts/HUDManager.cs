using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	Image UIClassImage;
	Slider UIHealthbar;
	Slider UISecondaryBar;

	void Start()
	{
		UIClassImage = GameObject.Find ("ClassImage").GetComponent<Image> ();
		UIHealthbar = GameObject.Find("Healthbar").GetComponent<Slider>();
		UISecondaryBar = GameObject.Find("Secondary Bar").GetComponent<Slider>();
	}

	public void UpdatePortrait(Sprite image)
	{
		UIClassImage.sprite = image;
	}

	public void UpdateHealth(float percent)
	{
		UIHealthbar.value = percent;
	}

	public void UpdateSecondary (float percent)
	{
		UISecondaryBar.value = percent;
	}
}
