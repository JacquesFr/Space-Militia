using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : Observer
{
    [SerializeField] private Transform closeDestination = null;
    [SerializeField] private Transform openDestination = null;
    [SerializeField] private Transform waterLevelLow = null;
    [SerializeField] private Transform waterLevelHigh = null;
    [SerializeField] private float upSpeed = 0.0f;
    [SerializeField] private float downSpeed = 0.0f;
    [SerializeField] private float waterSpeed = 0.0f;
    [SerializeField] private GameObject water = null;

    private float tempDownSpeed = 0.0f;
    private Coroutine lastRoutine = null;
    private bool open = true;

    private void Start()
    {
        tempDownSpeed = downSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopCoroutine("Close");
    }

    public override void OnNotify(NotificationType type)
    {
        if(type == NotificationType.BUTTON_PRESSED)
        {
            if (open == true)
            {
                open = false;
                StopCoroutine("Open");
                lastRoutine = StartCoroutine("Close");
            }
            else
            {
                open = true;
                StopCoroutine("Close");
                lastRoutine = StartCoroutine("Open");
            }
        }
    }

    private IEnumerator Close()
    {
        float moveTime = (closeDestination.position - openDestination.position).magnitude;
        for (float t = 0f; transform.position != closeDestination.position; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, closeDestination.position, t / moveTime * downSpeed);
            water.transform.position = Vector3.Lerp(water.transform.position, closeDestination.position, t / moveTime * waterSpeed);
            yield return 0;
        }
        for (float t = 0f; water.transform.position != waterLevelHigh.position; t += Time.deltaTime)
        {
            water.transform.position = Vector3.Lerp(water.transform.position, waterLevelHigh.position, Time.deltaTime);
            yield return 0;
        }

        water.transform.position = waterLevelLow.position;
        transform.position = closeDestination.position;
        yield return null;
    }

    private IEnumerator Open()
    {
        downSpeed = tempDownSpeed;

        float moveTime = (openDestination.position - closeDestination.position).magnitude;
        for (float t = 0f; transform.position != openDestination.position; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, openDestination.position, t / moveTime * upSpeed);
            water.transform.position = Vector3.Lerp(water.transform.position, waterLevelLow.position, t / moveTime * waterSpeed);
            yield return 0;
        }

        float waterMoveTime = (waterLevelHigh.position - waterLevelLow.position).magnitude;
        for (float t = 0f; water.transform.position != waterLevelLow.position; t += Time.deltaTime)
        {
            water.transform.position = Vector3.Lerp(water.transform.position, waterLevelLow.position, t / moveTime * waterSpeed);
            yield return 0;
        }

        water.transform.position = waterLevelHigh.position;
        transform.position = openDestination.position;
        yield return null;
    }
}