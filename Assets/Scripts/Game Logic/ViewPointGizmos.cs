using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPointGizmos : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.DrawSphere(transform.position, 0.4f);
    }
}