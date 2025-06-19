using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = .8f;
    [SerializeField] private Transform[] backgrounds; // Assign BG1 and BG2 here
    private float backgroundWidth;

    void Start()
    {
        // Get width from the first background's sprite size
        SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
        backgroundWidth = sr.bounds.size.x;
    }

    void Update()
    {
        foreach (Transform background in backgrounds)
        {
            background.position += Vector3.left * scrollSpeed * Time.deltaTime;

            //Debug.Log(backgroundWidth);
            // If background moved completely off screen, reset its position
            if (background.position.x < -backgroundWidth)
            {
                background.position = new Vector3(background.position.x + (backgroundWidth * 2), background.position.y, background.position.z);
            }
        }
    }
}
