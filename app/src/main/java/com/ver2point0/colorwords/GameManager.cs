using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour {
	public GameObject summary;
	public Text scoreSummaryLabel;
	public Text highscoreSummaryLabel;

	public AudioSource audioSource;
	public AudioClip gameOverSound;
	public float gameOverDelay;

	public GameObject pauseScreen;
	public Button pauseButton;

	public Text titleText;

	public Sprite liveFullSprite;
	public Sprite liveEmptySprite;
	public int lives;
	public Image[] livesImageArray;
	public Text scoreText;
	public Text timeText;
	
	private int score;
	private float remainingTime;

	private bool gamePaused = false;
	private bool gameRunning = true;

	private AudioSource backgroundMusic;

	// Use this for initialization
	void Start () {
		backgroundMusic = GameObject.Find ("Background Music").GetComponent<AudioSource> ();
		score = 0;

		/*Checks the difficulty selected by the user and sets the game time accordingly*/
		if (GameHandler.difficulty == 0)
			remainingTime = 30.0f;
		else if (GameHandler.difficulty == 1)
			remainingTime = 20.0f;
		else if (GameHandler.difficulty == 2)
			remainingTime = 10.0f;

		/*Changes the Title label based on the game mode the user selected*/
		if (GameHandler.mode == GameHandler.FILL_MODE) {
			titleText.text = "FILL";
		} else if (GameHandler.mode == GameHandler.SPELL_MODE) {
			titleText.text = "SPELL";
		}

	}

	/*This callback is called by the Pause Button*/
	public void togglePause() {
		gamePaused = !gamePaused;
		if (gamePaused) {
			backgroundMusic.Pause();
		} else {
			backgroundMusic.Play();
		}

		pauseButton.gameObject.SetActive(!gamePaused);
		pauseScreen.SetActive (gamePaused);
	}

	// Update is called once per frame
	void Update () {
		if (gameRunning && !gamePaused) {
			remainingTime -= Time.deltaTime;
			UpdateTimeText();

			if (remainingTime <= 0.0f) {
				remainingTime = 0.0f;
				OnGameOver();
			}
		}
	}

	public void AddToScore(int add) {
		score += add;
		updateScoreText ();
	}

	public void updateScoreText() {
		scoreText.text = "Score: " + score.ToString ();
	}

	public void AddToLive(int add) {
		lives += add;
		UpdateLivesText ();

		if (lives <= 0) {
			OnGameOver();
		}
	}

	public void AddToTime(float timeAdd) {
		remainingTime += timeAdd;

		remainingTime = Mathf.Max (remainingTime, 0);
	}

	public void UpdateLivesText() {
		int livesLost = livesImageArray.Length - lives;

		//Debug.Log (livesLost);
		for (int i = 0; i < lives; i++) {
			livesImageArray[i].sprite = liveFullSprite;
		}

		for (int i = livesImageArray.Length - livesLost; i < livesImageArray.Length; i++) {
			livesImageArray[i].sprite = liveEmptySprite;
		}
	}

	public void UpdateTimeText() {
		timeText.text =  ((int)remainingTime).ToString();
	}

	public void OnGameOver() {
		gameRunning = false;
		pauseButton.gameObject.SetActive (false);

		StartCoroutine (showGameOverSummary ());
	}

	private IEnumerator showGameOverSummary(){
		yield return new WaitForSeconds (gameOverDelay);

		audioSource.clip = gameOverSound;
		audioSource.Play ();


		string highScoreKey = GameHandler.mode == GameHandler.FILL_MODE ?
			GameConstants.HIGHSCORE_FILL : GameConstants.HIGHSCORE_SPELL;

		highScoreKey += GameHandler.difficulty.ToString ();

		int savedHighScore = PlayerPrefs.GetInt (highScoreKey, 0);

		if (savedHighScore < score) {
			savedHighScore = score;
			PlayerPrefs.SetInt(highScoreKey, savedHighScore);
			PlayerPrefs.Save();
		}

		string leaderboardId;
		//The highscoreKey string is composed by one of the GameConstants strings for each mode
		//plus the difficulty constant that we are running on(0,1 or 2)
		switch (highScoreKey) {
		case "HIGHSCORE_FILL0":
			leaderboardId = GameConstants.FILL_EASY_LEADERBOARD;
			break;
		case "HIGHSCORE_FILL1":
			leaderboardId = GameConstants.FILL_MED_LEADERBOARD;
			break;
		case "HIGHSCORE_FILL2":
			leaderboardId = GameConstants.FILL_HARD_LEADERBOARD;
			break;
		case "HIGHSCORE_SPELL0":
			leaderboardId = GameConstants.SPELL_EASY_LEADERBOARD;
			break;
		case "HIGHSCORE_SPELL1":
			leaderboardId = GameConstants.SPELL_MED_LEADERBOARD;
			break;
		case "HIGHSCORE_SPELL2":
			leaderboardId = GameConstants.SPELL_HARD_LEADERBOARD;
			break;
		default:
			leaderboardId = GameConstants.SPELL_HARD_LEADERBOARD;
			break;
		}

		Social.ReportScore (score, leaderboardId, (bool success) => {
			//Handle success or failure

		});

		scoreSummaryLabel.text = "Score: " + score;
		highscoreSummaryLabel.text = "Highscore\n" + savedHighScore;
		summary.SetActive (true);
	}

	public bool isGameRunning()  {
		return gameRunning;
	}

	public bool isGamePaused() {
		return gamePaused;
	}
}
