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
	StoreManager storeStage;
	public FarmUI farmUI;
	public StoreUI storeUI;

	// Use this for initialization
	void Start( )
	{
		gameTime = 0.0f;
		LinkGameResource();

		//sleep farm
		farmUI.SleepCanvas();
		farmUI.enabled = false;

		//sleep store
		storeUI.SleepCanvas();
		storeUI.enabled = false;
	}
	
	// Update is called once per frame
	void Update( )
	{
		mousePosition = Input.mousePosition;

		//farm stage
		if (farmStage.CheckOnGame())
		{
			gameTime += Time.deltaTime;
			farmUI.SetGameTime( gameTime );
			farmStage.ProcessStageEvent( mousePosition );
			farmUI.LinkCropItem( farmStage.GetCropItem() );
		}

		//store stage placement item step
		if (!storeStage.PlaceComplete && !farmStage.CheckOnGame())
		{			
			storeStage.ProcessPlacementEvent( mousePosition );
		}

		//store stage 
		if (storeStage.CheckOnGame())
		{
			gameTime += Time.deltaTime;
			storeUI.SetGameTime( gameTime );
			storeStage.ProcessStageEvent( mousePosition );
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
		storeStage = GetComponent<StoreManager>();

		//link UI data
		farmUI = GameObject.FindGameObjectWithTag( "FarmCanvas" ).GetComponent<FarmUI>();
		storeUI = GameObject.FindGameObjectWithTag( "StoreCanvas" ).GetComponent<StoreUI>();
	}

	//move crop item data (farm to store)
	void TransferCropItem( )
	{
		cropData = farmStage.GetCropItem().ToArray();
	}

	//game start / end policy
	//farm
	public void StartFarmGame( )
	{
		//mainUI.enabled = false;
		farmUI.enabled = true;
		farmUI.WakeUpCanvas();
		farmStage.StartFarmGame();
	}

	public void EndFarmGame( )
	{
		farmStage.EndFarmGame();
		farmUI.SleepCanvas();
		farmUI.enabled = false;
		gameTime = 0.0f;
	}

	//store
	public void StartStoreGame( )
	{
		
		storeUI.enabled = true;
		storeUI.WakeUpCanvas();
		storeUI.LinkCropItem( farmStage.GetCropItem().ToArray() );
		storeStage.StartStoreGame();
	

	}

	public void EndStoreGame( )
	{
		storeStage.EndStoreGame();
		storeUI.SleepCanvas();
		storeUI.enabled = false;
		gameTime = 0.0f;
		//mainUI.enabled = true;
	}




}
