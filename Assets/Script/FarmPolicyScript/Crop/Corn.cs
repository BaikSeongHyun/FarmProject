using UnityEngine;
using System;
using System.Collections.Generic;

	public class Corn : Crop
	{
		//complex data field

			
		//constructor - corn
		public Corn ()
		{
			cropName = "Corn";
			growTime = 20f;
			requireResource = new Resource[2];
			requireResource[0] = Resource.Water; 
			requireResource[1] = Resource.Fertilizer;
		}

		//set average price
		public void SetAveragePrice()
		{
			averagePrice = new Dictionary<Rank, float>();
			averagePrice.Add(Rank.S, 100f);
			averagePrice.Add(Rank.A, 80f);
			averagePrice.Add(Rank.B, 60f);
			averagePrice.Add(Rank.C, 40f);
			averagePrice.Add(Rank.F, 10f);
		}

	}


