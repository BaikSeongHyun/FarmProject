using UnityEngine;
using System.Collections;

public class Thief : Human
{
	//complex data field
	CropItem presentItem;
	//constructor - no parameter
	public Thief()
	{
		moveSpeed = 10f;
		disposition = 0.0f;
		money = 0;
		state = State.Thief;
	}

	//another method
	public void StolenCrop(CropItem data)
	{
		presentItem = new CropItem( data );
	}
}
