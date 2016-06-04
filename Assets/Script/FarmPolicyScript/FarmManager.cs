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

	public GameObject tempSearchFarm;
	public FarmFieldPolicy tempPolicy;
	public CropItem tempCropItem;
	//dynamic set tis array
	public GameObject[] cropData;
	public Crop[] cropGroup;
	public List<CropItem> saveCropItem;


	// initialize this script
	void Start( )
	{
		onGame = true;
		gameTime = 0.0f;
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
			tempSearchFarm = hitinfo.collider.gameObject;
			tempPolicy = tempSearchFarm.GetComponent<FarmFieldPolicy>();
			tempPolicy.ProcessEvent( cropGroup[0], tempCropItem );
			AddCropItem( tempCropItem );
		}
	}
	//harvest crop and input storage
	void AddCropItem( CropItem data )
	{
		if (data.GetCropName() != null)
		{
			Debug.Log( data.GetCropName() );
			saveCropItem.Add( data );
		}
	}

	//farm game start or reStart
	void StartFarmGame( )
	{
		onGame = true;
	}

	//farm game close
	void EndFarmGame( )
	{
		onGame = false;
		gameTime = 0.0f;
	}
		
}
