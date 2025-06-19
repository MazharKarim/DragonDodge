using UnityEngine;
using UnityEngine.Audio;

public class ObstacleTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip score;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.clip = score;
            audioSource.Play();
            Score.instance.UpdateScore();
        }
    }
}
