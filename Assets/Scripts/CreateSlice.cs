using UnityEngine;

public class CreateSlice : MonoBehaviour
{
    public GameObject lemonSlicePrefab;
    public GameObject orangeSlicePrefab;
    public GameObject limeSlicePrefab;
    private AudioSource soundManager;

    private void Start()
    {
        soundManager = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {    
        if (other.gameObject.CompareTag("Lemon"))
        {
            soundManager.Play();
            Vector3 triggerPoint = other.transform.position;
            Quaternion triggerRotation = Quaternion.identity;
            Destroy(other.gameObject);
            Instantiate(lemonSlicePrefab, triggerPoint, triggerRotation);
        }
        if (other.gameObject.CompareTag("Orange"))
        {
            soundManager.Play();
            Vector3 triggerPoint = other.transform.position;
            Quaternion triggerRotation = Quaternion.identity;
            Destroy(other.gameObject);
            Instantiate(orangeSlicePrefab, triggerPoint, triggerRotation);
        }
        if (other.gameObject.CompareTag("Lime"))
        {
            soundManager.Play();
            Vector3 triggerPoint = other.transform.position;
            Quaternion triggerRotation = Quaternion.identity;
            Destroy(other.gameObject);
            Instantiate(limeSlicePrefab, triggerPoint, triggerRotation);
        }
    }
}
