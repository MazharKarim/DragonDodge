using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRevive : MonoBehaviour
{
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float reviveDuration = 3f;

    private GameObject[] obstacles;
    private List<Collider2D> obstacleColliders = new();

    private void Awake()
    {
        if (shieldEffect != null) shieldEffect.SetActive(false);
    }

    void FixedUpdate()
    {
    }

    public void GetObstacleColliders()
    {
        obstacleColliders.Clear(); // clear previous data

        if (obstacles == null) return;

        foreach (GameObject obj in obstacles)
        {
            if (obj == null) continue;

            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null)
            {
                obstacleColliders.Add(col);
            }
        }
    }

    public void DisableObstacleColliders()
    {
        foreach (Collider2D col in obstacleColliders)
        {
            if (col != null)
                col.enabled = false;
        }
    }

    public void EnableObstacleColliders()
    {
        foreach (Collider2D col in obstacleColliders)
        {
            if (col != null)
                col.enabled = true;
        }
    }

    public void Revive()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GetObstacleColliders();
        DisableObstacleColliders();

        Time.timeScale = 1f;

        // Move to revive point
        transform.position = new Vector3(-1.5f, 0.75f, 0);
        shieldEffect.SetActive(true);
        playerSprite.enabled = true;
        playerSprite.color = new Color(1, 1, 1, 0.5f);

        // Start fancy revive
        StartCoroutine(ReviveSequence());
    }

    private IEnumerator ReviveSequence()
    {
        float blinkTime = 0.2f;
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < reviveDuration)
        {
            elapsed += blinkTime;
            yield return new WaitForSeconds(blinkTime);

            visible = !visible;
            playerSprite.enabled = visible;
        }

        EndReviveVisuals();
    }

    private void EndReviveVisuals()
    {
        shieldEffect.SetActive(false);
        playerSprite.enabled = true;
        playerSprite.color = new Color(1, 1, 1, 1f);

        EnableObstacleColliders();

        Debug.Log("Revive visuals reset");
    }
}
