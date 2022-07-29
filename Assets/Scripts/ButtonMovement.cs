using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float speed = 10f;
    [Tooltip("Where the button stops in x coordinates")]
    [SerializeField] float xDestination = 0f;

    [Header("States")]
    bool isMoving = true;

    void Update()   // Moves button to the right until it reaches the given x coordinates in the world.
    {
        if (isMoving)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (transform.localPosition.x >= xDestination)
        {
            transform.localPosition = new Vector2(xDestination,transform.localPosition.y);
            isMoving = false;
        }
    }
}
