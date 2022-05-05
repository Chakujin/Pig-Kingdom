using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxNormal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pieces;
    public GameObject diamond;
    public GameObject heart;
    public GameObject box;
    //public BoxCollider2D myCollider;
    public Animator myAnimation;
    public float timeQuitPieces;

    void Start()
    {
        
    }

    public void hited()
    {
        StartCoroutine(breackBox());

        //Drop items chance
        int i_heart = Random.Range(0,2);

        switch (i_heart)
        {
            case 0:
                break;

            case 1:
                GameObject instHeart = Instantiate(heart, transform);
                instHeart.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,3),ForceMode2D.Impulse);
                break;
            default:
                Debug.Log("Switch error");
                break;
        }

        int i_diamond = Random.Range(0,2);

        switch (i_diamond)
        {
            case 0:
                break;

            case 1:
                GameObject instDiamond = Instantiate(diamond, transform);
                instDiamond.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,3), ForceMode2D.Impulse);
                break;
            default:
                Debug.Log("Switch error");
                break;
        }
    }

    private IEnumerator breackBox()
    {
        FindObjectOfType<AudioManager>().Play("BoxBreack");
        myAnimation.SetTrigger("hit");
        yield return new WaitForSeconds(0.1f);
        box.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        //SpawnPieces
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].SetActive(true);

            float forcex = Random.Range(-3, 3);
            float forcey = Random.Range(0, 3);

            pieces[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex, forcey), ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(timeQuitPieces);

        foreach (GameObject pice in pieces)
        {
            pice.SetActive(false);
        }
    }
}
