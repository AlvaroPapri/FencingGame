using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numRoundsToWin = 3;
    public int m_StartWait = 2;
    public GameObject blueWinBackground, redWinBackground;
    public GameObject[] players;
    public Transform[] spawnPositions;

    private int currentRound;
    private int blueRoundWins;
    private int redRoundWins;

    private GameObject roundWinner;
    private GameObject gameWinner;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentRound = 0;
        SpawnPlayers();
        
        StartCoroutine(GameLoop());
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            GameObject prefab = Instantiate(players[i], spawnPositions[i].position, players[i].transform.rotation);
        }
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            Debug.Log("THE WINNER IS " + gameWinner.tag);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    IEnumerator RoundStarting()
    {
        Debug.Log("ROUND STARTING");
        ResetPlayers();
        DisablePlayersControl();

        currentRound++;
        Debug.Log("ROUND " + currentRound);
        
        yield return new WaitForSeconds(m_StartWait);
    }

    IEnumerator RoundPlaying()
    {
        Debug.Log("ROUND PLAYING");
        EnablePlayersControl();

        while (!OnePlayerLeft())
        {
            yield return null;
        }
    }

    IEnumerator RoundEnding()
    {
        DisablePlayersControl();
        
        roundWinner = null;
        roundWinner = ReturnRoundWinner();
        
        // roundWinner.CompareTag("BluePlayer") ? blueRoundWins++ : redRoundWins++;
        if (roundWinner != null && roundWinner.CompareTag("BluePlayer"))
        {
            blueRoundWins++;
        }
        else
        {
            redRoundWins++;
        }

        gameWinner = ReturnGameWinner();
        
        yield return new WaitForSeconds(m_StartWait);
    }

    void ResetPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnPositions[i].position;
        }
    }

    public void SetupWinner(GameObject winner)
    {
        if (winner.CompareTag("BluePlayer"))
            blueWinBackground.SetActive(true);
        
        if (winner.CompareTag("RedPlayer"))
            redWinBackground.SetActive(true);

        Time.timeScale = 0;
    }

    //TODO: Fix error here
    private bool OnePlayerLeft()
    {
        int playersAlive = 0;
        
        for (int i = 0; i < players.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("BluePlayer").activeSelf)
            {
                playersAlive++;
            }

            if (GameObject.FindGameObjectWithTag("RedPlayer").activeSelf)
            {
                playersAlive++;
            }
        }
        
        if (playersAlive == 1)
            return true;
        
        return false;
    }

    void DisablePlayersControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMovement>().enabled = false;
            players[i].GetComponent<PlayerAttack>().enabled = false;
        }
    }

    void EnablePlayersControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMovement>().enabled = true;
            players[i].GetComponent<PlayerAttack>().enabled = true;
        }
    }

    GameObject ReturnRoundWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeSelf)
            {
                return players[i];
            }
        }

        return null;
    }

    GameObject ReturnGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].CompareTag("BluePlayer"))
            {
                if (blueRoundWins == numRoundsToWin)
                {
                    Debug.Log("BLUE PLAYER WINS");
                    // SetupWinner(players[i]);
                }
            } else if (players[i].CompareTag("RedPlayer"))
            {
                if (redRoundWins == numRoundsToWin)
                {
                    Debug.Log("RED PLAYER WINS");
                    // SetupWinner(players[i]);
                }
            }
        }

        return null;
    }
}
