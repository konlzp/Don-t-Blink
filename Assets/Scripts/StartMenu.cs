using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour {
    public GameController gameController;
    public GameObject gameUI;

    void Start()
    {
        gameUI.SetActive(false);
    }

	public void GameStart()
    {
        gameUI.SetActive(true);
        gameController.GameStart();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
