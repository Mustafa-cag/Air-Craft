using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject tapToStartButton;
    public GameObject gamePanel;
    public GameObject objPool;

    public GameObject losePanel;
    public GameObject winPanel;

    #region Singleton
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    public void TapToStart()
    {
        CameraController.instance.isStarted = true;
        tapToStartButton.SetActive(false);
        objPool.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void NextButton()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt("Level", (PlayerPrefs.GetInt("Level") + 1));
            PlayerPrefs.SetInt("TotalLevel", (PlayerPrefs.GetInt("TotalLevel") + 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            PlayerPrefs.SetInt("LvlNumber", PlayerPrefs.GetInt("LvlNumber") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("TotalLevel", (PlayerPrefs.GetInt("TotalLevel") + 1));
            SceneManager.LoadScene(1);

            PlayerPrefs.SetInt("LvlNumber", PlayerPrefs.GetInt("LvlNumber") + 1);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public IEnumerator losePanelActiveTime()
    {
        yield return new WaitForSeconds(3);
        losePanel.SetActive(true);
    }

    public IEnumerator winActiveTime()
    {
        yield return new WaitForSeconds(5);
        winPanel.SetActive(true);
    }


}
