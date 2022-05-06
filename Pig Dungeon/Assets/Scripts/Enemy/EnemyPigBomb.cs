using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPigBomb : EnemyClass
{
    public Transform detectRigth;
    public Transform detectLeft;
    public Transform detectedPoint;
    public Transform spawnBomb;
    public Transform spawnBombInverted;

    public GameObject bomb;
    public LayerMask playerLayer;
    public Vector2 sizeDetectors;
    public Vector2 detectRange;

    public float reloadtime;
    private bool b_startAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth += maxHealth;
        StartCoroutine(StartDialog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Collider2D[] hitEnemyes = Physics2D.OverlapBoxAll(detectedPoint.position, detectRange, playerLayer);
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
                    if (b_startAttack == false)
                    {
                        b_startAttack = true;
                        StartCoroutine(startAttack());
                        mySpriteRenderer.flipX = false;
                        move = false;
                    }
                }
            }

            foreach (Collider2D playerLeft in detectLeftPlayer)
            {
                detectHitRigth = false;
                detectHitLeft = true;
                //Detect Player Left
                foreach (Collider2D player in hitEnemyes)
                {
                    if (b_startAttack == false)
                    {
                        b_startAttack = true;
                        StartCoroutine(startAttack());
                        mySpriteRenderer.flipX = true;
                        move = false;
                    }
                }
            }
        }
    }

    private IEnumerator startAttack()
    {
        EnemyAnimator.SetTrigger("Attack");

        if (detectHitLeft == true)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (detectHitRigth == true)
        {
            mySpriteRenderer.flipX = true;
        }

        yield return new WaitForSeconds(0.2f);
        EnemyAnimator.SetTrigger("Reload");

        if (detectHitLeft == true)
        {
            GameObject mybomb = Instantiate(bomb, spawnBombInverted);
            FindObjectOfType<AudioManager>().Play("BombDrop");
            mybomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(4, 0), ForceMode2D.Impulse);
        }
        else if (detectHitRigth == true)
        {
            GameObject mybomb = Instantiate(bomb, spawnBomb);
            FindObjectOfType<AudioManager>().Play("BombDrop");
            mybomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(-4, 0), ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(reloadtime);
        b_startAttack = false;
    }

    //Draw Gizmos
    private void OnDrawGizmosSelected()
    {
        if (detectedPoint == null)
            return;

        Gizmos.DrawWireCube(detectedPoint.position, new Vector3(detectRange.x,detectRange.y,1));
        Gizmos.DrawWireCube(detectRigth.position, new Vector3(sizeDetectors.x, sizeDetectors.y, 1));
        Gizmos.DrawWireCube(detectLeft.position, new Vector3(sizeDetectors.x, sizeDetectors.y, 1));
    }
}
