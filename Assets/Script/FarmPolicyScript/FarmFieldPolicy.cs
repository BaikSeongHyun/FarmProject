using UnityEngine;
using System.Collections;

public class FarmFieldPolicy : MonoBehaviour
{
	//simple data field
	public bool onCrop;
	public bool create1st;
	public bool create2nd;
	public bool create3rd;
	public bool createComplete;
	public float grewTime;
	public float resourceTime;
	GameObject presentTexture;
	public string fieldName;
	public FarmState presentState;
	public Vector3 presentPosition;
	public Quaternion presentRotation;
	private Crop.Rank tempRank;

	//complex data field
	public Crop presentCrop;

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
		presentTexture = null;
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

	//click event process
	public void ProcessEvent( Crop data, CropItem itemData )
	{
		itemData.SetCropName( null );
		switch(presentState)
		{
			case FarmState.Empty:
				PlantCrop( data );
				break;
			case FarmState.FirstStep:
				FarmWork();
				break;
			case FarmState.SecondStep:
				FarmWork();
				break;
			case FarmState.ThirdStep:
				FarmWork();
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
		resourceTime = ((1 / presentCrop.GetGrowTime()) * 100) + 1;
	}

	//grow crop
	void GrowCrop( )
	{
		if (onCrop && presentState == FarmState.FirstStep && !create1st)
		{
			presentTexture = (GameObject)Instantiate( presentCrop.GetTexture( 0 ), presentPosition, presentRotation );
			//water supply
			create1st = true;
		}
		else if (onCrop && presentState == FarmState.SecondStep && !create2nd)
		{
			Destroy( presentTexture );
			presentTexture = (GameObject)Instantiate( presentCrop.GetTexture( 1 ), presentPosition, presentRotation );
			//Fertilizer supply
			create2nd = true;
		}
		else if (onCrop && presentState == FarmState.ThirdStep && !create3rd)
		{
			Destroy( presentTexture );
			presentTexture = (GameObject)Instantiate( presentCrop.GetTexture( 2 ), presentPosition, presentRotation );
			create3rd = true;
		}
		else if (onCrop && presentState == FarmState.Complete && !createComplete)
		{
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
	void FarmWork( )
	{
		//water supply
		if (presentState == FarmState.FirstStep)
		{
			
		}
		//Fertilizer supply
		else if (presentState == FarmState.SecondStep)
		{
			Debug.Log( "Second resource active" );
		}

	}

	//set crop Rank
	Crop.Rank SetCropRank( )
	{
		return Crop.Rank.S;
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
