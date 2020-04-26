using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class ResultWindowManager : MonoBehaviour
{
    
    public GameObject resultRowPref;
    public Transform resultsContainer;
    public ResultRowController currentPlayerRow;


    private void UpdateCurrentResultWindow(string score, int medalColorIndex)
    {
        currentPlayerRow.UpdateRowInfo(PlayerDataManager.Instance.name, score, PlayerDataManager.Instance.playerFlag, PlayerDataManager.Instance.medalColors[medalColorIndex]);
    }

    private void SpawnLeaders()
    {
        
    }
}
