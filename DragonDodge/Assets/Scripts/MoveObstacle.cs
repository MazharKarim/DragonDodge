using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 1.8f;

    // Update is called once per frame
    private void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
    }
}
