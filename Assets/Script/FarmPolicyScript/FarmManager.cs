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
	//set automatic this array
	public GameObject[] cropData;
	public Crop[] cropGroup;
	public List<CropItem> saveCropItem;


	// initialize this script
	void Start( )
	{
		onGame = false;
		presentResource = Crop.Resource.Default;
		LinkCropData();
		tempCropItem = new CropItem();
		saveCropItem = new List<CropItem>();
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
				tempPolicy.ProcessEvent( cropGroup[0], tempCropItem, presentResource );
				AddCropItem( tempCropItem );
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
	void AddCropItem( CropItem data )
	{
		if (data.GetCropName() != null)
		{			
			saveCropItem.Add( new CropItem( data ) );
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
	public bool GetOnGame( )
	{
		return onGame;
	}

	//crop item
	public List<CropItem> GetCropItem( )
	{
		return saveCropItem;
	}
}
