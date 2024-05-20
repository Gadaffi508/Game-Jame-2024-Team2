using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public TMP_Text subtitleText;

    void Start()
    {
        // Ensure the subtitle text is not visible at the start
        if (subtitleText != null)
        {
            subtitleText.text = "";
            subtitleText.gameObject.SetActive(false);
        }
    }

    // Call this method to display subtitles
    public void ShowSubtitle(string subtitle)
    {
        if (subtitleText != null)
        {
            // Set the subtitle text and make it visible
            subtitleText.text = subtitle;
            subtitleText.gameObject.SetActive(true);
        }
    }

    // Call this method to hide subtitles
    public void HideSubtitle()
    {
        if (subtitleText != null)
        {
            // Clear the subtitle text and hide the text element
            subtitleText.text = "";
            subtitleText.gameObject.SetActive(false);
        }
    }
}
