using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    [SerializeField] private ActionBasedController handController;

    private Animator animator;
    // private AudioSource soundManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        // soundManager = GetComponent<AudioSource>();

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
            // Debug.Log(controller.selectAction.action.ReadValue<float>());
            // if (animator.GetBool("IsGrabbing") == false && isGrabbing == true)
            // {
            //     soundManager.Play();
            // }
            animator.SetBool("IsGrabbing", isGrabbing);
        }
    }
}
