using UnityEngine;
using System.Collections;

public class FarmManager : MonoBehaviour
{
	//simple data field
	public int countFarmField = 4;

	//complex data field
	//array link will be inspector
	FarmField[] farm;

	// initialize this script
	void Start ()
	{
		farm = new FarmField[countFarmField];
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
