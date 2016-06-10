using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SellUI : MonoBehaviour
{
	//simple datafield
	int moveX;
	int presentIndex;
	int price;
	float gameTime;
	int tempPrice;

	//complex data field
	public Sprite soldCrop;
	public Sprite stolenCrop;
	public Sprite[] itemSprite;
	public CropItem[] cropData;
	public GameObject[] button;
	Scrollbar timeBar;
	SellManager manager;
	GameObject priceSetPopUp;

	// initialize this script
	void Start( )
	{
		timeBar = transform.Find( "TimeBar" ).GetComponent<Scrollbar>();
		manager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<SellManager>();
		priceSetPopUp = GameObject.Find( "PriceSetPopUp" );
		priceSetPopUp.GetComponent<Canvas>().enabled = false;
	}

	//another method

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

	//link crop item data and create button
	public void LinkCropItem( CropItem[] data )
	{
		//set cropData
		cropData = new CropItem[data.Length];
		for (int i = 0; i < data.Length; i++)
			cropData[i] = data[i];
		
		//button create
		button = new GameObject[cropData.Length];
		for (int i = 0; i < cropData.Length; i++)
			button[i] = MakeNewItemButton( cropData[i], i );
	
	}

	//set game time and renewal scroll bar value
	public void SetGameTime( float time )
	{
		gameTime = time;
		timeBar.value = (1 - gameTime / 60f);
	}

	//make button method
	GameObject MakeNewItemButton( CropItem itemData, int index )
	{
		//create button object
		GameObject button = new GameObject();
		button.name = "CropItemButton";
		button.AddComponent<RectTransform>();
		button.AddComponent<CanvasRenderer>();
		button.AddComponent<Image>();
		button.AddComponent<Button>();

		//move parent object
		button.transform.parent = transform;

		//set button object information
		//pos
		button.transform.position = new Vector3( 20f, 20f, 0f );
		//image
		button.GetComponent<Image>().sprite = SetSprite( itemData.Name );
		//add call back method
		button.GetComponent<Button>().onClick.AddListener( ( ) =>
		{
			PopUpCropItem( itemData, index );
		} );

		//return object
		return button;
	}

	//button sprite select
	Sprite SetSprite( string name )
	{
		switch(name)
		{
			case "Corn":
				return itemSprite[0];
			case "WaterMelon":
				return itemSprite[1];
		}

		return null;
	}

	//button event method - dynamic crop button
	public void PopUpCropItem( CropItem data, int index )
	{
		price = 0;
		presentIndex = index;
		priceSetPopUp.GetComponent<Canvas>().enabled = true;
		priceSetPopUp.transform.Find( "AveragePrice" ).GetComponent<Image>().sprite = manager.SetAverageCropTable( data.Name );
	}

	//button event method - confirm price and send crop item by sell manager
	public void ConfirmPrice( )
	{
		cropData[presentIndex].Price = price;
		//send object and destroy object
		if (manager.LinkPresentCropItem( cropData[presentIndex], presentIndex ))
		{
			//button[index].GetComponent<Image>().sprite = soldCrop;
			button[presentIndex].GetComponent<Button>().enabled = false;
			priceSetPopUp.GetComponent<Canvas>().enabled = false;
		}
	}

	//sprite set sold out
	public void SoldOutCrop( int index )
	{
		button[index].GetComponent<Image>().sprite = soldCrop;
	}

	//sprite set Stolen
	public void StolenCrop( int index )
	{
		button[index].GetComponent<Image>().sprite = stolenCrop;
	}
}
