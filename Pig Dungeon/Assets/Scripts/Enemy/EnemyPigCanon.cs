using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPigCanon : EnemyClass
{
    //Pig
    public GameObject canonPig;
    public Transform detectRangePosition;
    public LayerMask TargetPlayer;

    public Vector2 detectRange;
    private bool b_start = false;
    [SerializeField]private float f_cadence = 4f;
    private float f_currentTime;

    //Canon
    public Animator CanonAnimator;
    public Transform CanonBallSpawn;
    public GameObject canonBall;
    private Vector2 v_force;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth += maxHealth;

        //If is fliped add negative force when instantiate canon ball
        if (canonPig.transform.localScale.x == -1)
        {
            v_force = new Vector2(4,0);
        }
        else { v_force = new Vector2(-4,0); }
    }

    // Update is called once per frame
    void Update()
    {
        f_currentTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Collider2D[] detectPlayer = Physics2D.OverlapBoxAll(detectRangePosition.position, detectRange, 0, TargetPlayer);

        if (f_cadence <= f_currentTime)
        {
          
          foreach (Collider2D player in detectPlayer)
          {
            Debug.Log("disparo");
            if (b_start == false)
            {
             b_start = true;
             StartCoroutine(AttackStart());
            }
          }

        }
    }

    private IEnumerator AttackStart()
    {
        EnemyAnimator.SetTrigger("MatchOn");
        yield return new WaitForSeconds(0.5f);
        CanonAnimator.SetTrigger("Shoot");

        GameObject myCanonBall = Instantiate(canonBall,CanonBallSpawn); // Get instande and add Force
        myCanonBall.GetComponent<Rigidbody2D>().AddForce(v_force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);
        f_currentTime = 0;
        b_start = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(detectRangePosition.position, detectRange);
    }
}
