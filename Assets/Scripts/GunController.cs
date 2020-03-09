using UnityEngine;
using ZenvaVR;

[RequireComponent(typeof(ObjectPool))]
public class GunController : MonoBehaviour
{
	public float ammo = 12f;
	public float maxAmmo = 12f;
	public float bulletSpeed = 50f;
	public RectTransform ammoIndicator;
	public Transform gunTip;

	ObjectPool bulletPool;

	void Awake()
	{
		bulletPool = GetComponent<ObjectPool>();
		bulletPool.InitPool();
		RefreshUI();

		if (gunTip == null)
		{
			gunTip = transform;
		}
	}

	public bool CanShoot()
	{
		return ammo > 0;
	}

	public void Shoot()
	{
		// get a bullet from the pool
		GameObject bullet = bulletPool.GetObj();

		// position the bullet at the tip of the gun
		Quaternion rotation = new Quaternion(transform.rotation.z, 0, transform.rotation.z, transform.rotation.w);
		bullet.transform.SetPositionAndRotation(gunTip.position, rotation);

		// apply velocity
		Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
		rigidbody.velocity = transform.forward * bulletSpeed;

		// decrease ammo count
		ammo--;

		RefreshUI();
	}

	internal void Recharge(float amount)
	{
		// add ammo, but don't go over the max
		ammo = Mathf.Min(ammo + amount, maxAmmo);

		RefreshUI();
	}

	void RefreshUI()
	{
		ammoIndicator.localScale = new Vector3(ammo / maxAmmo, 1f, 1f);
	}
}
