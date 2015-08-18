using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class SummaryButtons : MonoBehaviour {
	public AudioSource buttonAudio;

	void Start() {
		Chartboost.cacheInterstitial (CBLocation.Default);
	}

	public void onReplayClick() {
		if (Chartboost.hasInterstitial (CBLocation.Default)) {
			showAds ();
		} else {
			Application.LoadLevel ("MainScene");
		}
	}

	public void onMenuClick() {
		Destroy (GameObject.Find ("Background Music"));
		Application.LoadLevel ("MenuScene");
	}


	public void showAds() {
		Chartboost.didCloseInterstitial += onAdClosed;
		Chartboost.showInterstitial (CBLocation.Default);
	}

	public void onAdClosed(CBLocation location) {
		Chartboost.didCloseInterstitial -= onAdClosed;
		Application.LoadLevel ("MainScene");
	}
}
