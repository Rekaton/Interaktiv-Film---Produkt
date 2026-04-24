using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [Header("References")]
    public VideoPlayer videoPlayer;
    public GameObject choiceUI;

    [Header("Timing")]
    public float showChoiceTime;

    private bool hasShownChoices = false;

    void Start()
    {
        if (choiceUI != null)
        {
            choiceUI.SetActive(false);
        }
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (!hasShownChoices && videoPlayer.time >= showChoiceTime)
        {
            ShowChoices();
        }
    }

    void ShowChoices()
    {
        hasShownChoices = true;
        if (choiceUI != null)
        {
            choiceUI.SetActive(true);
        }


        // hvis video skal pauses
        //videoPlayer.Pause();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (choiceUI == null)
        {
            hasShownChoices = false;
            GameManager.Instance.OnVideoFinished(hasShownChoices);
        }

    }
}