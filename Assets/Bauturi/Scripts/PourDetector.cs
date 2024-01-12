using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    [SerializeField]
    private Material liquid;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Juicer"))
        {
            if (collision.gameObject.CompareTag("Lime"))
            {
                isPouring = true;
                StartPour();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameObject.CompareTag("Juicer"))
        {
            if (collision.gameObject.CompareTag("Lime"))
            {
                isPouring = false;
                EndPour();
            }
        }
    }

}
