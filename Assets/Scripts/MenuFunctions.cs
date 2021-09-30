using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    // Start new game
    public void NewGame()
    {
      // Go to Scene 1 (New Game)
      SceneManager.LoadScene(1);
    }

    // Quit Application
    public void QuitGame()
    {
      // Debug Message
      Debug.Log("QUIT GAME");
      // Quit Game
      Application.Quit();
    }

}
