using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public GameController gameController;
    public AudioClip monsterScream;

    private AudioSource monsterSound;
    
    private new Light light;

    void Start ()
    {
        light = GetComponent<Light> ();
        monsterSound = GetComponent<AudioSource> ();
    }

    void OnTriggerEnter (Collider other)
    {
        // Make player lose!
        if (other.tag == "Monster") {
            PlayScreamSound ();
            gameController.GameOver ();
            light.intensity = 2;
        }
    }

    public void PlayScreamSound ()
    {
        monsterSound.clip = monsterScream;
        monsterSound.Play ();

    }
}
