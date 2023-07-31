using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class PlaceTiles : MonoBehaviour
{
    [SerializeField] private DungeonGenData _data;

    [SerializeField] private Tilemap _mapWall;
    [SerializeField] private Tilemap _mapFloor;
    [SerializeField] private TileBase _tileWall;
    [SerializeField] private TileBase _tileFloor;

    private GameObject _colliderParent;

    [SerializeField] private UnityEvent _onRemoveRooms;
    [SerializeField] private UnityEvent _onGenerationDone;

    private void Start()
    {
        _colliderParent = GameObject.Find("Pooled Objects");
    }

    public void StartGen()//Starts process of spawning/replacing tiles
    {
        _onRemoveRooms.Invoke();
        Invoke("SetTiles", 0.2f);
    }

    void SetTiles()
    {
        int _gridRangeX = Mathf.RoundToInt(_data.DungeonSize.x);
        int _gridRangeY = Mathf.RoundToInt(_data.DungeonSize.y);

        //loops trough grid to place/replace floors and walls
        for (int x = -_gridRangeX; x < _gridRangeX; x++)
        {
            for (int y = -_gridRangeY; y < _gridRangeY; y++)
            {
                _mapWall.SetTile(new Vector3Int(x, y, 0), null);
                _mapFloor.SetTile(new Vector3Int(x, y, 0), null);

                if (!Physics.Raycast(new Vector3(x, 1, y), Vector3.down, 2))
                    _mapWall.SetTile(new Vector3Int(x, y, 0), _tileWall);
                else
                    _mapFloor.SetTile(new Vector3Int(x, y, 0), _tileFloor);
            }
        }
        
        foreach (var child in _colliderParent.GetComponentsInChildren<Transform>())
        {
            if (!child.CompareTag("DungeonGenHandler"))
                ObjectPoolManager.ReturnObjectToPool(child.gameObject);
        }

        //Tells 'GenerateRooms' that dungeon generation has finished and makes it ready for reactivation
        _onGenerationDone.Invoke();
    }
}
