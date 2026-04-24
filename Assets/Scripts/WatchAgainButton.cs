using UnityEngine;

public class WatchAgainButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.StartNewPlaythrough();
    }
}
