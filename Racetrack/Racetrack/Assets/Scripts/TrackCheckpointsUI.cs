using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpointsUI : MonoBehaviour
{
    [SerializeField] private TrackCheckpoints tc;

    private void Start()
    {
        tc.OnCorrectCheckpoint += tc_OnCorrectCheckpoint;
        tc.OnWrongCheckpoint += tc_OnWrongCheckpoint;
        gameObject.SetActive(false);
    }

    private void tc_OnCorrectCheckpoint(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void tc_OnWrongCheckpoint(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
