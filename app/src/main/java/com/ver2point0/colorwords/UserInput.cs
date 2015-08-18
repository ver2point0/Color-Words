using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UserInput : MonoBehaviour {

	private int rightAnswersBonusTime; //The bonus time the user gets when he gets *rightAnswerBonus* answers in a row
	public int rightAnswersBonus; //The right answers the user needs to get the bonus
	private int rightAnswersSoFar; //The right answers in a row the user has

	public AudioSource audioSource;
	public AudioClip rightSound;
	public AudioClip wrongSound;

	public int pointsPerAnswer;
	public int timePenalty;
	public GameManager gameManager;

	public Text answerText;


	public Button button0;
	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;
	public Button button5;
	public Button button6;
	public Button button7;
	public Button button8;
	public Button button9;

	private int fillColorButton0;
	private int fillColorButton1;
	private int fillColorButton2;
	private int fillColorButton3;
	private int fillColorButton4;
	private int fillColorButton5;
	private int fillColorButton6;
	private int fillColorButton7;
	private int fillColorButton8;
	private int fillColorButton9;

	private int nameColorButton0;
	private int nameColorButton1;
	private int nameColorButton2;
	private int nameColorButton3;
	private int nameColorButton4;
	private int nameColorButton5;
	private int nameColorButton6;
	private int nameColorButton7;
	private int nameColorButton8;
	private int nameColorButton9;
	

	//The color names pool that will appear on the buttons
	public string[] colorNames = {"Red", "Green", "Blue", 
		"Pink", "Yellow", "White", "Indigo", "Black", "Orange", "Purple"};

	//The color pool that will be used as background for the buttons
	public Color[] backgroundColors = {Color.red, Color.green, Color.blue,
		new Color(233, 30, 99), Color.yellow, Color.white, new Color(63, 81, 181),
		Color.black, new Color(255, 152, 0), new Color(156, 39, 176)};

	//The color pool for the text on the buttons
	public Color[] textColors = new Color[10];

	//This variable can hold the index of the backgroundColors array that the user must match
	private int rightAnswer;

	//This holds the game mode we are currently on (GameHandler.FILL_MODE or GameHandler.SPELL_MODE)
	private int gameMode;

	// Use this for initialization
	void Start () {
		rightAnswersSoFar = 0;
		gameMode = GameHandler.mode;
		if (GameHandler.difficulty == 0) {
			rightAnswersBonusTime = 6;
			timePenalty = 3;
		} else if (GameHandler.difficulty == 1) {
			rightAnswersBonusTime = 4;
			timePenalty = 2;
		} else if (GameHandler.difficulty == 2) {
			rightAnswersBonusTime = 2;
			timePenalty = 1;
		}
		assignAnswers ();
	}
	
	// Update is called once per frame
	void Update () {
		onGamePause (gameManager.isGamePaused ());
	}

	private void onGamePause(bool paused) {
		answerText.enabled = !paused;

		//Disables or enables the buttons based on the paused variable; false = disabled
		button0.gameObject.SetActive (!paused);
		button1.gameObject.SetActive (!paused);
		button2.gameObject.SetActive (!paused);
		button3.gameObject.SetActive (!paused);
		button4.gameObject.SetActive (!paused);
		button5.gameObject.SetActive (!paused);
		button6.gameObject.SetActive (!paused);
		button7.gameObject.SetActive (!paused);
		button8.gameObject.SetActive (!paused);
		button9.gameObject.SetActive (!paused);
	}

	private void assignAnswers() {
		//We get a random number in the range of 0 to the length of the colors pool

		rightAnswer = Random.Range (0, backgroundColors.Length);
		if (gameMode == GameHandler.SPELL_MODE) {
			/*If we are on spell mode the rightAnswer holds the color name  index that we
			 will use to name the current color label*/
			answerText.color = backgroundColors [Random.Range (0, backgroundColors.Length)];
			answerText.text = colorNames [rightAnswer];
		} else if (gameMode == GameHandler.FILL_MODE) {
			/*If we are on fill mode the rightAnswer holds the color background index that we
			 will use to coloring current color label*/
			answerText.color = backgroundColors [rightAnswer];
			answerText.text = colorNames [Random.Range(0, colorNames.Length)];
		}

		/*
		 * We use fillColorList and textColorList as pools where we can get random indexes from,
		 * so we don't get repeat color names or backgrounds for the buttons, everytime we assign a
		 * name and a color to a button we remove those indexes from the lists
		 */
		List<int> fillColorsList = new List<int> ();
		List<int> textColorList = new List<int>();
		for (int i = 0; i < 10; i++) {
			fillColorsList.Add(i);
			textColorList.Add(i);
		}

		int random = Random.Range (0, fillColorsList.Count);
		fillColorButton0 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton0);
		nameColorButton0 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton1 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton1);
		nameColorButton1 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton2 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton2);
		nameColorButton2 = textColorList [random];
		textColorList.RemoveAt (random);


		random = Random.Range (0, fillColorsList.Count);
		fillColorButton3 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton3);
		nameColorButton3 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton4 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton4);
		nameColorButton4 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton5 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton5);
		nameColorButton5 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton6 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton6);
		nameColorButton6 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton7 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton7);
		nameColorButton7 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton8 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton8);
		nameColorButton8 = textColorList [random];
		textColorList.RemoveAt (random);

		random = Random.Range (0, fillColorsList.Count);
		fillColorButton9 = fillColorsList [random];
		fillColorsList.RemoveAt (random);

		random = getDifferentRandom (0, textColorList.Count, fillColorButton9);
		nameColorButton9 = textColorList [random];
		textColorList.RemoveAt (random);
		ColorizeButtons ();
	}

	public void ColorizeButtons () {
		/*We use the random variables that we got in assignAnswers() to 
		 set the text and background colors of the buttons*/
		button0.image.color = backgroundColors [fillColorButton0];
		colorizeText (button0, fillColorButton0, nameColorButton0);

		button1.image.color = backgroundColors [fillColorButton1];
		colorizeText (button1, fillColorButton1, nameColorButton1);

		button2.image.color = backgroundColors [fillColorButton2];
		colorizeText (button2, fillColorButton2, nameColorButton2);

		button3.image.color = backgroundColors [fillColorButton3];
		colorizeText (button3, fillColorButton3, nameColorButton3);

		button4.image.color = backgroundColors [fillColorButton4];
		colorizeText (button4, fillColorButton4, nameColorButton4);

		button5.image.color = backgroundColors [fillColorButton5];
		colorizeText (button5, fillColorButton5, nameColorButton5);

		button6.image.color = backgroundColors [fillColorButton6];
		colorizeText (button6, fillColorButton6, nameColorButton6);

		button7.image.color = backgroundColors [fillColorButton7];
		colorizeText (button7, fillColorButton7, nameColorButton7);

		button8.image.color = backgroundColors [fillColorButton8];
		colorizeText (button8, fillColorButton8, nameColorButton8);

		button9.image.color = backgroundColors [fillColorButton9];
		colorizeText (button9, fillColorButton9, nameColorButton9);
	}

	public void colorizeText(Button b, int fillColorIndex, int colorNameIndex) {
		Text t = b.transform.FindChild ("Text").GetComponent<Text> ();
		t.color = textColors [fillColorIndex];
		t.text = colorNames [colorNameIndex];
	}

	/*This is the callback that every button in the game calls, passing its own id. We 
	 use that id to compare the answer the button holds against the rightAnswer variable*/
	public void OnButtonClick(int buttonId) {

		if (!gameManager.isGameRunning ()) {
			return;
		}

		switch (buttonId) {
		case 0:
			checkAnswer(fillColorButton0);
			break;

		case 1:
			checkAnswer(fillColorButton1);
			break;

		case 2:
			checkAnswer(fillColorButton2);
			break;

		case 3:
			checkAnswer(fillColorButton3);
			break;

		case 4:
			checkAnswer(fillColorButton4);
			break;
		case 5:
			checkAnswer(fillColorButton5);
			break;
		case 6:
			checkAnswer(fillColorButton6);
			break;
		case 7:
			checkAnswer(fillColorButton7);
			break;
		case 8:
			checkAnswer(fillColorButton8);
			break;

		case 9:
			checkAnswer(fillColorButton9);
			break;

		default:
			break;
		}
	}

	public void checkAnswer(int userAnswer) {
		/*This method checks the answer provided by the user against the rightAnswer variable,
		 if it's the same it adds points to the score and increments the rightAnswersSoFar variable. If it's
		 not the same, we penalize the user with some time loss and take one life from him*/
		if (userAnswer == rightAnswer) {

			gameManager.AddToScore (pointsPerAnswer);

			rightAnswersSoFar++;
			if (rightAnswersSoFar == rightAnswersBonus) {
				gameManager.AddToTime(rightAnswersBonusTime);
			}
			rightAnswersSoFar = rightAnswersSoFar % rightAnswersBonus;

			audioSource.clip = rightSound;
		} else {
			rightAnswersSoFar = 0;
			gameManager.AddToLive(-1);
			gameManager.AddToTime(-timePenalty);
			audioSource.clip = wrongSound;
		}

		audioSource.Play ();
		assignAnswers ();
	}

	public int getDifferentRandom(int from, int to, int diff) {
		int random = Random.Range (from, to);
		return random;
	}
}
