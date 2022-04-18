using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;



// public class MLCarAttempt : Agent
// {
//    [SerializeField] private TrackCheckpoints trackCheckpoints;
//    [SerializeField] private Transform spawnPosition;
//    private CarController carController;
//    private void Awake() {
//        carController = GetComponent<CarController>();
//    }
//    private void Start() {
//        trackCheckpoints.OnPlayerCorrectCheckpoint += TrackCheckpoints_OnCarCorrectCheckpoint;
//        trackCheckpoints.OnPlayerWrongCheckpoint += TrackCheckpoints_OnCarWrongCheckpoint;
//    }
//    private void TrackCheckpoints_OnCarWrongCheckpoint(object sender, System.EventArgs e) {
//        if (e.carTransform == transform) {
//            AddReward(-1f);
//        }
//    }
//   private void TrackCheckpoints_OnCarCorrectCheckpoint(object sender, System.EventArgs e) {
//        if (e.carTransform == transform) {
//            AddReward(1f);
//        }
//   }
//   public override void OnEpisodeBegin() {
//        transform.position = new Vector3(-100.95f, 1.26f, 8.38f);
//        trackCheckpoints.ResetCheckpoint(transform);
//     //    stop car
//    }

//    public override void CollectObservations(VectorSensor sensor) {
//        Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(transform).transform.forward;
//        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
//        sensor.AddObservation(directionDot);
//    }
//    public override void OnActionReceived(float[] vectorAction) {
//        float forwardAmount = 0f;
//        float turnAmount = 0f;
//        switch(vectorAction[0]) {
//            case 0: forwardAmount = 0f; break;
//            case 1: forwardAmount = +1f; break;
//            case 2: forwardAmount = -1f; break;
//        }

//       switch(vectorAction[1]) {
//           case 0: turnAmount = 0f; break;
//           case 1: turnAmount = +1f; break;
//           case 2: turnAmount = -1f; break;
//         }
//     }
  

//   public override void Heuristic(float [] actionsOut) {
//       int forwardAction = 0;
//       if (Input.GetKey(KeyCode.W)) forwardAction = 1;
//       if (Input.GetKey(KeyCode.S)) forwardAction = 2;
//       int turnAction = 0;
//       if (Input.GetKey(KeyCode.A)) turnAction = 1;
//       if (Input.GetKey(KeyCode.D)) turnAction = 2;
//        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
//        discreteActions[0] = forwardAction;
//        discreteActions[1] = turnAction;
//   }

//    private void OnCollisionEnter(Collision collision) {
//        if (collision.gameObject.TryGetComponent<BadWall>(out BadWall wall)) {
//            AddReward(-0.5f);
//        }
//    }


//     private void OnCollisionStay(Collision collision) {
//          if (collision.gameObject.TryGetComponent<BadWall>(out BadWall wall)) {
//             AddReward(-0.1f);
//         }
//     }
//  }




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