using UnityEngine;

public class ItemController : MonoBehaviour
{
	public float horizontalRotation = 15f;
	Vector3 rotationAmount;

	void Awake()
	{
		rotationAmount = new Vector3(transform.rotation.x, horizontalRotation);
	}
	void Update()
	{
		transform.Rotate(rotationAmount * Time.deltaTime);
	}
}
