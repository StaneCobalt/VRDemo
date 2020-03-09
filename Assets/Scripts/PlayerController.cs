using UnityEngine;

[RequireComponent(typeof(PlayerCollectableController))]
// [RequireComponent(typeof(PlayerFreeTeleportController))]
public class PlayerController : MonoBehaviour
{
	public GunController gunController;

	PlayerCollectableController playerCollectableController;

	// PlayerFreeTeleportController playerTeleportController;

	void Awake()
	{
		playerCollectableController = GetComponent<PlayerCollectableController>();
		// playerTeleportController = GetComponent<PlayerFreeTeleportController>();
	}

	void OnEnable()
	{
		playerCollectableController.OnCollect += HandleCollection;
	}

	void OnDisable()
	{
		playerCollectableController.OnCollect -= HandleCollection;
	}

	private void HandleCollection(GameObject item)
	{
		gunController?.Recharge(6f);
	}

	//void Update()
	//{
	//	// check that we're not selecting
	//	if (playerCollectableController.IsSelecting()) // || playerTeleportController.IsSelecting())
	//	{
	//		return;
	//	}

	//	// check for button press (mobile only)
	//	if (Input.GetButtonDown("Fire1"))
	//	{
	//		OnShootButton();
	//	}
	//}

	public void OnShootButton ()
	{
		if (gunController != null)
		{
			if (gunController.CanShoot() && !playerCollectableController.IsSelecting())
			{
				gunController.Shoot();
			}
		}
	}
}
