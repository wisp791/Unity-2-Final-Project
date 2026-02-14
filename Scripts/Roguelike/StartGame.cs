using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject panel;
    public GameObject game;

    private bool started = false;
    public void GameStart()
    {
        if (started) return;
        started = true;
        menu.SetActive(false);
        game.SetActive(true);
        panel.SetActive(false);
    }
}
