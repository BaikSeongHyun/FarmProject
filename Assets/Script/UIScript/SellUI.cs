using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SellUI : MonoBehaviour
{
	//simple datafield
	int moveX;
	float gameTime;
	int countCrop;

	//complex data field
	public Sprite[] itemSprite;
	public List<CropItem> cropData;
	public List<GameObject> button;
	Scrollbar timeBar;

	// initialize this script
	void Start( )
	{
		GameObject temp = GameObject.FindGameObjectWithTag( "GameManager" );
		timeBar = transform.Find( "TimeBar" ).GetComponent<Scrollbar>();
		button = new List<GameObject>();
		cropData = new List<CropItem>();
	}

	//update data
	void Update( )
	{
		timeBar.value = (1 - gameTime / 60f);

		if (button.ToArray().Length != countCrop)
		{
			button.Clear();
			for (int i = 0; i < countCrop; i++)
				button.Add( MakeNewItemButton( cropData[i], i ) );
		}
	}

	//property
	public float GameTime
	{
		set { gameTime = value; }
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

	//link crop item data
	public void LinkCropItem( CropItem[] data )
	{
		cropData.Clear();
		countCrop = data.Length;
		for (int i = 0; i < data.Length; i++)
			cropData.Add( data[i] );
	}

	// click crop item - for sell item
	public void SelectCropItem( )
	{

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
			PopUpCropItem();
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

	//buttom event method
	public void PopUpCropItem( )
	{
		Debug.Log( "Active pop up call back" );
	}

}
