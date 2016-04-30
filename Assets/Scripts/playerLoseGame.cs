using UnityEngine;
using System.Collections;

public class PlayerLoseGame : MonoBehaviour {

	public GameObject monsters;
    public GameController gameController;

	void OnTriggerStay(Collider other){
		//Make player lose!
		if (other.gameObject.transform.parent.gameObject.name == monsters.name) {
            gameController.GameOver();
		}
	}

}
