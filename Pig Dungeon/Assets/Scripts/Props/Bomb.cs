using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D myRb;
    public LayerMask playerTrigger;
    public Animator myAnimator;
    public Transform colliderPosition;
    [SerializeField]private bool b_Start = false;

    public float radiusRange;
    private float f_currentTime = 0f;
    private bool b_boom = false;

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
            StartCoroutine(StartExplosion());
        }
    }

    private void FixedUpdate()
    {
        //If detect player explote 
        if (b_Start == true) {
            Collider2D[] playerHited = Physics2D.OverlapCircleAll(colliderPosition.position, radiusRange, playerTrigger);

            if (b_boom == false)
            {
                foreach (Collider2D player in playerHited)
                {
                    b_boom = true;
                    StartCoroutine(StartHitPlayer(player));
                }
            }
        }
    }

    private IEnumerator StartHitPlayer(Collider2D player)
    {
        //Debug.Log("hiteo");
        player.GetComponentInParent<PlayerMovement>().TakeDamage(1);
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator StartExplosion()
    {
        FindObjectOfType<AudioSource>().Play();

        CameraPlayer.Instance.ShakeCamera(3f, 0.25f); // ShakeCam
        myAnimator.SetTrigger("boom");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(colliderPosition.position, radiusRange);
    }
}
