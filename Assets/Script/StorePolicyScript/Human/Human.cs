﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Human : MonoBehaviour
{
	//simple data field
	public float moveSpeed;
	public float interpolate;
	public float disposition;
	public State state;
	public Transform target;
	public bool onShopping;
	public bool onMove;
	public bool onBargain;
	public float bargainPrice;

	//complex data field
	StoreManager manager;
	public Image popUpImage;
	public Sprite[] image;
	public StoreFieldPolicy presentStore;

	//enum field
	public enum State
	{
		Default,
		Customer,
		Thief}

	;

	//constructor - no parameter
	public Human()
	{
		moveSpeed = 0f;
		disposition = 0.0f;
		state = State.Default;
		onShopping = false;
		onMove = true;
		onBargain = false;
		bargainPrice = 0;
	}
	//constructor - self parameter
	public Human( Human data )
	{
		moveSpeed = data.moveSpeed;
		disposition = data.disposition;
		state = data.state;
	}

	//initialize this script
	void Start( )
	{
		onShopping = false;
		onMove = true;
		onBargain = false;
		manager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<StoreManager>();
		popUpImage = transform.Find( "PopUpHuman" ).GetComponent<Image>();
		popUpImage.enabled = false;
		target = GameObject.FindGameObjectWithTag( "TestObject" ).GetComponent<Transform>();
		moveSpeed = 5f;
	}

	void Update( )
	{
		if (onMove && !onShopping)
		{
			interpolate = (target.position.sqrMagnitude / transform.position.sqrMagnitude) * 0.01f;
			transform.position = (Vector3.Lerp( transform.position, target.position, moveSpeed * interpolate ));
		}
		else if (onMove && onShopping)
		{
			transform.position = (Vector3.Lerp( transform.position, target.position, moveSpeed * 0.0005f ));
		}

		if (onShopping)
			target = GameObject.FindGameObjectWithTag( "StartPos" ).GetComponent<Transform>();
	}

	//property
	public bool OnBargain
	{
		get { return onBargain; }
	}

	//another method

	//Human move by target
	public void MoveTarget( )
	{

	}

	//Human move by outside
	public void MoveOutside( )
	{

	}

	//collision at store field
	void OnCollisionEnter( Collision col )
	{
		presentStore = col.gameObject.GetComponent<StoreFieldPolicy>();

		if (presentStore != null)
		{
			if (presentStore.OnStore)
			{
				//thief policy
				if (state == State.Thief)
					StealCrop( presentStore );

				//customer policy
				if (state == State.Customer)
					BuyCrop( presentStore );				
			}
		}
	}

	//thief policy method
	public void StealCrop( StoreFieldPolicy field )
	{
		field.StealCrop();
		popUpImage.enabled = true;
		popUpImage.sprite = image[1];
		//drawing canvas for steal
		onShopping = true;
	}

	//customer policy method
	public void BuyCrop( StoreFieldPolicy field )
	{
		Crop[] cropData = manager.GetCropGroup();
		float averagePrice = 0.0f;

//		for (int i = 0; i < cropData.Length; i++)
//		{
		if (cropData[0].Name == field.PresentItem.Name)
		{
			averagePrice = cropData[0].GetAveragePrice( field.PresentItem.Rank );
		}
//		}

		//if no data in average price -> no buy
		if (field.PresentItem.Price <= disposition * averagePrice)
		{
			Debug.Log( "Enter Buy Crop 1st if" );
			field.SoldOutCropItem();
			onShopping = true;
		}
		else if (disposition * averagePrice <= (field.PresentItem.Price) * 0.95f)
		{
			bargainPrice = (disposition * averagePrice) * 0.95f;
			onBargain = true;
			onMove = false;
			popUpImage.enabled = true;
			popUpImage.sprite = image[0];
		}
		else
		{
			//no buy
			Debug.Log( "Enter No Buy" );
			onShopping = true;
		}
	}

	public void SuccessBargain( )
	{		
		popUpImage.enabled = false;
		presentStore.BargainSoldOutCrop( (int)bargainPrice );
		onShopping = true;
		onMove = true;
	}

	public void FailureBargain( )
	{
		popUpImage.enabled = false;
		onShopping = true;
		onMove = true;
	}

	//get / set method
	public int BargainPrice( )
	{
		return (int)bargainPrice;
	}

}
