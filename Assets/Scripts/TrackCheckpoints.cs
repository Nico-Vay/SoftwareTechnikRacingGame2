using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckPointSingle checkPointSingle = checkpointSingleTransform.GetComponent<CheckPointSingle>();
            checkPointSingle.SetTrackCheckpoints(this);
        }
    }

    public void AgentThroughCheckpoint(CheckPointSingle checkpointSingle)
    {
        Debug.Log(checkpointSingle.transform.name);
    }
}
