using UnityEngine;
using System.Collections;

public class Customer : Human
{
	//constructor - no parameter
	public Customer()
	{
		moveSpeed = 5f;
		disposition = Random.Range( 0.9f, 1.1f );
		state = State.Customer;
	}
}


