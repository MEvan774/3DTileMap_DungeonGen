using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GenerateRooms : MonoBehaviour
{
    [SerializeField] private DungeonGenData _data;

    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private Vector2 _MinMaxSize;

    private bool _isGenerating = false;

    public void StartDungeonGeneration()
    {
        if (!_isGenerating)
        {
            _isGenerating = true;

            _data.MinMaxRoomSize = _MinMaxSize;

            for (int i = 0; i < _data.RoomsAmount; i++)
            {
                GameObject room = ObjectPoolManager.SpawnObject(_roomPrefab, GetRandomPointInCircle(), Quaternion.identity, ObjectPoolManager.PoolType.DungeonColliders);
                room.GetComponent<RoomPhysics>().enabled = enabled;
                room.gameObject.GetComponent<BoxCollider>().size = new Vector3(Mathf.Round(Random.Range(_MinMaxSize.x, _MinMaxSize.y)), 1, Mathf.Round(Random.Range(_MinMaxSize.x, _MinMaxSize.y)));
                _data.Rooms.Add(room);
            }
        }
    }

    public void OnGenerationDone()//resets values for next generation
    {
        _isGenerating = false;
        _data._roomDone = 0;
        _data.MainRooms.Clear();
        _data.Rooms.Clear();
        _data.isDone = false;
    }

    private Vector3 GetRandomPointInCircle()
    {
        Vector3 spawnPoint = Random.insideUnitCircle * _data.SpawnCircleRadius;
        return new Vector3(Mathf.Round(spawnPoint.x * _data.CircleInset.x), 0, 
                            Mathf.Round(spawnPoint.y * _data.CircleInset.y));
    }
}
