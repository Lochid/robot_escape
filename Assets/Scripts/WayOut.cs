using UnityEngine;

public class WayOut : MonoBehaviour
{
    public LevelManager LevelManager;
    public AudioSource saveSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LevelManager.savedLemmings++;
            saveSound.Play();
        }
    }
}
