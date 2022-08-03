using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numRoundsToWin = 3;
    public int m_StartWait = 2;
    public GameObject blueWinBackground, redWinBackground;
    public Transform blueSpawnPositions;
    public Transform redSpawnPositions;
    public GameObject bluePlayer;
    public GameObject redPlayer;

    [SerializeField] private int currentRound;
    [SerializeField] private int blueRoundWins;
    [SerializeField] private int redRoundWins;

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
        Instantiate(bluePlayer, blueSpawnPositions.position, blueSpawnPositions.rotation);
        Instantiate(redPlayer, redSpawnPositions.position, redSpawnPositions.rotation);
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
        bluePlayer.SetActive(true);
        redPlayer.SetActive(true);
        
        bluePlayer.transform.position = blueSpawnPositions.position;
        redPlayer.transform.position = redSpawnPositions.position;
    }

    public void SetupWinner(GameObject winner)
    {
        if (winner.CompareTag("BluePlayer"))
            blueWinBackground.SetActive(true);
        
        if (winner.CompareTag("RedPlayer"))
            redWinBackground.SetActive(true);

        Time.timeScale = 0;
    }

    private bool OnePlayerLeft()
    {
        if (!bluePlayer.activeSelf || !redPlayer.activeSelf)
            return true;

        return false;
    }

    void DisablePlayersControl()
    {
        bluePlayer.GetComponent<PlayerMovement>().enabled = false;
        bluePlayer.GetComponent<PlayerAttack>().enabled = false;
        redPlayer.GetComponent<PlayerMovement>().enabled = false;
        redPlayer.GetComponent<PlayerAttack>().enabled = false;
    }

    void EnablePlayersControl()
    {
        bluePlayer.GetComponent<PlayerMovement>().enabled = true;
        bluePlayer.GetComponent<PlayerAttack>().enabled = true;
        redPlayer.GetComponent<PlayerMovement>().enabled = true;
        redPlayer.GetComponent<PlayerAttack>().enabled = true;
    }

    GameObject ReturnRoundWinner()
    {
        if (bluePlayer.activeSelf)
        {
            return bluePlayer;
        }

        return redPlayer;
    }

    GameObject ReturnGameWinner()
    {
        if (blueRoundWins == numRoundsToWin)
        {
            SetupWinner(bluePlayer);
        } else if (redRoundWins == numRoundsToWin)
        {
            SetupWinner(redPlayer);
        }

        return null;
    }
}
