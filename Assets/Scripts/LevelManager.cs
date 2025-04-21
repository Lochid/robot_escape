using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TMP_Text left;
    public TMP_Text saved;
    public TMP_Text savedWin;
    public TMP_Text savedLose;
    public int savedLemmings = 0;
    public int needToSave = 1;
    public Spawner spawner;
    public SceneAsset nextScene;

    public GameObject ui;
    public GameObject winModal;
    public GameObject loseModal;
    public AudioSource musicSound;
    public AudioSource wonSound;
    bool gamedOver = false;

    // Update is called once per frame
    void Update()
    {
        var allObjects = Resources.FindObjectsOfTypeAll<Lemings>();
        if (allObjects.Count() == 1 && !gamedOver)
        {
            gamedOver = true;
            GameOver();
        }
        saved.text = "Saved:" + savedLemmings.ToString() + "/" + needToSave.ToString();
        savedWin.text = "You saved:" + savedLemmings.ToString() + "/" + needToSave.ToString();
        savedLose.text = "You saved:" + savedLemmings.ToString() + "/" + needToSave.ToString();
        left.text = "Left:" + (spawner.count - spawner.times + allObjects.Count() - 1).ToString() + "/" + spawner.count.ToString();
    }

    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadScene()
    {
        var sceneName = nextScene.name;
        SceneManager.LoadScene(sceneName);
    }
    void GameOver()
    {
        ui.SetActive(false);
        musicSound.Stop();
        if (savedLemmings >= needToSave)
        {
            wonSound.Play();
            winModal.SetActive(true);
        }
        else
        {
            loseModal.SetActive(true);
        }
    }
}
