using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject BlueWinBackground, RedWinBackground;

    private void Awake()
    {
        Instance = this;
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
