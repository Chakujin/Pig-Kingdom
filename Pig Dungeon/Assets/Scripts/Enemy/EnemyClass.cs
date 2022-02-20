using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Animator EnemyAnimator;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        EnemyAnimator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        StartCoroutine(Die());

        EnemyAnimator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
