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
	public Texture tempData;
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
		moveX = 10;

		for (int i = 0; i < itemList.Length; i++)
		{
			moveX += 20;
			GUI.DrawTexture( new Rect( 10 + moveX, 10, 10, 10 ), tempData );
		}

		timeBar.value = (1 - gameTime / 60f);
	}

	//property
	public float GameTime
	{
		set { gameTime = value; }
	}

	//another method

	//wake up canvas - UI activate step
	public void WakeUpCanvas()
	{
		Canvas temp = GetComponent<Canvas>();
		temp.enabled = true;
	}

	//sleep canvas - UI sleep step
	public void SleepCanvas()
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
}
