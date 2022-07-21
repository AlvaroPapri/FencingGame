using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numRoundsToWin = 3;
    public int m_StartWait = 3;
    public GameObject blueWinBackground, redWinBackground;
    public GameObject[] players;
    public Transform[] spawnPositions;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
    }

    IEnumerator RoundStarting()
    {
        ResetPlayers();
        DisablePlayersMove();
        
        yield return m_StartWait;
    }

    IEnumerator RoundPlaying()
    {
        EnablePlayersMove();
        
        yield return m_StartWait;
    }

    void ResetPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnPositions[i].position;
        }
    }
    
    public void setupWinner(GameObject winner)
    {
        if (winner.CompareTag("BluePlayer"))
            blueWinBackground.SetActive(true);
        
        if (winner.CompareTag("RedPlayer"))
            redWinBackground.SetActive(true);

        Time.timeScale = 0;
    }

    void DisablePlayersMove()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerAttack>().DisableMove();
        }
    }
    
    void EnablePlayersMove()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerAttack>().EnableMove();
        }
    }
}
