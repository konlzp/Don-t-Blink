using UnityEngine;
using System.Collections;

public class flashLightCollider : MonoBehaviour {

    public GameObject monsters;

	void OnTriggerStay(Collider other){
        //Make the monsters freeze!
        if (other.gameObject.transform.parent.gameObject.name == monsters.name)
        {
            Debug.Log(other.gameObject);
            Debug.Log("hello");
        }
	}
}
