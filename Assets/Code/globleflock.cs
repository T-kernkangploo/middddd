using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globleflock : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject goalPrefab;
    public static int tankSize =5;
    public static Vector3 goalPos = Vector3.zero;

    static int numFish = 10;
    public static GameObject[] allfish=new GameObject[numFish];
    void Start()
    {
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize));
            allfish[1] = (GameObject) Instantiate(fishPrefab,pos, Quaternion.identity);
        }
    }

    void Update()
    {
        if (Random.Range(0, 10000) < 50)
        {
                goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize));
           // goalPrefab.transform.position = goalPos;//
        }
    }
}
