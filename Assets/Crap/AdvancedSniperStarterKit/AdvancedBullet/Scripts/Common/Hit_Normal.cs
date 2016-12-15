using UnityEngine;
using System.Collections;

public class Hit_Normal : AS_BulletHiter
{
	public override void OnHit (RaycastHit hit, AS_Bullet bullet)
	{
		float distance = Vector3.Distance (bullet.pointShoot, hit.point);
		AddAudio (hit.point);

		base.OnHit (hit, bullet);
	}
}
