using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text displayText;
	public Text winText;
    public GameController gameController;

	private bool gameOver = false;

	//want six minutes (12AM - 6AM)
	float countTime;

	// Use this for initialization
	void Start () {
		winText.text = "";
		countTime = 360;
		setTime();
        displayText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (countTime > 0) {
			countTime -= Time.deltaTime;
		}
		setTime();
	}


	// update the display of timer and win text
	void setTime(){
		if (countTime <= 0 && gameOver == false) {
            gameController.GameWon();
			countTime = 0;
			winText.text = "YOU SURVIVED THE NIGHT";
		}
	}

	public void zeroTime(){
		countTime = 0;
		gameOver = true;
		setTime ();
	}

}
