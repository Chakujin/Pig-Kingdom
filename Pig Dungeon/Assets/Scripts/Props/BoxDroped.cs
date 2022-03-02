using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDroped : MonoBehaviour
{
    public Animator myAnimator;
    public GameObject box;
    public GameObject[] pieces;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(1);
            StartCoroutine(Breack());
        }
        if (collision.tag == "Background")
        {
            StartCoroutine(Breack());
        }
    }

    private IEnumerator Breack()
    {
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
