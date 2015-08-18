using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

/*Callbacks called by different buttons in the menu and difficulty selection screens*/
public class UserSelection : MonoBehaviour {
	public AudioSource buttonAudio;

	public void onLeaderboardsClick() {
		Social.Active.ShowLeaderboardUI ();

		buttonAudio.Play();
	}

	public void onTutorialClick() {
		Application.LoadLevel ("TutorialScene");
		buttonAudio.Play ();
	}

	public void onFillClick() {
		GameHandler.mode = GameHandler.FILL_MODE;
		//Load the difficulty scene
		Application.LoadLevel ("DifficultyScene");
		buttonAudio.Play ();
	}

	public void onSpellClick() {
		GameHandler.mode = GameHandler.SPELL_MODE;
		//Load the difficulty scene
		Application.LoadLevel ("DifficultyScene");
		buttonAudio.Play ();
	}

	public void onEasyClick() {
		GameHandler.difficulty = 0;
		Application.LoadLevel ("MainScene");
		buttonAudio.Play ();
	}

	public void onMediumClick() {
		GameHandler.difficulty = 1;
		Application.LoadLevel ("MainScene");
		buttonAudio.Play ();
	}

	public void onHardClick() {
		GameHandler.difficulty = 2;
		Application.LoadLevel ("MainScene");
		buttonAudio.Play ();
	}
}
