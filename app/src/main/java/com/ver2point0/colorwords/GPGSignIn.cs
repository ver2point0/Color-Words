using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GPGSignIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate ();
		Social.localUser.Authenticate ((bool access) => {
			//Debug.Log("Success signing in => " + access);
		});
	}
}
