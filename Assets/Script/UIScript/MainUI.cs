using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour
{
	//complex data field
	GameManager manager;

	//initialize this script
	void Start( )
	{
		manager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>();
	}

	//another method

	public void MainMenuControl(bool state)
	{
		//main menu on
		if (state)
		{
			transform.Find( "MainMenuBack" ).GetComponent<Image>().enabled = true;
			transform.Find( "StartPrototypeGameButton" ).GetComponent<Image>().enabled = true;
			transform.Find( "ShopButton" ).GetComponent<Image>().enabled = true;
			transform.Find( "SearchItemButton" ).GetComponent<Image>().enabled = true;
			transform.Find( "ReplaceButton" ).GetComponent<Image>().enabled = true;		
		}
		//main menu off
		else
		{
			transform.Find( "MainMenuBack" ).GetComponent<Image>().enabled = false;
			transform.Find( "StartPrototypeGameButton" ).GetComponent<Image>().enabled = false;
			transform.Find( "ShopButton" ).GetComponent<Image>().enabled = false;
			transform.Find( "SearchItemButton" ).GetComponent<Image>().enabled = false;
			transform.Find( "ReplaceButton" ).GetComponent<Image>().enabled = false;		
		}
	}

}
