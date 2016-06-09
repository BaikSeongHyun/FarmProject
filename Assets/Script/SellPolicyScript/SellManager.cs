using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SellManager : MonoBehaviour
{
	//simple data field
	public bool onGame;
	const float RayCastMaxDistance = 100.0f;
	public int money;

	//complex data field
	//this array will be set automaic
	public CropItem presentCrop;
	public List<CropItem> cropList;
	public GameObject[] cropData;
	public Crop[] cropGroup;

	//initialize this script
	void Start( )
	{
		onGame = false;
		money = 100;
		cropList = new List<CropItem>();
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
				tempPolicy.ProcessEvent(presentCrop);
			}

		}
	}

	//sell cropitem
	public void SellCrop( int cropIndex )
	{
		//add money & remove crop item in list
		CropItem temp = cropList[cropIndex];
		money += temp.Price;
		cropList.RemoveAt( cropIndex );
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
		for (int i = 0; i < data.Length; i++)
			cropList.Add( data[i] );
	}

	public List<CropItem> GetCropItem()
	{
		return cropList;
	}

	//on game
	public bool CheckOnGame( )
	{
		return onGame;
	}
}
