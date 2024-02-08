using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;
    private AudioSource soundManager;

    private bool isPouring = false;
    private Stream currentStream = null;

    [SerializeField]
    private Material liquid;
    private void Start()
    {
        soundManager = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float currentAngle = CalculatePourAngle();
        // print("Current Angle: " + currentAngle);

        if (!gameObject.CompareTag("Juicer"))
        {
            bool pourCheck = Mathf.Abs(currentAngle) > pourThreshold;

            if (isPouring != pourCheck)
            {
                isPouring = pourCheck;

                if (isPouring)
                {
                    StartPour();
                }
                else
                {
                    EndPour();
                }
            }
        }
    }


    private void StartPour()
    {
        print("Start Pouring");
        currentStream = CreateStream();
        currentStream.liquidColor = ColorOfMaterial();
        soundManager.Play();
        if (currentStream != null)
        {
            currentStream.Begin();
        }
        else
        {
            Debug.LogError("Error creating Stream object.");
        }
    }
    private void EndPour()
    {
        print("End Pouring");
        soundManager.Stop();
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle()
    {
      float pourAngle = Mathf.Asin(transform.forward.y) * Mathf.Rad2Deg;
      return pourAngle;
    }


    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

    private Color ColorOfMaterial()
    {
        
        Color albedoColor = liquid.color;
        //  albedoColor.r *= 255;
        //         albedoColor.g *= 255;
        //         albedoColor.b *= 255;

        return albedoColor;
    }

    private void SetPourColor(Color color)
    {
        liquid.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Juicer") && isPouring == false)
        {
            if (collision.gameObject.CompareTag("Lime"))
            {
                isPouring = true;
                SetPourColor(new Color(0.2f, 0.94f, 0.0f));
                StartPour();
            }
            else if (collision.gameObject.CompareTag("Lemon"))
            {
                isPouring = true;
                SetPourColor(new Color(0.89f, 1.0f, 0.0f));
                StartPour();
            } 
            else if (collision.gameObject.CompareTag("Orange"))
            {
                isPouring = true;
                SetPourColor(new Color(1.0f, 0.67f, 0.0f));
                StartPour();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameObject.CompareTag("Juicer") && isPouring == true)
        {
            if (collision.gameObject.CompareTag("Lime") || collision.gameObject.CompareTag("Lemon") || collision.gameObject.CompareTag("Orange"))
            {
                isPouring = false;
                EndPour();
            }
            
        }
    }

}
