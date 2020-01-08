﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    // IDs for power up
    // 0 - Triple Shot
    // 1 - Speed
    // 3 - Shields
    [SerializeField]
    private int powerupID;

    // Update is called once per frame
    void Update()
    {
        // Mode down at a speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // When we leave the screen, destroy this obj
        if(transform.position.y <= -5.0f){
            Destroy(this.gameObject);
        }
    }

    //OnTriggerCollision
    private void OnTriggerEnter2D(Collider2D other) {
        // Only be collect by the Player
        if(other.tag == "Player"){

            Player player = other.transform.GetComponent<Player>();
            if(player != null){

                switch(powerupID){
                    case 0: 
                        player.ActiveTripleShot();
                        break;

                    case 1:
                        player.SpeedUp();
                        break;

                    case 2:
                        Debug.Log("Shield Collected");
                        break;
                    default:
                        Debug.Log("Default case");
                        break;

                }
                
            }
            // On collected, destroy it
            Destroy(this.gameObject);
            
        }
        
    }
    
}