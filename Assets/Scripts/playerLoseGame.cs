using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerLoseGame : MonoBehaviour {

	public GameObject monsters;
	public Text winText;
	public Timer timer;

	// Use this for initialization
	void Start () {
		Debug.Log ("game start, player alive");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		//Make player lose!
//		Debug.Log (other.gameObject.name);
		if (other.gameObject.transform.parent.gameObject.name == monsters.name) {
			//Debug.Log ("player loses");
			winText.text = "GAME OVER";
			timer.zeroTime ();
		}
	}

}
