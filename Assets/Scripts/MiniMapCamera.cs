using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
  public Transform Player;

  public Camera MainCamera;

  public bool RotateWithPlayer = true;

  public KeyCode _key;

  private bool MapActive = true;

  public GameObject MiniMap;

  public void Start()
  {
      // initialise camera position
    SetPosition();

    SetRotation();
  }

  void LateUpdate()
  {
    // if player exists actualise minimap camera position
    if(Player != null)
    {
      SetPosition();

      if (RotateWithPlayer && MainCamera)
      {
        SetRotation();
      }

      // if M is pressed and minimap is activated, turn off minimap
      if(Input.GetKeyDown(KeyCode.M))
      {
        if(MapActive==true)
        {
          MapActive = false;
          MiniMap.SetActive(MapActive);
        }
        // if M is pressed and minimap is turned off, turn minimap on
        else if(MapActive==false)
        {
          MapActive = true;
          MiniMap.SetActive(MapActive);
        }
      }
    }
  }

  // follow players position with minimap camera
  private void SetPosition()
  {
    var newPos = Player.position;
    newPos.y = transform.position.y;

    transform.position = newPos;
  }

  private void SetRotation()
  {
    transform.rotation =
        Quaternion.Euler(90.0f, MainCamera.transform.eulerAngles.y, 0.0f);
  }
}
