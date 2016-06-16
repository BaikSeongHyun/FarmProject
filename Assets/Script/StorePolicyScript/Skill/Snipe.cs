using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Snipe : Skill
{
	//complex data field
	public Sprite SnipeSprite;

	//constructor - no parameter
	public Snipe()
	{
		skillName = "Snipe";
		skillPrice = 20;
		originCounter = 2;
		skillCounter = originCounter;
	}

	//another method

	//skill active - use override
	public override void ActiveSkill( )
	{
		//if counter no exist -> Do not active skill
		if (skillCounter < 0)
		{
			Debug.Log( "Counter off" );
			return;
		}


	}


}

