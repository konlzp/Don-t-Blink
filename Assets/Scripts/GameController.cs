using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{

    public Text uiText;
    public Timer timer;
    public Light mainLight;
    public AudioClip backGroundMusic;
    public AudioClip lullabyMusic;
    public bool gameOn = false;
    public GameObject startMenu;
    public GameObject gameUI;

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

        if (mainLight.intensity != 0) {
            if (audioSource.clip != lullabyMusic) {
                audioSource.clip = lullabyMusic;
                audioSource.time = 20;
                audioSource.Play ();
            }
        } else {
            if (audioSource.clip != backGroundMusic) {
                audioSource.clip = backGroundMusic;
                audioSource.Play ();
            }
        }
    }

    public void GameStart()
    {
        gameOn = true;
        gameUI.SetActive(true);
    }

    public void GameWon()
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
}
