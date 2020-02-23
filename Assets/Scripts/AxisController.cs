using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace StaneCobalt
{
	public class AxisController : MonoBehaviour
	{
		static readonly Dictionary<string, InputFeatureUsage<float>> availableFeatures = new Dictionary<string, InputFeatureUsage<float>>
		{
			{ "trigger", CommonUsages.trigger },
			{ "grip", CommonUsages.grip },
			{ "indexTouch", CommonUsages.indexTouch },
			{ "thumbTouch", CommonUsages.thumbTouch },
			{ "indexFinger", CommonUsages.indexFinger },
			{ "middleFinger", CommonUsages.middleFinger },
			{ "ringFinger", CommonUsages.ringFinger },
			{ "pinkyFinger", CommonUsages.pinkyFinger }
		};

		public enum FeatureOptions
		{
			trigger,
			grip,
			indexTouch,
			thumbTouch,
			indexFinger,
			middleFinger,
			ringFinger,
			pinkyFinger
		}

		public FeatureOptions feature;
		public InputDeviceRole inputDeviceRole;
		public UnityEvent OnPress, OnRelease;

		[Range(0,1)]
		public float _threshhold = 0;

		List<InputDevice> _inputDevices;
		float _inputValue;
		bool _isPressed;
		InputFeatureUsage<float> _selectedFeature;

		private void Awake()
		{
			_inputDevices = new List<InputDevice>();
			string featureLabel = Enum.GetName(typeof(FeatureOptions), feature);
			availableFeatures.TryGetValue(featureLabel, out _selectedFeature);
		}

		void Update()
		{
			InputDevices.GetDevicesWithRole(inputDeviceRole, _inputDevices);
			_inputDevices.ForEach((d) =>
			{
				if (d.TryGetFeatureValue(_selectedFeature, out _inputValue) && _inputValue > _threshhold)
				{
					if (!_isPressed)
					{
						_isPressed = true;
						OnPress.Invoke();
					}
				}
				else if (_isPressed)
				{
					_isPressed = false;
					OnRelease.Invoke();
				}
			});
		}
	}
}
