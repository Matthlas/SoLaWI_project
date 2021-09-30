using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField]
    public KeyCode _key;
    public GameObject pauseBG;
    public GameObject pauseText;

    private void Update()
    {
        // if P is pressed, activate pause menu background and info text and stop in game time
        if(Input.GetKeyDown(KeyCode.P))
        {
          Time.timeScale = 0;
          pauseBG.SetActive(true);
          pauseText.SetActive(true);
        }
        // if O is pressed, start in game time again and hide pause background and info text
        else if(Input.GetKeyDown(KeyCode.O))
        {
          Time.timeScale = 1;
          pauseBG.SetActive(false);
          pauseText.SetActive(false);
        }
    }
}
