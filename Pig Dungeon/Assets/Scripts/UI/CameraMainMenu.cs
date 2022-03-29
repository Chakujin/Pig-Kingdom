using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraMainMenu : MonoBehaviour
{
    public Camera myCamera;
    public float positionMove1;
    public float positionMove2;
    public float time;
    public Ease easing;

    // Start is called before the first frame update
    void Start()
    {
        Change();
    }

    private void Change()
    {
        myCamera.transform.DOMoveX(positionMove1, time).SetEase(easing).OnComplete(Change2);
    }
    private void Change2()
    {
        myCamera.transform.DOMoveX(positionMove2, time).SetEase(easing).OnComplete(Change);
    }
}
