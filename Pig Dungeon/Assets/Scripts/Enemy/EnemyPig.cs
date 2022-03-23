﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyClass
{
    public float speed;
    public float detectRange;
    public LayerMask playerLayer;
    private bool b_startAttack = false;

    public Transform detectedPoint;
    public Transform detectRigth;
    public Transform detectLeft;
    public Transform[] PointMove;

    private int i_currentPoint;
    private Vector2 v_moveDirection;

    [SerializeField]private Vector2 sizeDetectors;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(StartDialog());
    }

    // Update is called once per frame
    void Update()
    {
        if (die == false) {
            //Move and Detect
            if (Vector2.Distance(transform.position, PointMove[i_currentPoint].transform.position) < 0.1 && b_startAttack == false) //Change Point Move
            {
                EnemyAnimator.SetBool("Move", false);
                i_currentPoint++;
                i_currentPoint %= PointMove.Length;
                StartCoroutine(StopMove());
            }
            else if (move == true && b_startAttack == false) // Move character
            {
                EnemyAnimator.SetBool("Move", true);
                v_moveDirection = transform.position + PointMove[i_currentPoint].position;
                transform.position = Vector2.MoveTowards(transform.position, PointMove[i_currentPoint].transform.position, Time.deltaTime * speed);

                if (v_moveDirection.x - transform.position.x < transform.position.x)
                {
                    mySpriteRenderer.flipX = false;
                    //Debug.Log("Izquierda");
                }
                else if (v_moveDirection.x - transform.position.x > transform.position.x)
                {
                    mySpriteRenderer.flipX = true;
                    //Debug.Log("Derecha");
                }
            }
        }
        if (die == true)
        {
            move = false;
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] hitEnemyes = Physics2D.OverlapCircleAll(detectedPoint.position, detectRange, playerLayer);
        Collider2D[] detectRigthPlayer = Physics2D.OverlapBoxAll(detectRigth.position, sizeDetectors, 0, playerLayer);
        Collider2D[] detectLeftPlayer = Physics2D.OverlapBoxAll(detectLeft.position, sizeDetectors, 0, playerLayer);

        if (b_startAttack == false && die == false ) {

            foreach(Collider2D playerRigth in detectRigthPlayer)
            {
                detectHitRigth = true;
                detectHitLeft = false;
                //Detect Player Rigth
                foreach (Collider2D player in hitEnemyes)
                {
                    StartCoroutine(startAttack(player));
                    b_startAttack = true;
                    mySpriteRenderer.flipX = false;
                    move = false;
                }
            }

            foreach (Collider2D playerLeft in detectLeftPlayer)
            {
                detectHitRigth = false;
                detectHitLeft = true;
                //Detect Player Left
                foreach (Collider2D player in hitEnemyes)
                {
                    StartCoroutine(startAttack(player));
                    b_startAttack = true;
                    mySpriteRenderer.flipX = true;
                    move = false;
                }
            }
        }
    }

    private IEnumerator startAttack( Collider2D playerDetected)
    {
        EnemyAnimator.SetTrigger("Attack");
        EnemyAnimator.SetBool("Move",false);
        playerDetected.GetComponentInParent<PlayerMovement>().TakeDamage(1);

        if(detectHitLeft == true)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (detectHitRigth == true)
        {
            mySpriteRenderer.flipX = true;
        }

        yield return new WaitForSeconds(2f);
        b_startAttack = false;
        move = true;
    }

    private IEnumerator StopMove()
    {
        move = false;
        yield return new WaitForSeconds(1f);
        move = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(detectedPoint.position, detectRange);
    }
}
