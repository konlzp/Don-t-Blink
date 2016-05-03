using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    public GameController gameController;
    public GameObject gameUI;

    void Start ()
    {
        gameUI.SetActive (false);
    }

    public void Play ()
    {
        gameUI.SetActive (true);
        gameController.GameStart ();
    }

    public void Exit ()
    {
        Application.Quit ();
    }
}
