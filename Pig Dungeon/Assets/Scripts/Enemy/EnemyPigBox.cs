using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPigBox : MonoBehaviour
{
    public Animator boxAnimator;
    public Transform Boxransform;
    public float detectRange;
    public LayerMask TargetPlayer;
    public GameObject box;
    public GameObject pigInside;

    public GameObject[] boxPice;

    private bool b_open = false;
    private float f_currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_currentTime += Time.deltaTime;

        if(f_currentTime >= 3f)
        {
            f_currentTime = 0;
            boxAnimator.SetTrigger("loock");
        }
    }
    private void FixedUpdate()
    {
        Collider2D[] detectPlayer = Physics2D.OverlapCircleAll(Boxransform.position, detectRange, TargetPlayer);

        if (b_open == false)
        {
            foreach (Collider2D player in detectPlayer)
            {
                b_open = true;
                StartCoroutine(openBox());
            }
        }
    }

    private IEnumerator openBox()
    {
        boxAnimator.SetTrigger("Detected");
        yield return new WaitForSeconds(0.2f);
        box.SetActive(false);

        for (int i = 0; i < boxPice.Length; i++)

        {
            boxPice[i].SetActive(true);
            int randomx = Random.Range(-5, 5);
            int randomy = Random.Range(0, 3);
            boxPice[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(randomx, randomy), ForceMode2D.Impulse);
        }

        pigInside.SetActive(true);

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < boxPice.Length; i++)

        {
            boxPice[i].SetActive(false);
        }
        this.enabled = false; //Eneable Script
    }
}
