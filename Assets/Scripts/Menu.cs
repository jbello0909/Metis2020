﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void Back()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
}
