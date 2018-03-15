﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuesCurve : MonoBehaviour
{

    public bool initDone = false;
    public Transform[] curvePoints;
    public bool isLoop = false;
    public float angleTolerance = 1;
    [Range(0f, 1f)]
    public float maxVelocity = 0;

    [SerializeField][Tooltip("Target you want object to turn to. Leave null if you want to keep rotation")]
    private Transform LookAtTarget;

    public GameObject go;
    private float totalTime;
    private float time;

    private void OnDrawGizmos()
    {
        if (!initDone)
        {
            InitCurvePoints();
        }

        Color color;
        #region draw curve
        for (int i = 0; i < curvePoints.Length - 1; i += 2)
        {
            Vector3 start = curvePoints[i].position;
            float t = 0f;

            while (t < 1)
            {
                t += 0.015625f; // 1 divided by 64

                Vector3 end = MathHelp.GetCurvePosition(
                    curvePoints[i].position,
                    curvePoints[i + 1].position,
                    curvePoints[i + 2].position,
                    t);

                float velocity = Vector3.Distance(start, end);
                float grayShade = Mathf.Clamp01(velocity / maxVelocity);
                color = new Color(grayShade, grayShade, grayShade);

                Debug.DrawLine(start, end, color);

                start = end;
            }
        }
        #endregion
        #region draw lines
        for (int i = 0; i < curvePoints.Length; i += 2)
        {
            // draw straight line
            {
                if (i == 0)
                {   
                    if (isLoop)
                    {   // first and last lines with angle check
                        float startEndAngle = MathHelp.AngleBetweenVector3(
                            curvePoints[curvePoints.Length - 2].position,
                            curvePoints[curvePoints.Length - 1].position,
                            curvePoints[i].position,
                            curvePoints[i + 1].position);

                        if (startEndAngle > -angleTolerance && startEndAngle < angleTolerance)
                        {
                            color = Color.white;
                        }
                        else
                        {
                            color = Color.yellow;
                        }

                        Debug.DrawLine(curvePoints[curvePoints.Length - 2].position, curvePoints[curvePoints.Length - 1].position, color);
                        Debug.DrawLine(curvePoints[i].position, curvePoints[i + 1].position, color);
                        continue;
                    }
                    // first line no angle check needed
                    color = Color.white;
                    Debug.DrawLine(curvePoints[i].position, curvePoints[i + 1].position, color);
                    continue;
                }
                else if (i == curvePoints.Length - 1)
                {
                    if (isLoop) continue;
                    // last line no angle check needed
                    color = Color.white;
                    Debug.DrawLine(curvePoints[i - 1].position, curvePoints[i].position, color);
                    continue;
                }
                // check angle and set correct color
                float angle = MathHelp.AngleBetweenVector3(
                    curvePoints[i - 1].position,
                    curvePoints[i].position,
                    curvePoints[i + 1].position);

                if (angle > -angleTolerance && angle < angleTolerance)
                {
                    color = Color.white;
                }
                else
                {
                    color = Color.yellow;
                }

                Debug.DrawLine(curvePoints[i - 1].position, curvePoints[i].position, color);
                Debug.DrawLine(curvePoints[i].position, curvePoints[i + 1].position, color);
            }
        }
#endregion
    }

    private void Update()
    {
        totalTime += Time.deltaTime * 0.5f;
        // skip odds
        if ((int)totalTime % 2 == 1)
        {
            totalTime++;
        }
        // reset
        if (totalTime > curvePoints.Length - 2)
        {
            totalTime = 0f;
        }

        int index = (int)totalTime;
        time = totalTime - index;

        go.transform.position = MathHelp.GetCurvePosition(
            curvePoints[index].position,
            curvePoints[index + 1].position,
            curvePoints[index + 2].position,
            time);

        if (LookAtTarget != null)
        {
            go.transform.LookAt(LookAtTarget);
        }
    }

    private void InitCurvePoints()
    {
        if (!initDone)
        {
            curvePoints = new Transform[transform.childCount];
            for (int i = 0; i < curvePoints.Length; i++)
            {
                curvePoints[i] = transform.GetChild(i);
            }
            initDone = true;
        }
    }
}