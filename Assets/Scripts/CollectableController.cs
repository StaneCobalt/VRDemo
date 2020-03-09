using System;
using UnityEngine;
using VRStandardAssets.Utils;
using Zenva.VR;

// [RequireComponent(typeof(VRInteractiveItem))]
[RequireComponent(typeof(Interactable))]
public class CollectableController : MonoBehaviour
{
	[Serializable]
	public class ItemStat
	{
		public string label;
		public float amount;
	}

	// general container for item properties
	public ItemStat[] properties;

	// VRInteractiveItem vrItem;
	PlayerCollectableController playerCollectableController;

	void Awake()
	{
		// vrItem = GetComponent<VRInteractiveItem>();
		playerCollectableController = FindObjectOfType<PlayerCollectableController>();

		if (playerCollectableController == null)
		{
			Debug.LogError("No PlayerCollectableController found in scene.");
		}
	}

	//void OnEnable()
	//{
	//	// subscribe to events
	//	vrItem.OnOver += HandleOver;
	//	vrItem.OnOut += HandleOut;
	//	vrItem.OnClick += HandleClick;
	//}

	//void OnDisable()
	//{
	//	// unsubscribe to events
	//	vrItem.OnOver -= HandleOver;
	//	vrItem.OnOut -= HandleOut;
	//	vrItem.OnClick -= HandleClick;
	//}

	// when the player consumes the item
	public void HandleClick()
	{
		// collect the item
		playerCollectableController.Collect(gameObject);

		// destroy this item instance
		Destroy(gameObject);
	}

	// when we stop looking at the item
	public void HandleOut()
	{
		// so we know we're looking away
		playerCollectableController.SelectionOut();

		Highlight(false);
	}

	// when we look at the item
	public void HandleOver()
	{
		Vector3 collectablePos = playerCollectableController.gameObject.transform.position;
		// check if the item is within collecting distance
		if (Vector3.Distance(transform.position, collectablePos) <= playerCollectableController.maxDistance)
		{
			// so we know we are selecting the item
			playerCollectableController.SelectionOver();

			Highlight(true);
		}
	}

	void Highlight(bool flag)
	{
		GetComponent<Renderer>().material.SetFloat("_Outline", flag ? 0.002f : 0);
	}

	public float GetPropertyAmount(string label)
	{
		for (int i = 0; i < properties.Length; i++)
		{
			if (properties[i].label == label)
			{
				return properties[i].amount;
			}
		}
		return 0;
	}
}
