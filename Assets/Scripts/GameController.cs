﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{

    public Text uiText;
    public Timer timer;
    public Light mainLight;
    public AudioClip HomePageMusic;
    public AudioClip backGroundMusic;
    public AudioClip lullabyMusic;
    public GameObject startMenu;
    public GameObject gameUI;

    private bool gameOn = false;
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource> ();
        gameUI.SetActive (false);
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.R)) {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        } else if (Input.GetKeyDown (KeyCode.Escape)) {
            Application.Quit ();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GameStart();
        }

        if (mainLight.intensity != 0) {
            if (audioSource.clip != lullabyMusic) {
                audioSource.clip = lullabyMusic;
                audioSource.time = 20;
                audioSource.Play ();
            }
        } else {
            if (audioSource.clip != backGroundMusic && gameOn == true) {
                audioSource.clip = backGroundMusic;
                audioSource.Play ();
            }else if(audioSource.clip != HomePageMusic && gameOn == false) {
                audioSource.clip = HomePageMusic;
                audioSource.Play ();
            }
        }
    }

    public void GameStart ()
    {
        gameOn = true;
        Cursor.visible = false;
        startMenu.SetActive(false);
        gameUI.SetActive (true);
    }

    public void GameWon ()
    {
        gameOn = false;
        mainLight.intensity = 2;
    }

    public void GameOver ()
    {
        gameOn = false;
        timer.zeroTime ();
        uiText.text = "You died";
    }
        
    public bool IsGameActive {
        get { return gameOn; }
    }
}
