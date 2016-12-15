using UnityEngine;
using System.Collections;

public class SelectedWeapons {

	protected string name;
	protected float precision;
	protected float recoil;
	protected string type;
	protected string description;
	protected string FiringMode;
	protected float price;
	protected int bullets = 0;
	protected string fileLocation;


	//add 3d body
	//add motion difference!!
	// add animation here 

}

public abstract class selectedPrimaryWeapon : SelectedWeapons {
	protected GameObject Player;
	public selectedPrimaryWeapon(){
		Player = GameObject.FindGameObjectWithTag("Player");
		type = "primary";
		description = "unknown";
		price = 0;
		precision = 0;
		price = 0;
		recoil = 1.0f;
	}
	
	public virtual string getDescription ()
	{
		return description;
	}

	public virtual float getPrice ()
	{
		return price;
	}

	public virtual float getPrecision ()
	{
		return precision;
	}

	public abstract GameObject mount3DModel ();


}
