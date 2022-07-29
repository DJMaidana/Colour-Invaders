using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    bool isMoving = true;
    [SerializeField] float speed = 10f;
    [SerializeField] float xDestination = 0f;

    void Update()
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
