using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    public event EventHandler OnAgentCorrectCheckpoint;
    public event EventHandler OnAgentWrongCheckpoint;

    private List<CheckPointSingle> checkPointSingleList;
    private int nextCheckpointsingleIndex;
    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkPointSingleList = new List<CheckPointSingle>();

        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckPointSingle checkPointSingle = checkpointSingleTransform.GetComponent<CheckPointSingle>();

            checkPointSingle.SetTrackCheckpoints(this);

            checkPointSingleList.Add(checkPointSingle);
        }

        nextCheckpointsingleIndex = 0;
    }

    public void AgentThroughCheckpoint(CheckPointSingle checkpointSingle)
    {
        if (checkPointSingleList.IndexOf(checkpointSingle) == nextCheckpointsingleIndex)
        {
            //CheckPointSingle correctCheckpointSingle = checkPointSingleList[nextCheckpointsingleIndex];
            //correctCheckpointSingle.Hide();
            //Correct Checkpoint
            nextCheckpointsingleIndex = nextCheckpointsingleIndex + 1 % checkPointSingleList.Count;
            OnAgentCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //Wrong
            OnAgentWrongCheckpoint?.Invoke(this, EventArgs.Empty);

            //CheckPointSingle correctCheckpointSingle = checkPointSingleList[nextCheckpointsingleIndex];
            //correctCheckpointSingle.Show(); 
        }
    }
}
