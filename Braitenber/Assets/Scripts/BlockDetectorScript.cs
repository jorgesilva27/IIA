using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class BlockDetectorScript : MonoBehaviour {

	public float angle;
	public float strength;
	public int numObjects;
	public GameObject closestObject;
	private bool useAngle = true;

	void Start () {
		strength = 0;
		numObjects = 0;
		if (angle >= 360) {
			useAngle = false;
		}
	}

	void Update () {
		strength = 0;
		GetClosestBlock ();
	}

	// Get linear output value
	public float GetLinearOutput()
	{
		return strength;
	}

	// Get gaussian output value
	public virtual float GetGaussianOutput()
	{
		throw new NotImplementedException ();
	}

	// Returns all "Block" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllBlocks()
	{
		return GameObject.FindGameObjectsWithTag ("Block");
	}


	// Returns all "Block" tagged objects that are within the view angle of the Sensor. 
	// Only considers the angle over the y axis. Does not consider objects blocking the view.
	GameObject[] GetVisibleBlocks()
	{
		ArrayList visibleBlocks = new ArrayList();
		float halfAngle = angle / 2.0f;

		GameObject[] blocks = GameObject.FindGameObjectsWithTag ("Block");

		foreach (GameObject block in blocks) {
			Vector3 toVector = (block.transform.position - transform.position);
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			float angleToTarget = Vector3.Angle (forward, toVector);

			if (angleToTarget <= halfAngle) {
				visibleBlocks.Add (block);
			}
		}

		return (GameObject[])visibleBlocks.ToArray(typeof(GameObject));
	}


	// 
	public void GetClosestBlock(){
		float minDistance = float.MaxValue;
		GameObject[] blocks = GetAllBlocks ();
		GameObject[] visibleBlocks = GetVisibleBlocks ();
		numObjects = blocks.Length;
		float [] distancesToBlocks = new float[numObjects];
		for(int i=0;i<numObjects;i++) {
			distancesToBlocks [i] = Vector3.Distance (transform.position, blocks [i].transform.position);
			if (distancesToBlocks [i] < minDistance) {
				minDistance = distancesToBlocks [i];
				closestObject = blocks [i];
			}
		}
		if (useAngle == true) {
			for (int i = 0; i < numObjects; i++) {
				if (closestObject == visibleBlocks [i]) {
					strength += 1.0f / minDistance;
				}
			} 
		} else {
			if (numObjects > 0) {
				strength += 1.0f /minDistance;
			}
		}

	}




}

