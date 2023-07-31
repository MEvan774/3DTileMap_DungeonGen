using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prim : MonoBehaviour
{
    [HideInInspector] public List<Transform> nodes; // List of node transforms in the graph

    private List<Transform> mstNodes = new List<Transform>(); // Minimum spanning tree points
    private List<Transform> remainingNodes; // Nodes not yet added to the MST

    private List<Transform> nodeOne = new List<Transform>();
    private List<Transform> nodeTwo = new List<Transform>();

    private DungeonGenData data;
    private HallWayGen hallway;

    private void Start()
    {
        data = GetComponent<DungeonGenData>();
        hallway = GetComponent<HallWayGen>();
    }

    public void StartAlgorithm()
    {
        nodes.Clear();
        mstNodes.Clear();
        nodeOne.Clear();
        nodeTwo.Clear();

        //nodes = data.MainRooms.ToArray();
        // Iterate through the GameObject list
        if (data.MainRooms.Count == 0)
        {
            Debug.Log("NoRoomsContained!");
            return;
        }


        foreach (GameObject obj in data.MainRooms)
        {
            // Access the transform component of each GameObject
            Transform transform = obj.transform;

            // Add the transform to the new list
            nodes.Add(transform);
        }

        if (nodes.Count == 0)
        {
            Debug.LogError("Please assign node transforms to the 'Nodes' list.");
            return;
        }

        remainingNodes = new List<Transform>(nodes);

        // Select a random starting node
        Transform startNode = nodes[Random.Range(0, nodes.Count)];
        AddNodeToMST(startNode);

        // Repeat until all nodes are added to the MST
        while (remainingNodes.Count > 0)
        {
            float minDistance = float.MaxValue;
            Transform closestNode = null;

            // Find the closest node to the MST
            foreach (Transform mstNode in mstNodes)
            {
                foreach (Transform node in remainingNodes)
                {
                    float distance = Vector3.Distance(mstNode.position, node.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestNode = node;

                        nodeOne.Add(mstNode);
                        nodeTwo.Add(node);
                        Debug.DrawLine(mstNode.position, node.position, Color.green, duration: float.PositiveInfinity);
                    }
                }
            }

            if (closestNode != null)
                AddNodeToMST(closestNode);
        }

        hallway.GenHallways(nodeOne.ToArray(), nodeTwo.ToArray());
    }

    private void AddNodeToMST(Transform node)
    {
        mstNodes.Add(node);
        remainingNodes.Remove(node);

        // Add any necessary logic for visually representing the MST
        // For example, you could draw lines between connected nodes.
        Debug.Log("Added node to MST: " + node.name);
    }
}
