﻿using System;
using UnityEngine;

namespace Core
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted,
        }

        [SerializeField] private Mode mode;

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(Camera.main!.transform);
                    break;
                case Mode.LookAtInverted:
                    var position = transform.position;
                    Vector3 dirFromCamera = position - Camera.main!.transform.position;
                    transform.LookAt(position + dirFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = Camera.main!.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -Camera.main!.transform.forward;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}