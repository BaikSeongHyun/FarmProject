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

	//menu control
	public void MainMenuControl(bool state)
	{
		//main menu on

		transform.Find( "MainMenuBack" ).GetComponent<Image>().enabled = state;
		transform.Find( "StartPrototypeGameButton" ).GetComponent<Image>().enabled = state;
		transform.Find( "ShopButton" ).GetComponent<Image>().enabled = state;
		transform.Find( "SearchItemButton" ).GetComponent<Image>().enabled = state;
		transform.Find( "ReplaceButton" ).GetComponent<Image>().enabled = state;		
	}

	public void SetMoneyInfor()
	{
		transform.Find( "UpsideItem" ).Find( "MoneyText" ).GetComponent<Text>().text = manager.Money.ToString();
	}
}
