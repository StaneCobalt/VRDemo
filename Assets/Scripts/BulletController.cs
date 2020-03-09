using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float lifespan = 3f;

	void OnEnable()
	{
		// waits for the lifespan duration, then calls DeactivateBullet
		Invoke("DeactivateBullet", lifespan);
	}

	void OnDisable()
	{
		// cancel pending invokation
		CancelInvoke();
	}

	void DeactivateBullet()
	{
		gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other)
	{
		gameObject.SetActive(false);
	}
}
