using UnityEngine;
using System.Collections;

public class CarBehaviour2b : CarBehaviour {

	void Update()
	{
		//Read sensor values
		float leftSensor = LeftBD.GetLinearOutput();
		float rightSensor = RightBD.GetLinearOutput ();

		//Calculate target motor values
		m_LeftWheelSpeed = leftSensor * MaxSpeed;
		m_RightWheelSpeed = rightSensor * MaxSpeed;
	}
}