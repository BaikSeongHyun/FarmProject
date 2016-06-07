using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
	//simple data field
	protected Crop.Resource resourceName;

	//constructor
	public Resource()
	{
		resourceName = Crop.Resource.Default;
	}

	//get / set method
	public Crop.Resource GetResource()
	{
		return resourceName;
	}

}


