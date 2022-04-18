// Copyright (c)2017 Reece Robinson. All rights reserved.

using UnityEngine;
using System.Collections;

public class TrafficLightController : MonoBehaviour {

	// This script is responsible for switching out the emission texture for the traffic light lamp illumination.

	// The five texture slots correspond to the automation states needed for traffic light phase control.
	public Texture walkEmissionTexture;
	public Texture dontwalkEmissionTexture;
	public Texture StopEmissionTexture;
	public Texture WarnEmissionTexture;
	public Texture GoEmissionTexture;

	public bool lightsEnabled= true; // You can disable a light if requried.
	public float flashInterval = 5.0f; // When lights are flashing this value is the flash period in seconds.

	private Material mat;
	public float stopTime = 6f;
	public float goTime = 6f;
	public float warnTime = 2f;
	private float timer;
	private bool toggleOn= true;
	private PhaseState currentState;
	public PhaseState startPhase;
	public WallScript wallScr;

	void Awake()
    {
		wallScr = GetComponentInChildren<WallScript>();
    }

	void  Start (){
		mat = GetComponent<Renderer>().material;
		timer = 0;
		currentState = startPhase;
	}

	void  Update (){
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			if (currentState == PhaseState.Stop)
			{
				// toggleOn = !toggleOn;
				ApplyState(PhaseState.Go);
				timer = goTime;
				
			}
			else if (currentState == PhaseState.Go)
			{
				ApplyState(PhaseState.Warn);
				timer = warnTime;
			}
			else if (currentState == PhaseState.Warn)
			{
				ApplyState(PhaseState.Stop);
				timer = stopTime;
			}
			
		}
	}

	void  ApplyEmissionTexture ( Texture texture  ){
		mat.SetTexture("_EmissionMap", texture);
		mat.SetColor("_EmissionColor", lightsEnabled?Color.white:Color.black); // Use this to turn off the lights as necessary
		mat.EnableKeyword("_EMISSION");
	}

	void  ApplyState ( PhaseState newState  ){
		currentState = newState;
		lightsEnabled = true;
		switch (newState) {
		case PhaseState.Walk:
			if(walkEmissionTexture != null ) {
				ApplyEmissionTexture(walkEmissionTexture);
				wallScr.ColorChange(Color.green);
			}
			break;
		case PhaseState.DontWalk:
			if(dontwalkEmissionTexture != null) {
				ApplyEmissionTexture(dontwalkEmissionTexture);
				
			}
			break;
		case PhaseState.Stop:
			if(StopEmissionTexture != null) {
				ApplyEmissionTexture(StopEmissionTexture);
				wallScr.ColorChange(Color.red);
				}
			break;
		case PhaseState.Warn:
			if(WarnEmissionTexture != null) {
				ApplyEmissionTexture(WarnEmissionTexture);
				
				}
			break;
		case PhaseState.Flash:
			if(!toggleOn) {
				lightsEnabled = false;
			}
			if(WarnEmissionTexture != null) {
				ApplyEmissionTexture(WarnEmissionTexture);
			}
			break;
		case PhaseState.Go:
			if(GoEmissionTexture != null) {
				ApplyEmissionTexture(GoEmissionTexture);
				wallScr.ColorChange(Color.green);
				}
			break;
		case PhaseState.Off:
			lightsEnabled = false;
			if(StopEmissionTexture != null) {
				ApplyEmissionTexture(StopEmissionTexture);
			}
			break;
		default:
			Debug.Log ("Unknown Traffic Light State");
			break;
		}
	}
}