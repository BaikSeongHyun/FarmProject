using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoreUI : MonoBehaviour
{
	//simple datafield
	int moveX;
	int presentIndex;
	int price;
	float gameTime;
	int tempPrice;

	//complex data field
	public Sprite soldCrop;
	public Sprite stolenCrop;
	public Sprite[] rankSprite;
	public Sprite[] numberSprite;
	public Sprite[] itemSpriteColor;
	public Sprite[] itemSpriteGray;
	public CropItem[] cropData;
	public GameObject[] button;
	Scrollbar timeBar;
	StoreManager manager;
	GameObject priceSetPopUp;
	GameObject bargainPopUp;
	Human presentBargainCustomer;

	// initialize this script
	void Start ()
	{
		timeBar = transform.Find ("TimeBar").GetComponent<Scrollbar> ();
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<StoreManager> ();
		priceSetPopUp = GameObject.Find ("PriceSetPopUp");
		priceSetPopUp.GetComponent<Canvas> ().enabled = false;
		bargainPopUp = GameObject.Find ("BargainPopUp");
		bargainPopUp.GetComponent<Canvas> ().enabled = false;
	}

	//another method

	//wake up canvas - UI activate step
	public void WakeUpCanvas ()
	{
		Canvas temp = GetComponent<Canvas> ();
		temp.enabled = true;
	}

	//sleep canvas - UI sleep step
	public void SleepCanvas ()
	{
		Canvas temp = GetComponent<Canvas> ();
		temp.enabled = false;
	}

	//link crop item data and create button
	public void LinkCropItem (CropItem[] data)
	{
		//set cropData
		cropData = new CropItem[data.Length];
		for (int i = 0; i < data.Length; i++)
			cropData [i] = data [i];
		
		//button create
		button = new GameObject[cropData.Length];
		for (int i = 0; i < cropData.Length; i++)
			button [i] = CreateNewItemButton (cropData [i], i);
	
	}

	//set game time and renewal scroll bar value
	public void SetGameTime (float time)
	{
		gameTime = time;
		timeBar.value = (1 - gameTime / 60f);
	}

	//make button method
	GameObject CreateNewItemButton (CropItem itemData, int index)
	{
		//create button object
		GameObject button = new GameObject ();
		button.name = "CropItemButton";
		button.AddComponent<RectTransform> ();
		button.AddComponent<CanvasRenderer> ();
		button.AddComponent<Image> ();
		button.AddComponent<Button> ();

		//move parent object
		button.transform.parent = transform;

		//set button object information
		//pos
		button.transform.localPosition = new Vector3 (-340f + (index * 60f), 150f, 0f);
		button.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		//button.transform.GetComponent<RectTransform>().sizeDelta = ( new Vector2( 0.5f, 0.5f ) );
		//image
		button.GetComponent<Image> ().sprite = SetSprite (itemData.Name, false);
		//add call back method
		button.GetComponent<Button> ().onClick.AddListener (() =>
		{
			PopUpCropItem (itemData, index);
		});

		//return object
		return button;
	}

	//button sprite select
	Sprite SetSprite (string name, bool place)
	{
		if (!place)
		{
			switch (name)
			{
				case "Corn":
					return itemSpriteColor [0];
				case "Carrot":
					return itemSpriteColor [1];
				case "Pumpkin":
					return itemSpriteColor [2];
			}
		}
		if (place)
		{
			switch (name)
			{
				case "Corn":
					return itemSpriteGray [0];
				case "Carrot":
					return itemSpriteGray [1];
				case "Pumpkin":
					return itemSpriteGray [2];
			}
		}

		return null;
	}

	//section pop up cropItem
	//button event method - dynamic crop button
	public void PopUpCropItem (CropItem data, int index)
	{
		if (!manager.CheckAllField ())
		{
			//pop up message
			Debug.Log ("Store field is full");
			return;
		}
		price = 0;
		transform.Find ("PriceSetPopUp").transform.Find ("Rank").GetComponent<Image> ().sprite = SetRankSprite (data.Rank);
		transform.Find ("PriceSetPopUp").transform.Find ("Item").GetComponent<Image> ().sprite = SetSprite(data.Name, false);
		ChangeNumberImage ();
		presentIndex = index;
		priceSetPopUp.GetComponent<Canvas> ().enabled = true;
		priceSetPopUp.transform.Find ("AveragePrice").GetComponent<Image> ().sprite = manager.SetAverageCropTable (data.Name);
	}

	//return sprite for rank icon
	Sprite SetRankSprite (Crop.Rank rank)
	{
		switch (rank)
		{
			case Crop.Rank.S:
				return rankSprite [0];
			case Crop.Rank.A:
				return rankSprite [1];
			case Crop.Rank.B:
				return rankSprite [2];
			case Crop.Rank.C:
				return rankSprite [3];
		}

		return null;
	}

	//button event method - price +-
	public void SetPlusPrice (int key)
	{
		switch (key)
		{
			case 100:
				price += 100;
				break;
			case 10:
				price += 10;
				break;
			case 1:
				price += 1;
				break;
		}
		ChangeNumberImage ();
	}

	public void SetMinusPrice (int key)
	{
		switch (key)
		{
			case 100:
				price -= 100;
				break;
			case 10:
				price -= 10;
				break;
			case 1:
				price -= 1;
				break;
		}
		ChangeNumberImage ();

	}
	//set number index for set price
	int FindNumberIndex (int key)
	{
		price = Mathf.Abs (price);
		int tempValue = price;
		if (key == 1)
			return (tempValue % 10);
		else if (key == 10)
			return ((tempValue / 10) % 10);
		else if (key == 100)
			return (tempValue / 100);
		else
			return 0;
	}

	public void ChangeNumberImage ()
	{
		transform.Find ("PriceSetPopUp").transform.Find ("Hundred").GetComponent<Image> ().sprite = numberSprite [FindNumberIndex (100)];
		transform.Find ("PriceSetPopUp").transform.Find ("Ten").GetComponent<Image> ().sprite = numberSprite [FindNumberIndex (10)];
		transform.Find ("PriceSetPopUp").transform.Find ("One").GetComponent<Image> ().sprite = numberSprite [FindNumberIndex (1)];
	}
	//button event method - cancel set price
	public void CancelSetPrice ()
	{
		priceSetPopUp.GetComponent<Canvas> ().enabled = false;
	}
		
	//button event method - confirm price and send crop item by store manager
	public void ConfirmPrice ()
	{
		cropData [presentIndex].Price = price;
		//send object and destroy object
		if (manager.LinkPresentCropItem (cropData [presentIndex], presentIndex))
		{
			button [presentIndex].GetComponent<Image> ().sprite = SetSprite (cropData [presentIndex].Name, true);
			button [presentIndex].GetComponent<Button> ().enabled = false;
			priceSetPopUp.GetComponent<Canvas> ().enabled = false;
		}
	}

	//sprite set sold out
	public void SoldOutCrop (int index)
	{
		button [index].GetComponent<Image> ().sprite = soldCrop;
	}

	//sprite set Stolen
	public void StolenCrop (int index)
	{
		button [index].GetComponent<Image> ().sprite = stolenCrop;
	}

	//clear button object
	public void ClearItemButton ()
	{
		for (int i = 0; i < button.Length; i++)
		{
			Destroy (button [i]);
		}
	}
		
	//section pop up bargain
	//button event method
	public void PopUpBargain (Human human)
	{
		//pop up canvas for bargain
		bargainPopUp.GetComponent<Canvas> ().enabled = true;
		human = presentBargainCustomer;
	}

	public void ComfirmBargain ()
	{
		bargainPopUp.GetComponent<Canvas> ().enabled = false;
		presentBargainCustomer.SuccessBargain ();
	}

	public void RejectBargain ()
	{
		bargainPopUp.GetComponent<Canvas> ().enabled = false;
		presentBargainCustomer.FailureBargain ();
	}
}
