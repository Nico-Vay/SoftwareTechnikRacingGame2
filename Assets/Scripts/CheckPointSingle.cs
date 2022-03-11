using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSingle : MonoBehaviour
{

    private TrackCheckpoints trackCheckpoints;
    private MeshRenderer meshRenderer;
    private bool isCheckpointActive = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        Show();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MoveToGoalAgent>(out MoveToGoalAgent agent))
        {
            trackCheckpoints.AgentThroughCheckpoint(this);
        }
    }
    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }

    public void Show()
    {
        meshRenderer.enabled = true;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
    }

    public void OnCheckpointEnter(MoveToGoalAgent moveToGoalAgent)
    {
        if (isCheckpointActive)
        {
            moveToGoalAgent.AddReward(+1f);
            isCheckpointActive = false;
        }
    }

    public void Reset()
    {
        isCheckpointActive = true;
    }
}
