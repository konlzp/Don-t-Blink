using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerLoseGame : MonoBehaviour {

	public GameObject monsters;
    public GameController gameController;

	void OnTriggerStay(Collider other){
		//Make player lose!
//		Debug.Log (other.gameObject.name);
		if (other.gameObject.transform.parent.gameObject.name == monsters.name) {
            gameController.GameOver();
		}
	}

}
