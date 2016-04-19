using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text displayText;
	public Text winText;

	private bool gameOver = false;

	//want six minutes (12AM - 6AM)
	float countTime;

	// Use this for initialization
	void Start () {
		Debug.Log ("Hello world!");
		winText.text = "";
		countTime = 360;
		setTime();

	
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
		displayText.text = "Seconds left: " + countTime.ToString();
		if (countTime <= 0 && gameOver == false) {
			//winning state
			countTime = 0;
			displayText.text = "Seconds left: " + countTime.ToString();
			winText.text = "YOU SURVIVED THE NIGHT";
		}
	}

	public void zeroTime(){
		countTime = 0;
		gameOver = true;
		setTime ();
	}

}
