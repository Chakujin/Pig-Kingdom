using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePointMove : MonoBehaviour
{
    public GameObject[] mySpacePoint;
    public GameObject pigTarget;
    private Transform f_startpos;
    // Start is called before the first frame update
    void Start()
    {
        f_startpos = pigTarget.transform;
        foreach (GameObject pos in mySpacePoint)
        {
            pos.transform.position = new Vector2(pos.transform.position.x, f_startpos.transform.position.y);
        }
    }
}
