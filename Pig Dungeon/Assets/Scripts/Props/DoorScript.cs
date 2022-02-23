using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator myAnimator;
    public BoxCollider2D myColliderDoor;
    public bool spawnDoor;

    private void Awake()
    {
        if(spawnDoor == true)
        {
            myColliderDoor.enabled = false;
            myAnimator.SetTrigger("DoorTest");
            //StartCoroutine(openMyDoor());
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    //Enter Door trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().doorTrigger = true;
        }
    }
    //Exit door trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().doorTrigger = false;
        }
    }

    private IEnumerator openMyDoor()
    {
        myAnimator.SetBool("DoorIn", true);
        yield return new WaitForSeconds(0.1f);
        myAnimator.SetBool("DoorIn", false);
    }
}
