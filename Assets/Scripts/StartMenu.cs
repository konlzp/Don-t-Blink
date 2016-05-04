using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    public GameController gameController;

    public void Play ()
    {
        gameController.GameStart ();
    }

    public void Exit ()
    {
        Application.Quit ();
    }
}
