using UnityEngine;
using System.Collections;

public class Thief : Human
{
	//constructor - no parameter
	public Thief()
	{
		moveSpeed = 10f;
		disposition = 0.0f;
		state = State.Thief;
	}
}
