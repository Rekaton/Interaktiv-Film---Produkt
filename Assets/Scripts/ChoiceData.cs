using UnityEngine;

[CreateAssetMenu(fileName = "NewChoice", menuName = "InteractiveFilm/Choice")]
public class ChoiceData : ScriptableObject
{
    public int valueAChange;
    public int valueBChange;
    public int valueCChange;

    public string nextScene;
}
