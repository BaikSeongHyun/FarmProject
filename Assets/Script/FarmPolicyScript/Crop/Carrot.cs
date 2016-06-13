using System;
using System.Collections.Generic;

public class Carrot : Crop
{
	//constructor - watermelon
	public Carrot()
	{
		cropName = "Carrot";
		growTime = 40f;
		requireResource = new Resource[2];
		requireResource[0] = Resource.Water; 
		requireResource[1] = Resource.Fertilizer;

		averagePrice = new Dictionary<Rank, float>();
		averagePrice.Add( Rank.S, 100f );
		averagePrice.Add( Rank.A, 80f );
		averagePrice.Add( Rank.B, 60f );
		averagePrice.Add( Rank.C, 40f );
		averagePrice.Add( Rank.F, 10f );
	}
}


