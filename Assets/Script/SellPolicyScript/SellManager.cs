using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SellManager : MonoBehaviour
{
	//simple data field
	public bool onGame;

	//complex data field
	public CropItem[] cropList;

	//initialize this script
	void Start( )
	{
		onGame = false;
	}

	//another method
	//process game event - mouse click event
	public void ProcessStageEvent( Vector2 mousePosition )
	{

	}

	//sell game start or restart
	public void StartSellGame( )
	{
		onGame = true;
	}

	//sell game close
	public void EndSellGame( )
	{
		onGame = false;
	}

	//get / set method
	//set crop item -> link farm stage;
	public void SetCropItem( CropItem[] data )
	{
		cropList = new CropItem[data.Length];
		for (int i = 0; i < cropList.Length; i++)
			cropList[i] = data[i];
	}

}
