using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour {

    private GameObject panel;
    private GameObject replayButton;
    private GameObject finishText;

    private void Start()
    {
        panel = GameObject.Find("Panel") as GameObject;
        replayButton = GameObject.Find("ReplayButton") as GameObject;
        finishText = GameObject.Find("FinishText") as GameObject;

        panel.SetActive(false);
        replayButton.SetActive(false);
        finishText.SetActive(false);
    }

    public void setGameOverUI()
    {
        panel.SetActive(true);
        replayButton.SetActive(true);
    }

    public void setFinishUI()
    {
        finishText.SetActive(true);
        replayButton.SetActive(true);
    }
}
