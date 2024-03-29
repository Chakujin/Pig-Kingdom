﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDiamond : MonoBehaviour
{
    public Animator myAnimator;
    public float sizeCollider;
    public Transform colliderPos;
    public LayerMask playerLayer;

    private bool b_take = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        Collider2D[] detectplayer = Physics2D.OverlapCircleAll(colliderPos.position, sizeCollider, playerLayer);

        if (b_take == false)
        {
            foreach (Collider2D player in detectplayer)
            {
                b_take = true;
                player.GetComponentInParent<PlayerMovement>().takeDiamond();
                StartCoroutine(taked());
            }
        }
    }

    private IEnumerator taked()
    {
        myAnimator.SetTrigger("take");
        FindObjectOfType<AudioManager>().Play("Diamond");
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(colliderPos.position, sizeCollider);
    }
}
