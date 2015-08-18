using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {

	public Sprite soundOn;
	public Sprite soundOff;
	private Image childImage;

	public bool sound;

	// Use this for initialization
	void Start () {
		sound = PlayerPrefs.GetInt ("SOUND_ON", 1) != 0;
		childImage = transform.FindChild ("Image").GetComponent<Image> ();

		if (!sound) {
			//Turn off the game sound
			childImage.sprite = soundOff;
		} else {
			childImage.sprite = soundOn;
		}

		AudioListener.pause = !sound;

	}
	
	public void toggleSound() {
		sound = !sound;
		PlayerPrefs.SetInt ("SOUND_ON", sound ? 1 : 0);
		PlayerPrefs.Save ();

		if (!sound) {
			//Turn off the game sound
			childImage.sprite = soundOff;
		} else {
			childImage.sprite = soundOn;
		}
		AudioListener.pause = !sound;
	}
}
