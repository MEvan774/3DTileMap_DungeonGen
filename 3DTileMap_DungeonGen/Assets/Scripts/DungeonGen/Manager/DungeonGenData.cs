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
    [HideInInspector] public List<GameObject> MainRooms = new List<GameObject>();

    [HideInInspector] public Vector2 MinMaxRoomSize;
    [HideInInspector] public int MinimumMainRoomSize = 6;

    [HideInInspector] public int _roomDone;
    [HideInInspector] public bool isDone = false;

    [Header("Dungeon params")]
    public algoritmType dungeonType;

    [Header("Rooms spawn circle")]
    public int RoomsAmount = 40;
    public float SpawnCircleRadius = 10;
    public Vector2 CircleInset = Vector2.one;

    [Header("Dungeon size")]
    public Vector2 DungeonSize = new Vector2(120, 120);



    public void OnRoomDone()
    {
        _roomDone++;

        //if all rooms are in place (or removed) it will go to the next phase of the generation
        if(_roomDone >= RoomsAmount && !isDone)
        {
            isDone = true;
            gameObject.GetComponent<GetMainRooms>().SortGameObjectsByCollider();
        }
    }
}
