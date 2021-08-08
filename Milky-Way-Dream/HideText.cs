using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideText : MonoBehaviour
{
    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        CloseUI();
    }

    private void CloseUI()
    {
        if (Input.GetKeyDown("a"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
      
        if (Input.GetKeyDown("d"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }

        if (Input.GetKeyDown("space"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }

    }
}