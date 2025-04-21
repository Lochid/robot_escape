using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TMP_Text left;
    public TMP_Text saved;
    public int savedLemmings = 0;
    public int needToSave = 1;
    public Spawner spawner;

    // Update is called once per frame
    void Update()
    {
        var allObjects = Resources.FindObjectsOfTypeAll<Lemings>();
        if(allObjects.Count() == 1)
        {
            GameOver();
        }
        saved.text = "Saved:" + savedLemmings.ToString() + "/" + needToSave.ToString();
        left.text = "Left:" + (spawner.count - spawner.times + allObjects.Count() - 1).ToString() + "/" + spawner.count.ToString();
    }

    void GameOver()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
