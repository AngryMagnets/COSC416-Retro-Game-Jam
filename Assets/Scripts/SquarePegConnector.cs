using UnityEngine;
using System;

[RequireComponent (typeof(MeshBuilder))]
public class SquarePegConnector : MonoBehaviour
{
    [SerializeField] int pegGenerateCount = 1;
    [SerializeField] int[] flipValues;
    [SerializeField] bool isStart = false;

    private void OnEnable()
    {
        if (isStart)
        {
            isStart = false;
            AdjustPeg();
        }
    }

    public void AdjustPeg()
    {
        if (pegGenerateCount > 0)
        {
            pegGenerateCount--;
                MeshBuilder thisMesh = GetComponent<MeshBuilder>();

                Vector2[] thisCorners = thisMesh.GetCorners();
                Vector2 thisScaledCorner = thisCorners[1];
                thisScaledCorner.Scale(new Vector2(thisMesh.GetSquish().x, thisMesh.GetSquish().y));
                double thisWidth = transform.localScale.x;
                Vector2 thisNewCorners;
                double thisAngle = (float)Math.Abs(Mathf.Rad2Deg * Math.Atan((thisCorners[1].x - thisScaledCorner.x) / (thisCorners[1].y - thisScaledCorner.y)));
                double thisRotation = transform.localEulerAngles.z * Mathf.Deg2Rad;

                Vector2 otherScaledCorner = thisCorners[0];
                otherScaledCorner.Scale(new Vector2(thisMesh.GetSquish().x, thisMesh.GetSquish().y));
                double otherWidth = transform.localScale.x;
                Vector2 otherNewCorners;
                double otherAngle = (float)Math.Abs(Mathf.Rad2Deg * Math.Atan((thisCorners[0].x - otherScaledCorner.x) / (thisCorners[0].y - otherScaledCorner.y)));
                double otherRotation = transform.localEulerAngles.z * Mathf.Deg2Rad;

                //if (thisCorners[0].y - otherCorners[0].y == 0)
                if (!flipContains())
                {
                    if (thisCorners[0].y < 0)
                    {
                        otherRotation = (thisAngle + otherAngle + transform.localEulerAngles.z) * Mathf.Deg2Rad;
                        thisNewCorners = new Vector2((float)(thisWidth / 2 * (Math.Cos(thisRotation) + Math.Sin(thisRotation))),
                            (float)(thisWidth / -2 * (Math.Cos(thisRotation) - Math.Sin(thisRotation))));
                        otherNewCorners = new Vector2((float)(otherWidth / -2 * (Math.Cos(otherRotation) - Math.Sin(otherRotation))),
                            (float)(otherWidth / -2 * (Math.Cos(otherRotation) + Math.Sin(otherRotation))));
                    }
                    else
                    {
                        otherRotation = (-1*(thisAngle + otherAngle) + transform.localEulerAngles.z) * Mathf.Deg2Rad;
                        thisNewCorners = new Vector2((float)(thisWidth / 2 * (Math.Cos(thisRotation) - Math.Sin(thisRotation))),
                            (float)(thisWidth / 2 * (Math.Cos(thisRotation) + Math.Sin(thisRotation))));
                        otherNewCorners = new Vector2((float)(otherWidth / -2 * (Math.Cos(otherRotation) + Math.Sin(otherRotation))),
                            (float)(otherWidth / 2 * (Math.Cos(otherRotation) - Math.Sin(otherRotation))));

                    }
                }
                else
                {
                    if (thisCorners[0].y > 0)
                    {
                    thisNewCorners = new Vector2((float)(thisWidth / 2 * (Math.Cos(thisRotation) - Math.Sin(thisRotation))),
                        (float)(thisWidth / 2 * (Math.Cos(thisRotation) + Math.Sin(thisRotation))));
                    otherNewCorners = new Vector2((float)(otherWidth * thisMesh.GetSquish().x / -2 * Math.Cos(otherRotation) + otherWidth * thisMesh.GetSquish().y / 2* Math.Sin(otherRotation)),
                            (float)(otherWidth * thisMesh.GetSquish().y / -2 * Math.Cos(otherRotation) - otherWidth * thisMesh.GetSquish().x / 2 *Math.Sin(otherRotation)));

                    }
                    else
                    {
                    thisNewCorners = new Vector2((float)(thisWidth / 2 * (Math.Cos(thisRotation) + Math.Sin(thisRotation))),
                            (float)(thisWidth / -2 * (Math.Cos(thisRotation) - Math.Sin(thisRotation))));
                        otherNewCorners = new Vector2((float)(otherWidth * thisMesh.GetSquish().x / -2 * Math.Cos(otherRotation) - otherWidth * thisMesh.GetSquish().y / 2* Math.Sin(otherRotation)),
                            (float)(otherWidth * thisMesh.GetSquish().y / 2 * Math.Cos(otherRotation) - otherWidth * thisMesh.GetSquish().x / 2*Math.Sin(otherRotation)));

                    }
                }
                MeshBuilder adjacentSquares = Instantiate(this.gameObject, new Vector2(transform.localPosition.x, transform.localPosition.y) + (thisNewCorners - otherNewCorners),
                    Quaternion.Euler(0, 0, (float)(otherRotation * Mathf.Rad2Deg))).GetComponent<MeshBuilder>();
            if (flipContains())
            {
                adjacentSquares.bezierIntensity = -adjacentSquares.bezierIntensity;
                adjacentSquares.cornerPoints[0].y = -adjacentSquares.cornerPoints[0].y;
                adjacentSquares.cornerPoints[1].y = -adjacentSquares.cornerPoints[1].y;
            }
            adjacentSquares.GetComponent<SquarePegConnector>().AdjustPeg();
            
        }
    }

    private bool flipContains()
    {
        for (int i = 0; i<flipValues.Length;i++)
        {
            if (pegGenerateCount == flipValues[i]) return true;
        }
        return false;
    }
}
