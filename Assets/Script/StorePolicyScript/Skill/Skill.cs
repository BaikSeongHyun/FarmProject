using UnityEngine;
using UnityEngine.UI;
using System;

public class Skill : MonoBehaviour
{
	//simple data field
	public string skillName;
	protected int originCounter;
	public int skillPrice;
	public int skillCounter;

	//complex data field

	//constructor - no parameter
	public Skill()
	{

	}

	//property
	//Skill name
	public string SkillName
	{
		get { return skillName; }
	}
	//skill buy cost
	public int Cost
	{
		get { return (skillPrice * originCounter); }
	}

	//another method

	//skill active - use override
	public virtual void ActiveSkill( )
	{		
	}

	//reset present skill
	public int ResetSkill( )
	{
		int result = skillPrice * skillCounter;
		ReloadCounter();
		return result;
	}

	//check skill counter
	public bool CheckSkillIsEmpty( )
	{
		if (skillCounter <= 0)
			return true;
		else
			return false;
	}

	//reload counter
	public void ReloadCounter( )
	{
		skillCounter = originCounter;
		Debug.Log( originCounter );
	}

}