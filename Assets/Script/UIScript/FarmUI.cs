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
	GameManager rootSource;
	FarmManager dataSource;
	Scrollbar timeBar;

	void Start( )
	{
		GameObject temp = GameObject.FindGameObjectWithTag( "GameManager" );
		rootSource = temp.GetComponent<GameManager>();
		dataSource = temp.GetComponent<FarmManager>();

		timeBar = transform.Find("TimeBar").GetComponent<Scrollbar>();
		LinkCropItem( dataSource.GetCropItem() );
	}

	void Update()
	{
		gameTime = rootSource.GameTime;
		LinkCropItem( dataSource.GetCropItem() );
		timeBar.value = (1 - gameTime / 60f);
	}

	//draw UI & Update data;
	void OnGUI( )
	{
		moveX = 10;
		for (int i = 0; i < itemList.Length; i++)
		{
			moveX += 20;
			GUI.DrawTexture( new Rect( 10, 10 + moveX, 10, 10 ), tempData );
		}
	}

	//another method

	//link crop item data
	public void LinkCropItem( List<CropItem> data )
	{
		itemList = new CropItem[data.ToString().Length];
		itemList = data.ToArray();
	}
}
