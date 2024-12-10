using TMPro;
using UnityEngine;

public class ShowingPool : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberObjectAppearances;
    [SerializeField] private TMP_Text _numberCreatedObjects;
    [SerializeField] private TMP_Text _numberActiveObjects;

    public void Show(ViewInfo viewInfo)
    {
        _numberObjectAppearances.text = viewInfo.NumberObjectAppearances.ToString();
        _numberCreatedObjects.text = viewInfo.NumberCreatedObjects.ToString();
        _numberActiveObjects.text = viewInfo.NumberActiveObjects.ToString();
    }
}