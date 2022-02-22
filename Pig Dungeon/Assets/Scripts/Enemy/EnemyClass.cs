using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public bool die = false;
    public bool move = true;
    public bool detectHitRigth = false;
    public bool detectHitLeft = false;

    public Animator EnemyAnimator;
    public SpriteRenderer mySpriteRenderer;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(PigDamage());
    }
    public void Dead()
    {
        EnemyAnimator.SetBool("IsDead", true);
        die = true;
        move = false;
        StartCoroutine(Die());
    }

    public IEnumerator PigDamage()
    {
        move = false;
        EnemyAnimator.SetBool("Move", false);
        EnemyAnimator.SetTrigger("Hurt");

        //Impulse (Dont Work with AddForce)
        if (detectHitLeft == true)
        {
            mySpriteRenderer.flipX = true;
            //Debug.Log("ForceLeft");
        }
        else if (detectHitRigth == true)
        {
            mySpriteRenderer.flipX = false;
            //Debug.Log("ForceRigth");
        }

        if (currentHealth <= 0)
        {
            Dead();
        }
        yield return new WaitForSeconds(2f);
        move = true;
    }
    public IEnumerator Die()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
