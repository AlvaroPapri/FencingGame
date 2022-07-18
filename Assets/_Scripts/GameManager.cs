using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject BlueWinBackground, RedWinBackground;

    private void Start()
    {
        gm = this;
        
    }

    public void setupWinner(GameObject winner)
    {
        if (winner.CompareTag("BluePlayer"))
            BlueWinBackground.SetActive(true);
        
        if (winner.CompareTag("RedPlayer"))
            RedWinBackground.SetActive(true);

        Time.timeScale = 0;
    }
}
