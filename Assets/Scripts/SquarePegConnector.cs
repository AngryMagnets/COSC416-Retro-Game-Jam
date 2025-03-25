using UnityEngine;
using System;

/// <summary>
/// MonoBehviour <c>SquarePegConnector</c> Runs in editor
/// <para>
/// Only to be used in level creation, can be removed before final build
/// </para>
/// <para>
/// Generates square pegs from this one that are aligned with each other <br/>
/// Must set <paramref name="isStart"></paramref> to true for it to generate. Will set back to false after generating. <br/>
/// <paramref name="flipValues"></paramref> Sets an index where the peg gets flipped and will generate start rotating in the opposite direction <br/>
/// X and Y scale of object must be equal
/// </para>
/// </summary>
[RequireComponent (typeof(MeshBuilder))]
[ExecuteInEditMode]
public class SquarePegConnector : MonoBehaviour
{
    [SerializeField] int pegGenerateCount = 1;
    [SerializeField] int[] flipValues;
    [SerializeField] bool isStart = false;

    private void OnEnable()
    {
        if (isStart)    //Only starts generating if this peg is the starting peg
        {
            isStart = false;
            AdjustPeg();
        }
    }

    //Do not touch, am not commenting this shit
    public void AdjustPeg()
    {
        if (pegGenerateCount > 0)
        {
            pegGenerateCount--;
                MeshBuilder thisMesh = GetComponent<MeshBuilder>();

                Vector2[] thisCorners = thisMesh.GetCorners();
                Vector2 thisScaledCorner = thisCorners[1];
                thisScaledCorner.Scale(new Vector2(thisMesh.GetSquish().x, thisMesh.GetSquish().y));
                Vector2 thisWidth = new Vector2(transform.localScale.x * Math.Abs(thisCorners[0].x),transform.localScale.y * Math.Abs(thisCorners[0].y));
                Vector2 thisNewCorners;
                double thisAngle = (float)Math.Abs(Mathf.Rad2Deg * Math.Atan((thisCorners[1].x - thisScaledCorner.x) / (thisCorners[1].y - thisScaledCorner.y)));
                double thisRotation = transform.localEulerAngles.z * Mathf.Deg2Rad;

                Vector2 otherScaledCorner = thisCorners[0];
                otherScaledCorner.Scale(new Vector2(thisMesh.GetSquish().x, thisMesh.GetSquish().y));
                Vector2 otherNewCorners;
                double otherAngle = (float)Math.Abs(Mathf.Rad2Deg * Math.Atan((thisCorners[0].x - otherScaledCorner.x) / (thisCorners[0].y - otherScaledCorner.y)));
                double otherRotation = transform.localEulerAngles.z * Mathf.Deg2Rad;

                if (!flipContains())
                {
                    if (thisCorners[0].y < 0)
                    {
                        otherRotation = (thisAngle + otherAngle + transform.localEulerAngles.z) * Mathf.Deg2Rad;
                        thisNewCorners = new Vector2((float)(thisWidth.x * Math.Cos(thisRotation) + thisWidth.y*Math.Sin(thisRotation)),
                            (float)(-thisWidth.y * Math.Cos(thisRotation) + thisWidth.x* Math.Sin(thisRotation)));
                        otherNewCorners = new Vector2((float)(-thisWidth.x * Math.Cos(otherRotation) + thisWidth.y*Math.Sin(otherRotation)),
                            (float)(-thisWidth.y * Math.Cos(otherRotation) - thisWidth.x*Math.Sin(otherRotation)));
                    }
                    else
                    {
                        otherRotation = (-1*(thisAngle + otherAngle) + transform.localEulerAngles.z) * Mathf.Deg2Rad;
                        thisNewCorners = new Vector2((float)(thisWidth.x * Math.Cos(thisRotation) - thisWidth.y*Math.Sin(thisRotation)),
                            (float)(thisWidth.y * Math.Cos(thisRotation) + thisWidth.x*Math.Sin(thisRotation)));
                        otherNewCorners = new Vector2((float)(-thisWidth.x * Math.Cos(otherRotation) - thisWidth.y*Math.Sin(otherRotation)),
                            (float)(thisWidth.y * Math.Cos(otherRotation) - thisWidth.x*Math.Sin(otherRotation)));

                    }
                }
                else
                {
                    if (thisCorners[0].y > 0)
                    {
                    thisNewCorners = new Vector2((float)(thisWidth.x * Math.Cos(thisRotation) - thisWidth.y*Math.Sin(thisRotation)),
                        (float)(thisWidth.y* Math.Cos(thisRotation) + thisWidth.x*Math.Sin(thisRotation)));
                    otherNewCorners = new Vector2((float)(-thisWidth.x * thisMesh.GetSquish().x * Math.Cos(otherRotation) + thisWidth.y * thisMesh.GetSquish().y* Math.Sin(otherRotation)),
                            (float)(-thisWidth.y * thisMesh.GetSquish().y * Math.Cos(otherRotation) - thisWidth.x * thisMesh.GetSquish().x *Math.Sin(otherRotation)));

                    }
                    else
                    {
                    thisNewCorners = new Vector2((float)(thisWidth.x * Math.Cos(thisRotation) + thisWidth.y*Math.Sin(thisRotation)),
                            (float)(-thisWidth.y * Math.Cos(thisRotation) + thisWidth.x*Math.Sin(thisRotation)));
                        otherNewCorners = new Vector2((float)(-thisWidth.x * thisMesh.GetSquish().x * Math.Cos(otherRotation) - thisWidth.y * thisMesh.GetSquish().y * Math.Sin(otherRotation)),
                            (float)(thisWidth.y * thisMesh.GetSquish().y * Math.Cos(otherRotation) - thisWidth.x * thisMesh.GetSquish().x *Math.Sin(otherRotation)));

                    }
                }
                MeshBuilder adjacentSquares = Instantiate(this.gameObject, new Vector2(transform.position.x, transform.position.y) + (thisNewCorners - otherNewCorners),
                    Quaternion.Euler(0, 0, (float)(otherRotation * Mathf.Rad2Deg))).GetComponent<MeshBuilder>();
            adjacentSquares.transform.parent = transform.parent;
            if (flipContains())
            {
                adjacentSquares.bezierIntensity = -adjacentSquares.bezierIntensity;
                adjacentSquares.cornerPoints[0].y = -adjacentSquares.cornerPoints[0].y;
                adjacentSquares.cornerPoints[1].y = -adjacentSquares.cornerPoints[1].y;
            }
            adjacentSquares.GetComponent<SquarePegConnector>().AdjustPeg();
            
        }
    }

    /// <summary>
    /// Method <c>flipContains</c> checks if current peg is to be flipped
    /// </summary>
    /// <returns></returns>
    private bool flipContains()
    {
        for (int i = 0; i<flipValues.Length;i++)
        {
            if (pegGenerateCount == flipValues[i]) return true;
        }
        return false;
    }
}
