using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HallWayGen : MonoBehaviour
{
    [SerializeField] PlaceTiles tilePlacer;
    [SerializeField] DungeonGenData _data;

    [SerializeField] Tilemap map;
    [SerializeField] LayerMask ignoreLayer;

    [SerializeField] GameObject _corridor;

    public void GenHallways(Transform[] nodeOne, Transform[] nodeTwo)
    {
        Vector3 xDir = Vector3.zero;
        Vector3 zDir = Vector3.zero;

        for (int i = 0; i < nodeOne.Length; i++)
        {
            if(nodeOne[i].position.z < nodeTwo[i].position.z)
                zDir = Vector3.forward;
            else
                zDir = Vector3.back;

            if (nodeOne[i].position.x < nodeTwo[i].position.x)
                xDir = Vector3.left;
            else
                xDir = Vector3.right;

            CheckHallway(nodeOne[i].position, nodeOne[i].position + (xDir * 1000), nodeTwo[i].position, nodeTwo[i].position + (zDir * 1000));
        }

        Invoke("PlaceTiles", 0.5f);
    }

    void PlaceTiles()
    {
        tilePlacer.StartGen();
    }

    void CheckHallway(Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2)
    {
        Vector3 intersection;
        Vector3 aDiff = a2 - a1;
        Vector3 bDiff = b2 - b1;
        if (LineLineIntersection(out intersection, a1, aDiff, b1, bDiff))
        {
            //Debug.Log("Intersecting at: " + intersection);

            /*--Shows hallway intersection to make corridors--
            Debug.DrawLine(intersection, intersection + (Vector3.up * 20), Color.yellow, Mathf.Infinity);

            Debug.DrawLine(a1, intersection, Color.red, Mathf.Infinity);
            Debug.DrawLine(b1, intersection, Color.blue, Mathf.Infinity);
            */

            //spawns two colliders to create a hallway connecting 2 rooms
            StartCoroutine(OnCreateCorridor(a1, a2, b1, b2, intersection));

            float aSqrMagnitude = aDiff.sqrMagnitude;
            float bSqrMagnitude = bDiff.sqrMagnitude;

            if ((intersection - a1).sqrMagnitude <= aSqrMagnitude
                 && (intersection - a2).sqrMagnitude <= aSqrMagnitude
                 && (intersection - b1).sqrMagnitude <= bSqrMagnitude
                 && (intersection - b2).sqrMagnitude <= bSqrMagnitude)
            {
                // there is an intersection between the two segments and 
                //   it is at intersection
            }
        }
    }

    IEnumerator OnCreateCorridor(Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2, Vector3 intersection)
    {
        Vector2 _pointDist = new Vector2(Vector3.Distance(a1, intersection), Vector3.Distance(b1, intersection));

        GameObject _corridorA = ObjectPoolManager.SpawnObject(_corridor, new Vector3((a1.x + intersection.x) / 2.0f, (a1.y + intersection.y) / 2.0f, (a1.z + intersection.z) / 2.0f), Quaternion.identity, ObjectPoolManager.PoolType.DungeonColliders); // midpoint between A B);
        _corridorA.GetComponent<BoxCollider>().size = new Vector3(_pointDist.x, 1, 1);

        GameObject _corridorB = ObjectPoolManager.SpawnObject(_corridor, new Vector3((b1.x + intersection.x) / 2.0f, (b1.y + intersection.y) / 2.0f, (b1.z + intersection.z) / 2.0f), Quaternion.identity, ObjectPoolManager.PoolType.DungeonColliders); // midpoint between A B);
        _corridorB.GetComponent<BoxCollider>().size = new Vector3(1, 1, _pointDist.y);

        yield return null;
    }

    public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1,
                                            Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parallel
        if (Mathf.Abs(planarFactor) < 0.0001f
                && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2)
                    / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }
        else
        {
            intersection = Vector3.zero;
            return false;
        }
    }
}