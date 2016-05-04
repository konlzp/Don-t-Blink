using UnityEngine;
using System.Collections;
using UnityTest;

public class TestLightOn : MonoBehaviour {
	public Light mainLight;
	public MonsterMover monsterMover;
	public GameController gameController;
	private GameObject monster;

	private int frames = 0;

	// Use this for initialization
	void Start () {
		monster = monsterMover.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (frames == 1) {
			gameController.GameStart ();
			mainLight.intensity = 0.2f;
		} else if (frames == 30) {
			IntegrationTest.Assert (monsterMover.State == MonsterMover.MonsterState.Stunned);
			IntegrationTest.Pass ();
		}
		frames++;
	}
}
