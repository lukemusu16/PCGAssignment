using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    
    public TrackCheckpoints tc;

    private void Start()
    {
        tc = GetComponentInParent<TrackCheckpoints>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<BoxCollider>())
        {
            
            tc.PassedCheckpoint(this);
            
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints tc)
    {
        this.tc = tc;
    }
}
