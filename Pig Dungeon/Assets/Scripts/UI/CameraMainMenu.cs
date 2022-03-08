using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraMainMenu : MonoBehaviour
{
    public CinemachineVirtualCamera myCamera;
    public float positionMove1;
    public float positionMove2;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Change()
    {
        myCamera.transform.DOMoveX(positionMove1, 1f).SetEase(Ease.InSine).OnComplete(Change2);
    }
    private void Change2()
    {
        myCamera.transform.DOMoveX(positionMove2, 1f).SetEase(Ease.InSine).OnComplete(Change);
    }
}
