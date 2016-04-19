using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterMover : MonoBehaviour
{
	public GameObject path;
	public float speed;
	private float rspeed = 10.0F;

	private List<Transform> waypoints = new List<Transform> (); // List of waypoints
	private int numWaypoints;
	private int waypointIndex = 0;
	private Transform nextWaypoint;

	void Start ()
	{
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
		if (transform.position == nextWaypoint.position) { // Arrived at waypoint, need to move to next 
			StartMoveToNextWaypoint ();
		} else {
			// Move object towards
			transform.position = Vector3.MoveTowards (transform.position, nextWaypoint.position, speed);

			// Face the waypoint
			Vector3 targetDir = nextWaypoint.position - transform.position;
			float step = rspeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
			Debug.DrawRay(transform.position, newDir, Color.red);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}

	private void MoveToNextWaypoint ()
	{
	}

	// Sets the next waypoint
	private void StartMoveToNextWaypoint ()
	{
		if (waypointIndex < numWaypoints) {
			nextWaypoint = waypoints [waypointIndex];
			waypointIndex++;
		}
	}
}
