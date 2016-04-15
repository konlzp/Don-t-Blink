using UnityEngine;
using System.Collections;

public class flashLightCollider : MonoBehaviour {

	void OnTriggerStay(Collider other){
		//Make the monsters freeze!

		Debug.Log (other.gameObject);
		Debug.Log ("hello");
	}
}
