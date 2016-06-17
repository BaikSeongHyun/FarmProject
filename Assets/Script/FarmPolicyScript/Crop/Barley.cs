using System;
using System.Collections.Generic;

public class Barley : Crop
{
	//constructor - strawberry
	public Barley()
	{
		cropName = "Barley";
		growTime = 25f;
		seedCount = 0;
		seedPrice = 20;
		requireResource = new Resource[2];
		requireResource[0] = Resource.Water; 
		requireResource[1] = Resource.Fertilizer;

		averagePrice = new Dictionary<Rank, float>();
		averagePrice.Add( Rank.S, 110f );
		averagePrice.Add( Rank.A, 90f );
		averagePrice.Add( Rank.B, 40f );
		averagePrice.Add( Rank.C, 20f );
		averagePrice.Add( Rank.F, 0f );
	}
}


