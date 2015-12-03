using UnityEngine;
using System.Collections;
using System;

public class ProbeGenerator : MonoBehaviour
{

    #region Exposed Properties
    [Tooltip("The dimensions of the individual light probes\nThe higher the value, the more demanding the system is")]
    public float ProbeSize;

    [Tooltip("Check this option to use box colliders instead of sphere colliders for more accurate measurements")]
    public bool UseBoxColliders;

    [Tooltip("This option will ignore the ProbeSize property and determine the best probe size to fit within the bounds")]
    public bool UseBestFit;

    [Tooltip("The layer for probes")]
    public LayerMask ProbeLayer;
    #endregion

    #region Exposed Debug Components
    public Vector3 AreaSize;

    public Vector3 StartLocation, EndLocation;

    public int HeightCount, WidthCount;
    #endregion

    #region Components
    BoxCollider area;
    #endregion

    void Start()
    {
        if(UseBestFit) { DetermineBestFit(); }

        area = GetComponent<BoxCollider>();
        AreaSize = area.size;
        AreaSize.y = 1;
        var current_location = transform.position;

        // Get the locations, these display in the SCENE view as colored sphers
        StartLocation = current_location - area.size / 2f;
        EndLocation = current_location + area.size / 2f;

        // Calculate how many can actually fit in here
        WidthCount = (int)Mathf.Floor(AreaSize.x / ProbeSize);
        HeightCount = (int)Mathf.Floor(AreaSize.z / ProbeSize);

        // Offset for the bounds, otherwise the colliders would start in the center of the start location, meaning outside the box
        var offset = new Vector3(ProbeSize / 2, 1, ProbeSize / 2);
        

        for(var y = 0; y < HeightCount; y++)
            for(var x = 0; x < WidthCount; x++)
                CreateProbe(offset, y, x);
            
    }

    private void DetermineBestFit()
    {
        // Take the given size (or use a default value if they left it at 0) and the bounds of the area
        // If the size can divide evenly, it's the best fit
        // Otherwise GROW the size until it fits (growing is less costly to devices, maybe make it another option for the developer to choose growing or shrinking?)
    }

    private void CreateProbe(Vector3 offset, int y, int x)
    {
        var go = new GameObject("Probe: "+x+", "+y);

        // Make the probe a child of the "this" in the hierarchy
        go.transform.parent = transform;

        go.transform.position = StartLocation + offset + new Vector3(ProbeSize * x, 1, ProbeSize * y);

        // The layer is Layer 9 or Probe layer, Check the Project Settings > Physics to see the collision
        // Lights collide with Probes, but Probes don't collide with Probes
        // LayerMask should work here right? Couldn't get ProbeLayer to work.
        go.layer = 9;

        // Rigidbodies are needed to detect collisions, If you can guarentee a light will have a RB then the probe doesn't need one
        // Unless I'm missing something here, I could have sworn two Triggers could, well, trigger without having to have a rigidbody
        var rb = go.AddComponent<Rigidbody>();
        // Disable the physics system from using this probe
        rb.isKinematic = true;

        var col = go.AddComponent<BoxCollider>();
        col.size = Vector3.one * ProbeSize;         // instead you could use new Vector3(ProbeSize, 1f, ProbeSize) if you don't need the height value
        col.isTrigger = true;

        var probe = go.AddComponent<Probe>();
        probe.size = ProbeSize;
    }

    void OnDrawGizmos()
    {
        // Note: these only show up in the scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(StartLocation, 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(EndLocation, 1f);
    }

}
