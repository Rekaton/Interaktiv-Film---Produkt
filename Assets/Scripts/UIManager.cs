using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider barA;
    public Slider barB;
    public Slider barC;

    public GameObject choiceUI;

    void Start()
    {
        //choiceUI.SetActive(false);
        UpdateUI();
    }

    public void UpdateUI()
    {
        barA.value = GameManager.Instance.valueA;
        barB.value = GameManager.Instance.valueB;
        barC.value = GameManager.Instance.valueC;
    }

    public void ShowChoices()
    {
        choiceUI.SetActive(true);
    }
}
