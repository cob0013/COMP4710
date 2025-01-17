﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitGame() {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void StartGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void TrafficLightSimulator() {
        SceneManager.LoadScene("StopLight Testing");
    }
}
