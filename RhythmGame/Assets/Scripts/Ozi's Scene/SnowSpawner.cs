using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public const string SNOWSPAWNER_NAME = "Snow Spawner Yee~";
    public const int SNOW_SCORE = 15;
    public const float STACK_MUL = 0.1f;
    public GameObject snow;

    public GameObject[] objects = new GameObject[4];
    [Range(0, 60)] public int[] snowStack = new int[4] { 0, 0, 0, 0 };
    
    void Awake()
    {
        GameObject @object = GameObject.Find(SNOWSPAWNER_NAME);
        if (@object == null)
        {
            @object = new GameObject(SNOWSPAWNER_NAME);
            if(GameObject.Find("System") != null) { @object.transform.parent = GameObject.Find("System").transform; }

            SnowSpawner spanwer = @object.AddComponent<SnowSpawner>();
            spanwer.snow = snow;
            
            if(snow != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    spanwer.objects[i] = Instantiate(snow);
                    spanwer.objects[i].transform.parent = @object.transform;
                }
                
            }

            spanwer.objects[0].transform.position = new Vector3(NoteReader.NOTE_DISTANCE * -1.5f, NoteDown.JDG_LINE, spanwer.objects[0].transform.position.z);
            spanwer.objects[1].transform.position = new Vector3(NoteReader.NOTE_DISTANCE * -0.5f, NoteDown.JDG_LINE, spanwer.objects[1].transform.position.z);
            spanwer.objects[2].transform.position = new Vector3(NoteReader.NOTE_DISTANCE *  0.5f, NoteDown.JDG_LINE, spanwer.objects[2].transform.position.z);
            spanwer.objects[3].transform.position = new Vector3(NoteReader.NOTE_DISTANCE *  1.5f, NoteDown.JDG_LINE, spanwer.objects[3].transform.position.z);

            Destroy(this);
        }
    }

    void Update()
    {
        objects[0].transform.localScale = new Vector3(objects[0].transform.localScale.x, snowStack[0] * STACK_MUL, objects[0].transform.localScale.z);
        objects[1].transform.localScale = new Vector3(objects[1].transform.localScale.x, snowStack[1] * STACK_MUL, objects[1].transform.localScale.z);
        objects[2].transform.localScale = new Vector3(objects[2].transform.localScale.x, snowStack[2] * STACK_MUL, objects[2].transform.localScale.z);
        objects[3].transform.localScale = new Vector3(objects[3].transform.localScale.x, snowStack[3] * STACK_MUL, objects[3].transform.localScale.z);
    }

    public void SnowHit(NoteLine line)
    {
        snowStack[(int)line]++;
    }

    public int SnowClear(NoteLine line)
    {
        int returnValue = snowStack[(int)line] * SNOW_SCORE;

        snowStack[(int)line] = 0;

        return returnValue;
    }
}
