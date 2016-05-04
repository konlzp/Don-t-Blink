using UnityEngine;
using System.Collections;
using UnityTest;

public class TestFlashlight : MonoBehaviour
{
    public GameObject spotlight;
    public GameObject mainCamera;
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

            // Rotate away and turn on light
            MoveCamera (true);
            lightControl.ImmediatelyTurnOnLight ();
//            monster.transform.hasChanged = false;
        }

        else if (frame == 60 * 1) {
            // Make sure monster is still moving
            IntegrationTest.Assert (monsterMover.State == MonsterMover.MonsterState.Moving, "Failed on light on but not collided");
//            IntegrationTest.Assert (monster.transform.hasChanged == true);
//            monster.transform.hasChanged = false;

            // Turn off light and turn on collider
            MoveCamera (false);
            lightControl.ImmediatelyTurnOffLight ();
        }
       
        else if (frame == 60 * 2) {
            // Make sure monster is still moving
            IntegrationTest.Assert (monsterMover.State == MonsterMover.MonsterState.Moving, "Failed on collided but light off");
//            IntegrationTest.Assert (monster.transform.hasChanged == true);
//            monster.transform.hasChanged = false;

            // Turn on light
            lightControl.ImmediatelyTurnOnLight ();
        }

        else if (frame == 60 * 3) {
            // Make sure monster is stunned
            IntegrationTest.Assert (monsterMover.State == MonsterMover.MonsterState.Stunned, "Failed on light on and collided");
//            IntegrationTest.Assert (monster.transform.hasChanged == false);
            IntegrationTest.Pass ();
        }

        frame++;
    }

    private void MoveCamera (bool away)
    {
        if (away) {
            Debug.Log ("Moved camera away");
            mainCamera.transform.Translate (0, 20, 0);
        } else {
            Debug.Log ("Moved camera back");
            mainCamera.transform.Translate (0, -20, 0);
        }
       
    }
}
