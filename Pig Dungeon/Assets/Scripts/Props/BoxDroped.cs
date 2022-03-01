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

        }
        if(collision.tag == "Background")
        {
            for(int i = 0; i < pieces.Length; i++)
            {
                pieces[i].SetActive(true);

                float forcex = Random.Range(-3,3);
                float forcey = Random.Range(0,3);

                pieces[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex,forcey), ForceMode2D.Impulse);
            }
        }
    }

    private IEnumerator hitPlayer;
}
