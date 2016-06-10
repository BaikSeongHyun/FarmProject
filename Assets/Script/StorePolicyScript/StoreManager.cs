using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
	//simple data field
	public bool onGame;
	public bool placeComplete;
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
	public StoreFieldPolicy[] storeFieldGroup;

	//initialize this script
	void Start( )
	{
		onGame = false;
		placeComplete = false;
		money = 100;
		LinkCropData();
		LinkstoreFieldPolicy();
	}

	//property
	public bool PlaceComplete
	{
		get { return placeComplete; }
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
	void LinkstoreFieldPolicy( )
	{
		tempData = GameObject.FindGameObjectsWithTag( "StoreField" );
		storeFieldGroup = new StoreFieldPolicy[tempData.Length];
		for (int i = 0; i < storeFieldGroup.Length; i++)
		{
			if (tempData[i] != null)
			{
				storeFieldGroup[i] = tempData[i].GetComponent<StoreFieldPolicy>();
				SleepstoreField( storeFieldGroup[i] );
			}
		}
	}

	//store field enable set false
	void SleepstoreField(StoreFieldPolicy store)
	{
		store.enabled = false;
	}

	//process placement step
	public void ProcessPlacementEvent( Vector2 mousePosition )
	{
		if (Input.GetButtonDown( "Click" ))
		{
			Ray ray = Camera.main.ScreenPointToRay( mousePosition );
			RaycastHit hitinfo;

			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "StoreField" ) ))
			{
				GameObject tempSearch = hitinfo.collider.gameObject;
				StoreFieldPolicy tempPolicy = tempSearch.GetComponent<StoreFieldPolicy>();
				tempPolicy.enabled = true;
				tempPolicy.ProcessEvent(presentCrop, presentCropIndex);
			}

		}
	}

	//process game event - moveclick event
	public void ProcessStageEvent(Vector2 mousePosition)
	{

	}

	//store cropitem
	public void storeCrop( int cropIndex )
	{
		//add money & remove crop item in list
	
	}

	//store game start or restart
	public void StartStoreGame( )
	{
		onGame = true;
	}

	//store game close
	public void EndStoreGame( )
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

	//check empty store field
	public bool CheckAllField()
	{
		for(int i = 0; i < storeFieldGroup.Length; i++)
		{
			if(!storeFieldGroup[i].Onstore)
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
