using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBox : EnemyClass
{
    public float detectRange;
    public LayerMask playerLayer;
    private bool b_startAttack = false;

    public Transform detectedPoint;
    public Transform detectRigth;
    public Transform detectLeft;
    public GameObject MyBox;

    [SerializeField] private Vector2 sizeDetectors;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(die == true)
        {
            //Destroy(MyBox);
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

    private IEnumerator startAttack(Collider2D playerDetected)
    {
        EnemyAnimator.SetTrigger("Attack");
        playerDetected.GetComponentInParent<PlayerMovement>().TakeDamage(1);

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(detectedPoint.position, detectRange);
    }
}
