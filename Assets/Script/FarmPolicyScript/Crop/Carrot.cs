﻿using System;
using System.Collections.Generic;

public class Carrot : Crop
{
	//constructor - watermelon
	public Carrot()
	{
		cropName = "Carrot";
		growTime = 30f;
		seedCount = 0;
		seedPrice = 25;
		requireResource = new Resource[2];
		requireResource[0] = Resource.Water; 
		requireResource[1] = Resource.Fertilizer;

		averagePrice = new Dictionary<Rank, float>();
		averagePrice.Add( Rank.S, 120f );
		averagePrice.Add( Rank.A, 110f );
		averagePrice.Add( Rank.B, 100f );
		averagePrice.Add( Rank.C, 80f );
		averagePrice.Add( Rank.F, 60f );
	}
}


