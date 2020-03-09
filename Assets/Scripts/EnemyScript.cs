using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public float walkSpeed = 0.5f;
	public float angularSpeed = 1.0f;
	public float chasingDistance = 15f;

	Rigidbody rigidBody;
	Animator anim;
	PlayerController playerController;

	enum State { IDLE, ATTACKING, DEAD };
	State currentState = State.IDLE;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		playerController = FindObjectOfType<PlayerController>();

		if (playerController == null)
		{
			Debug.LogError("Player Controller not found in scene.");
		}

		// search for the player at an interval of 0.5 sec
		InvokeRepeating("LookForPlayer", 0, 0.5f);
	}

	void LookForPlayer()
	{
		if (currentState != State.IDLE)
		{
			return;
		}
		if (IsClose())
		{
			currentState = State.ATTACKING;
			anim.SetBool("sawPlayer", true);

			// we found the player, so stop searching
			CancelInvoke();
		}
	}

	bool IsClose()
	{
		Vector3 playerPos = playerController.transform.position;
		playerPos.y = 0;
		Vector3 enemyPos = transform.position;
		enemyPos.y = 0;
		return Vector3.Distance(playerPos, enemyPos) <= chasingDistance;
	}

	//// use fixed update for updating changes on physics
	//void FixedUpdate()
	//{
	//	// move the parent to the position of the child (the model)
	//	transform.position = transform.GetChild(0).position;

	//	// set the child to be in the origin of the parent
	//	transform.GetChild(0).localPosition = Vector3.zero;

	//	// only chase if we are attacking!
	//	if (currentState != State.ATTACKING) return;

	//	// instant rotation of the transform:
	//	transform.LookAt(playerController.transform.position);
	//}

	void FixedUpdate()
	{
		if (currentState != State.ATTACKING)
		{
			return;
		}

		// set the direction to rotate
		Vector3 direction = playerController.transform.position - transform.position;

		// Normalize will keep the Vector within a magnitude of 1
		direction.Normalize();

		// set the velocity
		rigidBody.velocity = direction * walkSpeed;

		// can rotate with this but it's not advised to edit transforms directly: transform.LookAt(playerController.transform.position);

		// rotation with angular speed
		// flat difference between player and enemy, we don't care about vertical distance here though
		Vector3 flatDiff = playerController.transform.position - transform.position;
		flatDiff.y = 0;

		// amount of rotation needed	--how much to rotate
		Quaternion targetRotation = Quaternion.LookRotation(flatDiff, Vector3.up);

		// angular rotation velocity	--how fast to rotate
		Vector3 eulerAngleVelocity = new Vector3(0, angularSpeed, 0);

		// delta rotation: distance = velocity * time	--how fast to rotate in relation to time
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);

		// rigid body rotation
		rigidBody.MoveRotation(targetRotation * deltaRotation);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Bullet"))
		{
			currentState = State.DEAD;
			anim.SetBool("isAlive", false);

			// disable the collider so it doesn't effect teleporting
			GetComponent<Collider>().enabled = false;

			// so the body doesn't magically fly away
			rigidBody.isKinematic = true;
		}
	}
}
