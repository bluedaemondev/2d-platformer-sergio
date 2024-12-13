using UnityEngine;
using TMPro;

public class PointsSceneHUD : MonoBehaviour
{
    TextMeshProUGUI guiText;

    private void Awake()
    {
        guiText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Called on Score Manager, after updating total scene score
    /// </summary>
    /// <param name="newTotalPoints">Total points in this level</param>
    public void SetDisplayText(int newTotalPoints)
    {
        guiText.text = "points: " + newTotalPoints;
    }
}
