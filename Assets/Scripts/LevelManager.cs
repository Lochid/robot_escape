using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int savedLemmings = 0;

    // Update is called once per frame
    void Update()
    {
        var allObjects = Resources.FindObjectsOfTypeAll<Lemings>();
        if(allObjects.Count() == 1)
        {
            GameOver();
        }
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
