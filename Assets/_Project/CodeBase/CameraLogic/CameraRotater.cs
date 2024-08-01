using Assets._Project.Config;
using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.CodeBase.CameraLogic
{
    public class CameraRotater : MonoBehaviour
    {
        [SerializeField] private VariableJoystick _variableJoystick;
        [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

        private RotateInput _rotateInput;
        private CameraRotateData _cameraRotateData;

        private float _currentXRotation;
        private float _currentYRotation;
        private float _sensivity;

        private Vector2 _lastDirection;
        private Vector3 _currentMousePosition;

        private Action _rotationCameraAction;

        public void Construct(GameConfig gameConfig, RotateInput rotateInput)
        {
            _cameraRotateData = gameConfig.CameraRotateData;
            _rotateInput = rotateInput ?? throw new ArgumentNullException(nameof(rotateInput));

            if (Application.isMobilePlatform)
            {
                _sensivity = _cameraRotateData.RotateSpeedMobile;
                _rotationCameraAction = HandleTouchInput;
            }
            else
            {
                _sensivity = _cameraRotateData.RotateSpeedPC;
                _rotationCameraAction = ControlRotation;
            }

            _rotateInput.Enable();
            _rotateInput.Mouse.MouseSrollWheel.performed += OnTouchMouseScrollWheel;
        }

        private void Update()
        {
            _rotationCameraAction.Invoke();
        }

        private void OnDisable()
        {
            _rotateInput.Disable();

            _rotateInput.Mouse.MouseSrollWheel.performed -= OnTouchMouseScrollWheel;
        }

        private void ControlRotation()
        {
            if (_variableJoystick.enabled && _currentMousePosition != UnityEngine.Input.mousePosition)
            {
                _cinemachineFreeLook.m_XAxis.m_InputAxisValue = _variableJoystick.Horizontal;
                _cinemachineFreeLook.m_YAxis.m_InputAxisValue = _variableJoystick.Vertical;

                _currentMousePosition = UnityEngine.Input.mousePosition;
            }
            else
            {
                _cinemachineFreeLook.m_XAxis.m_InputAxisValue = 0;
                _cinemachineFreeLook.m_YAxis.m_InputAxisValue = 0;
            }
        }

        private void HandleTouchInput()
        {
            if (UnityEngine.Input.touchCount == 2)
            {
                Touch touch1 = UnityEngine.Input.GetTouch(0);
                Touch touch2 = UnityEngine.Input.GetTouch(1);

                if (_variableJoystick.enabled)
                {
                    if (IsTouchWithinJoystick(touch1.position) && IsTouchWithinJoystick(touch2.position))
                    {
                        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                        Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                        float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                        float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                        OnTouchZoom(deltaMagnitudeDiff);
                    }
                }
            }
            else if (UnityEngine.Input.touchCount == 1)
            {
                ControlRotation();
            }
        }

        private bool IsTouchWithinJoystick(Vector2 touchPosition)
        {
            RectTransform joystickRect = _variableJoystick.GetComponent<RectTransform>();
            return RectTransformUtility.RectangleContainsScreenPoint(joystickRect, touchPosition, null);
        }

        private void OnTouchMouseScrollWheel(InputAction.CallbackContext context)
        {
            float scrollDelta = context.ReadValue<float>();

            OnTouchZoom(scrollDelta);
        }

        private void OnTouchZoom(float deltaMagnitudeDiff)
        {
            for (int i = 0; i < _cinemachineFreeLook.m_Orbits.Length; i++)
            {
                CinemachineFreeLook.Orbit orbit = _cinemachineFreeLook.m_Orbits[i];

                orbit.m_Radius = Mathf.Clamp(orbit.m_Radius - deltaMagnitudeDiff * _cameraRotateData.ZoomStep, _cameraRotateData.MinZoomDistance, _cameraRotateData.MaxZoomDistance);

                if (i == 2)
                {
                    orbit.m_Height = Mathf.Clamp(orbit.m_Height - deltaMagnitudeDiff * _cameraRotateData.ZoomStep, _cameraRotateData.MinZoomDistance, _cameraRotateData.MaxZoomDistance);
                }

                _cinemachineFreeLook.m_Orbits[i] = orbit;
            }
        }

        public void OnTouchPerformed(InputAction.CallbackContext context) =>
            Rotate(context.ReadValue<Vector2>());

        private void Rotate(Vector2 direction)
        {
            if (_lastDirection != direction)
            {
                _currentXRotation += direction.x * _sensivity * Time.deltaTime;
                _currentYRotation += -direction.y * _sensivity * Time.deltaTime;

                _currentYRotation = Mathf.Clamp(_currentYRotation, -45f, 90f);

                Quaternion rotationX = Quaternion.Euler(0, _currentXRotation, 0);
                Quaternion rotationY = Quaternion.Euler(_currentYRotation, 0, 0);

                transform.rotation = rotationX * rotationY;
                _lastDirection = direction;
            }
        }
    }
}
