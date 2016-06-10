using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
	//simple data field
	public float moveSpeed;
	public float disposition;
	public int money;
	public State state;

	//complex data field

	//enum field
	public enum State
	{
		Default,
		Customer,
		Thief
	};

	//constructor - no parameter
	public Human()
	{
		moveSpeed = 0f;
		disposition = 0.0f;
		money = 0;
		state = State.Default;
	}
	//constructor - self parameter
	public Human(Human data)
	{
		moveSpeed = data.moveSpeed;
		disposition = data.disposition;
		money = data.money;
		state = data.state;
	}

	//initialize this script
	void Start ()
	{
	
	}

}
