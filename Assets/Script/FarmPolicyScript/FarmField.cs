using UnityEngine;
using System.Collections;

public class FarmField : MonoBehaviour
{
	//simple data field
	public bool onCrop;
	public bool create1st;
	public bool create2nd;
	public bool create3rd;
	public bool createComplete;
	public float grewTime;
	public FarmState presentState;
	public Vector3 presentPosition;
	public Quaternion presentRotation;

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

	//initialization this script
	void Start( )
	{
		InitialzeCreate();	
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
	void InitialzeCreate( )
	{
		create1st = false;
		create2nd = false;
		create3rd = false;
		createComplete = false;
	}


	//plant crop
	void PlantCrop( Crop data )
	{
		presentCrop = new Crop( data );
	}

	//grow crop
	void GrowCrop( )
	{
		if (onCrop && presentState == FarmState.FirstStep && !create1st)
		{
			Instantiate( presentCrop.GetTexture( 0 ), presentPosition, presentRotation );
			create1st = true;
		}
		else if (onCrop && presentState == FarmState.SecondStep && !create2nd)
		{
			Instantiate( presentCrop.GetTexture( 1 ), presentPosition, presentRotation );
			create2nd = true;
		}
		else if (onCrop && presentState == FarmState.ThirdStep && !create3rd)
		{
			Instantiate( presentCrop.GetTexture( 2 ), presentPosition, presentRotation );
			create3rd = true;
		}
		else if (onCrop && presentState == FarmState.Complete && !createComplete)
		{
			Instantiate( presentCrop.GetTexture( 3 ), presentPosition, presentRotation );
			createComplete = true;
		}
	}

	//harvestCrop
	void HarvestCrop( out Crop data )
	{
		data = new Crop( presentCrop );
		presentCrop = null;
		InitialzeCreate();
	}

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
