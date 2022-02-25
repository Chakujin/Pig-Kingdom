using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D myRb;
    public CircleCollider2D myCollyder;
    public LayerMask playerTrigger;
    public Animator myAnimator;
    private bool b_Start = false;

    public float radiusRange;
    private float f_currentTime;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator.SetTrigger("Detonate");
    }

    // Update is called once per frame

    void Update()
    {
        f_currentTime += Time.deltaTime;
        if(f_currentTime >= 2f && b_Start == false)
        {
            b_Start = true;
            myCollyder.enabled = false;
            ExplosionNoHited();
        }
    }

    private void FixedUpdate()
    {
        //If detect player explote 
        if (b_Start == false) {
            Collider2D[] playerHited = Physics2D.OverlapCircleAll(transform.position, radiusRange, playerTrigger);

            foreach (Collider2D player in playerHited)
            {
                b_Start = true;
                StartCoroutine(StartExplosion(player));
            }
        }
    }

    //If dont detect player
    private void ExplosionNoHited()
    {

    }

    private IEnumerator StartExplosion(Collider2D player)
    {
        myCollyder.enabled = false;
        player.GetComponentInParent<PlayerMovement>().TakeDamage(1);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
