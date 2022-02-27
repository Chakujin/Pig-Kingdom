using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public Animator myAnimator;
    public CircleCollider2D myCollider;

    private bool b_stay = false;
    private Vector3 v_lastpost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (b_stay == true)
        {
            transform.position = v_lastpost;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            v_lastpost = transform.position;
            b_stay = true;
            collision.GetComponent<PlayerMovement>().TakeDamage(1);
            myAnimator.SetTrigger("Explote");
            StartCoroutine(Collision());
        }
        if(collision.tag == "Background")
        {
            v_lastpost = transform.position;
            b_stay = true;
            myAnimator.SetTrigger("Explote");
            StartCoroutine(Collision());
        }
    }

    private IEnumerator Collision()
    {
        myCollider.enabled = false;
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
}
