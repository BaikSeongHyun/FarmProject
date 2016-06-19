using UnityEngine;
using System.Collections;

public class StoreFieldPolicy : MonoBehaviour
{
	//simple data field
	public bool onStore;
	public int presentCropIndex;

	//complex data field
	GameObject presentTexture;
	public CropItem presentCrop;
	public StoreManager manager;
	public StoreUI storeUI;

	//standard method
	// initialize this script
	void Start ()
	{
		onStore = false;
		LinkData();
	}

	//property
	public bool OnStore
	{
		get{ return onStore; }
	}

	public int CropIndex
	{
		get { return presentCropIndex; }
	}

	public CropItem PresentItem
	{
		get { return presentCrop; }
	}


	//another method

	//link UI & manager
	public void LinkData ()
	{
		manager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<StoreManager>();
		storeUI = GameObject.FindGameObjectWithTag( "StoreCanvas" ).GetComponent<StoreUI>();
	}
	
	//click event process - mouse click event
	public void ProcessEvent (CropItem data, int index)
	{
		if (!onStore)
		{
			SetCropItem( data, index );
			onStore = true;
		}
	}

	//input store object
	void SetCropItem (CropItem data, int index)
	{
		LinkData();
		if (data != null)
		{
			presentCropIndex = index;
			presentCrop = new CropItem(data);
			onStore = true;
			presentTexture = (GameObject)Instantiate( manager.FindCropItemTexture( presentCrop.Name ), transform.position, new Quaternion(0f, 0f, 0f, 0f) );
		}
		//draw texture image 

	}

	//customer to player bargaining
	public void BargainSoldOutCrop (int price)
	{
		onStore = false;
		manager.AddMoney( price );
		storeUI.UpdateMoneyInfor( "Sold out", presentCrop.Price );
		presentCrop = null;
		Destroy( presentTexture );
		//send renewal data by store UI
	}

	//store crop item
	public void SoldOutCropItem ()
	{
		//Destroy( presentItem );
		manager.AddMoney( presentCrop.Price );
		storeUI.UpdateMoneyInfor( "Sold out", presentCrop.Price );
		onStore = false;
		presentCrop = null;
		Destroy( presentTexture );
		//send renewal data by store UI

	}

	//thief has been stolen crop item
	public void StealCrop (out int stealPrice)
	{
		//Destroy( presentItem );
		onStore = false;
		Destroy( presentTexture );

		stealPrice = presentCrop.Price;
		//send renewal data by store UI
	}

	//clear game data
	public void InitializeGameData ()
	{
		if (presentTexture != null)
			Destroy( presentTexture );
		presentCropIndex = 0;	
		presentCrop = null;
		onStore = false;
	}
}
