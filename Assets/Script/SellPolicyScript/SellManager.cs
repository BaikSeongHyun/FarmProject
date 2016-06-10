using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SellManager : MonoBehaviour
{
	//simple data field
	public bool onGame;
	const float RayCastMaxDistance = 100.0f;
	public int money;
	public int presentCropIndex;

	//complex data field
	//this array will be set automaic
	public CropItem presentCrop;
	public GameObject[] cropData;
	public Crop[] cropGroup;
	public Sprite[] cropAverageTable;
	public GameObject[] tempData;
	public SellFieldPolicy[] sellFieldGroup;

	//initialize this script
	void Start( )
	{
		onGame = false;
		money = 100;
	}

	//another method
	//initialize game data
	void LinkCropData( )
	{
		cropData = GameObject.FindGameObjectsWithTag( "Crop" );
		cropGroup = new Crop[cropData.Length];
		GameObject temp = GameObject.FindGameObjectWithTag( "Crop" );
		if (temp != null)
			cropGroup[0] = temp.GetComponent<Crop>();		
	}
	void LinkSellFieldPolicy( )
	{
		tempData = GameObject.FindGameObjectsWithTag( "SellField" );
		sellFieldGroup = new SellFieldPolicy[tempData.Length];
		for (int i = 0; i < sellFieldGroup.Length; i++)
		{
			if (tempData[i] != null)
			{
				sellFieldGroup[i] = tempData[i].GetComponent<SellFieldPolicy>();
				SleepSellField( sellFieldGroup[i] );
			}
		}
	}

	//sell field enable set false
	void SleepSellField(SellFieldPolicy sell)
	{
		sell.enabled = false;
	}

	//process game event - mouse click event
	public void ProcessStageEvent( Vector2 mousePosition )
	{
		if (Input.GetButtonDown( "Click" ) && onGame)
		{
			Ray ray = Camera.main.ScreenPointToRay( mousePosition );
			RaycastHit hitinfo;
			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "SellField" ) ))
			{
				GameObject tempSearch = hitinfo.collider.gameObject;
				SellFieldPolicy tempPolicy = tempSearch.GetComponent<SellFieldPolicy>();
				tempPolicy.ProcessEvent(presentCrop, presentCropIndex);
			}

		}
	}

	//sell cropitem
	public void SellCrop( int cropIndex )
	{
		//add money & remove crop item in list
	
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

	//crop button click 
	public bool LinkPresentCropItem(CropItem item, int index)
	{
		if (CheckAllField())
		{
			presentCropIndex = index;
			presentCrop = new CropItem( item );
			return true;
		}
		else
			return false;
	}

	//check empty sell field
	bool CheckAllField()
	{
		for(int i = 0; i < sellFieldGroup.Length; i++)
		{
			if(!sellFieldGroup[i].OnSell)
				return true;
		}
	
		return false;
	}

	//get / set method

	//on game
	public bool CheckOnGame( )
	{
		return onGame;
	}

	public Sprite SetAverageCropTable(string name)
	{
		switch(name)
		{
			case "Corn":
				return cropAverageTable[0];
		}

		return null;
	}
}
