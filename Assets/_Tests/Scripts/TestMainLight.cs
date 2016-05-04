using UnityEngine;
using System.Collections;
using UnityTest;

public class TestMainLight : MonoBehaviour
{
    public Light mainLight;
    public MonsterMover monsterMover;
    public GameController gameController;

    private GameObject monster;
    private int frame = 0;

    // Use this for initialization
    void Start ()
    {
        monster = monsterMover.gameObject;
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (frame == 1) {
            gameController.GameStart ();
            mainLight.intensity = 0.2f;
            monster.transform.hasChanged = false;
        } else if (frame == 30) {
            IntegrationTest.Assert (monsterMover.State == MonsterMover.MonsterState.Stunned);
            IntegrationTest.Assert (monster.transform.hasChanged == false);
            IntegrationTest.Pass ();
        }
        frame++;
    }
}
