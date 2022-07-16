using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float timeToLongStep;
    public float longStepSpeed;

    private float timeOfFirstPressed;
    private bool firstButtonPressed;
    private bool reset;

    private void Update()
    {
        // My Code
        if (gameObject.CompareTag("BluePlayer"))
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
        
        if (gameObject.CompareTag("RedPlayer"))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                MoveRight();
            }
        }
    }

    #region MOVE

    private void MoveLeft()
    {
        transform.position = new Vector3(transform.position.x - movementSpeed, transform.position.y,
            transform.position.z);
    }

    private void MoveRight()
    {
        transform.position = new Vector3(transform.position.x + movementSpeed, transform.position.y,
            transform.position.z);
    }

    #endregion

    #region TODO

    private void CheckDoublePressed()
    {
        if(Input.GetKeyDown(KeyCode.A) && firstButtonPressed) {
            if(Time.time - timeOfFirstPressed < 0.5f) {
                Debug.Log("DoubleClicked");
            } else {
                Debug.Log("Too late");
            }
 
            reset = true;
        }
 
        if(Input.GetKeyDown(KeyCode.A) && !firstButtonPressed) {
            firstButtonPressed = true;
            timeOfFirstPressed = Time.time;
        }
 
        if(reset) {
            firstButtonPressed = false;
            reset = false;
        } 
    }

    void Move(bool isLongStep)
    {
        
    }

    #endregion
}
