using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enemy car that moves across the screen.
 */
public class Car : MonoBehaviour
{
    // direction
    public CarInfo.Direction direction;
    // movement
    public float speed = 5f;
    private float maxX = 22f;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /**
     * Move the car and destroy it when out of bounds.
     */
    private void Move()
    {
        if (!canMove) return;
        // move in specified direction
        switch (direction)
        {
            case CarInfo.Direction.LEFT:
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
            case CarInfo.Direction.RIGHT:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
            default:
                Debug.LogError(string.Format("Car {0} has an invalid direction", GetInstanceID()));
                break;
        }
        // destroy self if out of range
        if (transform.position.x > maxX || transform.position.x < -maxX)
        {
            Destroy(gameObject);
        }
    }

    /**
     * Stop the damn car!
     */
    public void Stop()
    {
        canMove = false;
    }
}
