using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoreUI : MonoBehaviour
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
	public Sprite[] rankSprite;
	public Sprite[] numberSprite;
	public Sprite[] itemSpriteColor;
	public Sprite[] itemSpriteGray;
	public Sprite[] skillIcon;
	public Sprite[] moneyChangeIcon;
	public CropItem[] cropData;
	public List<string> moneyInfor;
	public List<int> moneyChange;
	GameObject[] button;
	GameObject[] skillImage;
	Scrollbar timeBar;
	StoreManager manager;
	GameObject priceSetPopUp;
	GameObject bargainPopUp;
	Human presentBargainCustomer;

	//standard method
	// initialize this script
	void Start( )
	{
		DataLink();
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

	//initialize data for start UI - priority check
	public void DataLink( )
	{
		timeBar = transform.Find( "TimeBar" ).GetComponent<Scrollbar>();
		manager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<StoreManager>();
		priceSetPopUp = GameObject.Find( "PriceSetPopUp" );
		priceSetPopUp.GetComponent<Canvas>().enabled = false;
		bargainPopUp = GameObject.Find( "BargainPopUp" );
		bargainPopUp.GetComponent<Canvas>().enabled = false;
		moneyInfor = new List<string>();
		moneyChange = new List<int>();
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
			button[i] = CreateNewItemButton( cropData[i], i );
	
	}

	//set game time and renewal scroll bar value
	public void SetGameTime( float time )
	{
		gameTime = time;
		timeBar.value = (1 - gameTime / 60f);
	}

	//make button method
	GameObject CreateNewItemButton( CropItem itemData, int index )
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
		button.transform.localPosition = new Vector3( -340f + (index * 60f), 150f, 0f );
		button.transform.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
		//button.transform.GetComponent<RectTransform>().sizeDelta = ( new Vector2( 0.5f, 0.5f ) );
		//image
		button.GetComponent<Image>().sprite = SetCropSprite( itemData.Name, false );
		//add call back method
		button.GetComponent<Button>().onClick.AddListener( ( ) =>
		{
			PopUpCropItem( itemData, index );
		} );

		//return object
		return button;
	}

	//button sprite select
	Sprite SetCropSprite( string name, bool place )
	{
		if (!place)
		{
			switch(name)
			{
				case "Corn":
					return itemSpriteColor[0];
				case "Carrot":
					return itemSpriteColor[1];
				case "Barley":
					return itemSpriteColor[2];
			}
		}
		if (place)
		{
			switch(name)
			{
				case "Corn":
					return itemSpriteGray[0];
				case "Carrot":
					return itemSpriteGray[1];
				case "Barley":
					return itemSpriteGray[2];
			}
		}

		return null;
	}

	//dynamic crop button on/off
	void ControlCropButton( bool state )
	{
		for (int i = 0; i < button.Length; i++)
			button[i].GetComponent<Image>().enabled = state;
	}

	//section pop up cropItem
	//button event method - dynamic crop button
	public void PopUpCropItem( CropItem data, int index )
	{
		if (!manager.CheckAllField())
		{
			//pop up message
			Debug.Log( "Store field is full" );
			return;
		}

		ControlCropButton( false );

		price = 0;
		transform.Find( "PriceSetPopUp" ).transform.Find( "Rank" ).GetComponent<Image>().sprite = SetRankSprite( data.Rank );
		transform.Find( "PriceSetPopUp" ).transform.Find( "Item" ).GetComponent<Image>().sprite = SetCropSprite( data.Name, false );
		ChangeNumberImage();
		presentIndex = index;
		priceSetPopUp.GetComponent<Canvas>().enabled = true;
		priceSetPopUp.transform.Find( "AveragePrice" ).GetComponent<Image>().sprite = manager.SetAverageCropTable( data.Name );
	}

	//return sprite for rank icon
	Sprite SetRankSprite( Crop.Rank rank )
	{
		switch(rank)
		{
			case Crop.Rank.S:
				return rankSprite[0];
			case Crop.Rank.A:
				return rankSprite[1];
			case Crop.Rank.B:
				return rankSprite[2];
			case Crop.Rank.C:
				return rankSprite[3];
		}

		return null;
	}

	//button event method - price +
	public void SetPlusPrice( int key )
	{
		switch(key)
		{
			case 100:
				if (FindNumberIndex( 100 ) == 9)
					break;
				price += 100;
				break;
			case 10:
				if (FindNumberIndex( 10 ) == 9)
					break;
				price += 10;
				break;
			case 1:
				if (FindNumberIndex( 1 ) == 9)
					break;
				price += 1;
				break;
		}
		ChangeNumberImage();
	}

	//button event method - price -
	public void SetMinusPrice( int key )
	{
		switch(key)
		{
			case 100:
				if (FindNumberIndex( 100 ) == 0)
					break;
				price -= 100;
				break;
			case 10:
				if (FindNumberIndex( 10 ) == 0)
					break;
				price -= 10;
				break;
			case 1:
				if (FindNumberIndex( 1 ) == 0)
					break;
				price -= 1;
				break;
		}
		ChangeNumberImage();

	}

	//set number index for set price
	int FindNumberIndex( int key )
	{
		price = Mathf.Abs( price );
		int tempValue = price;
		if (key == 1)
			return (tempValue % 10);
		else if (key == 10)
			return ((tempValue / 10) % 10);
		else if (key == 100)
			return (tempValue / 100);
		else
			return 0;
	}

	//set sprite for price number
	public void ChangeNumberImage( )
	{
		transform.Find( "PriceSetPopUp" ).transform.Find( "Hundred" ).GetComponent<Image>().sprite = numberSprite[FindNumberIndex( 100 )];
		transform.Find( "PriceSetPopUp" ).transform.Find( "Ten" ).GetComponent<Image>().sprite = numberSprite[FindNumberIndex( 10 )];
		transform.Find( "PriceSetPopUp" ).transform.Find( "One" ).GetComponent<Image>().sprite = numberSprite[FindNumberIndex( 1 )];
	}

	//button event method - cancel set price
	public void CancelPopUp( string name )
	{
		if (name == "Price")
			priceSetPopUp.GetComponent<Canvas>().enabled = false;
		else if (name == "Bargain")
			bargainPopUp.GetComponent<Canvas>().enabled = false;

		ControlCropButton( true );
	}
		
	//button event method - confirm price and send crop item by store manager
	public void ConfirmPrice( )
	{
		cropData[presentIndex].Price = price;
		//send object and destroy object
		if (manager.LinkPresentCropItem( cropData[presentIndex], presentIndex ))
		{
			button[presentIndex].GetComponent<Image>().sprite = SetCropSprite( cropData[presentIndex].Name, true );
			button[presentIndex].GetComponent<Button>().enabled = false;
			priceSetPopUp.GetComponent<Canvas>().enabled = false;
		}

		ControlCropButton( true );
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

	//clear button object
	public void ClearItemButton( )
	{
		for (int i = 0; i < button.Length; i++)
		{
			Destroy( button[i] );
		}
	}
		
	//section pop up bargain
	//button event method
	public void PopUpBargain( Human human )
	{
		//pop up canvas for bargain
		presentBargainCustomer = human;
		bargainPopUp.GetComponent<Canvas>().enabled = true;
		ControlCropButton( false );
		Debug.Log( "Open Pop up UI" );

		//set pop up data
		int index = human.Store.CropIndex;

		transform.Find( "BargainPopUp" ).Find( "Item" ).GetComponent<Image>().sprite = SetCropSprite( cropData[index].Name, false );
		transform.Find( "BargainPopUp" ).Find( "Rank" ).GetComponent<Image>().sprite = SetRankSprite( cropData[index].Rank );
		price = human.BargainPrice;
		transform.Find( "BargainPopUp" ).Find( "Hundred" ).GetComponent<Image>().sprite = numberSprite[FindNumberIndex( 100 )];
		transform.Find( "BargainPopUp" ).Find( "Ten" ).GetComponent<Image>().sprite = numberSprite[FindNumberIndex( 10 )];
		transform.Find( "BargainPopUp" ).Find( "One" ).GetComponent<Image>().sprite = numberSprite[FindNumberIndex( 1 )];

	}

	public void ComfirmBargain( )
	{
		bargainPopUp.GetComponent<Canvas>().enabled = false;
		presentBargainCustomer.SuccessBargain();
		ControlCropButton( true );
	}

	public void RejectBargain( )
	{
		bargainPopUp.GetComponent<Canvas>().enabled = false;
		presentBargainCustomer.FailureBargain();
		ControlCropButton( true );
	}

	public void ControlStoreGameButton( bool state )
	{		
		transform.Find( "StartStoreGame" ).GetComponent<Image>().enabled = state;
	}

	//make skill icon - use index
	public void DrawSkillImage( string name, int skillCount )
	{
		OffSkillImage();	

		skillImage = new GameObject[skillCount];

		for (int i = 0; i < skillCount; i++)
		{
			skillImage[i] = MakeSkillImage( name, i );
		}
	}

	//make skill icon image object
	public GameObject MakeSkillImage( string name, int index )
	{
		//create component
		GameObject image = new GameObject();
		image.name = "SkillImage";
		image.AddComponent<RectTransform>();
		image.AddComponent<CanvasRenderer>();
		image.AddComponent<Image>();

		//move parent object
		image.transform.parent = transform;

		//set component
		image.transform.localPosition = new Vector3( 230 + (index * 10f), 100f, 0f );
		image.transform.localScale = new Vector3( 0.3f, 0.3f, 0.3f );

		image.GetComponent<Image>().sprite = SetSkillSprite( name );

		return image;
	}

	//set skill icon
	public Sprite SetSkillSprite( string name )
	{
		switch(name)
		{
			case"BarrelDrop":
				return skillIcon[0]; 
			case"Snipe":
				return skillIcon[1]; 
			case"Lighting":
				return skillIcon[2]; 
		}

		return null;
	}

	//clear skill image
	public void OffSkillImage( )
	{
		if (skillImage != null)
		{
			for (int i = 0; i < skillImage.Length; i++)
			{
				Destroy( skillImage[i] );
			}
		}
	}

	//control money infor tab
	public void ControlMoneyInfor( bool state )
	{
		transform.Find( "MoneyBack" ).GetComponent<Image>().enabled = state;
		transform.Find( "Money1stSpace" ).GetComponent<Image>().enabled = state;
		transform.Find( "Money1stIcon" ).GetComponent<Image>().enabled = state;
		transform.Find( "Money1stText" ).GetComponent<Text>().enabled = state;
		transform.Find( "Money2ndSpace" ).GetComponent<Image>().enabled = state;
		transform.Find( "Money2ndIcon" ).GetComponent<Image>().enabled = state;
		transform.Find( "Money2ndText" ).GetComponent<Text>().enabled = state;
		transform.Find( "Money3rdSpace" ).GetComponent<Image>().enabled = state;
		transform.Find( "Money3rdIcon" ).GetComponent<Image>().enabled = state;
		transform.Find( "Money3rdText" ).GetComponent<Text>().enabled = state;
	}

	public void ControlMoneyInforItem( int index, bool state )
	{
		switch(index)
		{
			case 1:
				transform.Find( "Money1stSpace" ).GetComponent<Image>().enabled = state;
				transform.Find( "Money1stIcon" ).GetComponent<Image>().enabled = state;
				transform.Find( "Money1stText" ).GetComponent<Text>().enabled = state;
				break;
			case 2:
				transform.Find( "Money2ndSpace" ).GetComponent<Image>().enabled = state;
				transform.Find( "Money2ndIcon" ).GetComponent<Image>().enabled = state;
				transform.Find( "Money2ndText" ).GetComponent<Text>().enabled = state;
				break;
			case 3:
				transform.Find( "Money3rdSpace" ).GetComponent<Image>().enabled = state;
				transform.Find( "Money3rdIcon" ).GetComponent<Image>().enabled = state;
				transform.Find( "Money3rdText" ).GetComponent<Text>().enabled = state;
				break;
		}
	}


	//update money infor
	public void UpdateMoneyInfor( string infor, int money )
	{
		//add money infor
		moneyInfor.Add( infor );
		moneyChange.Add( money );

		//only use 3 data
		if (moneyChange.Count > 3 && moneyInfor.Count > 3)
		{
			moneyInfor.RemoveAt( 0 );
			moneyChange.RemoveAt( 0 );
		}

		//ui update
		if (moneyInfor.Count > 0)
		{
			ControlMoneyInforItem( 1, true );
			transform.Find( "Money1stText" ).GetComponent<Text>().text = moneyInfor[0] + " " + moneyChange[0].ToString();
			if (moneyChange[0] > 0)
				transform.Find( "Money1stIcon" ).GetComponent<Image>().sprite = moneyChangeIcon[0];
			else
				transform.Find( "Money1stIcon" ).GetComponent<Image>().sprite = moneyChangeIcon[1];
		}

		if (moneyInfor.Count > 1)
		{
			ControlMoneyInforItem( 2, true );
			transform.Find( "Money1stText" ).GetComponent<Text>().text = moneyInfor[1] + " " + moneyChange[1].ToString();
			if (moneyChange[1] > 0)
				transform.Find( "Money1stIcon" ).GetComponent<Image>().sprite = moneyChangeIcon[0];
			else
				transform.Find( "Money1stIcon" ).GetComponent<Image>().sprite = moneyChangeIcon[1];
		}

		if (moneyInfor.Count > 2)
		{
			ControlMoneyInforItem( 3, true );
			transform.Find( "Money1stText" ).GetComponent<Text>().text = moneyInfor[2] + " " + moneyChange[2].ToString();
			if (moneyChange[2] > 0)
				transform.Find( "Money1stIcon" ).GetComponent<Image>().sprite = moneyChangeIcon[0];
			else
				transform.Find( "Money1stIcon" ).GetComponent<Image>().sprite = moneyChangeIcon[1];
		}

	}
}

