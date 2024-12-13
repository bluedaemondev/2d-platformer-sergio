using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Score Manager es unico en nuestro juego. No pueden existir 2 gestores
// de puntaje, ya que traeria errores en la informacion
// Patron "Singleton"
public class ScoreManager : MonoBehaviour
{
    #region Singleton Def.
    // Atributo
    private static ScoreManager instance;

    // Propiedad 
    public static ScoreManager Instance
    {
        get => instance;
        private set => instance = value;
    }
    #endregion

    // atributo
    private int totalScore; // puntaje total

    [SerializeField]
    private PointsSceneHUD displayText;


    // propiedad
    public int TotalScore { get => totalScore; }

    private void Awake()
    {
        #region Definir Instance
        if (instance == null)
        {
            instance = this;
        }
        #endregion

        if (displayText == null) displayText = FindObjectOfType<PointsSceneHUD>();
    }

    public void AddScore(int ptsToAdd)
    {
        totalScore += ptsToAdd;
        displayText.SetDisplayText(totalScore);
    }


}
