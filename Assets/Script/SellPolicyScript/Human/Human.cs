using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
	//simple data field
	public float disposition;
	public State state
	//complex data field

	//enum field
	public enum State
	{
		Customer,
		Thief
	};

	//initialize this script
	void Start ()
	{
	
	}

}
