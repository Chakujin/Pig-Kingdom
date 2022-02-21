using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyClass
{
    public SpriteRenderer mySprite;
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
    private bool b_move = true;

    [SerializeField]private Vector2 sizeDetectors;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Move and Detect
        if (Vector2.Distance(transform.position, PointMove[i_currentPoint].transform.position) < 0.1  && b_startAttack == false)
        {
            i_currentPoint++;
            i_currentPoint %= PointMove.Length;
            StartCoroutine(StopMove());
        }
        else if (b_move == true && b_startAttack == false) // Move character
        {
            v_moveDirection = transform.position + PointMove[i_currentPoint].position;
            transform.position = Vector2.MoveTowards(transform.position, PointMove[i_currentPoint].transform.position, Time.deltaTime * speed);
            
            if(v_moveDirection.x - transform.position.x < transform.position.x)
            {
                mySprite.flipX = false;
                //Debug.Log("Izquierda");
            }
            else if (v_moveDirection.x - transform.position.x > transform.position.x)
            {
                mySprite.flipX = true;
                //detectRigth.position = new Vector2(detectRigth.position.x + 0.2f, detectRigth.position.y);
                //Debug.Log("Derecha");
            }  
        }

        if(die == true)
        {
            b_move = false;
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
                //Detect Player
                foreach (Collider2D player in hitEnemyes)
                {
                    StartCoroutine(startAttack(player));
                    Debug.Log("Detected Player Rigth");
                    b_startAttack = true;
                    mySprite.flipX = false;
                    b_move = false;
                }
            }

            foreach (Collider2D playerLeft in detectLeftPlayer)
            {
                //Detect Player
                foreach (Collider2D player in hitEnemyes)
                {
                    StartCoroutine(startAttack(player));
                    Debug.Log("Detected Player Left");
                    b_startAttack = true;
                    mySprite.flipX = true;
                    b_move = false;
                }
            }
        }
    }

    private IEnumerator startAttack( Collider2D playerDetected)
    {
        EnemyAnimator.SetTrigger("Attack");
        playerDetected.GetComponentInParent<PlayerMovement>().TakeDamage(1);

        //playerDetected.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1));

        yield return new WaitForSeconds(2f);
        b_startAttack = false;
        b_move = true;
    }

    private IEnumerator StopMove()
    {
        b_move = false;
        yield return new WaitForSeconds(1f);
        b_move = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(detectedPoint.position, detectRange);
    }
}
