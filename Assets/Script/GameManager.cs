using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	//simple data fiele
	public float gameTime;
	public Vector2 mousePosition;

	//complex data field
	FarmManager farmStage;
	SellManager sellStage;
	public FarmUI farmUI;
	public SellUI sellUI;

	// Use this for initialization
	void Start( )
	{
		gameTime = 0.0f;
		LinkGameResource();
	}
	
	// Update is called once per frame
	void Update( )
	{
		mousePosition = Input.mousePosition;
		if (farmStage.CheckOnGame())
		{
			gameTime += Time.deltaTime;
			farmUI.GameTime = gameTime;
			farmStage.ProcessStageEvent( mousePosition );
			farmUI.LinkCropItem(farmStage.GetCropItem());
			//temp code for test
			sellUI.LinkCropItem( farmStage.GetCropItem().ToArray() );
		}

		if (sellStage.CheckOnGame())
		{
			gameTime += Time.deltaTime;
			sellUI.GameTime = gameTime;
			sellStage.ProcessStageEvent( mousePosition );
			sellUI.LinkCropItem( sellStage.GetCropItem().ToArray() );
		}	
	}

	//property
	public float GameTime
	{
		get { return gameTime; }
	}

	//another method
	void LinkGameResource( )
	{
		//link stage data
		farmStage = GetComponent<FarmManager>();
		sellStage = GetComponent<SellManager>();

		//link UI data
		GameObject temp = GameObject.FindGameObjectWithTag( "FarmCanvas" );
		farmUI = temp.GetComponent<FarmUI>();

		GameObject temp2 = GameObject.FindGameObjectWithTag( "SellCanvas" );
		sellUI = temp2.GetComponent<SellUI>();
	}

	//move crop item data (farm to sell)
	void TransferCropItem( )
	{
		sellStage.SetCropItem( farmStage.GetCropItem().ToArray() );
	}

}
