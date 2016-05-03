using UnityEngine;
using System.Collections;
using UnityTest;

public class TestLightOn : MonoBehaviour {
	public Light mainlight;
	public MonsterMover monster;

	// Use this for initialization
	void Start () {
		mainlight.intensity = 0;
		monster.State = MonsterMover.MonsterState.Paused;
	}
	
	// Update is called once per frame
	void Update () {

		//Test when mainlight is on;
		monster.State = MonsterMover.MonsterState.Moving;
		mainlight.intensity = 1;

		if (monster.State != MonsterMover.MonsterState.Stunned) {
			IntegrationTest.Fail ();
		} 
		else {
			IntegrationTest.Pass ();
		}
	}
}
