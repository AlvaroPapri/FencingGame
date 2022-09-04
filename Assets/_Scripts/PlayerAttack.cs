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

    private void OnEnable()
    {
        canMove = true;
        rightCollider.gameObject.SetActive(false);
        leftCollider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.CompareTag("BluePlayer") && Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            StartCoroutine(Attack());
        }
        
        if (gameObject.CompareTag("RedPlayer") && Input.GetKeyDown(KeyCode.Slash) && canMove)
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
            transform.position = new Vector3(transform.position.x + attackStepSpeed, transform.position.y);
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            anim.ResetTrigger("Attack");
        }
        else  
        {
            // Activate left collider
            transform.position = new Vector3(transform.position.x - attackStepSpeed, transform.position.y);
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            anim.ResetTrigger("Attack");
        }

        canMove = true;
    }

    public void EnableAttack()
    {
        rightCollider.gameObject.SetActive(true);
        leftCollider.gameObject.SetActive(true);
    }
    
    public void DisableAttack()
    {
        rightCollider.gameObject.SetActive(false);
        leftCollider.gameObject.SetActive(false);
    }
}
