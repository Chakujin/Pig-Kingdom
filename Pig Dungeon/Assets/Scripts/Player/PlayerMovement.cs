using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float f_currentTime;

    public CharacterController2D controller;
    private float f_horizontalMove = 0f;
    public float runspeed;

    private bool b_jump;
    private bool b_crouch;

    public Animator playerAnimator;

    private Transform t_spawnPosition;
    public Transform attackPoint;
    public Transform LeftDetector;
    public Transform RigthDetector;
    public Vector2 sizeDetectors;

    public float attackRange = 0.5f;
    private int attackDamage = 10;
    public LayerMask enemyLayer;
    private float F_timeAttack = 0.7f;

    public int currentHealth;
    public int maxHealth;

    [SerializeField]private bool b_isRigth;
    [SerializeField]private bool b_isLeft;

    //Door
    public bool doorTrigger = false;
    private bool UsingDoor = false;

    private void Awake()
    {
        t_spawnPosition = GameObject.FindGameObjectWithTag("SpawnPlayer").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        transform.position = t_spawnPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        f_currentTime += Time.deltaTime;

        //Movement
        if (UsingDoor == false) {

            f_horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

            if (Input.GetButtonDown("Jump"))
            {
                b_jump = true;
                playerAnimator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Crouch"))
            {
                b_crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                b_crouch = false;
            }

            //Door
            if (doorTrigger == true)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(OpenDoor());
                    UsingDoor = true;
                }
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
        }

        //Animations
        playerAnimator.SetFloat("Speed",Mathf.Abs(f_horizontalMove));
    }

    private void FixedUpdate()
    {
        Collider2D[] detectRigthEnemy = Physics2D.OverlapBoxAll(RigthDetector.position, sizeDetectors, 0, enemyLayer);
        Collider2D[] detectLeftEnemy = Physics2D.OverlapBoxAll(LeftDetector.position, sizeDetectors, 0, enemyLayer);

        foreach (Collider2D detectEnemyRigth in detectRigthEnemy)
        {
            b_isRigth = true;
            b_isLeft = false;
        }
        foreach (Collider2D detectEnemyLeft in detectLeftEnemy)
        {
            b_isRigth = false;
            b_isLeft = true;
        }

        //Movement
        controller.Move(f_horizontalMove * Time.fixedDeltaTime, b_crouch, b_jump);
        b_jump = false;
    }

    #region ATTACKS

    public void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        Collider2D[] hitEnemyes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemyes)
        {
            enemy.GetComponent<EnemyPig>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerAnimator.SetTrigger("isHited");

        if (b_isRigth == true)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(20,5),ForceMode2D.Impulse);
        }else if (b_isLeft == true)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(transform.position.x,transform.position.y), ForceMode2D.Impulse);
        }

        if (currentHealth >= 0)
        {
            //Make Die
        }
    }
    #endregion
    #region ANIMATIONS
    //Animators
    public void OnLanding()
    {
        playerAnimator.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        playerAnimator.SetBool("IsCrouching", isCrouching);
    }

    private IEnumerator OpenDoor()
    {
        playerAnimator.SetBool("GoDoor",true);
        Debug.Log("OpenDoor");
        yield return new WaitForSeconds(3f);
    }

    #endregion

    //Draw Gizmos
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
