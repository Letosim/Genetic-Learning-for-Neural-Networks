using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elevator : MonoBehaviour
{
    public Transform navAreaTransform;//from    
    public Transform platformTransform;//to
    public Vector3 offset;
    public Vector3 directionOnEnter;

    public Vector3 initialPosition;
    public GameObject initialBlocker;
    public GameObject initialBounding;
    public GameObject initialPlatformBounding;

    public Vector3 targetPosition;
    public GameObject targetBlocker;
    public GameObject targetBounding;
    public GameObject targetPlatformBounding;

    public float time;
    public float delayTime;

    private Timer timer;
    public bool opening = true;
    public bool onDelay = false;

    public Vector3 frameOffset;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        timer.Start(time);
    }

    // Update is called once per frame
    void Update()
    {
        if (!onDelay)
        {
            Vector3 curPosition = platformTransform.position;

            if (opening)
                
                platformTransform.position = Vector3.Lerp(initialPosition, targetPosition, timer.Delta());
            else
                platformTransform.position = Vector3.Lerp(targetPosition, initialPosition, timer.Delta());

            frameOffset = platformTransform.position - curPosition;
        }

        if (timer.finished)
        {
            frameOffset = Vector3.zero;
            if (onDelay)
            {
                timer.Start(time);

                if (opening)
                {
                    opening = false;
                    targetBlocker.SetActive(true);
                    targetBounding.SetActive(true);
                }
                else
                {
                    opening = true;
                    initialBlocker.SetActive(true);
                    initialBounding.SetActive(true);
                }

                initialPlatformBounding.SetActive(true);
                targetPlatformBounding.SetActive(true);

                onDelay = false;
            }
            else
            {
                if (opening)
                { 
                    targetBlocker.SetActive(false);
                    targetPlatformBounding.SetActive(false);
                    targetBounding.SetActive(false);
                }
                else
                { 
                    initialBlocker.SetActive(false);
                    initialPlatformBounding.SetActive(false);
                    initialBounding.SetActive(false);
                }


                onDelay = true;
                timer.Start(delayTime);
            }
        }

        timer.Update();

        float offsetX = Mathf.Sqrt((navAreaTransform.position.x - platformTransform.position.x) * (navAreaTransform.position.x - platformTransform.position.x));
        offsetX = platformTransform.position.x < navAreaTransform.position.x ? offsetX * -1f : offsetX;
        float offsetY = Mathf.Sqrt((navAreaTransform.position.y - platformTransform.position.y) * (navAreaTransform.position.y - platformTransform.position.y));
        offsetY = platformTransform.position.y < navAreaTransform.position.y ? offsetY * -1f : offsetY;
        float offsetZ = Mathf.Sqrt((navAreaTransform.position.z - platformTransform.position.z) * (navAreaTransform.position.z - platformTransform.position.z));
        offsetZ = platformTransform.position.z < navAreaTransform.position.z ? offsetZ * -1f : offsetZ;

        offset = new Vector3(offsetX , offsetY, offsetZ);
    }

    public bool placeEntraces = false;
    public GameObject entraceInitial;
    public GameObject entraceTarget;
    public GameObject exitInitial;
    public GameObject exitTarget;

    private void OnDrawGizmos()
    {
        //entrace
        Gizmos.DrawSphere(navAreaTransform.position + new Vector3(0, platformTransform.localScale.y / 2f + .8f, -platformTransform.localScale.x / 2f), .5f);

        //exit
        //Gizmos.DrawSphere(navAreaTransform.position + new Vector3(0, platformTransform.localScale.y / 2f + .8f, platformTransform.localScale.x / 2f), .5f);

        if (placeEntraces)
        {
            entraceInitial.transform.position = initialPosition + new Vector3(0, platformTransform.localScale.y / 2f + .8f, -platformTransform.localScale.x / 2f - .2f);
            entraceTarget.transform.position = targetPosition + new Vector3(0, platformTransform.localScale.y / 2f + .8f, platformTransform.localScale.x / 2f + .2f);

            exitInitial.transform.position = navAreaTransform.position + new Vector3(0, platformTransform.localScale.y / 2f + .8f, -platformTransform.localScale.x / 2f + .1f);
            exitTarget.transform.position = navAreaTransform.position + new Vector3(0, platformTransform.localScale.y / 2f + .8f, platformTransform.localScale.x / 2f - .1f);
            placeEntraces = false;
        }
    }
}
