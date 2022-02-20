using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyClass
{
    public float detectRange;
    public LayerMask playerLayer;
    private bool b_startAttack = false;

    public Transform detectedPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Collider2D[] hitEnemyes = Physics2D.OverlapCircleAll(detectedPoint.position, detectRange, playerLayer);

        if (b_startAttack == false) {
            //Detect Player
            foreach (Collider2D player in hitEnemyes)
            {
                StartCoroutine(startAttack(player));
                Debug.Log("Detected Player");
                b_startAttack = true;
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(detectedPoint.position, detectRange);
    }
}
