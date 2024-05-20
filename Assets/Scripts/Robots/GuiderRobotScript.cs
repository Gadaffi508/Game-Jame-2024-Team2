using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public AudioClip audioClip; // Assign this in the Inspector
    public Transform destination; // The target destination
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private SubtitleManager subtitleManager; // Reference to the SubtitleManager script
    private bool hasTriggered = false; // Flag to track if trigger has been activated

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        subtitleManager = FindObjectOfType<SubtitleManager>(); // Find the SubtitleManager in the scene

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing from the GameObject.");
        }
        else
        {
            // Check if the agent is on a NavMesh
            if (!navMeshAgent.isOnNavMesh)
            {
                Debug.LogError("NavMeshAgent is not on a NavMesh. Ensure the GameObject is placed on a NavMesh.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            PlayAudioAndMove();
            hasTriggered = true; // Set the flag to true to prevent repeated triggering
        }
    }

    void PlayAudioAndMove()
    {
        if (audioClip != null)
        {
            // Create a new AudioSource component at runtime and assign the audio clip to it
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();

            // Show subtitles
            if (subtitleManager != null)
            {
                subtitleManager.ShowSubtitle("Oh, you must be the traveler! Quick! Follow me!");
            }

            // Move to destination after the audio finishes
            Invoke("MoveToDestination", audioClip.length);

            // Hide subtitles after the same duration
            Invoke("HideSubtitles", audioClip.length);
        }
        else
        {
            Debug.LogError("AudioClip is missing.");
        }
    }

    void MoveToDestination()
    {
        if (navMeshAgent != null && destination != null)
        {
            // Check if the agent is on a NavMesh before setting the destination
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.SetDestination(destination.position);
                Debug.Log("Setting destination to: " + destination.position);
            }
            else
            {
                Debug.LogError("NavMeshAgent is not on a NavMesh. Cannot set destination.");
            }
        }
        else
        {
            if (navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent is missing.");
            }
            if (destination == null)
            {
                Debug.LogError("Destination is not set.");
            }
        }
    }

    void HideSubtitles()
    {
        if (subtitleManager != null)
        {
            subtitleManager.HideSubtitle();
        }
    }
}
