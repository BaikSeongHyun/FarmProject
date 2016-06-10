using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	//simple data fiele
	public float gameTime;
	public Vector2 mousePosition;

	//complex data field
	CropItem[] cropData;
	FarmManager farmStage;
	SellManager sellStage;
	public FarmUI farmUI;
	public SellUI sellUI;

	// Use this for initialization
	void Start( )
	{
		gameTime = 0.0f;
		LinkGameResource();

		//sleep farm
		farmStage.enabled = false;
		farmUI.SleepCanvas();
		farmUI.enabled = false;

		//sleep store
		sellStage.enabled = false;
		sellUI.SleepCanvas();
		sellUI.enabled = false;
	}
	
	// Update is called once per frame
	void Update( )
	{
		mousePosition = Input.mousePosition;
		if (farmStage.CheckOnGame())
		{
			gameTime += Time.deltaTime;
			farmUI.SetGameTime( gameTime );
			farmStage.ProcessStageEvent( mousePosition );
			farmUI.LinkCropItem( farmStage.GetCropItem() );
		}

		if (!sellStage.PlaceComplete)
		{
//			gameTime += Time.deltaTime;
//			sellUI.SetGameTime( gameTime );
			sellStage.ProcessStageEvent( mousePosition );
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
		cropData = farmStage.GetCropItem().ToArray();
	}

	//game start / end policy
	//farm
	public void StartFarmGame( )
	{
		//mainUI.enabled = false;
		farmStage.enabled = true;
		farmUI.enabled = true;
		farmUI.WakeUpCanvas();
		farmStage.StartFarmGame();
	}

	public void EndFarmGame( )
	{
		farmStage.EndFarmGame();
		farmStage.enabled = false;
		farmUI.SleepCanvas();
		farmUI.enabled = false;
		gameTime = 0.0f;
	}

	//sell
	public void StartSellGame( )
	{
		sellStage.enabled = true;
		sellUI.enabled = true;
		sellUI.WakeUpCanvas();
		sellUI.LinkCropItem( farmStage.GetCropItem().ToArray() );
		sellStage.StartSellGame();

	}
	public void EndSellGame( )
	{
		sellStage.EndSellGame();
		sellStage.enabled = false;
		sellUI.SleepCanvas();
		sellUI.enabled = false;
		gameTime = 0.0f;
		//mainUI.enabled = true;
	}




}
