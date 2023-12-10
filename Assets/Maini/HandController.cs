using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    [SerializeField] private ActionBasedController handController;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (!handController)
        {
            Debug.LogError("Hand controller not assigned.");
        }
    }

    private void Update()
    {
        HandleInput(handController);
    }

    private void HandleInput(ActionBasedController controller)
    {
        if (controller)
        {
            bool isGrabbing = controller.selectAction.action.ReadValue<float>() > 0.0f;
            Debug.Log(controller.selectAction.action.ReadValue<float>());
            animator.SetBool("IsGrabbing", isGrabbing);
        }
    }
}
