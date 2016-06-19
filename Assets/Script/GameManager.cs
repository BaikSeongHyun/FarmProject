using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	//simple data fiele
	public float gameTime;
	public float stageTime;
	public Vector2 mousePosition;
	int originMoney;
	public int money;

	//complex data field
	CropItem[] cropData;
	FarmManager farmStage;
	StoreManager storeStage;
	public FarmUI farmUI;
	public StoreUI storeUI;
	public MainUI mainUI;

	// initialize this script
	void Start ()
	{
		money = 2000;
		gameTime = 0.0f;
		stageTime = 90f;
		LinkGameResource();		

		//sleep farm
		farmUI.SleepCanvas();
		farmUI.enabled = false;

		//sleep store
		storeUI.SleepCanvas();
		storeUI.enabled = false;
		
		//result sleep
		mainUI.ControlResultPopUp(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		mousePosition = Input.mousePosition;

		//farm stage
		if (farmStage.CheckOnGame())
		{
			gameTime += Time.deltaTime;
			farmUI.SetGameTime( );
			farmStage.ProcessStageEvent( mousePosition );
			farmUI.LinkCropItem( farmStage.GetCropItem() );
			if (gameTime >= stageTime)
			{
				gameTime = 0.0f;
				//go next stage	
				EndFarmGame();
				StartPreProcessStoreGame();
			}
		}

		//store stage placement item step
		if (!storeStage.PlaceComplete)
		{			
			storeStage.ProcessPlacementEvent( mousePosition );
		}

		//store stage 
		if (storeStage.CheckOnGame())
		{
			gameTime += Time.deltaTime;
			storeUI.SetGameTime( );
			storeStage.ProcessStageEvent( mousePosition );
			
			if (gameTime >= stageTime)
			{
				gameTime = 0.0f;
				//go next stage	
				EndStoreGame();
			}
		}
		CameraChanger();
		mainUI.SetMoneyInfor();		

	}

	//property
	//game time
	public float GameTime
	{
		get { return gameTime; }
	}
	//stage time
	public float StageTime
	{
		get { return stageTime; }
	}
	// game money
	public int Money
	{
		get { return money; }
		set { money = value; }
	}

	//another method
	void LinkGameResource ()
	{
		//link stage data
		farmStage = GetComponent<FarmManager>();
		storeStage = GetComponent<StoreManager>();

		//link UI data
		mainUI = GameObject.FindGameObjectWithTag( "MainCanvas" ).GetComponent<MainUI>();
		farmUI = GameObject.FindGameObjectWithTag( "FarmCanvas" ).GetComponent<FarmUI>();
		storeUI = GameObject.FindGameObjectWithTag( "StoreCanvas" ).GetComponent<StoreUI>();
	}

	//game start / end policy

	//farm
	public void StartPreProcessFarmGame ()
	{
		//start ui alogorithm -> no active canvas ( for pop up only)
		originMoney = money;
		farmUI.enabled = true;
		farmUI.DataLink();
		farmUI.OpenPreprocessPopUp();
		mainUI.MainMenuControl( false );
	}

	public void StartFarmGame ()
	{	
		farmStage.StartFarmGame();
	}

	public void EndFarmGame ()
	{
		farmUI.SleepCanvas();
		farmUI.enabled = false;
		farmStage.EndFarmGame();
		gameTime = 0.0f;
	}


	//store
	public void StartPreProcessStoreGame ()
	{
		storeStage.StartPreProcess();

		//place cropItem
		storeUI.enabled = true;
		storeUI.DataLink();
		storeUI.WakeUpCanvas();
		storeUI.ControlMoneyInforItem( 1, false );
		storeUI.ControlMoneyInforItem( 2, false );
		storeUI.ControlMoneyInforItem( 3, false );
		storeUI.LinkCropItem( farmStage.GetCropItem().ToArray() );
		storeUI.ControlStoreGameButton( true );
	}

	public void StartStoreGame ()
	{
		storeStage.StartStoreGame();
	}

	public void EndStoreGame ()
	{
		storeStage.EndStoreGame();
		storeUI.SleepCanvas();
		storeUI.ClearItemButton();
		storeUI.enabled = false;
		gameTime = 0.0f;
		mainUI.MainMenuControl( true );
		ProcessStageClear();
		InitializeGameData();
	}
	
	void ProcessStageClear()
	{
		if(money >= originMoney + 400)
		
			mainUI.PopUpResult("Success");
		
		else
			mainUI.PopUpResult("Failure");
	}
			

	//if game end -> all game data reset
	public void InitializeGameData ()
	{
		farmStage.InitializeGameData();
		storeStage.InitializeGameData();
	}
	
	//camera rotation set
	void CameraChanger ()
	{	
		//farm stage view
		if (farmStage.CheckOnGame())
		{
			Camera.main.transform.position = Vector3.Lerp( Camera.main.transform.position, new Vector3(-28.0f, 32.0f, -20.0f), Time.deltaTime );
			Camera.main.transform.rotation = Quaternion.Lerp( Camera.main.transform.rotation, new Quaternion(0.6f, 0.0f, 0.0f, 0.8f), Time.deltaTime );
		}
		else if (!storeStage.PlaceComplete || storeStage.CheckOnGame())
		{
			Camera.main.transform.position = Vector3.Lerp( Camera.main.transform.position, new Vector3(-2.0f, 30.0f, -22.0f), Time.deltaTime );
			Camera.main.transform.rotation = Quaternion.Lerp( Camera.main.transform.rotation, new Quaternion(0.6f, 0.0f, 0.0f, 0.8f), Time.deltaTime );
		}
		//main view
		else if (!farmStage.CheckOnGame() && !storeStage.CheckOnGame())
		{
			//rotate round
			Camera.main.transform.position = Vector3.Lerp( Camera.main.transform.position, new Vector3(18.9f, 24.4f, -29.2f), Time.deltaTime );
			Camera.main.transform.rotation = Quaternion.Lerp( Camera.main.transform.rotation, new Quaternion(0.2f, -0.3f, 0.1f, 0.9f), Time.deltaTime );
		}
	}
}