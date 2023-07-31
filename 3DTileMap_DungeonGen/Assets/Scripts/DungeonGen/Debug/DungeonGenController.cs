using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenController : MonoBehaviour
{
    [SerializeField] private GenerateRooms _genRooms;

    public void StartDungeonGen()
    {
        //Debug.Log("ITS GENERATING!");
        _genRooms.StartDungeonGeneration();
    }
}
