using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmManager : MonoBehaviour
{
	//simple data field
	public bool onGame;
	const float RayCastMaxDistance = 100.0f;
	public Crop.Resource presentResource;

	//complex data field
	public Crop presentSeed;
	public CropItem outputCropItem;

	//this array will be set automaic
	public GameManager gameManager;
	public FarmFieldPolicy[] farmFieldGroup;
	public FarmUI farmUI;
	public Crop[] cropGroup;
	public List<CropItem> saveCropItem;


	// initialize this script
	void Start( )
	{

		presentResource = Crop.Resource.Default;
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		LinkFarmFieldPolicy();
		farmUI = GameObject.FindGameObjectWithTag("FarmCanvas").GetComponent<FarmUI>();
		LinkCropData();
		outputCropItem = new CropItem();
		saveCropItem = new List<CropItem>();
	}

	//property

	public Crop PresentSeed
	{
		get { return presentSeed; }
		set { presentSeed = value; }
	}

	public Crop.Resource PresentResource
	{
		get { return presentResource; }
		set{ presentResource = value; }
	}
			

	//another method
	//initialize game data
	void LinkFarmFieldPolicy( )
	{
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "FarmField" );
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
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "Crop" );
		cropGroup = new Crop[tempData.Length];

		for (int i = 0; i < tempData.Length; i++)
		{
			if (tempData[i] != null)
				cropGroup[i] = tempData[i].GetComponent<Crop>();	
		}
	}

	//farm enable set false
	void SleepFarm( FarmFieldPolicy farm )
	{
		farm.enabled = false;
	}

	//precess game event - mouse click event
	public void ProcessStageEvent( Vector2 mousePosition )
	{
		if (Input.GetButtonDown( "Click" ))
		{
			Ray ray = Camera.main.ScreenPointToRay( mousePosition );
			RaycastHit hitinfo;
			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "FarmField" ) ))
			{
				GameObject tempSearch = hitinfo.collider.gameObject;
				FarmFieldPolicy tempPolicy = tempSearch.GetComponent<FarmFieldPolicy>();
				tempPolicy.enabled = true;
				tempPolicy.ProcessEvent( presentSeed, outputCropItem, presentResource );
				AddCropItem( outputCropItem, tempPolicy );
			}
		}
	}

	//harvest crop and input storage
	void AddCropItem( CropItem data, FarmFieldPolicy farm )
	{
		if (data.Rank != Crop.Rank.Default)
		{			
			saveCropItem.Add( new CropItem( data ) );
			data.SetDefault();
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
	
	//clear game data
	public void InitializeGameData()
	{
		saveCropItem.Clear();
		for(int i = 0; i < farmFieldGroup.Length; i++)
		{
			farmFieldGroup[i].InitializeGameData();
			farmFieldGroup[i].enabled = false;
		}
		farmUI.InitializeGameData();
	}

	//get / set method
	//on game
	public bool CheckOnGame( )
	{
		return onGame;
	}

	//stageMoney - return false => no add item
	public bool SetMoney( int index, bool add )
	{
		if (add)
		{
			if ((gameManager.Money - cropGroup[index].SeedPrice) < 0)
				return false;
			else
			{
				gameManager.Money -= cropGroup[index].SeedPrice;
				return true;
			}			
		}
		else
		{
			gameManager.Money += cropGroup[index].SeedPrice;
			return true;
		}
	}

	//crop group
	public Crop GetCropInformation( string name )
	{
		for (int i = 0; i < cropGroup.Length; i++)
		{
			if (cropGroup[i].Name == name)
				return cropGroup[i];
		}

		return null;
	}

	//crop item
	public List<CropItem> GetCropItem( )
	{
		return saveCropItem;
	}

	public Crop[] GetCropGroup( )
	{
		return cropGroup;
	}
	
	
	//search crop group
	public Crop GetCropSearchByName(string name)
	{
		for(int i = 0; i < cropGroup.Length; i++)
		{
			if(cropGroup[i].Name == name)
				return cropGroup[i];
		}
		return null;
	}
	


}
