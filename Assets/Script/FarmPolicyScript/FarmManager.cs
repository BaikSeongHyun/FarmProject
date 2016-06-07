using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmManager : MonoBehaviour
{
	//simple data field
	public bool onGame;
	public float gameTime;
	public Vector2 mousePosition;
	const float RayCastMaxDistance = 100.0f;

	//complex data field
	public CropItem tempCropItem;
	public Crop.Resource presentResource;
	//dynamic set tis array
	public GameObject[] cropData;
	public Crop[] cropGroup;
	public List<CropItem> saveCropItem;


	// initialize this script
	void Start( )
	{
		onGame = true;
		gameTime = 0.0f;
		presentResource = Crop.Resource.Default;
		LinkCropData();
		tempCropItem = new CropItem();
		saveCropItem = new List<CropItem>();
	}
	
	// Update is called once per frame
	void Update( )
	{
		mousePosition = Input.mousePosition;
		if (onGame)
		{
			gameTime += Time.deltaTime;	
	
			if (Input.GetButtonDown( "Click" ))
			{
				OnMouseClick();
			}

			if (gameTime >= 180f)
				EndFarmGame();
		}
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

	//mouse click event
	void OnMouseClick( )
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

	//harvest crop and input storage
	void AddCropItem( CropItem data )
	{
		if (data.GetCropName() != null)
		{			
			saveCropItem.Add( new CropItem( data ) );
		}
	}

	//farm game start or reStart
	public void StartFarmGame( )
	{
		onGame = true;
	}

	//farm game close
	public void EndFarmGame( )
	{
		onGame = false;
		gameTime = 0.0f;
	}
		
}
