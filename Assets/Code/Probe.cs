using UnityEngine;
using System.Collections;

public class Probe : MonoBehaviour
{
    public float size;

    public bool IsInsideLight;

    // This is used to keep track of the collider that first entered the object incase multiple lights try hitting the probe. The probe only needs one hit afterall
    // This solves the problem of multiple light overlaps and when you duplicate and move the light around
    private Collider target;

    public void OnTriggerExit(Collider other)
    {
        if (target != other) { return; }

        IsInsideLight = false;
        target = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (target != null) { return; }

        IsInsideLight = true;
        target = other;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = IsInsideLight ? Color.blue : Color.white;
        Gizmos.DrawWireCube(transform.position, Vector3.one * size);
    }
}
