using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    bool isMoving = true;
    [SerializeField] float speed = 10f;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (transform.localPosition.x >= 0)
        {
            transform.localPosition = new Vector2(0,transform.localPosition.y);
            isMoving = false;
        }
    }
}
