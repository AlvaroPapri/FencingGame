using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameManager gm;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Sword"))
        {
            Destroy(gameObject);
            Debug.Log(col.gameObject.tag);
            gm.setupWinner(col.gameObject);
        }
    }
}
