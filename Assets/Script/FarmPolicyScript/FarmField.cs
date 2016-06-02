using UnityEngine;
using System.Collections;

public class FarmField : MonoBehaviour
{
	//simple data field

	//complex data field
	Vector3 presentPosition;
	Crop presentCrop;

	//enum field
	enum FarmState
	{
		Empty,
		FirstStep,
		SecondStep,
		ThirdStep,
		Complete}

	;

	//initialization this script
	void Start ()
	{
		presentPosition = transform.position;
		presentCrop = null;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void AddCrop(Crop data)
	{
		presentCrop = new Crop(data);
	}
}
