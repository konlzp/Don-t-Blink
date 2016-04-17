using UnityEngine;
using System.Collections;

public class flashLightCollider : MonoBehaviour {

    public GameObject monsters;
    public GameObject flashlight;

    private LightControl lightController;
    private MonsterController monsterController;

    void Start()
    {
        lightController = flashlight.GetComponent<LightControl>();
        monsterController = monsters.GetComponent<MonsterController>();
    }

	void OnTriggerStay(Collider other){
        //Make the monsters freeze!
        if (other.gameObject.transform.parent.gameObject.name == monsters.name && lightController.getLightStatus())
        {
            monsterController.makeItScream();
            Debug.Log(other.gameObject);
            Debug.Log("hello");
        }
	}
}
