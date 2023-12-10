using UnityEngine.InputSystem;
using UnityEngine;

    public class GrabAnimation1 : MonoBehaviour
    {
        [SerializeField] private Animator handAnimator;
        [SerializeField] private InputActionReference grabActionRef;

        private static readonly int GrabAnimation = Animator.StringToHash("Grab");

        private void OnEnable()
        {
            grabActionRef.action.performed += GrabAction_performed;
            grabActionRef.action.canceled += GrabAction_canceled;
        }

        private void GrabAction_performed(InputAction.CallbackContext obj)
        {
            handAnimator.SetFloat(GrabAnimation, 1);
        }
        
        private void GrabAction_canceled(InputAction.CallbackContext obj)
        {
            handAnimator.SetFloat(GrabAnimation, 0);
        }

        private void OnDisable()
        {
            grabActionRef.action.performed -= GrabAction_performed;
            grabActionRef.action.canceled -= GrabAction_canceled;
        }
    }

