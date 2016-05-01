using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
    public AudioClip monsterScream;
    public float screamInterval;

    private AudioSource monsterSound;
    private float nextScream = 0;

    void Start()
    {
        monsterSound = GetComponent<AudioSource>();
    }

    public void makeItScream()
    {
        if(!monsterSound.isPlaying && Time.time > nextScream)
        {
            monsterSound.clip = monsterScream;
            monsterSound.Play();
            nextScream = Time.time + screamInterval;
        }
    }
}
