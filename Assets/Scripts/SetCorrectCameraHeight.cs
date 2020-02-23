using UnityEngine;
using UnityEngine.XR;

namespace StaneCobalt
{
	public class SetCorrectCameraHeight : MonoBehaviour
	{
		enum TrackingSpace
		{
			Stationary,
			RoomScale
		}

		[Header("Camera Settings")]

		[SerializeField]
		private TrackingSpace _trackingSpace = TrackingSpace.RoomScale;

		[SerializeField]
		private float _stationaryCameraYOffset = 1.36144f;

		[SerializeField]
		private GameObject _cameraFloorOffsetObject;

		void Awake()
		{
			if (!_cameraFloorOffsetObject)
			{
				Debug.LogWarning("No camera container specified for vr rig using attached gameobject.");
				_cameraFloorOffsetObject = this.gameObject;
			}
		}

		void Start()
		{
			SetCameraHeight();
		}

		void SetCameraHeight()
		{
			float cameraYOffset = _stationaryCameraYOffset;

			if (_trackingSpace == TrackingSpace.Stationary)
			{
				XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
				InputTracking.Recenter();
			}
			else if (_trackingSpace == TrackingSpace.RoomScale)
			{
				if (XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale))
				{
					cameraYOffset = 0;
				}
			}
			if (_cameraFloorOffsetObject)
			{
				_cameraFloorOffsetObject.transform.localPosition = new Vector3(
					_cameraFloorOffsetObject.transform.localPosition.x,
					cameraYOffset,
					_cameraFloorOffsetObject.transform.localPosition.z
				);
			}
		}
	}
}
