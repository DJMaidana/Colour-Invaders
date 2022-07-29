using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringSelector : MonoBehaviour
{
    void FixedUpdate()
    {
        MakeFront();
    }

    private void MakeFront()    // Casts a ray upwards and the first enemy it hits is allowed to shoot
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);   //  Cast a ray upwards, returns the first object it collides with
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<Enemy>().isInFront = true;    //  Enable enemy firing
        }
    }
}
