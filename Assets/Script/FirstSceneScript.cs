using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneScript : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Level", 1);
        if (PlayerPrefs.HasKey("Level"))
        {
            int level = PlayerPrefs.GetInt("Level");
            SceneManager.LoadScene(level);
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("TotalLevel", 1);
            PlayerPrefs.SetInt("CoinDeger", 0);
            SceneManager.LoadScene(1);
        }

    }
}
