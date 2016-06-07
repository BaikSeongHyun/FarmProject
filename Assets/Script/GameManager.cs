using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	//simple data fiele
	public float gameTime;
	public Vector2 mousePosition;

	//complex data field
	FarmManager farmStage;
	SellManager sellStage;

	// Use this for initialization
	void Start( )
	{
		gameTime = 0.0f;
		farmStage = GetComponent<FarmManager>();
		sellStage = GetComponent<SellManager>();

	}
	
	// Update is called once per frame
	void Update( )
	{
		mousePosition = Input.mousePosition;
		if (farmStage.GetOnGame())
			farmStage.ProcessStageEvent(mousePosition);		
	}
	//another method
	//move crop item data (farm to sell) 
	void TransferCropItem()
	{
		sellStage.SetCropItem( farmStage.GetCropItem().ToArray() );
	}

}
