using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////
//
//  This class is used to select the closest enemy to the ground and enable its weapons.
//
////////////////////////////////////////
public class FiringSelector : MonoBehaviour
{
    void FixedUpdate()
    {
        MakeFront();
    }

    private void MakeFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);   //  Cast a ray upwards, returns the first object it collides with
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().isInFront = true;    //  Enable enemy firing
        }
    }
}
