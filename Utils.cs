using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// BIM data를 불러와서 높이값을 생성하자.
// 1.구글의 위성사진을 가져와서 B/W로 전환하고 높이값을 추출한다.
// 2.추출한 값을 리스트에 [] 놓고 chunk에서 하나씩  추출하여 지형도를 완성한다.


public class Utils
{

	static int maxHeight = 150;
	static float smooth = 0.01f;
	static int octaves = 4;
	static float persistence = 0.5f;


  
    public static int GenerateHeight(float x, float z) // 높이를 생성을 정한다.
    {
       

        CustomTerrain customTerrain = GameObject.Find("Terrain").GetComponent<CustomTerrain>();

    
        float height = Map(0, maxHeight, 0, 1, customTerrain.GetHeight((int)x,(int)z)); // 성공 

        return (int) height;//지도 데이터를 가져와서 (int) height를 추출하면 됨!!!!!!! 
    }

    private static float Map(int newmin, int newmax, int origmin, int origmax, float getHeightMap)
    {
        return Mathf.Lerp(newmin, newmax, Mathf.InverseLerp(origmin, origmax, getHeightMap));//??????????????????????????????
    }


    //static float Map(float newmin, float newmax, float origmin, float origmax, float value)
    //{
    //    return Mathf.Lerp(newmin, newmax, Mathf.InverseLerp(origmin, origmax, value));
    //}



    static float fBM(float x, float z, int oct, float pers)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        for(int i = 0; i < oct ; i++) //octaves 값이 4이기 때문에 4의 값만 생성한다. 4칸만 생성하고 이것을 다시 4X4X4 만큼 chunks로 만들어 냄, heightmap에서도 4의 크기만큼 값을 정수형으로 가져오며 /
        {
                total += Mathf.PerlinNoise(x * frequency, z * frequency) * amplitude;

                maxValue += amplitude;

                amplitude *= pers;
                frequency *= 2;
        }

        return total/maxValue; // fBM값을 생성
    }
}