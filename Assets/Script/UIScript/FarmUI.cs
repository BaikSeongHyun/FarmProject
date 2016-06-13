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
	Button[] cropItem;
	CropItem[] itemList;
	Scrollbar timeBar;

	// initialize this script
	void Start( )
	{
		timeBar = transform.Find( "TimeBar" ).GetComponent<Scrollbar>();
		itemList = new CropItem[0];
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
			case "Pumpkin":
				return cropTexture[2];
		}

		return null;

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
}
