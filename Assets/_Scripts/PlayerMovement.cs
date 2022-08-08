using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    private Animator anim;

    private PlayerAttack _playerAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        // My Code
        if (gameObject.CompareTag("BluePlayer") && _playerAttack.canMove)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
                
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
        }
        
        if (gameObject.CompareTag("RedPlayer") && _playerAttack.canMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
        }
    }

    #region MOVE

    private void MoveLeft()
    {
        Animating();
        transform.position = new Vector3(transform.position.x - movementSpeed, transform.position.y,
            transform.position.z);
    }

    private void MoveRight()
    {
        Animating();
        transform.position = new Vector3(transform.position.x + movementSpeed, transform.position.y,
            transform.position.z);
    }

    void Animating()
    {
        if (anim.GetBool("Move"))
        {
            anim.SetBool("Move", false);
        }
        else
        {
            anim.SetBool("Move", true);
        }
    }

    #endregion
}
