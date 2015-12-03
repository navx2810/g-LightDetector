using UnityEngine;
using System.Collections;

public class Probe : MonoBehaviour
{
    public float size;

    public bool IsInsideLight;

    public void OnTriggerExit(Collider other)
    {
        IsInsideLight = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        IsInsideLight = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = IsInsideLight ? Color.blue : Color.white;
        Gizmos.DrawWireCube(transform.position, Vector3.one * size);
    }
}
