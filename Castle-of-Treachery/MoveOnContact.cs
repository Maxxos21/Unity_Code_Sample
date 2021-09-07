using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnContact : MonoBehaviour
{


    //Configs
    [SerializeField] Vector3 velocity;
    private bool moving;


    //When "player" Hits the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moving = true;
            collision.collider.transform.SetParent(transform);
        }
    }


    //When "player" leaves the platform 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
            moving = false;
        }
    }

    //each frame update 
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }
}
