using UnityEngine;

public class ContinuousGroundController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1.8f;
    [SerializeField] private Transform[] grounds; // Assign BG1 and BG2 here
    private float groundWidth;

    void Start()
    {
        // Get width from the first ground's sprite size
        SpriteRenderer sr = grounds[0].GetComponent<SpriteRenderer>();
        groundWidth = sr.bounds.size.x;
    }

    void Update()
    {
        foreach (Transform ground in grounds)
        {
            ground.position += Vector3.left * scrollSpeed * Time.deltaTime;

            //Debug.Log(groundWidth);
            // If ground moved completely off screen, reset its position
            if (ground.position.x < -groundWidth)
            {
                ground.position = new Vector3(ground.position.x + (groundWidth * 2), ground.position.y, ground.position.z);
            }
        }
    }
}
