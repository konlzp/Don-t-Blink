using UnityEngine;
using System.Collections;

public class PlayerLoseGame : MonoBehaviour {

    public GameObject monsters;
    public GameController gameController;

    private MonsterController monsterController;
    private Light flashLight;

    void Start()
    {
        monsterController = monsters.GetComponent<MonsterController>();
        flashLight = GetComponent<Light>();
    }

	void OnTriggerStay(Collider other){
		//Make player lose!
		if (other.gameObject.transform.parent.gameObject.name == monsters.name) {
            gameController.GameOver();
            monsterController.makeItScream();
            flashLight.intensity = 2;
		}
	}

}
