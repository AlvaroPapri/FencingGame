using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private GameObject enemy;

    public Collider2D rightCollider;
    public Collider2D leftCollider;
    
    private void Start()
    {
        enemy = gameObject.CompareTag("BluePlayer") ? GameObject.FindGameObjectWithTag("RedPlayer") : 
                    GameObject.FindGameObjectWithTag("BluePlayer");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        if (enemy.transform.position.x > transform.position.x)
        {
            // Activate right collider
            rightCollider.transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            rightCollider.transform.gameObject.SetActive(false);
        }
        else  
        {
            // Activate left collider
            leftCollider.transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            leftCollider.transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Sword"))
        {
            // DIE
            Debug.Log("HIT!!!!");
        }
    }
}
