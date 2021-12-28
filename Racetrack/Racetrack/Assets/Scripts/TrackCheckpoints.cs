using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackCheckpoints : MonoBehaviour
{
    public event EventHandler OnCorrectCheckpoint;
    public event EventHandler OnWrongCheckpoint;

    private List<CheckpointHandler> checkpointList;
    private int nextCheckpoint = 0;

    [SerializeField]
    private GameObject gm;


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

            if (nextCheckpoint == 3)
            {
                gm.GetComponent<GameManager>().enabled = false;
                GetComponent<TrackCheckpoints>().enabled = false;
                SceneManager.LoadScene("Main");
                gm.GetComponent<GameManager>().enabled = true;
                GetComponent<TrackCheckpoints>().enabled = true;

            }
            else
            {
                nextCheckpoint++;
                OnCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
            }
            


        }
        else
        {
            print("wrong");
            OnWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }
}
