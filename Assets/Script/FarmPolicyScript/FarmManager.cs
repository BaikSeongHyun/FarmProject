using UnityEngine;
using System.Collections;

public class FarmManager : MonoBehaviour
{
	//simple data field
	public float gameTime;
	//complex data field
	//dynamic set tis array
	public GameObject[] farmField;
	public GameObject[] cropData;
	public FarmFieldPolicy[] farmFieldGroup;
	public Crop[] cropGroup;

	// initialize this script
	void Start( )
	{
		gameTime = 0.0f;
		LinkFarmFieldPolicy();
		LinkCropData();
	}
	
	// Update is called once per frame
	void Update( )
	{
		gameTime += Time.deltaTime;	
		if (Input.GetKey( KeyCode.P ))
		{
			Debug.Log("Press P");
		}

	}

	//another method
	void LinkFarmFieldPolicy( )
	{
		farmField = GameObject.FindGameObjectsWithTag( "FarmField" );
		farmFieldGroup = new FarmFieldPolicy[farmField.Length];
		for (int i = 0; i < farmFieldGroup.Length; i++)
		{
			if (farmField[i] != null)
				farmFieldGroup[i] = farmField[i].GetComponent<FarmFieldPolicy>();
		}
	
	}

	void LinkCropData()
	{
		cropData = GameObject.FindGameObjectsWithTag( "Crop" );
		cropGroup = new Crop[cropData.Length];
		GameObject temp = GameObject.FindGameObjectWithTag( "Crop" );
		if (temp != null)
			cropGroup[0] = temp.GetComponent<Crop>();
	}
}
