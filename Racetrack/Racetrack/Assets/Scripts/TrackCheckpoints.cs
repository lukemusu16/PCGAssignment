using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    public event EventHandler OnCorrectCheckpoint;
    public event EventHandler OnWrongCheckpoint;

    private List<CheckpointHandler> checkpointList;
    private int nextCheckpoint = 0;

    private void Start()
    {
        Invoke("setup", 0.5f);
    }

    void setup()
    {
        Transform checkpointsTransform = GameObject.Find("Checkpoints").transform;

        checkpointList = new List<CheckpointHandler>();

        foreach (Transform checkpoint in checkpointsTransform)
        {
            print("boi");
            CheckpointHandler cp = checkpoint.GetComponent<CheckpointHandler>();
            cp.SetTrackCheckpoints(this);
            checkpointList.Add(cp);
        }
    }

    public void PassedCheckpoint(CheckpointHandler cp)
    {
        if (checkpointList.IndexOf(cp) == nextCheckpoint)
        {
            print("correct");
            nextCheckpoint++;
            OnCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            print("wrong");
            OnWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }
}
