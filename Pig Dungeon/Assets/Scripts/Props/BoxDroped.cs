using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDroped : MonoBehaviour
{
    public Animator myAnimator;
    public GameObject box;
    public GameObject[] pieces;
    public BoxCollider2D mycollider;
    public Transform boxTransform;
    private Vector3 m_currentPosition;
    private bool b_hited = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(b_hited == true)
        {
            boxTransform.position = m_currentPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_currentPosition = transform.position;  // dont move position
            b_hited = true;
            mycollider.enabled = false;

            collision.GetComponent<PlayerMovement>().TakeDamage(1);
            StartCoroutine(Breack());
        }
        if (collision.tag == "Background")
        {
            m_currentPosition = transform.position;  // dont move position
            b_hited = true;
            mycollider.enabled = false;
            StartCoroutine(Breack());
        }
    }

    private IEnumerator Breack()
    {
        FindObjectOfType<AudioSource>().Play();
        myAnimator.SetTrigger("hit");
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].SetActive(true);

            float forcex = Random.Range(-3, 3);
            float forcey = Random.Range(0, 3);

            pieces[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex, forcey), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.1f);
        box.SetActive(false);
        yield return new WaitForSeconds(4f);
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].SetActive(false);
        }
    }
}
