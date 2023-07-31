using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPhysics : MonoBehaviour
{
    private DungeonGenData _data;

    [SerializeField] private Collider _moveCollider; // The collider containing the objects you want to move away from
    [SerializeField] private float _moveSpeed = 5f; // The speed at which objects move away
    [SerializeField] private Rigidbody _rb;
    private Vector3 _previousPosition; // Stores the previous position of the object
    private float _stationaryThreshold = 0.001f;

    private bool isOnPosition = false;
    private Vector2 minRange, maxRange;

    [HideInInspector] public bool IsRoomStatic = false;

    [SerializeField] private int _maxRangeOffset = 10;

    private void Start()
    {
        _data = GameObject.FindGameObjectWithTag("DungeonGenHandler").GetComponent<DungeonGenData>();

        minRange = new Vector2(_data.DungeonSize.x - _moveCollider.bounds.size.x / 2 - 10, _data.DungeonSize.y - _moveCollider.bounds.size.z / 2 - 10);
        maxRange = new Vector2(_data.DungeonSize.x - _moveCollider.bounds.size.x / 2 - _maxRangeOffset, _data.DungeonSize.y - _moveCollider.bounds.size.z / 2 - _maxRangeOffset);

        Debug.Log("Min = " + minRange + "MaxRange = " + maxRange);

        Debug.DrawRay(new Vector3(-minRange.x, 0, -minRange.y), Vector3.up, Color.blue, float.PositiveInfinity);
        Debug.DrawRay(new Vector3(maxRange.x, 0, maxRange.y), Vector3.up, Color.red, float.PositiveInfinity);

        // Initialize the previous position to the current position at the start
        _previousPosition = transform.position;
    }

    private void OnEnable()
    {
        _previousPosition = transform.position;

        isOnPosition = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY;
        _moveCollider.isTrigger = true;
        //if (!IsRoomStatic)
        // StartCoroutine(MovePhysics());
    }

    IEnumerator MovePhysics()
    {
        //Collider[] colliders;

        // Get all the colliders within the specified moveCollider
        while (Vector3.Distance(transform.position, _previousPosition) > _stationaryThreshold)
        {
            bool isWithinRange = CheckIfWithinRange(transform.position, -maxRange, maxRange);

            if (!isWithinRange)
            {
                Debug.LogWarning("REMOVINGROOM!!!!");
                isOnPosition = true;
                _rb.constraints = RigidbodyConstraints.FreezeAll;
                _data.OnRoomDone();
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }


            Collider[] colliders = Physics.OverlapBox(_moveCollider.bounds.center, _moveCollider.bounds.extents, Quaternion.identity);

                foreach (Collider collider in colliders)
                {
                    // Check if the collider belongs to a valid game object
                    GameObject obj = collider.gameObject;



                    if (obj != gameObject) // Exclude self from moving away
                    {
                        // Calculate the direction from the object to this game object
                        Vector3 direction = obj.transform.position - transform.position;

                        // Normalize the direction vector to get a consistent speed
                        direction.Normalize();

                        // Apply translate in the opposite direction to move away from the room
                        obj.transform.Translate(direction * _moveSpeed * Time.deltaTime);
                    }
                }
            _previousPosition = transform.position;

            yield return null;
        }

            isOnPosition = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            int x = Mathf.RoundToInt(transform.position.x);
            int z = Mathf.RoundToInt(transform.position.z);
            while (transform.position.x != x && transform.position.z != z)
            {
                transform.position = new Vector3(x, 0, z);
            }
            _data.OnRoomDone();
            //Debug.Log("x =" + x + " z = " + z);
            _moveCollider.isTrigger = false;
            this.enabled = !this.enabled;

        //yield return null;
    } 


    
    private void Update()
    {
        if (!IsRoomStatic)
        {
            // Get all the colliders within the specified moveCollider

            bool isWithinRange = CheckIfWithinRange(transform.position, -maxRange, maxRange);

            if (!isWithinRange)
            {
                isOnPosition = true;
                _rb.constraints = RigidbodyConstraints.FreezeAll;
                int x = Mathf.RoundToInt(transform.position.x);
                int z = Mathf.RoundToInt(transform.position.z);
                _data.OnRoomDone();
                ObjectPoolManager.ReturnObjectToPool(gameObject);
                return;
            }
            

            Collider[] colliders = Physics.OverlapBox(_moveCollider.bounds.center, _moveCollider.bounds.extents, Quaternion.identity);

            if (!isOnPosition)
                foreach (Collider collider in colliders)
                {
                    // Check if the collider belongs to a valid game object
                    GameObject obj = collider.gameObject;


                    if (obj != gameObject) // Exclude self from moving away
                    {
                        // Calculate the direction from the object to this game object
                        Vector3 direction = obj.transform.position - transform.position;

                        // Normalize the direction vector to get a consistent speed
                        direction.Normalize();

                        // Apply translate in the opposite direction to move away from the room
                        obj.transform.Translate(direction * _moveSpeed * Time.deltaTime);
                    }
                }

            if (Vector3.Distance(transform.position, _previousPosition) <= _stationaryThreshold)
            {
                isOnPosition = true;
                _rb.constraints = RigidbodyConstraints.FreezeAll;
                int x = Mathf.RoundToInt(transform.position.x);
                int z = Mathf.RoundToInt(transform.position.z);
                while (transform.position.x != x && transform.position.z != z)
                {
                    transform.position = new Vector3(x, 0, z);
                }
                _data.OnRoomDone();
                //Debug.Log("x =" + x + " z = " + z);
                _moveCollider.isTrigger = false;
                this.enabled = !this.enabled;
            }
            _previousPosition = transform.position;
        }
        else
        {
            isOnPosition = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _data.OnRoomDone();
            _moveCollider.isTrigger = false;
            this.enabled = !this.enabled;
        }
    }
        

    private bool CheckIfWithinRange(Vector3 position, Vector2 minRange, Vector2 maxRange)
    {
        // Check if the object's position is within the range
        bool isWithinXRange = (position.x >= minRange.x && position.x <= maxRange.x);
        bool isWithinYRange = (position.z >= minRange.y && position.z <= maxRange.y);

        return (isWithinXRange && isWithinYRange);
    }
}