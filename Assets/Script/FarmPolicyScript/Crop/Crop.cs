using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crop : MonoBehaviour
{
	//simple data field
	public string cropName;
	public float growTime;
	protected static int growStep = 4;

	//complex data field
	public Dictionary<Rank, float> averagePrice;
	public Resource[] requireResource;
	public Sprite[] resourceImage;
	public GameObject[] textureData = new GameObject[growStep];


	//enum field
	public enum Resource
	{
		Default,
		Water,
		Fertilizer}

	;

	public enum Rank
	{
		S,
		A,
		B,
		C,
		F}
	;

	//constructor
	//no parameter - for inheritance instance
	public Crop()
	{
		averagePrice = new Dictionary<Rank, float>();
	}

	//crop parameter - for add crop by farm field
	public Crop( Crop data )
	{
		this.cropName = data.cropName;
		this.growTime = data.growTime;
		SetResource( data.requireResource );
		this.averagePrice = data.averagePrice;
	}

	//method


	//get / set method
	//name
	public string GetCropName( )
	{
		return cropName;
	}

	//resource
	public void SetResource( Resource[] data )
	{
		requireResource = new Resource[data.Length];
		for (int i = 0; i < requireResource.Length; i++)
			requireResource[i] = data[i];
	}
	public Resource GetRequireResource(int i )
	{
		//only use one/two
		return requireResource[i];
	}

	//averagePrice
	public Dictionary<Rank, float> GetAveragePrice( )
	{
		return averagePrice;
	}

	//grow time
	public float GetGrowTime( )
	{
		return growTime;
	}

	//texture
	public GameObject GetTexture( int step )
	{
		return textureData[step];
	}

	//resource image sprite
	public Sprite GerResourceImage(int data)
	{
		return resourceImage[data];
	}
		
}

