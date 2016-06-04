using System;
using UnityEngine;

[System.Serializable]
public class CropItem
{
	//simple data field
	public string cropName;
	public Crop.Rank cropRank;
	public int price;


	//constructor
	public CropItem()
	{
		cropName = null;
	}

	//get / set method
	//cropName
	public void SetCropName(string data)
	{
		cropName = data;
	}
	public string GetCropName()
	{
		return cropName;
	}

	//Rank
	public void SetRank(Crop.Rank data)
	{
		cropRank = data;
	}
	public Crop.Rank GetRank()
	{
		return cropRank;
	}
}
