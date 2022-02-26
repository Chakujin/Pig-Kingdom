using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPigCanon : EnemyClass
{
    //Pig
    public Transform detectRangePosition;
    public LayerMask TargetPlayer;

    public float detectRange;
    private bool b_start = false;

    //Canon
    public Animator CanonAnimator;
    public Transform CanonBallSpawn;
    public GameObject canonBall;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth += maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Collider2D[] detectPlayer = Physics2D.OverlapCircleAll(detectRangePosition.position, detectRange, TargetPlayer);

        if (b_start == false)
        {

            foreach (Collider2D player in detectPlayer)
            {
                b_start = true;
                StartCoroutine(AttackStart());
            }
        }
    }

    private IEnumerator AttackStart()
    {
        yield return new WaitForSeconds(1f);
        b_start = false;
    }
}
