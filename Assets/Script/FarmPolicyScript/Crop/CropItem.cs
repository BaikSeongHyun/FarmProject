using System;
using UnityEngine;

[System.Serializable]
public class CropItem
{
	//simple data field
	public string cropName;
	public Crop.Rank cropRank;
	public int price;


	//constructor - no parameter
	public CropItem()
	{
		cropName = null;
		cropRank = Crop.Rank.Default;
		price = 0;
	}
	//constructor - self parameter
	public CropItem( CropItem data )
	{
		cropName = data.cropName;
		cropRank = data.cropRank;
		price = data.price;
	}

	//property
	//crop name
	public string Name
	{
		set { cropName = value; }
		get{ return cropName; }
	}
	//crop rank
	public Crop.Rank Rank
	{
		set{ cropRank = value; }
		get { return cropRank; }
	}
	//price
	public int Price
	{
		set { price = value; }
		get { return price; }
	}
		
}
