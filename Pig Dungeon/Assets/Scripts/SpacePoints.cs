using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePoints : MonoBehaviour
{
    public Color color = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
