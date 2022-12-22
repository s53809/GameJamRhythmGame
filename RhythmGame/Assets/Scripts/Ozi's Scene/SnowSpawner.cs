using Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public const string SNOWSPAWNER_NAME = "Snow Spawner Yee~";
    public const float SNOW_SPAWN_Y = 10.0f;
    public const int SNOW_SCORE = 15;
    public const float STACK_MUL = 0.1f;
    public GameObject[] snow;

    private Queue<GameObject> snows = new Queue<GameObject>();
    [ReadOnly] public int count = 0;
    
    void Awake()
    {
        GameObject @object = GameObject.Find(SNOWSPAWNER_NAME);
        if (@object == null)
        {
            @object = new GameObject(SNOWSPAWNER_NAME);
            if(GameObject.Find("System") != null) { @object.transform.parent = GameObject.Find("System").transform; }

            SnowSpawner spanwer = @object.AddComponent<SnowSpawner>();
            spanwer.snow = snow;
            
            Destroy(this);
        }
    }

    void Update()
    {
        count = snows.Count;
    }

    [ContextMenu("Spawn")]
    public void SnowSpawn()
    {
        GameObject newSnow = Instantiate(snow[Random.Range(0, 3)]);
        newSnow.transform.position = new Vector3(Random.Range(NoteReader.NOTE_DISTANCE * -1.5f, NoteReader.NOTE_DISTANCE * 1.5f), SNOW_SPAWN_Y, 0);
        newSnow.transform.parent = gameObject.transform;

        snows.Enqueue(newSnow);
    }

    [ContextMenu("Pop")]
    public void SnowPop()
    {
        if (snows.Count > 0)
        {
            SnowFade sf = snows.First().GetComponent<SnowFade>();
            if (sf != null) { sf.isFire = true; }
            snows.Dequeue();
        }
    }
    public int SnowHit()
    {
        SnowFade sf = snows.First().GetComponent<SnowFade>();
        if(sf != null) { sf.isFire = true; }
        snows.Dequeue();

        return SNOW_SCORE;
    }
    public int SnowClear()
    {
        int result = snows.Count * SNOW_SCORE;

        for(int i = 0, max = snows.Count; i < max; i++)
        {
            SnowFade sf = snows.First().GetComponent<SnowFade>();
            if (sf != null) { sf.isFire = true; }
            snows.Dequeue();
        }

        
        return result;
    }
}
