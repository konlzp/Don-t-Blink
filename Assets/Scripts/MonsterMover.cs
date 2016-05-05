using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterMover : MonoBehaviour
{
    public enum MonsterState
    {
        Moving,
        Stunned,
        Paused,
    };

    private MonsterState state = MonsterState.Paused;

    // Waypoint
    public GameObject path;
    public float defaultSpeed;
    private float speed;
    private List<Transform> waypoints = new List<Transform> ();
    private int numWaypoints;
    private int waypointIndex = 0;
    private Transform nextWaypoint;
    private float rspeed = 10.0F;

    // Flashlight
    public GameObject spotlight;
    private bool collided = false;
    private LightControl lightControl;
    private const float STUN_DURATION = 5.00F;
    private float stunTime = 0.0F;
    private float pauseTime = 0.0F;

    // Animation
    private Animator animator = null;
    private new Animation animation = null;
    private bool animationOn = true;

    // Mainlight
    public Light mainLight;

    // Game Controller
    public GameController gameController;

    void Start ()
    {
        // Get components
        lightControl = spotlight.GetComponent<LightControl> ();
        animator = GetComponent<Animator> ();
        animation = GetComponent<Animation> ();

        // Stop animation 
         SetAnimationOff ();

        // Set initial speed
        speed = defaultSpeed;

        // Set up waypoints list
        foreach (Transform child in path.transform) {
            waypoints.Add (child);
        }
        numWaypoints = waypoints.Count;

        // Start moving to first waypoint
        StartMoveToNextWaypoint ();
    }

    void Update ()
    {
        if (!gameController.IsGameActive) {
            return;
        }

        switch (state) {
        case MonsterState.Moving:
            HandleMovingState ();
            break;
        case MonsterState.Stunned:
            HandleStunnedState ();
            break;
        case MonsterState.Paused:
            HandlePausedState ();
            break;
        }
    }

    private void HandleMovingState ()
    {
        // Go to Stunned state if affected by flashlight or mainlight
        if (IsAffectedByFlashlight () || IsMainlightOn ()) {
            SetAnimationOff ();
            state = MonsterState.Stunned;
        } else {
            if (transform.position == nextWaypoint.position) { // Arrived at waypoint, need to move to next
                StartMoveToNextWaypoint ();
            } else {
                // Move object towards waypoint
                transform.position = Vector3.MoveTowards (transform.position, nextWaypoint.position, speed);

                // Face the waypoint
                Vector3 targetDir = nextWaypoint.position - transform.position;
                float step = rspeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
                Debug.DrawRay (transform.position, newDir, Color.red);
                transform.rotation = Quaternion.LookRotation (newDir);
            }
        }
    }

    private void HandleStunnedState ()
    {
        // Decrease stun if currently stunned and not affected by light
        if (!IsAffectedByFlashlight () && !IsMainlightOn () && gameController.IsGameActive) { // Decrease stun
            stunTime -= Time.deltaTime;
        } else if (IsAffectedByFlashlight () || IsMainlightOn ()) { // Reset stun if light is on monster
            if (stunTime < STUN_DURATION) {
                stunTime = STUN_DURATION;
            }
        }

        // Set state to Moving if stun time runs out
        if (stunTime <= 0.0F) {
            SetAnimationOn ();
            state = MonsterState.Moving;
        }
    }

    private void HandlePausedState ()
    {
        if (IsAffectedByFlashlight() || IsMainlightOn())
        {
            SetAnimationOff();
            state = MonsterState.Stunned;
        }
        else {
            pauseTime -= Time.deltaTime;
            SetAnimationOn();

            // Set state to Moving if pause timer runs out
            if (pauseTime <= 0.0F)
            {
                state = MonsterState.Moving;
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "flashlight") {
            //Debug.Log (other.gameObject.name + " entered " + gameObject.name + "'s trigger");
            collided = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "flashlight") {
            //Debug.Log (other.gameObject.name + " exited " + gameObject.name + "'s trigger");
            collided = false;
        }
    }

    // Sets the next waypoint
    private void StartMoveToNextWaypoint ()
    {
        if (waypointIndex < numWaypoints) {
            nextWaypoint = waypoints [waypointIndex];
            WaypointSetting ws = nextWaypoint.gameObject.GetComponent<WaypointSetting> ();
            if (ws != null) {
                SetSpeedToWaypointSpeed (ws);
                SetPauseToWaypointPause (ws);
            }

            waypointIndex++;
        }
    }

    // Change to Paused state if there is a pause time
    private void SetPauseToWaypointPause (WaypointSetting ws)
    {
        pauseTime = ws.pauseTime;
        if (pauseTime > 0.0F) {
            SetAnimationOff ();
            state = MonsterState.Paused;
        }
    }

    // Sets speed of monster to waypoint's speed if it is > 0, otherwise set it to default speed
    private void SetSpeedToWaypointSpeed (WaypointSetting ws)
    {
        float waypointSpeed = ws.speed;
        if (waypointSpeed > 0.0F) {
            speed = waypointSpeed;
        } else {
            speed = defaultSpeed;
        }
    }

    private void SetAnimationOn ()
    {
        if (!animationOn) { // Turn on
            Debug.Log ("Starting animation for " + gameObject.name);
            if (animator != null) {
                animator.StartPlayback ();
            } else if (animation != null) {
                animation.Play ();
            }
            animationOn = true;
        }
    }

    private void SetAnimationOff ()
    {
        if (animationOn) { // Turn off
            Debug.Log ("Stopping animation for " + gameObject.name);
            if (animator != null) {
                animator.Stop ();
            } else if (animation != null) {
                animation.Stop ();
            }
            animationOn = false;
        }
    }

    private bool IsMainlightOn ()
    {
        return mainLight.intensity != 0;
    }

    private bool IsAffectedByFlashlight ()
    {
        return collided && lightControl.IsLightOn;
    }

    public MonsterState State {
        get { return state; }
    }
}
