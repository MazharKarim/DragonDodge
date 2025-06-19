using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 2.5f;
    [SerializeField] private float _heightRange = 2.25f;
    [SerializeField] private GameObject _obstacle;

    [SerializeField] private float _timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > _maxTime)
        {
            SpawnObstacle();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-_heightRange, _heightRange));
        GameObject obstacle = Instantiate(_obstacle, spawnPosition, Quaternion.identity);

        Destroy(obstacle, 15f);
    }
}
