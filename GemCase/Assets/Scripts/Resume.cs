using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public Button PauseGameButton;
    public GameObject ScrollView;

    private void Start()
    {
        //ScrollView.SetActive(false);
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        ScrollView.SetActive(true);
    }

    public void CloseButton()
    {
        ScrollView.SetActive(false);
        Time.timeScale = 1;
    }
}
