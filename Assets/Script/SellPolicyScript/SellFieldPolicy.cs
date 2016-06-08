using UnityEngine;
using System.Collections;

public class SellFieldPolicy : MonoBehaviour
{
	//simple data field
	public bool onSell;

	//complex data field
	CropItem presentItem;

	// initialize this script
	void Start( )
	{
		onSell = false;
	}

	//property
	public bool Selled
	{
		get{ return onSell; }
	}


	//another method

	//click event process
	public void ProcessEvent( )
	{

	}

	//input sell object
	void SetCropItem(CropItem data)
	{
		//draw 
		presentItem = new CropItem(data);
		onSell = true;
	}

	//customer to player baergaining
	public void BargainingCrop(int price)
	{

	}

	//sell crop item
	public void SellCropItem(out int price )
	{
		price = presentItem.Price;
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
