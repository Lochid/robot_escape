using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject lemming;
    public int count = 5;
    public float timeout = 3;
    public int times = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnPrefab());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPrefab()
    {
        while (times < count)
        {
            var inst = Instantiate(lemming);
            inst.transform.position = transform.position;
             times++;
            yield return new WaitForSeconds(timeout);
        }
    }
}
