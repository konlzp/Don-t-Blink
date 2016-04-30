using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterMover : MonoBehaviour
{
    // Waypoint
    public GameObject path;
    public float speed;
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

    // Animation
    private Animator animator = null;
    // Underscore prevents naming warning
    private Animation animation_ = null;
    private bool animationOn = false;

    // Mainlight
    public Light mainLight;

    void Start ()
    {
        // Get components
        lightControl = spotlight.GetComponent<LightControl> ();
        animator = GetComponent<Animator> ();
        animation_ = GetComponent<Animation> ();

        // Start animation
        SetAnimationOn ();
    
        // Set up waypoints list
        foreach (Transform child in path.transform) {
            waypoints.Add (child);
        }
        numWaypoints = waypoints.Count;

        // Start moving to first waypoint
        StartMoveToNextWaypoint ();
    }

    void FixedUpdate ()
    {
        // Move if not frozen and light is OFF
        //     | ON   OFF
        // ----+----------
        //  IN | STOP MOVE
        // OUT | MOVE MOVE
        if (!IsAffectedByFlashlight () && !IsStunned () && !IsMainlightOn ()) { // Move
            if (transform.position == nextWaypoint.position) { // Arrived at waypoint, need to move to next 
                StartMoveToNextWaypoint ();
            } else {
                // Move object towards
                transform.position = Vector3.MoveTowards (transform.position, nextWaypoint.position, speed);

                // Face the waypoint
                Vector3 targetDir = nextWaypoint.position - transform.position;
                float step = rspeed * Time.fixedDeltaTime;
                Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
                Debug.DrawRay (transform.position, newDir, Color.red);
                transform.rotation = Quaternion.LookRotation (newDir);
            }
        }
    }

    void Update ()
    {
        // Decrease stun if currentntly stunned and not affected by light
        if (IsStunned () && !IsAffectedByFlashlight () && !IsMainlightOn ()) { // Decrease stun
            stunTime -= Time.deltaTime;
            SetAnimationOn ();
        } else if (IsAffectedByFlashlight () || IsMainlightOn ()) { // Reset stun if light is on monster
            stunTime = STUN_DURATION;
            SetAnimationOff ();
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "flashlight") {
            Debug.Log (other.gameObject.name + " entered " + gameObject.name + "'s trigger");
            collided = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "flashlight") {
            Debug.Log (other.gameObject.name + " exited " + gameObject.name + "'s trigger");
            collided = false;
        }
    }

    // Sets the next waypoint
    private void StartMoveToNextWaypoint ()
    {
        if (waypointIndex < numWaypoints) {
            nextWaypoint = waypoints [waypointIndex];
            waypointIndex++;
        }
    }

    private void SetAnimationOn ()
    {
        if (!animationOn) { // Turn on
            Debug.Log ("Starting animation for " + gameObject.name);
            if (animator != null) {
                animator.StartPlayback ();
            } else if (animation_ != null) {
                animation_.Play ();
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
            } else if (animation_ != null) {
                animation_.Stop ();
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
        return collided && lightControl.getLightStatus ();
    }

    private bool IsStunned ()
    {
        return stunTime > 0.0F;
    }
}
