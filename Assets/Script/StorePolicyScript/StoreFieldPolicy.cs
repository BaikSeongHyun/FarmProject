using UnityEngine;
using System.Collections;

public class StoreFieldPolicy : MonoBehaviour
{
	//simple data field
	public bool onstore;
	public int presentCropIndex;

	//complex data field
	public CropItem presentCrop;

	// initialize this script
	void Start( )
	{
		onstore = false;
	}

	//property
	public bool Onstore
	{
		get{ return onstore; }
	}

	//another method

	//click event process - mouse click event
	public void ProcessEvent( CropItem data, int index )
	{
		if (!onstore)
		{
			SetCropItem( data, index );
		}
	}

	//input store object
	void SetCropItem( CropItem data, int index )
	{
		presentCropIndex = index;
		presentCrop = new CropItem( data );
		onstore = true;

		//draw texture image 

	}

	//customer to player baergaining
	public void BargainingCrop( int price )
	{

	}

	//store crop item
	public void SoldOutCropItem( )
	{
		//Destroy( presentItem );
		onstore = false;
	}

	//thief has been stolen crop item
	public void StolenCrop( )
	{
		//Destroy( presentItem );
		onstore = false;
	}
}
