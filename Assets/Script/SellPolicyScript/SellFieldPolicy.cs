using UnityEngine;
using System.Collections;

public class SellFieldPolicy : MonoBehaviour
{
	//simple data field
	public bool onSell;
	public int presentCropIndex;

	//complex data field
	public CropItem presentCrop;

	// initialize this script
	void Start( )
	{
		onSell = false;
	}

	//property
	public bool OnSell
	{
		get{ return onSell; }
	}

	//another method

	//click event process - mouse click event
	public void ProcessEvent( CropItem data, int index )
	{
		if (!onSell)
		{
			SetCropItem( data, index );
		}
	}

	//input sell object
	void SetCropItem( CropItem data, int index )
	{
		presentCropIndex = index;
		presentCrop = new CropItem( data );
		onSell = true;

		//draw texture image 

	}

	//customer to player baergaining
	public void BargainingCrop( int price )
	{

	}

	//sell crop item
	public void SoldOutCropItem( )
	{
		//Destroy( presentItem );
		onSell = false;
	}

	//thief has been stolen crop item
	public void StolenCrop( )
	{
		//Destroy( presentItem );
		onSell = false;
	}
}
