using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Thief : Human
{
	//constructor - no parameter
	public Thief()
	{
		moveSpeed = 10f;
		state = State.Thief;
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
		moveSpeed = 10f;

		SetTarget();
		SetDisposition();
	}
}
