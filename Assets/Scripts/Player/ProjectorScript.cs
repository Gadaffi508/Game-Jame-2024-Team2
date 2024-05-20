using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TriggerInteraction : MonoBehaviour
{
    public Canvas interactCanvas;
    public TMP_Text interactionText;
    public TMP_Text timerText;
    public float interactionTime = 5f;
    private float countdown;
    private bool playerInRange = false;
    private bool interactionSucceeded = false;

    void Start()
    {
        interactCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange)
        {
            countdown -= Time.deltaTime;
            timerText.text = Mathf.Floor(countdown).ToString("00") + ":" + ((countdown % 1) * 1000).ToString("000");
            interactionText.text = "Press 'E' to interact";

            if (countdown <= 0f)
            {
                if (!interactionSucceeded)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteract();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInRange)
        {
            playerInRange = true;
            countdown = interactionTime;
            interactCanvas.gameObject.SetActive(true);
        }
    }


    void OnInteract()
    {
        interactionSucceeded = true;
        interactCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene("FutureTimeScene");
    }
}
