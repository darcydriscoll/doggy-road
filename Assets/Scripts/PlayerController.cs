using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player-related functionality.
 */
public class PlayerController : MonoBehaviour
{
    // movement
    private bool isStopped = false;
    private Vector3 lastPos;
    private bool canMove = true;
    private float moveDistance = 2f;
    // level boundaries
    private float maxX = 14;
    private float minZ = -11;
    private float maxZ = 11;
    // input
    private float horizontalInput;
    private float verticalInput;
    

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // move
        MovePlayer();
    }

    /**
     * Move the player in a specified direction.
     */
    void MovePlayer()
    {
        if (isStopped) return;
        // update inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // reset movement flag
        if (horizontalInput == 0 && verticalInput == 0)
        {
            canMove = true;
        }
        // move
        else if (canMove)
        {
            canMove = false;
            // track last position
            if (lastPos != transform.position)
            {
                lastPos = transform.position;
            }
            // left/right
            if (horizontalInput != 0)
            {
                // x constraint
                float newX = transform.position.x + moveDistance * horizontalInput;
                if (newX >= -maxX && newX <= maxX)
                {
                    // move left or right
                    transform.Translate(moveDistance * horizontalInput, 0, 0);
                }
            }
            // up/down
            else if (verticalInput == 1 && (transform.position.z == minZ || transform.position.z == maxZ - 3))
            {
                // move from start to road, or road to finish
                transform.Translate(0, 0, 3);
            }
            else if (verticalInput == -1 && transform.position.z == minZ + 3)
            {
                // move from road to start
                transform.Translate(0, 0, -3);
            }
            else if (transform.position.z != minZ)
            {
                // misc. up and down with z constraint
                transform.Translate(0, 0, moveDistance * verticalInput);
            }   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // tree
        if (collision.gameObject.CompareTag("Tree"))
        {
            transform.position = lastPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // car
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        }
    }

    /**
     * End the game.
     */
    private void GameOver()
    {
        Debug.Log("Game over!");
        // stop moving
        Stop();
        // stop all cars
        GameObject.FindGameObjectWithTag("Enemy Manager").BroadcastMessage("Stop");
    }

    private void Stop()
    {
        isStopped = true;
    }
}
