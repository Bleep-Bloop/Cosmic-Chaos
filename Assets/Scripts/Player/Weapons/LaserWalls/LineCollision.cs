using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(LaserWalls))]
public class LineCollision : MonoBehaviour
{
    [Header("Components")]
    private LaserWalls laserWallsComponent; 
    private PolygonCollider2D polygonCollider2D; // The collider for the lines.

    [Header("Runtime")]
    List<Vector2> colliderPoints = new List<Vector2>(); // The points to draw a collision shape between.
    bool StartCollision;

    private void Start()
    {
        laserWallsComponent = GetComponent<LaserWalls>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;
        StartCollision = false;
    }

    private void LateUpdate()
    {
        // Get all positions from the line renderer
        Vector3[] positions = laserWallsComponent.GetPositions();

        // If we have enough points to draw a line
        if(positions.Count() >= 2)
        {

            // Start laser collision after a small to prevent instant player collision
            Invoke("ActivateLasers", laserWallsComponent.laserStartDelay);

            // Get the number of line between two points
            int numberOfLines = positions.Length - 1;

            // Make as many paths for each different line as we have lines
            polygonCollider2D.pathCount = numberOfLines;

            // Get Collider points between two consecutive points
            for (int i = 0; i < numberOfLines; i++)
            {
                // Get the two next points
                List<Vector2> currentPositions = new List<Vector2>
                {
                    positions[i],
                    positions[i+1]
                };

                List<Vector2> currentColliderPoints = CalculateColliderPoints(currentPositions);
                polygonCollider2D.SetPath(i, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
            }
        }
        else
        {
            polygonCollider2D.pathCount = 0;
        }
    }

    private List<Vector2> CalculateColliderPoints(List<Vector2> positions)
    {
        // Old two point system
        // Get all positions on the line renderer
        //Vector3[] positions = lc.GetPositions();

        // Get the width of the line
        float width = laserWallsComponent.GetWidth(); // Remember to set this to .4 in linerederer component

        // m = (y2 - y1) / (x2 - x1)
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        // Calculate vertex offset from line point
        Vector2[] offsets = new Vector2[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        // Generate the Colliders Verticles
        List<Vector2> colliderPositions = new List<Vector2> {

            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1],

        };

        return colliderPositions;
    }


    /// <summary>
    /// Calls from LateUpdate when a laser is able to be created.
    /// Takes delay time from LaserWallsComponent and calls LaserWallsComponent::StartLasers
    /// </summary>
    private void ActivateLasers()
    {
        laserWallsComponent.StartLasers();
    }

}
