using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;
    private Vector3 targetPosition = Vector3.zero;
    public Color liquidColor;
    

private void Awake()
{
    lineRenderer = GetComponent<LineRenderer>();
    splashParticle = GetComponentInChildren<ParticleSystem>();
}


    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        // Debug.Log("*****Name: " + bottle);

        lineRenderer.material.SetColor("_Color", liquidColor);
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf) 
        {
            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);
            yield return null; 
        }
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        while(!HasReachedPosition(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
        Destroy(gameObject);
    }

private Vector3 FindEndPoint()
{
    RaycastHit hit;
    Ray ray = new Ray(transform.position, Vector3.down);

    if (Physics.Raycast(ray, out hit, 2.0f))
    {
        HandleCupInteraction(hit);
    }
    else
    {
        Debug.Log("No object hit.");
    }

    Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);
    return endPoint;
}

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime*2f);
        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
        while(gameObject.activeSelf)
        {
            var mainModule = splashParticle.main;
            mainModule.startColor = liquidColor;

            splashParticle.gameObject.transform.position=targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);
            splashParticle.gameObject.SetActive(isHitting);

            yield return null;

        }
        yield return null;
    }


private void HandleCupInteraction(RaycastHit hit)
{
        // Debug.Log("Hit object: " + hit.collider.gameObject.name);
        // Debug.Log("Hit point: " + hit.point);
        // Debug.Log("Hit normal: " + hit.normal);
    if (hit.collider != null && hit.collider.CompareTag("Cup"))
    {
        Cup cup = hit.collider.GetComponent<Cup>();
        cup.liquidColor = liquidColor;
        if (cup != null)
        {
            cup.StartFilling();
        }
        else
        {
            Debug.Log("Cup script not found on the Cup GameObject.");
        }
    }
    // else
    // {
    //     Debug.Log("Cup tag is wrong");
    // }
}



}
