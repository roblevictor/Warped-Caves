using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GroundCheck : MonoBehaviour
{
    PlayerController Player;
    
    
    void Start()
    {
        Player = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
        
    }


    void OnCollisionEnter2D(Collision2D collisor)
    {
        if (collisor.gameObject.layer == 6)
        {
            Player.IsGrounded = !Player.IsGrounded;

        }

    }
   
}
