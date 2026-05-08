using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public Color colorPlayer;
    public Color colorEnemy;

    public string namePlayer;
    public string nameEnemy;

    private string saveWinnerkey = "SavedWinner";
    private string savescorekey = "SavedScore";

    private static SaveController _instance;

    public static SaveController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SaveController não existe na cena!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
       if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public string GetName(bool isPlayer)
    {
        return isPlayer ? namePlayer : nameEnemy;
    }

    public void Reset()
    {
        namePlayer = "";
        nameEnemy = "";
        colorPlayer = Color.white;
        colorEnemy = Color.white;
    }

    public void SaveWinner(string winner)
    {
        PlayerPrefs.SetString(saveWinnerkey, winner);
    }
    
    public string GetLastWinner()
    { 
        return PlayerPrefs.GetString(saveWinnerkey);
    }

    public void SaveScores(string score)
    {
        PlayerPrefs.SetString(savescorekey, score);
    }

    public string GetlastScore()
    {
        return PlayerPrefs.GetString(savescorekey);
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
