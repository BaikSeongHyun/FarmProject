﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FarmFieldPolicy : MonoBehaviour
{
	//simple data field
	public bool onCrop;
	public bool create1st;
	public bool create2nd;
	public bool create3rd;
	public bool createComplete;
	public bool firstResource;
	public bool secondResource;
	public float grewTime;
	public float resourceTime;
	public string fieldName;
	public Sprite temp;
	public FarmState presentState;
	public Vector3 presentPosition;
	public Quaternion presentRotation;
	private Crop.Rank tempRank;

	//complex data field
	public Crop presentCrop;
	GameObject presentTexture;
	public Image resourceImage;

	//enum field
	public enum FarmState
	{
		Empty,
		FirstStep,
		SecondStep,
		ThirdStep,
		Complete}

	;

	//initialize this script
	void Start( )
	{
		InitialzeCreate();
		InitialzeResource();
		presentTexture = null;
		resourceImage = transform.Find( "FarmCanvas" ).GetComponent<Image>();
		resourceImage.enabled = false;
		Debug.Log( resourceImage );
	}
	
	// Update is called once per frame
	void Update( )
	{
		presentPosition = transform.position;
		presentRotation = transform.rotation;
		SetFarmState();

		if (presentCrop != null)
		{
			onCrop = true;
			grewTime += Time.deltaTime;	
			GrowCrop();
		}
		else
		{
			onCrop = false;
			grewTime = 0.0f;
		}
		
	}

	//another method
	//texture create parameter initialze
	void InitialzeCreate( )
	{
		create1st = false;
		create2nd = false;
		create3rd = false;
		createComplete = false;
	}

	void InitialzeResource( )
	{
		firstResource = false;
		secondResource = false;
	}

	//click event process
	public void ProcessEvent( Crop data, CropItem itemData, Crop.Resource resource )
	{
		itemData.SetCropName( null );
		switch(presentState)
		{
			case FarmState.Empty:
				PlantCrop( data );
				break;
			case FarmState.FirstStep:
				break;
			case FarmState.SecondStep:
				FarmWork( resource );
				break;
			case FarmState.ThirdStep:
				FarmWork( resource );
				break;
			case FarmState.Complete:
				itemData.SetCropName( presentCrop.GetCropName() );
				HarvestCrop( out tempRank );
				itemData.SetRank( tempRank );
				break;
		}

	}

	//plant crop
	void PlantCrop( Crop data )
	{
		presentCrop = data;
		InitialzeResource();
		resourceTime = ((1 / presentCrop.GetGrowTime()) * 100) + 1;

	}

	//grow crop
	void GrowCrop( )
	{
		if (onCrop && presentState == FarmState.FirstStep && !create1st)
		{
			//crop texture update
			presentTexture = (GameObject)Instantiate( presentCrop.GetTexture( 0 ), presentPosition, presentRotation );
			create1st = true;
		}
		else if (onCrop && presentState == FarmState.SecondStep && !create2nd)
		{
			//require resource on - water
			resourceImage.enabled = true;
			resourceImage.sprite = presentCrop.GerResourceImage( 0 );

			//crop texture update
			Destroy( presentTexture );
			presentTexture = (GameObject)Instantiate( presentCrop.GetTexture( 1 ), presentPosition, presentRotation );
			create2nd = true;
		}
		else if (onCrop && presentState == FarmState.ThirdStep && !create3rd)
		{
			//require resource change - fertilizer
			resourceImage.enabled = true;
			resourceImage.sprite = presentCrop.GerResourceImage( 1 );

			//crop texture update
			Destroy( presentTexture );
			presentTexture = (GameObject)Instantiate( presentCrop.GetTexture( 2 ), presentPosition, presentRotation );
			create3rd = true;
		}
		else if (onCrop && presentState == FarmState.Complete && !createComplete)
		{
			//desapire resource image
			resourceImage.enabled = false;

			//crop texture update
			Destroy( presentTexture );
			presentTexture = (GameObject)Instantiate( presentCrop.GetTexture( 3 ), presentPosition, presentRotation );
			createComplete = true;
		}
	}

	//harvestCrop
	void HarvestCrop( out Crop.Rank data )
	{
		presentCrop = null;
		Destroy( presentTexture );
		InitialzeCreate();
		grewTime = 0.0f;
		data = SetCropRank();
	}

	//work farm - supply resource
	void FarmWork( Crop.Resource resource )
	{
		//first item supply
		if (presentState == FarmState.SecondStep)
		{
			if (presentCrop.GetRequireResource( 0 ) == resource)
			{
				firstResource = true;
				resourceImage.enabled = false;
			}
		}
		//second item supply
		else if (presentState == FarmState.ThirdStep)
		{
			if (presentCrop.GetRequireResource( 1 ) == resource)
			{
				secondResource = true;
				resourceImage.enabled = false;
			}
		}

	}

	//see item
	void SupplyItem( Crop.Resource resource )
	{
		if (presentState == FarmState.SecondStep)
		{
	
			Debug.Log( "First resource active" );
		}
		else if (presentState == FarmState.ThirdStep)
		{
			Debug.Log( "Second resource active" );
		}
	}

	//set crop Rank
	Crop.Rank SetCropRank( )
	{
		if (firstResource && secondResource)
			return Crop.Rank.S;
		else if (firstResource || secondResource)
			return Crop.Rank.A;

		return Crop.Rank.B;
	}

	//set farm state
	void SetFarmState( )
	{
		if (grewTime == 0.0f)
			presentState = FarmState.Empty;
		else if (grewTime < presentCrop.GetGrowTime() / 3)
			presentState = FarmState.FirstStep;
		else if (grewTime < (presentCrop.GetGrowTime() / 3) * 2)
			presentState = FarmState.SecondStep;
		else if (grewTime < presentCrop.GetGrowTime())
			presentState = FarmState.ThirdStep;
		else if (presentCrop.GetGrowTime() < grewTime)
			presentState = FarmState.Complete;
	}
}
