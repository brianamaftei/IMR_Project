using UnityEngine;

public class CreateSlice : MonoBehaviour
{
    public GameObject lemonSlicePrefab;
    public GameObject orangeSlicePrefab;
    public GameObject limeSlicePrefab;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Lemon"))
        {
            Vector3 triggerPoint = other.transform.position;
            Quaternion triggerRotation = Quaternion.identity;
            Destroy(other.gameObject);
            GameObject slice = Instantiate(lemonSlicePrefab, triggerPoint, triggerRotation);
        }
        if (other.gameObject.CompareTag("Orange"))
        {
            Vector3 triggerPoint = other.transform.position;
            Quaternion triggerRotation = Quaternion.identity;
            Destroy(other.gameObject);
            GameObject slice = Instantiate(orangeSlicePrefab, triggerPoint, triggerRotation);
        }
        if (other.gameObject.CompareTag("Lime"))
        {
            Vector3 triggerPoint = other.transform.position;
            Quaternion triggerRotation = Quaternion.identity;
            Destroy(other.gameObject);
            GameObject slice = Instantiate(limeSlicePrefab, triggerPoint, triggerRotation);
        }
    }
}
