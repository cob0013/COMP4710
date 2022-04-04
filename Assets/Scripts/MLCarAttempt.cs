using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MLCarAttempt : Agent
{

    [SerializeField] private Transform targetTransorm;


    public override void OnEpisodeBegin()
    {

        transform.position = new Vector3(-100.95f, 1.26f, 8.38f);
        transform.eulerAngles = new Vector3(0, 90f, 0);

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransorm.position);
        
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[1];
        float moveZ = vectorAction[0];

        float moveSpeed = 5f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

    }



    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[1] = Input.GetAxisRaw("Vertical");
        actionsOut[0] = -Input.GetAxisRaw("Horizontal");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<RewardWall>(out RewardWall reward))
        {
            SetReward(1f);
        }
        if (other.TryGetComponent<BadWall>(out BadWall wall))
        {
            AddReward(-1f);
            EndEpisode();
        }

    }   

}
