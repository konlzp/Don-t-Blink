using UnityEngine;
using System.Collections;

public class CameraMoving : MonoBehaviour {

	public GameObject monsters;

    // Use this for initialization
    void Start () {
    }

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        float newPitch = pitch - speedV * Input.GetAxis("Mouse Y");
        if (Mathf.Abs(newPitch) < 60)
        {
            pitch = newPitch;
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }


}
