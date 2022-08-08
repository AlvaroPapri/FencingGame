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
    public GameObject bluePrefab;
    public GameObject redPrefab;

    private GameObject bluePlayerClone;
    private GameObject redPlayerClone;

    private PlayerMovement bluePlayerMovement;
    private PlayerMovement redPlayerMovement;
    private PlayerAttack bluePlayerAttack;
    private PlayerAttack redPlayerAttack;

    [SerializeField] private int currentRound;
    [SerializeField] private int blueRoundWins;
    [SerializeField] private int redRoundWins;

    private GameObject roundWinner;
    private GameObject gameWinner;

    #region Instantiate & Start Game Loop

    private void Awake()
    {
        Instance = this;
        
        currentRound = 0;
        SpawnPlayers();
    }

    private void Start()
    {
        bluePlayerMovement = bluePlayerClone.GetComponent<PlayerMovement>();
        redPlayerMovement = redPlayerClone.GetComponent<PlayerMovement>();
        bluePlayerAttack = bluePlayerClone.GetComponent<PlayerAttack>();
        redPlayerAttack = redPlayerClone.GetComponent<PlayerAttack>();
        
        StartCoroutine(GameLoop());
    }

    private void SpawnPlayers()
    {
        bluePlayerClone = Instantiate(bluePrefab, blueSpawnPositions.position, blueSpawnPositions.rotation);
        redPlayerClone = Instantiate(redPrefab, redSpawnPositions.position, redSpawnPositions.rotation);
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

    #endregion

    #region Round's Flow

    IEnumerator RoundStarting()
    {
        ResetPlayers();
        DisablePlayersControl();

        currentRound++;
        
        yield return new WaitForSeconds(m_StartWait);
    }

    IEnumerator RoundPlaying()
    {
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

    #endregion

    #region Reset and Player's Control

    void ResetPlayers()
    {
        bluePlayerClone.SetActive(true);
        redPlayerClone.SetActive(true);
        
        bluePlayerClone.transform.position = blueSpawnPositions.position;
        redPlayerClone.transform.position = redSpawnPositions.position;
    }

    void DisablePlayersControl()
    {
        bluePlayerMovement.enabled = false;
        bluePlayerAttack.enabled = false;
        redPlayerMovement.enabled = false;
        redPlayerAttack.enabled = false;
    }

    void EnablePlayersControl()
    {
        bluePlayerMovement.enabled = true;
        bluePlayerAttack.enabled = true;
        redPlayerMovement.enabled = true;
        redPlayerAttack.enabled = true;
    }

    #endregion

    #region Round and Game Winners Checkers

    private bool OnePlayerLeft()
    {
        if (!bluePlayerClone.activeSelf || !redPlayerClone.activeSelf)
            return true;

        return false;
    }

    GameObject ReturnRoundWinner()
    {
        if (bluePlayerClone.activeSelf)
        {
            return bluePlayerClone;
        }

        return redPlayerClone;
    }

    GameObject ReturnGameWinner()
    {
        if (blueRoundWins == numRoundsToWin)
        {
            SetupWinner(bluePlayerClone);
        } else if (redRoundWins == numRoundsToWin)
        {
            SetupWinner(redPlayerClone);
        }

        return null;
    }

    public void SetupWinner(GameObject winner)
    {
        if (winner.CompareTag("BluePlayer"))
            blueWinBackground.SetActive(true);
        
        if (winner.CompareTag("RedPlayer"))
            redWinBackground.SetActive(true);

        Time.timeScale = 0;
    }

    #endregion
}
