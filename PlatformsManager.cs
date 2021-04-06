using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour, ISetupObject
{
    private static float _distance = 5.0f;
    //public static float getDistance() => _distance;

    private const int _platformsOnLevelLimit = 15;

    public static int _platformsCount = 0;
    private Vector3 _currentSpawnPoint;
    private List<Vector3> _spawnPoints;
    private float widthOfOnePlatform;
    private Transform _lastCreated;
    private Vector3 _playersPosition;
    private Vector3 _test;
    private GameObject _platformPrefab;

    private List<Transform> _platformsCoords;

    private void Awake()
    {
        _platformPrefab = (GameObject)Resources.Load("Prefabs/Platform", typeof(GameObject));
    }

    private void Start()
    {
        _platformsCoords = new List<Transform>();
    }

    public void SetupObject()
    {
        _currentSpawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        _playersPosition = _currentSpawnPoint;

        Vector3 _cameraTopLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 3f));
        Vector3 _cameraBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 3f));

        float actualWidth = _cameraBottomRight.x - _cameraTopLeft.x;
        widthOfOnePlatform = actualWidth / 3;

        _spawnPoints = new List<Vector3>(3);
        _spawnPoints.Add(new Vector3(_cameraTopLeft.x + widthOfOnePlatform, 0.3f, _currentSpawnPoint.z));
        _spawnPoints.Add(new Vector3(_cameraTopLeft.x + widthOfOnePlatform * 1.5f, 0.3f, _currentSpawnPoint.z));
        _spawnPoints.Add(new Vector3(_cameraTopLeft.x + widthOfOnePlatform * 2f, 0.3f, _currentSpawnPoint.z));

        _currentSpawnPoint = _spawnPoints[1];
        _currentSpawnPoint.z += _distance;

        while(_platformsCount < _platformsOnLevelLimit)
        {
            if (_lastCreated)
            {
                _currentSpawnPoint = _lastCreated.position;
                _currentSpawnPoint.z += _distance;
                _currentSpawnPoint.x = _spawnPoints[Random.Range(0, 3)].x;
            }

            GameObject platform = Instantiate(_platformPrefab, _currentSpawnPoint, Quaternion.identity, transform);
            _platformsCount++;
            platform.tag = "Platform";
            //platform.transform.SetParent(transform);
            platform.transform.localScale = new Vector3(widthOfOnePlatform * 0.5f, 0.1f, widthOfOnePlatform * 1.5f);
            //platform.transform.position = _currentSpawnPoint;
            platform.AddComponent<Platform>().SetSpeed(5f);
            _platformsCoords.Add(platform.transform);
            _lastCreated = platform.transform;
        }

        _test = _lastCreated.position;
        _test.z -= _distance;
    }

    public void MovePlatformWhichColliderWithDeadLine(Transform transform)
    {
        _test.x = _spawnPoints[Random.Range(0, 3)].x;
        transform.position = _test;
        _platformsCoords.Remove(transform);
        _platformsCoords.Add(transform);
    }

    public float GetTime(Transform transform)
    {
        int index = _platformsCoords.IndexOf(transform);
        return Mathf.Abs((_platformsCoords[index + 1].position.z - _playersPosition.z) / transform.GetComponent<Platform>().GetSpeed());
    }
}
