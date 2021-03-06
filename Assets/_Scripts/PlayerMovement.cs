using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // My Code
        if (gameObject.CompareTag("BluePlayer") && gameObject.GetComponent<PlayerAttack>().canMove)
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
        
        if (gameObject.CompareTag("RedPlayer") && gameObject.GetComponent<PlayerAttack>().canMove)
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
