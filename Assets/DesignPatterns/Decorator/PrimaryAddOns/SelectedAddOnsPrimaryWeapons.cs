using UnityEngine;
using System.Collections;

public abstract class selectedAddOnsPrimaryWeapon : selectedPrimaryWeapon{
	
	public abstract override string getDescription ();
	public abstract override float getPrecision ();
	public abstract override float getPrice ();
	
}
