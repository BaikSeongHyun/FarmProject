using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour
{
	//complex data field
	GameManager manager;
	public Sprite[] result;

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

	//set money text
	public void SetMoneyInfor()
	{
		transform.Find( "UpsideItem" ).Find( "MoneyText" ).GetComponent<Text>().text = manager.Money.ToString();
	}	
	
	//pop up result
	public void PopUpResult(string state)
	{
		ControlResultPopUp(true);

		transform.Find("Result").GetComponent<Image>().sprite = SetResultSprite(state);
	}

	//control image(pop up) for result
	public void ControlResultPopUp (bool state)
	{
		transform.Find( "Result" ).GetComponent<Image>().enabled = state;
		transform.Find( "CancelResult" ).GetComponent<Image>().enabled = state;
	}

	//set sprite for result pop up
	public Sprite SetResultSprite (string state)
	{
		switch (state)
		{
			case "Success":
				return result[0];
			case"Failure":
				return result[1];
		}

		return null;
	}
	
	//cancel - button event cancel
	public void Cancel(string name)
	{
		if(name == "Result")
			ControlResultPopUp(false);
	}
		
}
