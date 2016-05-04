using UnityEngine;
using System.Collections;
using UnityTest;

public class TestLightsOff : MonoBehaviour
{
    public Light mainLight;
    public GameObject spotlight;
    public MonsterMover monsterMover;
    public GameController gameController;

    private LightControl lightControl;
    private GameObject monster;
    private int frame = 0;

    void Start ()
    {
        monster = monsterMover.gameObject;
        lightControl = spotlight.GetComponent<LightControl> ();
    }
	
    void Update ()
    {
        if (frame == 1) {
            gameController.GameStart ();
            mainLight.intensity = 0.0f;
            lightControl.ImmediatelyTurnOffLight ();
//            monster.transform.hasChanged = false;
        } else if (frame == 60) {
            IntegrationTest.Assert (monsterMover.State == MonsterMover.MonsterState.Moving);
//            IntegrationTest.Assert (monster.transform.hasChanged == true);
            IntegrationTest.Pass ();
        }
        frame++;
    }
}
