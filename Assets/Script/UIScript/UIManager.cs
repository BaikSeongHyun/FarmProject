using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{

	// initialize this script
	void Start( )
	{
		Screen.SetResolution( Screen.width, Screen.width * 16 / 9, true );
	}

}
