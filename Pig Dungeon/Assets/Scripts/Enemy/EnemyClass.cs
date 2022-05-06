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

    public GameObject Dialog;
    public Animator DialogAnim;

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("HitEnemy");

        currentHealth -= damage;
        StartCoroutine(PigDamage());
    }
    public void Dead()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDie");
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

        if (detectHitLeft == true)
        {
            mySpriteRenderer.flipX = true;
        }
        else if (detectHitRigth == true)
        {
            mySpriteRenderer.flipX = false;
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
    public IEnumerator StartDialog()
    {
        Dialog.SetActive(true);
        yield return new WaitForSeconds(2f);
        DialogAnim.SetTrigger("Stop");
        yield return new WaitForSeconds(0.5f);
        Dialog.SetActive(false);
    }
}
