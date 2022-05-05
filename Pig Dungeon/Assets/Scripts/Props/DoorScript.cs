﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator myAnimator;
    public BoxCollider2D myColliderDoor;
    public bool spawnDoor;
    public AudioSource OpenAudio;

    private void Awake()
    {
        OpenAudio = FindObjectOfType<AudioSource>();

        if (spawnDoor == true)
        {
            myColliderDoor.enabled = false;
            myAnimator.SetTrigger("DoorTest");
        }
    }

    //Enter Door trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerMovement myplayer = collision.GetComponent<PlayerMovement>();

            myplayer.doorTrigger = true;
            myplayer.takeDoor(this);

            myplayer.fButtonMove();
        }
    }
    //Exit door trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement myplayer = collision.GetComponent<PlayerMovement>();
            myplayer.doorTrigger = false;

            myplayer.fButtonBye();            
        }
    }
}
