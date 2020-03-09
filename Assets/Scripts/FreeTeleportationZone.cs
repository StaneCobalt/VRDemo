using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class FreeTeleportationZone : MonoBehaviour
{
	VRInteractiveItem vrItem;
	VREyeRaycaster vrEyeRaycaster;
	PlayerFreeTeleportController playerController;

	void Awake()
	{
		// get vr interactive item component
		vrItem = GetComponent<VRInteractiveItem>();

		// get vr eye raycaster
		vrEyeRaycaster = FindObjectOfType<VREyeRaycaster>();

		if (vrEyeRaycaster == null)
		{
			Debug.LogError("No VR Eye Raycaster found in the scene.");
		}

		playerController = FindObjectOfType<PlayerFreeTeleportController>();

		if (playerController == null)
		{
			Debug.LogError("No Player Free Teleport Controller found in the scene.");
		}
	}

	void OnEnable()
	{
		// subscribe to events
		vrEyeRaycaster.OnRaycasthit += HandleShowTarget;
		vrItem.OnOut += HandleOut;
		vrItem.OnClick += HandleClick;
	}

	void OnDisable()
	{
		// unsubscribe from events
		vrEyeRaycaster.OnRaycasthit -= HandleShowTarget;
		vrItem.OnOut -= HandleOut;
		vrItem.OnClick -= HandleClick;
	}

	void HandleClick()
	{
		playerController.Teleport();
	}

	void HandleOut()
	{
		playerController.HideTarget();
	}

	void HandleShowTarget(RaycastHit hit)
	{
		// check that we are looking at this object
		if (!vrItem.IsOver)
		{
			return;
		}
		playerController.ShowTarget(hit.point);
	}
}
