using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FarmUI : MonoBehaviour
{
	//simple datafield
	int moveX;
	float gameTime;

	//complex data field
	public Texture[] cropTexture;
	public Sprite[] resourceSprite;
	int[] buySeedCount;
	Button[] cropItem;
	CropItem[] itemList;
	Scrollbar timeBar;
	Canvas preProcessPopUp;
	public MainUI mainUI;
	public FarmManager manager;
	public GameManager gameManager;

	// initialize this script
	void Start( )
	{
		DataLink();
	}

	//draw UI & Update data;
	void OnGUI( )
	{
		moveX = 0;

		for (int i = 0; i < itemList.Length; i++)
		{
			moveX += 80;
			GUI.DrawTexture( new Rect( moveX, 40, 50, 50 ), SetTexture( itemList[i].Name ) );
		}

		timeBar.value = (1 - gameTime / 60f);
		UpdateSeedCount();
	}

	//property
	public float GameTime
	{
		set { gameTime = value; }
	}

	//another method

	//initialize data for start UI - priority check
	public void DataLink()
	{
		timeBar = transform.Find( "TimeBar" ).GetComponent<Scrollbar>();
		preProcessPopUp = transform.Find( "PreProcessFarmGame" ).GetComponent<Canvas>();
		itemList = new CropItem[0];
		mainUI = GameObject.FindGameObjectWithTag( "MainCanvas" ).GetComponent<MainUI>(); 
		manager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<FarmManager>();
		gameManager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>();
		buySeedCount = new int[3];
		buySeedCount[0] = 0;
		buySeedCount[1] = 0;
		buySeedCount[2] = 0;
		SeedUpdate();
		StageMoneyUpdate();
	}
	//wake up canvas - UI activate step
	public void WakeUpCanvas( )
	{
		Canvas temp = GetComponent<Canvas>();
		temp.enabled = true;
	}

	//sleep canvas - UI sleep step
	public void SleepCanvas( )
	{
		Canvas temp = GetComponent<Canvas>();
		temp.enabled = false;
	}

	//button sprite select
	Texture SetTexture( string name )
	{
		switch(name)
		{
			case "Corn":
				return cropTexture[0];
			case "Carrot":
				return cropTexture[1];
			case "Barley":
				return cropTexture[2];
		}

		return null;
	}

	//link crop item data
	public void LinkCropItem( List<CropItem> data )
	{
		itemList = new CropItem[data.ToArray().Length];
		itemList = data.ToArray();
	}

	//set game time and renewal scroll bar value
	public void SetGameTime( float time )
	{
		gameTime = time;
		timeBar.value = (1 - gameTime / 60f);
	}

	//control farm select section
	public void ControlFarmMenu( bool state )
	{



	}

	//open preprocess popup
	public void OpenPreprocessPopUp( )
	{
		preProcessPopUp.enabled = true;
	}

	//mouse click event method - seedCount for preprocess
	public void SeedAdd( int index )
	{
		if (buySeedCount[index] + 1 > 9)
			return;

		if (!manager.SetMoney( index, true ))
			return;
		
		buySeedCount[index]++;
		SeedUpdate();
		StageMoneyUpdate();
	}

	public void SeedSubtract( int index )
	{
		if (buySeedCount[index] - 1 < 0)
			return;
		if (!manager.SetMoney( index, false ))
			return;
		buySeedCount[index]--;
		SeedUpdate();
		StageMoneyUpdate();
	}

	//mouse click event method - confirm buy seed
	public void ConfirmBuySeed( )
	{
		manager.GetCropInformation( "Corn" ).SeedCount = buySeedCount[0];
		manager.GetCropInformation( "Carrot" ).SeedCount = buySeedCount[1];
		manager.GetCropInformation( "Barley" ).SeedCount = buySeedCount[2];

		preProcessPopUp.enabled = false;
		WakeUpCanvas();
		GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>().StartFarmGame();
	}

	//mouse clieck event method - cancel preprocess
	public void CancelPreProcess( )
	{
		preProcessPopUp.enabled = false;
		mainUI.MainMenuControl( true );
	}

	//mouse click event method - present seed
	public void InputPresentSeed( int index )
	{
		switch(index)
		{
			case 0:
				manager.PresentSeed = manager.GetCropGroup()[0];
				break;
			case 1:
				manager.PresentSeed = manager.GetCropGroup()[1];
				break;
			case 2:
				manager.PresentSeed = manager.GetCropGroup()[2];
				break;
		}

		transform.Find( "SeedPresent" ).GetComponent<Image>().sprite = manager.PresentSeed.GetIcon( 0 );
	}

	//mouse click event method - present resource
	public void SetResource( int index )
	{
		switch(index)
		{
			case 0:
				manager.PresentResource = Crop.Resource.Water;
				break;
			case 1:
				manager.PresentResource = Crop.Resource.Fertilizer;
				break;
			case 2:
				manager.PresentResource = Crop.Resource.Default;
				break;
		}

		transform.Find( "ResourcePresent" ).GetComponent<Image>().sprite = resourceSprite[index];
	}

	//update seedCount for preprocess
	void SeedUpdate( )
	{
		transform.Find( "PreProcessFarmGame" ).Find( "Crop01Text" ).GetComponent<Text>().text = buySeedCount[0].ToString();
		transform.Find( "PreProcessFarmGame" ).Find( "Crop02Text" ).GetComponent<Text>().text = buySeedCount[1].ToString();
		transform.Find( "PreProcessFarmGame" ).Find( "Crop03Text" ).GetComponent<Text>().text = buySeedCount[2].ToString();
	}

	//update pre process stage money
	void StageMoneyUpdate( )
	{
		transform.Find( "PreProcessFarmGame" ).Find( "MoneyText" ).GetComponent<Text>().text = gameManager.Money.ToString();
	}

	//update seed count
	void UpdateSeedCount( )
	{
		transform.Find( "Seed1stText" ).GetComponent<Text>().text = manager.GetCropInformation( "Corn" ).SeedCount + " pcs";
		transform.Find( "Seed2ndText" ).GetComponent<Text>().text = manager.GetCropInformation( "Carrot" ).SeedCount + " pcs";
		transform.Find( "Seed3rdText" ).GetComponent<Text>().text = manager.GetCropInformation( "Barley" ).SeedCount + " pcs";
	}
}
