using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKing : EnemyClass
{
    public LayerMask playerLayer;
    public GameObject[] platformScape;
    public float speed;
    public float detectRange;
    private bool b_startAttack = false;

    public Transform detectedPoint;
    public Transform detectRigth;
    public Transform detectLeft;
    private Transform m_playerTransform;

    [SerializeField] private Vector2 sizeDetectors;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        foreach (GameObject platform in platformScape)
        {
            platform.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (die == false)
        {
            if (Vector2.Distance(transform.position, m_playerTransform.position) < 0.1 && b_startAttack == false) //In front of player
            {
                StartCoroutine(StopMove());
            }
            else if (b_startAttack == false && move == true)
            {
                EnemyAnimator.SetBool("Move", true);
                //Move to player
                transform.position = Vector2.MoveTowards(transform.position, m_playerTransform.position, Time.deltaTime * speed);
            }
        }

        if(die == true)
        {
            move = false;
            foreach(GameObject platform in platformScape)
            {
                platform.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] hitEnemyes = Physics2D.OverlapCircleAll(detectedPoint.position, detectRange, playerLayer);
        Collider2D[] detectRigthPlayer = Physics2D.OverlapBoxAll(detectRigth.position, sizeDetectors, 0, playerLayer);
        Collider2D[] detectLeftPlayer = Physics2D.OverlapBoxAll(detectLeft.position, sizeDetectors, 0, playerLayer);


        if (b_startAttack == false && die == false)
        {

            foreach (Collider2D playerRigth in detectRigthPlayer)
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

    private IEnumerator StopMove()
    {
        EnemyAnimator.SetBool("Move", false);
        move = false;
        yield return new WaitForSeconds(2f);
        move = true;
    }

    private IEnumerator startAttack(Collider2D playerDetected)
    {
        EnemyAnimator.SetTrigger("Attack");
        EnemyAnimator.SetBool("Move", false);
        playerDetected.GetComponentInParent<PlayerMovement>().TakeDamage(1);

        //Flip sprite
        if (detectHitLeft == true)
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
}
