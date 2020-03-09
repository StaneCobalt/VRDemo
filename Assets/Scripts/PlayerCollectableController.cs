using UnityEngine;

public class PlayerCollectableController : MonoBehaviour
{
	// max distance at which the player can collect an item
	public float maxDistance;

	// flag for whether we're selecting the item or not
	bool isSelecting = false;

	// event for when item is collected
	public delegate void OnCollectedEventHandler(GameObject item);
	public event OnCollectedEventHandler OnCollect;

	public void Collect(GameObject item)
	{
		// trigger the event
		OnCollect?.Invoke(item);
		isSelecting = false;
	}

	// called when selecting an item
	public void SelectionOver()
	{
		isSelecting = true;
	}

	// called when deselecting an item
	public void SelectionOut()
	{
		isSelecting = false;
	}

	public bool IsSelecting()
	{
		return isSelecting;
	}
}
