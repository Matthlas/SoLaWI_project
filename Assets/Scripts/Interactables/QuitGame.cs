using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
  [SerializeField]
  public KeyCode _key;

  private void Update()
  {
      // if Q is pressed, quit game
      if(Input.GetKeyDown(KeyCode.Q))
      {
        Application.Quit();
      }
    }
}
