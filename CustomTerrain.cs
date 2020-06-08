using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;



[ExecuteInEditMode] // 변경사항 즉시 적용, 

public class CustomTerrain : MonoBehaviour {
    
    public Vector2 randomHeightRange = new Vector2(0,0.1f);
    public Texture2D heightMapImage;
    public Vector3 heightMapScale = new Vector3(1, 1, 1);


    public Terrain terrain;
    //public TerrainData terrainData; // 이 부분의 데이타가 매우 많아짐. 터레인의 크기에 따라서 ... 이데이터를 Utils.fBM대신에 입력데이터로 넣어보자!!!!!!
    public static TerrainData terrainData; //


    public void RandomTerrain()// 2    heightMap에 적용하는  x, z값을 랜덤생성하여 터레인 제ㅈ
    {
        float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapWidth,
                                                          terrainData.heightmapHeight);
        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int z = 0; z < terrainData.heightmapHeight; z++) // 3D에서 터레인의 가로세로 크기를 x, z로 정함. y는 높이까 사용하지 않음.
            {
                heightMap[x, z] += UnityEngine.Random.Range(randomHeightRange.x, randomHeightRange.y);//x,y는 min / max height value !
            }
        }
        terrainData.SetHeights(0, 0, heightMap); // 0,0에서 시작하여 모든 위치의 높이값을 랜덤으로 만들어서 테레인 데이터를 만든다. 매우 복잡한 랜덤 터레인이 만들어짐 ㅎ
        //Debug.Log("terraindata: " + terrainData);

    }

    public float[,] GetHeightMap;


    public void LoadTexture()
    {
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapWidth, terrainData.heightmapHeight];


        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int z = 0; z < terrainData.heightmapHeight; z++)
            {
                heightMap[x, z] = heightMapImage.GetPixel((int)(x * heightMapScale.x),
                                                          (int)(z * heightMapScale.z)).grayscale * heightMapScale.y;
                //Debug.Log("terraindata: " + heightMap[x, z]);
            }
        }
        GetHeightMap = heightMap;

        terrainData.SetHeights(0, 0, heightMap);

    }

    public float GetHeight(int x, int z) //0~ 1사이에 float 값. 
    {
        return heightMapImage.GetPixel((int)(x * heightMapScale.x),
                                                          (int)(z * heightMapScale.z)).grayscale * heightMapScale.y;

    }




    public void ResetTerrain()
    {
        float[,] heightMap;
        heightMap = new float[terrainData.heightmapWidth, terrainData.heightmapHeight];
        for (int x = 0; x < terrainData.heightmapWidth; x++)
        {
            for (int z = 0; z < terrainData.heightmapHeight; z++)
            {
                heightMap[x, z] = 0;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);

    }

    void OnEnable()// 1
    {
        Debug.Log("Initialising Terrain Data");
        terrain = this.GetComponent<Terrain>(); //터레인을 잡아서 터레인에 넣음
        terrainData = Terrain.activeTerrain.terrainData; //모든 터레인의 데이터가 terrainData에 담아/
    }

    void Awake()
    {
        SerializedObject tagManager = new SerializedObject(
                              AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        AddTag(tagsProp, "Terrain");
        AddTag(tagsProp, "Cloud");
        AddTag(tagsProp, "Shore");

        //apply tag changes to tag database
        tagManager.ApplyModifiedProperties();

        //take this object
        this.gameObject.tag = "Terrain";
    }

    void AddTag(SerializedProperty tagsProp, string newTag)
    {
        bool found = false;
        //ensure the tag doesn't already exist
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(newTag)) { found = true; break; }
        }
        //add your new tag
        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
            newTagProp.stringValue = newTag;
        }
    }

    // Use this for initialization
	void Start () {



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
