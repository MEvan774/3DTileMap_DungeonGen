using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New levels", menuName = "DungeonGen")]
public class GenLevels : ScriptableObject
{
    [SerializeField]public Levels[] Dungeonlevels;
}

[System.Serializable]
public class Levels
{
    [Header("Dungeon params")]
    public algoritmType dungeonType;

    [Header("Rooms spawn circle")]
    public int RoomsAmount = 40;
    public float SpawnCircleRadius = 10;
    public Vector2 CircleInset = Vector2.one;

    [Header("Dungeon size")]
    public Vector2 DungeonSize = new Vector2(120, 120);

    public Vector2 MinMaxSize;

    [Header("Rooms")]
    public int MainRoomsAmount;
}
