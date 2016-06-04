﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crop : MonoBehaviour
{
	//simple data field
	protected string cropName;
	protected float growTime;
	protected Resource[] requireResource;
	protected Rank rank;
	protected float sellPrice;
	protected static int growStep = 4;
	public GameObject[] textureData = new GameObject[growStep];

	//complex data field
	protected Dictionary<Rank, float> averagePrice;

	//enum field
	public enum Resource
	{
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

	public Resource[] GetRequireResource( )
	{
		return requireResource;
	}
	//rank
	public void SetRank( Rank _rank )
	{
		rank = _rank;
	}

	public Rank GetRank( )
	{
		return rank;
	}

	//sellPrice
	public void SetSellPrice( float _sellPrice )
	{
		sellPrice = _sellPrice;
	}

	public float GetSellPrice( )
	{
		return sellPrice;
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

	public GameObject GetTexture( int step )
	{
		return textureData[step];
	}

}

