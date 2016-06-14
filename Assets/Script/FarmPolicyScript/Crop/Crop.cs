using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crop : MonoBehaviour
{
	//simple data field
	public string cropName;
	public float growTime;
	public int seedCount;
	public int seedPrice;
	protected static int growStep = 4;

	//complex data field
	public Dictionary<Rank, float> averagePrice;
	public Resource[] requireResource;
	public Sprite[] resourceImage;
	public Sprite[] cropIcon;
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
		Default,
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

	//crop parameter - self parameter
	public Crop( Crop data )
	{
		this.cropName = data.cropName;
		this.growTime = data.growTime;
		SetResource( data.requireResource );
		this.averagePrice = data.averagePrice;
	}

	//property
	//crop name
	public string Name
	{
		get { return cropName; }
	}

	public int SeedCount
	{
		get { return seedCount; }
		set{ seedCount = value; }
	}

	public int SeedPrice
	{
		get { return seedPrice; }
	}

	//grow time
	public float GrowTime
	{
		get { return growTime; }
	}

	//another method
	public int BuySeed( )
	{
		seedCount++;

		return seedPrice;
	}

	//get / set method
	//resource
	public void SetResource( Resource[] data )
	{
		requireResource = new Resource[data.Length];
		for (int i = 0; i < requireResource.Length; i++)
			requireResource[i] = data[i];
	}

	public Resource GetRequireResource( int i )
	{
		//only use one/two
		return requireResource[i];
	}

	//averagePrice
	public Dictionary<Rank, float> GetAveragePrice( )
	{
		return averagePrice;
	}

	public float GetAveragePrice( Rank rank )
	{
		float result;
		averagePrice.TryGetValue( rank, out result );

		return result;
	}

	//texture
	public GameObject GetTexture( int step )
	{
		return textureData[step];
	}

	//resource image sprite
	public Sprite GetResourceImage( int data )
	{
		return resourceImage[data];
	}

	//IconImage
	public Sprite GetIcon( int index )
	{
		if (index == 0)
			return cropIcon[0];
		else if (index == 1)
			return cropIcon[1];

		return null;
	}
}

