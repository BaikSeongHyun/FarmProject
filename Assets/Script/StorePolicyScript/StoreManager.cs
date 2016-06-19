using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
	//simple data field
	public bool onGame;
	public bool placeComplete;
	public bool onSkill;
	const float RayCastMaxDistance = 100.0f;
	public int presentCropIndex;
	public float generateCycle;
	public float generateCoolTime;

	//complex data field
	//this array will be set automatic
	public CropItem presentCrop;
	public GameObject[] cropData;
	public Crop[] cropGroup;
	public Skill[] skillGroup;
	public Sprite[] cropAverageTable;
	public Skill presentSkill;
	public StoreFieldPolicy[] storeFieldGroup;
	public StoreUI storeUI;
	public GameObject[] humanObject;
	public Vector3[] startPos;
	public Human[] humanGroup;
	public GameManager gameManager;


	//initialize this script
	void Start ()
	{
		onGame = false;
		placeComplete = true;
		onSkill = false;
		presentCrop = null;
		generateCoolTime = 0.0f;
		generateCycle = 10f;
		humanGroup = new Human[10];
		LinkCropData();
		LinkSkillData();
		LinkStoreFieldPolicy();
		LinkStartPos();
		storeUI = GameObject.FindGameObjectWithTag( "StoreCanvas" ).GetComponent<StoreUI>();
		gameManager = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>();
	}

	//property
	public bool PlaceComplete
	{
		get { return placeComplete; }
	}

	//another method
	//initialize game data
	//crop data
	void LinkCropData ()
	{
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "Crop" );
		cropGroup = new Crop[tempData.Length];

		for (int i = 0; i < cropGroup.Length; i++)
		{
			if (tempData[i] != null)
				cropGroup[i] = tempData[i].GetComponent<Crop>();	
		}
	}
	//skill data
	void LinkSkillData ()
	{
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "Skill" );
		skillGroup = new Skill[tempData.Length];

		for (int i = 0; i < skillGroup.Length; i++)
		{
			if (tempData[i] != null)
				skillGroup[i] = tempData[i].GetComponent<Skill>();
		}
	}
	//store field policy script
	void LinkStoreFieldPolicy ()
	{
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "StoreField" );
		storeFieldGroup = new StoreFieldPolicy[tempData.Length];
		for (int i = 0; i < storeFieldGroup.Length; i++)
		{
			if (tempData[i] != null)
			{
				storeFieldGroup[i] = tempData[i].GetComponent<StoreFieldPolicy>();
				SleepStoreField( storeFieldGroup[i] );
			}
		}
	}
	//link human generate position
	void LinkStartPos ()
	{
		GameObject[] tempData = GameObject.FindGameObjectsWithTag( "StartPos" );
		startPos = new Vector3[tempData.Length];

		for (int i = 0; i < startPos.Length; i++)
		{
			if (tempData[i] != null)
			{
				startPos[i] = tempData[i].transform.position;
			}
		}
	}

	//store field enable set false
	void SleepStoreField (StoreFieldPolicy store)
	{
		
	}

	//process placement step
	public void ProcessPlacementEvent (Vector2 mousePosition)
	{
		if (Input.GetButtonDown( "Click" ))
		{
			Ray ray = Camera.main.ScreenPointToRay( mousePosition );
			RaycastHit hitinfo;

			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "StoreField" ) ) && presentCrop != null )
			{
				GameObject tempSearch = hitinfo.collider.gameObject;
				StoreFieldPolicy tempPolicy = tempSearch.GetComponent<StoreFieldPolicy>();
				tempPolicy.enabled = true;
				tempPolicy.ProcessEvent( presentCrop, presentCropIndex );
				presentCrop = null;
			}

		}
	}

	//process game event - moveclick event
	public void ProcessStageEvent (Vector2 mousePosition)
	{
		//skill event
		if (Input.GetButtonDown( "Click" ) && onSkill)
		{
			presentSkill.ActiveSkill();
			storeUI.DrawSkillImage( presentSkill.SkillName, presentSkill.SkillCounter );
			if (presentSkill.CheckSkillIsEmpty())
			{
				ResetSkill();
				storeUI.ClearSkillImage();
			}
		}
		else if (Input.GetButtonDown( "SkillOff" ) && onSkill)
		{
			ResetSkill();
			storeUI.ClearSkillImage();
		}

		//human event
		if (Input.GetButtonDown( "Click" ) && !onSkill)
		{
			Ray ray = Camera.main.ScreenPointToRay( mousePosition );
			RaycastHit hitinfo;

			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "Human" ) ))
			{
				Human human = hitinfo.collider.gameObject.GetComponent<Human>();
				if (human.OnBargain)
				{
					storeUI.PopUpBargain( human );
				}
			}
		}
		
		if (Input.GetButtonDown( "Click" ))
		{
			Ray ray = Camera.main.ScreenPointToRay( mousePosition );
			RaycastHit hitinfo;

			if (Physics.Raycast( ray, out hitinfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer( "StoreField" ) ) && presentCrop != null )
			{
				GameObject tempSearch = hitinfo.collider.gameObject;
				StoreFieldPolicy tempPolicy = tempSearch.GetComponent<StoreFieldPolicy>();
				tempPolicy.enabled = true;
				tempPolicy.ProcessEvent( presentCrop, presentCropIndex );
				presentCrop = null;
			}

		}

		GenerateHuman();
	}
	//find texture for store field crop item texture
	public GameObject FindCropItemTexture (string name)
	{
		GameObject temp = null;
		for (int i = 0; i < cropGroup.Length; i++)
			if (cropGroup[i].Name == name)
				temp = cropGroup[i].GetItemTexture();

		return temp;
	}

	//store cropitem
	public void storeCrop (int cropIndex)
	{
		//add money & remove crop item in list
	
	}

	//start pre process store game
	public void StartPreProcess ()
	{
		placeComplete = false;
	}

	//store game start or restart
	public void StartStoreGame ()
	{
		onGame = true;
		placeComplete = true;
	}

	//store game close
	public void EndStoreGame ()
	{
		onGame = false;
	}

	//crop button click
	public bool LinkPresentCropItem (CropItem item, int index)
	{
		if (CheckAllField())
		{
			presentCropIndex = index;
			presentCrop = new CropItem(item);
			return true;
		}
		else
			return false;
	}

	//check empty store field
	public bool CheckAllField ()
	{
		for (int i = 0; i < storeFieldGroup.Length; i++)
		{
			//no sell -> is empty
			if (!storeFieldGroup[i].OnStore)
				return true;
		}
	
		return false;
	}
	
	//check Field all empty
	public bool CheckAllFieldIsEmpty()
	{
		for (int i = 0; i < storeFieldGroup.Length; i++)
		{
			//no sell -> is empty
			if (storeFieldGroup[i].OnStore)
				return false;
		}

		return true;
	}
	//sold or kill thief
	public void AddMoney (int value)
	{
		gameManager.Money += value;
	}

	//set present skill
	public void SetPresentSkill (string name)
	{
		for (int i = 0; i < skillGroup.Length; i++)
		{
			if (skillGroup[i].SkillName == name)
			{
				if (gameManager.Money >= skillGroup[i].Cost)
				{
					gameManager.Money -= skillGroup[i].Cost;
					storeUI.UpdateMoneyInfor( "Use skill", -skillGroup[i].Cost );
					onSkill = true;
					presentSkill = skillGroup[i];
					storeUI.DrawSkillImage( name, presentSkill.SkillCounter );
					storeUI.ControlSelectSkillButton(false);
					return;
				}
			}
		}
	}

	//reset skill
	public void ResetSkill ()
	{ 	
		int changeAmount = presentSkill.ResetSkill();
		gameManager.Money += changeAmount;

		if(changeAmount > 0)
			storeUI.UpdateMoneyInfor( "Return skill", changeAmount );

		storeUI.ControlSelectSkillButton(true);
		presentSkill = null;
		onSkill = false;
	}

	//generate human process
	public void GenerateHuman ()
	{
		generateCoolTime += Time.deltaTime;
		if (generateCoolTime >= generateCycle)
		{
			for (int i = 0; i < humanGroup.Length; i++)
			{
				if (humanGroup[i] == null)
				{
					GameObject tempData = (GameObject)Instantiate( humanObject[Random.Range( 0, 2 )], startPos[Random.Range( 0, 3 )] + new Vector3(0f, 0f, 3f), new Quaternion(0f, 0f, 0f, 0f) );
					humanGroup[i] = tempData.GetComponent<Human>();
					humanGroup[i].DataSetUp();
					generateCoolTime = 0.0f;
					break;
				}
			}
		}
	}

	//set human destination field
	public Transform GetSellingStoreField ()
	{
		for (int i = 0; i < storeFieldGroup.Length; i++)
		{
			if (storeFieldGroup[i].OnStore)
				return storeFieldGroup[i].transform;
		}

		return null;
	}

	//human death process
	public void CustomerDeath ()
	{
		gameManager.Money -= 100;
		storeUI.UpdateMoneyInfor( "Kill Customer", -100 );
	}

	public void ThiefDeath (bool onShopping, int stealPrice)
	{
		if (onShopping)
		{
			gameManager.Money += (stealPrice * 2);
			storeUI.UpdateMoneyInfor( "Kill Thief", stealPrice * 2 );
		}
		else
		{
			gameManager.Money -= 100;
			storeUI.UpdateMoneyInfor( "Kill Customer", -100 );
		}			
	}

	//clear game data
	public void InitializeGameData()
	{
		storeUI.InitializeGameData();
		for(int i = 0; i < storeFieldGroup.Length; i++)
			storeFieldGroup[i].InitializeGameData();

		for(int i = 0; i < humanGroup.Length; i++)
		{
			if(humanGroup[i] != null)
				Destroy(humanGroup[i].gameObject);
		}
		presentSkill = null;
		presentCrop = null;
		presentCropIndex = 0;
	}

	//get / set method

	//on game
	public bool CheckOnGame ()
	{
		return onGame;
	}

	//return crop infor (key = name)
	public Sprite SetAverageCropTable (string name)
	{
		switch (name)
		{
			case "Corn":
				return cropAverageTable[0];
			case "Carrot":
				return cropAverageTable[1];
			case "Barley":
				return cropAverageTable[2];
		}

		return null;
	}

	//return crop group
	public Crop[] GetCropGroup ()
	{
		return cropGroup;
	}
}
