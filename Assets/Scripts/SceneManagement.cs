using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    public Player playerScript;
    public Animator startTextAnim;
    public GameObject startText;
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        levelText.text = (PlayerPrefs.GetInt("level")+1).ToString();
        stack.cubes.Clear();
    }
    public void StartGame()
    {
        startTextAnim.enabled = false;
        Destroy(startText);
        playerScript.enabled = true;
    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("level",PlayerPrefs.GetInt("level")+1);
        SceneManager.LoadScene(0);
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
