using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;

    [SerializeField] private TrackCheckpoints trackCheckpoints;

    private CarController carController;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        //targetTransform.localPosition = new Vector3(Random.Range(-105f, -110f), 0, Random.Range(103f, 113f));
        trackCheckpoints.ResetCheckpoints();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX =  actions.ContinuousActions[0];
        float moveZ =  actions.ContinuousActions[1];

        float moveSpeed = 8f;

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+10f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
        if (other.TryGetComponent<CheckPointSingle>(out CheckPointSingle checkPoint))
        {
            checkPoint.OnCheckpointEnter(this);
        }
    }
}
