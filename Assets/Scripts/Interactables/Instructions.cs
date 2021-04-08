using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    [SerializeField]
    public KeyCode _key;

    private Boolean instructionButtonShown = true;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showInstructionButton());
    }

    private void Update()
    {
        if(Input.GetKeyDown(_key))
        {
            this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
            this.gameObject.GetComponentInChildren<Image>().enabled = true;
            this.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;

        }
        else if(Input.GetKeyUp(_key))
        {
            this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
            this.gameObject.GetComponentInChildren<Image>().enabled = false;
            this.gameObject.GetComponent<TextMeshProUGUI>().enabled = instructionButtonShown;
        }
    }


    private IEnumerator showInstructionButton()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            this.gameObject.GetComponent<TextMeshPro>().enabled = false;
            instructionButtonShown = false;
            yield return new WaitForSeconds(120);
            this.gameObject.GetComponent<TextMeshPro>().enabled = true;
            instructionButtonShown = true;
        }
    }
}
