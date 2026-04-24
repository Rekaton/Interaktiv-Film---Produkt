using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public ChoiceData choiceData;

    public void OnClick()
    {
        GameManager.Instance.ApplyChoice(choiceData);

    }
}
