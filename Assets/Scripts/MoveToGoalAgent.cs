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

    public Rigidbody agentRigidbody;

    public float _moveSpeed;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-0f, 0f), 0, Random.Range(-0f, 0f));
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
        int moveX =  actions.DiscreteActions[0];//0 = Don't move; 1 = Left; 2 = Right;
        int moveZ =  actions.DiscreteActions[1];//0 = Don't move; 1 = Back; 2 = Forward;

        Vector3 addForce = new Vector3(0, 0, 0);

        switch (moveX)
        {
            case 0: addForce.x = 0f; break;
            case 1: addForce.x = -1f; break;
            case 2: addForce.x = +1f; break;
        }

        switch (moveZ)
        {
            case 0: addForce.z = 0f; break;
            case 1: addForce.z = -1f; break;
            case 2: addForce.z = +1f; break;
        }

        float moveSpeed = _moveSpeed;

        agentRigidbody.velocity = addForce * moveSpeed + new Vector3(0, agentRigidbody.velocity.y, 0);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: discreteActions[0] = 1; break;
            case  0: discreteActions[0] = 0; break;
            case +1: discreteActions[0] = 2; break;
        }

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: discreteActions[1] = 1; break;
            case  0: discreteActions[1] = 0; break;
            case +1: discreteActions[1] = 2; break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+20f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-5f);
            EndEpisode();
        }
        if (other.TryGetComponent<CheckPointSingle>(out CheckPointSingle checkPoint))
        {
            checkPoint.OnCheckpointEnter(this);
        }
    }
}
