using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI uiWinner;
    public TextMeshProUGUI uiScore;
    // Start is called before the first frame update
    void Start()
    {
        SaveController.Instance.Reset();
        string lastWinner = SaveController.Instance.GetLastWinner();

        if (lastWinner != "")
        {
            uiWinner.text = "Last Winner: " + lastWinner;
        }
        else
        {
            uiWinner.text = "";
        }

        string lastScore = SaveController.Instance.GetlastScore();

        if (lastScore != "")
        {
            uiScore.text = "Max points: \n" + lastScore;
        }
        else
        {
            uiScore.text = "";
        }
    }
}