using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum algoritmType
{
    tinyKeep,
    rogue
}

public class DungeonGenData : MonoBehaviour
{
    [HideInInspector] public List<GameObject> Rooms = new List<GameObject>();
    public List<GameObject> MainRooms = new List<GameObject>();
    public int RoomsAmount = 40;

    public float SpawnCircleRadius = 10;
    public Vector2 CircleInset = Vector2.one;

    public Vector2 DungeonSize = new Vector2(120, 120);
    [HideInInspector] public Vector2 MinMaxRoomSize;
    [HideInInspector] public int MinimumMainRoomSize = 6;

    [HideInInspector] public int _roomDone;
    public algoritmType dungeonType;

    public bool isDone = false;

    public void OnRoomDone()
    {
        _roomDone++;

        if(_roomDone >= RoomsAmount && !isDone)
        {
            isDone = true;
            gameObject.GetComponent<GetMainRooms>().SortGameObjectsByCollider();
        }
    }
}
