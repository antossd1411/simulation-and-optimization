using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject[] panels;
    public void showPanels() {
        for (int i = 0; i < panels.Length; i++) {
            panels[i].SetActive(!panels[i].activeSelf);
        }
    }
}
