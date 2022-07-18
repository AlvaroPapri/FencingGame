using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject enemy;

    public float attackStepSpeed;
    public bool canMove;

    public Collider2D rightCollider;
    public Collider2D leftCollider;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        enemy = gameObject.CompareTag("BluePlayer") ? GameObject.FindGameObjectWithTag("RedPlayer") : 
                    GameObject.FindGameObjectWithTag("BluePlayer");
        canMove = true;
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
        canMove = false;
        
        if (enemy.transform.position.x > transform.position.x)
        {
            // Activate right collider
            rightCollider.transform.gameObject.SetActive(true);
            transform.position = new Vector3(transform.position.x + attackStepSpeed, transform.position.y);
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            anim.ResetTrigger("Attack");
            rightCollider.transform.gameObject.SetActive(false);
        }
        else  
        {
            // Activate left collider
            leftCollider.transform.gameObject.SetActive(true);
            transform.position = new Vector3(transform.position.x - attackStepSpeed, transform.position.y);
            yield return new WaitForSeconds(0.5f);
            leftCollider.transform.gameObject.SetActive(false);
        }

        canMove = true;
    }
}
