using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Sword"))
        {
            Debug.Log(col.gameObject.transform.parent.tag);
            GameManager.Instance.setupWinner(col.gameObject.transform.parent.gameObject);
            gameObject.SetActive(false);
        }
    }
}
