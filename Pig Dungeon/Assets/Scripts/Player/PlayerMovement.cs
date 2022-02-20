using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float f_currentTime;

    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runspeed;

    private bool b_jump;
    private bool b_crouch;

    public Animator playerAnimator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    private int attackDamage = 10;
    public LayerMask enemyLayer;
    private float F_timeAttack = 0.7f;

    public int currentHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_currentTime += Time.deltaTime;
        //Movement
        f_horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

        if (Input.GetButtonDown("Jump"))
        {
            b_jump = true;
            playerAnimator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            b_crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            b_crouch = false;
        }

        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            if (f_currentTime >= F_timeAttack)
            {
                f_currentTime = 0f;
                Attack();
            }
        }

        //Animations
        playerAnimator.SetFloat("Speed",Mathf.Abs(f_horizontalMove));
    }

    private void FixedUpdate()
    {
        //Movement
        controller.Move(f_horizontalMove * Time.fixedDeltaTime, b_crouch, b_jump);
        b_jump = false;
    }

    public void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        Collider2D[] hitEnemyes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemyes)
        {
            enemy.GetComponent<EnemyPig>().TakeDamage(attackDamage);
            Debug.Log("hited");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player take damage");
    }

    //Animators
    public void OnLanding()
    {
        playerAnimator.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        playerAnimator.SetBool("IsCrouching", isCrouching);
    }

    //Draw Gizmos
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
