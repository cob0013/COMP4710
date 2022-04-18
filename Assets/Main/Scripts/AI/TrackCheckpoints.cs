using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{

    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    private List<CheckPointSingle> checkPointsingleList;
    private int nextCheckpointSingleIndex;
   private void Awake() {
       Transform checkpointsTransform = transform.Find("Checkpoints");

       checkPointsingleList = new List<CheckPointSingle>();

       foreach (Transform checkpointSingleTransform in checkpointsTransform) {
           CheckPointSingle checkPointSingle = checkpointSingleTransform.GetComponent<CheckPointSingle>();
           checkPointSingle.SetTrackCheckpoints(this);


           checkPointsingleList.Add(checkPointSingle);
       }

       nextCheckpointSingleIndex = 0;
   }


    public void PlayerThroughCheckpoint(CheckPointSingle checkPointSingle) {
        if (checkPointsingleList.IndexOf(checkPointSingle) == nextCheckpointSingleIndex) {
            //Correct Checkpoint
            Debug.Log("Correct");
            nextCheckpointSingleIndex++;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else {
            //Wrong Checkpoint
            Debug.Log("Wrong");
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    

           
    }
}
