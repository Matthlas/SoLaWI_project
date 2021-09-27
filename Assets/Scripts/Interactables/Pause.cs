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
        if(Input.GetKeyDown(KeyCode.P))
        {
          Time.timeScale = 0;
          pauseBG.SetActive(true);
          pauseText.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
          Time.timeScale = 1;
          pauseBG.SetActive(false);
          pauseText.SetActive(false);
        }
    }
}
