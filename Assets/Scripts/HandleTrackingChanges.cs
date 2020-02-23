using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace StaneCobalt
{
	public class HandleTrackingChanges : MonoBehaviour
	{
		public InputDeviceRole deviceRole;
		public GameObject controller;

		List<InputDevice> _devices;

		void Awake()
		{
			controller.SetActive(false);
			_devices = new List<InputDevice>();
		}

		void Update()
		{
			Check();
		}

		void Check()
		{
			InputDevices.GetDevicesWithRole(deviceRole, _devices);

			if(controller.activeInHierarchy && _devices.Count == 0)
			{
				controller.SetActive(false);
			}
			else if(!controller.activeInHierarchy && _devices.Count > 0)
			{
				controller.SetActive(true);
			}
		}
	}
}
