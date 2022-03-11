using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpointsUI : MonoBehaviour
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    private void Start()
    {
        trackCheckpoints.OnAgentCorrectCheckpoint += TrackCheckpoints_OnAgentCorrectCheckpoint;
        trackCheckpoints.OnAgentWrongCheckpoint += TrackCheckpoints_OnAgentWrongCheckpoint;

        Show();
    }

    private void TrackCheckpoints_OnAgentCorrectCheckpoint(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void TrackCheckpoints_OnAgentWrongCheckpoint(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
