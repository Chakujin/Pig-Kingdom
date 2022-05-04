using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public GameObject[] hearts;
    public PlayerMovement m_playermovement;
    [SerializeField] private int i_currentHeal;
    public Animator[] animatorHearts;

    private GameManager m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();

        i_currentHeal = m_playermovement.currentHealth;

        for (int i = 0; i<m_playermovement.currentHealth; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public void addHeart()
    {
        i_currentHeal = m_playermovement.currentHealth;
        m_gameManager.playerHeal = i_currentHeal;

        switch (i_currentHeal)
        {
            case 0:
                break;
            case 1:
                hearts[0].SetActive(true);
                break;
            case 2:
                hearts[1].SetActive(true);
                break;
            case 3:
                hearts[2].SetActive(true);
                break;
            default:
                Debug.Log("Peta");
                break;
        }
    }

    public void quitHeart()
    {
        i_currentHeal = m_playermovement.currentHealth;
        m_gameManager.playerHeal = i_currentHeal;

        if (i_currentHeal <= m_playermovement.maxHealth && i_currentHeal >= 0)
        {
            StartCoroutine(quitHearts(i_currentHeal));
        }
    }

    private IEnumerator quitHearts(int heal)
    {
        //Debug.Log("damage");
        animatorHearts[heal].SetTrigger("hit");
        yield return new WaitForSeconds(0.3f);
        hearts[heal].SetActive(false);
    }
}
