using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSingle : MonoBehaviour
{


  private TrackCheckpoints trackCheckpoints;
  private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent<UserCar>(out UserCar userCar)) {
          trackCheckpoints.PlayerThroughCheckpoint(this);
      }

  }

  public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints) {
      this.trackCheckpoints = trackCheckpoints;
  }

}
