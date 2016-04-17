using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
    public AudioClip monsterScream;

    private AudioSource monsterSound;

    void Start()
    {
        monsterSound = GetComponent<AudioSource>();
    }

    public void makeItScream()
    {
        if(!monsterSound.isPlaying)
        {
            monsterSound.clip = monsterScream;
            monsterSound.Play();
        }
    }
}
