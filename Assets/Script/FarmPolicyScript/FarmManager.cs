using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmManager : MonoBehaviour
{
	//simple data field
	public bool onGame;
	const float RayCastMaxDistance = 100.0f;

	//complex data field
	public CropItem tempCropItem;
	public Crop.Resource presentResource;
	//this array will be set automaic
	public GameObject[] tempData;
	public FarmFieldPolicy[] farmFieldGroup;
	public Crop[] cropGroup;
	public List<CropItem> saveCropItem;


	// initialize this script
	void Start( )
	{
		presentResource = Crop.Resource.Default;
		LinkFarmFieldPolicy();
		LinkCropData();
		tempCropItem = new CropItem();
		saveCropItem = new List<CropItem>();
	}

	//another method
	//initialize game data
	void LinkFarmFieldPolicy( )
	{
		tempData = GameObject.FindGameObjectsWithTag( "FarmField" );
		farmFieldGroup = new FarmFieldPolicy[tempData.Length];
		for (int i = 0; i < farmFieldGroup.Length; i++)
		{
			if (tempData[i] != null)
			{
				farmFieldGroup[i] = tempData[i].GetComponent<FarmFieldPolicy>();
				SleepFarm( farmFieldGroup[i] );
			}
		}
	}
	void LinkCropData( )
	{
		tempData = GameObject.FindGameObjectsWithTag( "Crop" );
		cropGroup = new Crop[tempData.Length];
		GameObject temp = GameObject.FindGameObjectWithTag( "Crop" );
		if (temp != null)
			cropGroup[0] = temp.GetComponent<Crop>();		
	}

	//farm enable set false
	void SleepFarm(FarmFieldPolicy farm)
	{
		farm.enabled = false;
	}

	//precess game event - mouse click event
	public void ProcessStageEvent( Vector2 mousePosition )
	{
		if (Input.GetButtonDown( "Click" ) && onGame)
		{
			Ray ray = Camera.main.ScreenPointToRay( mousePosition );
			RaycastHit hitinfo;
			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "FarmField" ) ))
			{
				GameObject tempSearch = hitinfo.collider.gameObject;
				FarmFieldPolicy tempPolicy = tempSearch.GetComponent<FarmFieldPolicy>();
				tempPolicy.enabled = true;
				tempPolicy.ProcessEvent( cropGroup[0], tempCropItem, presentResource );
				AddCropItem( tempCropItem, tempPolicy );
			}

			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "Resource" ) ))
			{
				GameObject tempSearch = hitinfo.collider.gameObject;
				Resource temp = tempSearch.GetComponent<Resource>();
				presentResource = temp.GetResource();
			}
		}
	}

	//harvest crop and input storage
	void AddCropItem( CropItem data, FarmFieldPolicy farm )
	{
		if (data.Name != null)
		{			
			saveCropItem.Add( new CropItem( data ) );
			SleepFarm( farm );
		}
	}

	//farm game start or restart
	public void StartFarmGame( )
	{
		onGame = true;
	}

	//farm game close
	public void EndFarmGame( )
	{
		onGame = false;
	}

	//get / set method
	//on game
	public bool CheckOnGame( )
	{
		return onGame;
	}

	//crop item
	public List<CropItem> GetCropItem( )
	{
		return saveCropItem;
	}
}
