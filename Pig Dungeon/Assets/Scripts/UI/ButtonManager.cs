using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    private GameManager m_gameManager;
    public GameObject OptionMenu;
    public GameObject[] MainButtons;
    public Button continueButton;

    //Animations
    public Ease easing;
    public float distanceMove;
    public float timeMove;
    [SerializeField] private List <Vector3> m_StartPositionButtons;
    public RectTransform[] m_PositionButtons;

    private void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        OptionMenu.SetActive(false);

        if (m_gameManager.startGame == false)
        {
            continueButton.interactable = false;
        }

        //Save early positions
        for (int i = 0; i < MainButtons.Length; i++)
        {
            m_StartPositionButtons.Add(MainButtons[i].GetComponent<RectTransform>().anchoredPosition);
        }

        //Move
        foreach (RectTransform updatePos in m_PositionButtons)
        {
            updatePos.localPosition = new Vector3 (distanceMove, updatePos.localPosition.y, updatePos.localPosition.z);
        }
    }
    private void Start()
    {
        StartCoroutine(AnimateButtons());
    }

    //Voids to call
    public void Contiune()
    {
        m_gameManager.loadGame();

        SceneManager.LoadScene(m_gameManager.level);
    }

    public void NewGame()
    {
        m_gameManager.level = 1;
        m_gameManager.startGame = true;
        m_gameManager.saveGame();
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        OptionMenu.SetActive(true);

        foreach(GameObject buttons in MainButtons)
        {
            buttons.SetActive(false);
        }
    }

    public void BackOptions()
    {
        OptionMenu.SetActive(false);
        foreach (GameObject buttons in MainButtons)
        {
            buttons.SetActive(true);
        }
    }

    private IEnumerator AnimateButtons()
    {
        //Animate
        for (int i = 0; i < MainButtons.Length; i++)
        {
            m_PositionButtons[i].DOAnchorPosX(m_StartPositionButtons[i].x, timeMove).SetEase(easing);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
