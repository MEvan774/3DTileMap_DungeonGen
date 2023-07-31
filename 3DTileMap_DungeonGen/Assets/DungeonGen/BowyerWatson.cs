using UnityEngine;
using System.Collections.Generic;

public class BowyerWatson : MonoBehaviour
{
    /*
    [SerializeField] private DungeonGenData _data;

    public List<Vector3> points; // List of input points
    public List<Triangle> triangles; // List of resulting triangles

    public void StartAlgorithm()
    {

        for (int i = 0; i < _data.MainRooms.Count; i++)
        {
            points.Add(_data.MainRooms[i].transform.position);
        }

        // Create a super triangle that encompasses all input points
        float maxX = 40;
        float maxZ = 40;
        float minX = -40;
        float MinZ = -40;

        foreach (Vector3 point in points)
        {
            if (point.x > maxX) maxX = point.x;
            if (point.z > maxZ) maxZ = point.z;
            if (point.x < minX) minX = point.x;
            if (point.z < MinZ) MinZ = point.z;
        }

        float dx = maxX - minX;
        float dy = maxZ - MinZ;
        float deltaMax = Mathf.Max(dx, dy);
        float midX = (minX + maxX) / 2f;
        float midY = (MinZ + maxZ) / 2f;

        Vector3 p1 = new Vector3(midX - 20 * deltaMax, midY - deltaMax);
        Vector3 p2 = new Vector3(midX, midY + 20 * deltaMax);
        Vector3 p3 = new Vector3(midX + 20 * deltaMax, midY - deltaMax);

        Triangle superTriangle = new Triangle(p1, p2, p3);

        triangles.Add(superTriangle);

        // Iterate through each input point
        foreach (Vector3 point in points)
        {
            // Create a list to store the bad triangles
            List<Triangle> badTriangles = new List<Triangle>();

            // Find all triangles that are no longer valid due to the new point
            foreach (Triangle triangle in triangles)
            {
                if (triangle.IsPointInsideCircumcircle(point))
                {
                    badTriangles.Add(triangle);
                }
            }

            // Find the boundary edges of the polygonal hole
            List<Edge> polygon = new List<Edge>();

            foreach (Triangle badTriangle in badTriangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    Edge edge = badTriangle.GetEdge(i);
                    bool shared = false;

                    foreach (Triangle otherTriangle in badTriangles)
                    {
                        if (otherTriangle != badTriangle && otherTriangle.HasEdge(edge))
                        {
                            shared = true;
                            break;
                        }
                    }

                    if (!shared)
                    {
                        polygon.Add(edge);
                    }
                }
            }

            // Remove the bad triangles from the list
            foreach (Triangle badTriangle in badTriangles)
            {
                triangles.Remove(badTriangle);
            }

            // Create new triangles from the boundary edges and the new point
            foreach (Edge edge in polygon)
            {
                triangles.Add(new Triangle(edge.p1, edge.p2, point));
            }
        }

        // Remove triangles that contain any vertex of the super triangle
        triangles.RemoveAll(t => t.HasVertex(p1) || t.HasVertex(p2) || t.HasVertex(p3));
    }
}

// Simple Triangle class to store triangle vertices
[System.Serializable]
public class Triangle
{
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //this.p1 = p1;
        // this.p2 = p2;
        //this.p3 = p3;
        Debug.Log("NEW TRIANGLE");
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }

    // Check if a point is inside the circumcircle of the triangle
    public bool IsPointInsideCircumcircle(Vector3 point)
    {
        float ax = p1.x - point.x;
        float ay = p1.z - point.z;
        float bx = p2.x - point.x;
        float by = p2.z - point.z;
        float cx = p3.x - point.x;
        float cy = p3.z - point.z;

        float d = (ax * (by - cy)) + (bx * (cy - ay)) + (cx * (ay - by));
        if (d <= 0) return false;

        float ux = (ax * ax) + (ay * ay);
        float uy = (bx * bx) + (by * by);
        float uz = (cx * cx) + (cy * cy);

        float circumcircleRadius = (ux * (by - cy)) + (uy * (cy - ay)) + (uz * (ay - by));
        return circumcircleRadius > 0;
    }

    // Check if the triangle contains a specific vertex
    public bool HasVertex(Vector3 vertex)
    {
        return vertex == p1 || vertex == p2 || vertex == p3;
    }

    // Check if the triangle contains a specific edge
    public bool HasEdge(Edge edge)
    {
        return (edge.p1 == p1 && edge.p2 == p2) ||
               (edge.p1 == p2 && edge.p2 == p3) ||
               (edge.p1 == p3 && edge.p2 == p1);
    }

    // Get an edge of the triangle
    public Edge GetEdge(int index)
    {
        if (index == 0) return new Edge(p1, p2);
        if (index == 1) return new Edge(p2, p3);
        if (index == 2) return new Edge(p3, p1);

        throw new System.IndexOutOfRangeException("Invalid edge index: " + index);
    }
}

// Simple Edge class to represent an edge between two vertices
[System.Serializable]
public class Edge
{
    public Vector3 p1;
    public Vector3 p2;

    public Edge(Vector3 p1, Vector3 p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }
    */
}
