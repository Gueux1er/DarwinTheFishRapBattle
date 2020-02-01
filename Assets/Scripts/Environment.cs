using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + startPosition, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + endPosition, 0.4f);
    }
}
