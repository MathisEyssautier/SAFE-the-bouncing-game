using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelToDisplay;
    public void ExitFunction()
    {
        panel.SetActive(false);
        panelToDisplay.SetActive(true);
    }
}
