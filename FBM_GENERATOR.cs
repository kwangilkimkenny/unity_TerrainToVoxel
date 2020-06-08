using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBM_GENERATOR : MonoBehaviour
{

    static int maxHeight = 150;
    static float smooth = 0.01f;
    static int octaves = 4;
    static float persistence = 0.5f;

    static float fBM(float x, float z, int oct, float pers)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        for (int i = 0; i < oct; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, z * frequency) * amplitude;

            maxValue += amplitude;

            amplitude *= pers;
            frequency *= 2;
        }

        //Debug.Log("FBM:" + total / maxValue);

        return total / maxValue; // fBM값을 생성

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
