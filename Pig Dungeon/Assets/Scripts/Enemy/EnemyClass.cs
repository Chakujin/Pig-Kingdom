using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool die = false;
    public bool move = true;
    public Animator EnemyAnimator;
    //public Rigidbody2D myRb;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(PigDamage());

    }
    public void Dead()
    {
        StartCoroutine(Die());

        EnemyAnimator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        die = true;
    }

    public IEnumerator PigDamage()
    {
        move = false;
        EnemyAnimator.SetBool("Move", false);
        EnemyAnimator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Dead();
        }
        yield return new WaitForSeconds(2f);
        move = true;
    }
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
