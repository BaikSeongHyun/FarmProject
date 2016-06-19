using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Customer : Human
{
	//constructor - no parameter
	public Customer()
	{
		moveSpeed = 5f;
		state = State.Customer;
	}
	
	//another method
	public override void DataSetUp()
	{
		onShopping = false;
		onMove = true;
		onBargain = false;
		manager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<StoreManager>();
		popUpImage = transform.Find( "PopUpHuman" ).GetComponent<Image>();
		popUpImage.enabled = false;
		moveSpeed = 5f;
		
		SetTarget();
		SetDisposition();
	}
}