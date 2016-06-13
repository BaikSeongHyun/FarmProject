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
	//this array will be set automatic
	public CropItem presentCrop;
	public GameObject[] cropData;
	public Crop[] cropGroup;
	public Sprite[] cropAverageTable;
	public StoreFieldPolicy[] storeFieldGroup;
	public StoreUI storeUI;

	//initialize this script
	void Start( )
	{
		onGame = false;
		placeComplete = false;
		money = 100;
		LinkCropData();
		LinkstoreFieldPolicy();
		storeUI = GameObject.FindGameObjectWithTag( "StoreCanvas" ).GetComponent<StoreUI>();
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
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "Crop" );
		cropGroup = new Crop[tempData.Length];

		for (int i = 0; i < tempData.Length; i++)
		{
			if (tempData[i] != null)
				cropGroup[i] = tempData[i].GetComponent<Crop>();	
		}
	}
	void LinkstoreFieldPolicy( )
	{
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "StoreField" );
		storeFieldGroup = new StoreFieldPolicy[tempData.Length];
		for (int i = 0; i < storeFieldGroup.Length; i++)
		{
			if (tempData[i] != null)
			{
				storeFieldGroup[i] = tempData[i].GetComponent<StoreFieldPolicy>();
				SleepStoreField( storeFieldGroup[i] );
			}
		}
	}

	//store field enable set false
	void SleepStoreField(StoreFieldPolicy store)
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
		Ray ray = Camera.main.ScreenPointToRay( mousePosition );
		RaycastHit hitinfo;

		if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "Human" ) ))
		{
			Human human = hitinfo.collider.gameObject.GetComponent<Human>();
			if(human.OnBargain)
			{
				storeUI.PopUpBargain( human );
			}
		}
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
			if(!storeFieldGroup[i].OnStore)
				return true;
		}
	
		return false;
	}

	//sold or kill thief
	public void AddMoney(int value)
	{
		money += value;
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

	public Crop[] GetCropGroup()
	{
		return cropGroup;
	}

}
