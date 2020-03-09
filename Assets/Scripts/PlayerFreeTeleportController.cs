using UnityEngine;
using VRStandardAssets.Utils;

public class PlayerFreeTeleportController : MonoBehaviour
{
	// teleportation target game object
	public GameObject target;

	// max teleportation distance
	public float maxDistance;

	// whether to show an arc for the target or not
	public bool showArc;

	// origin of the arc
	public Transform arcOrigin;

	// number of points in the arc
	public int arcPointCount;

	// the list of arc points
	Vector3[] arcPoints;

	// reticle
	Reticle reticle;

	// keep track of if we're showing the reticle
	bool isShowing;

	// line renderer
	LineRenderer line;

	void Awake()
	{
		HideTarget();

		// find reticle
		reticle = FindObjectOfType<Reticle>();

		if (reticle == null)
		{
			Debug.LogError("Reticle not found in the scene.");
		}

		if (showArc)
		{
			// get line renderer
			line = target.GetComponent<LineRenderer>();

			if (line == null)
			{
				Debug.LogError("Line Renderer not found on the target.");
			}

			// set number of arc points
			line.positionCount = arcPointCount;

			arcPoints = new Vector3[arcPointCount];
		}
	}

	// hide the target
	public void HideTarget()
	{
		target.SetActive(false);

		if (reticle != null)
		{
			reticle.Show();
		}

		isShowing = false;
	}

	// show target
	public void ShowTarget(Vector3 position)
	{
		if (Vector3.Distance(position, transform.position) <= maxDistance)
		{

			// if target is inactive, then activate it
			if (!target.activeInHierarchy)
			{
				target.SetActive(true);

				if (reticle != null)
				{
					reticle.Hide();
				}
			}

			// set teleport target to the position we are pointing at
			target.transform.position = position;

			isShowing = true;

			if (showArc)
			{
				DrawRay();
			}
		}
		else if (isShowing)
		{
			// if we were showing reticle, but it's too far away, then hide it
			HideTarget();
		}
	}

	// teleportation
	public void Teleport()
	{
		if (isShowing)
		{
			// player position will go to the target position
			transform.position = target.transform.position;
			HideTarget();
		}
	}

	void DrawRay()
	{
		// starting position of the arc
		// start is front of camera + an offset
		Vector3 start = arcOrigin.position;

		// ending position
		Vector3 end = target.transform.position;

		float arcY;

		// create all the points until the end
		for (int i = 0; i < arcPointCount; i++)
		{
			// arc effect
			arcY = Mathf.Sin((float)i / arcPointCount * Mathf.PI) / 2;

			// create point
			arcPoints[i] = Vector3.Lerp(start, end, (float)i / arcPointCount);
			arcPoints[i].y += arcY;
		}

		// assign points to the line renderer
		line.SetPositions(arcPoints);
	}

	public bool IsSelecting()
	{
		return isShowing;
	}
}
