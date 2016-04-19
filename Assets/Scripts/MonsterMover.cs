using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterMover : MonoBehaviour
{
	public GameObject path;
	public float speed;

	private List<Transform> waypoints = new List<Transform> ();
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
			transform.position = Vector3.MoveTowards (transform.position, nextWaypoint.position, speed);
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
