using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace StaneCobalt
{
	public class ButtonController : MonoBehaviour
	{
		static readonly Dictionary<string, InputFeatureUsage<bool>> availableFeatures = new Dictionary<string, InputFeatureUsage<bool>>
		{
			{ "triggerButton", CommonUsages.triggerButton },
			{ "gripButton", CommonUsages.gripButton },
			{ "thumbrest", CommonUsages.thumbrest },
			{ "primary2DAxisClick", CommonUsages.primary2DAxisClick },
			{ "primary2DAxisTouch", CommonUsages.primary2DAxisTouch },
			{ "menuButton", CommonUsages.menuButton },
			{ "secondaryButton", CommonUsages.secondaryButton },
			{ "secondaryTouch", CommonUsages.secondaryTouch },
			{ "primaryButton", CommonUsages.primaryButton },
			{ "primaryTouch", CommonUsages.primaryTouch }
		};

		public enum FeatureOptions
		{
			triggerButton,
			gripButton,
			thumbrest,
			primary2DAxisClick,
			primary2DAxisTouch,
			menuButton,
			secondaryButton,
			secondaryTouch,
			primaryButton,
			primaryTouch
		}

		public FeatureOptions feature;
		public InputDeviceRole inputDeviceRole;
		public UnityEvent OnPress, OnRelease;

		List<InputDevice> _inputDevices;
		bool _inputValue, _isPressed;
		InputFeatureUsage<bool> _selectedFeature;

		private void Awake()
		{
			_inputDevices = new List<InputDevice>();
			string featureLabel = Enum.GetName(typeof(FeatureOptions), feature);
			availableFeatures.TryGetValue(featureLabel, out _selectedFeature);
		}

		void Update()
		{
			InputDevices.GetDevicesWithRole(inputDeviceRole, _inputDevices);
			_inputDevices.ForEach((d) => {
				if (d.TryGetFeatureValue(_selectedFeature, out _inputValue) && _inputValue)
				{
					if(!_isPressed)
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
