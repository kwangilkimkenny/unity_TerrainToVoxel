using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {

	public Material cubeMaterial;
	public Block[,,] chunkData; //청크데이터는 리스트롸 3개의 값을 설정해야한다. x, y, z
	public GameObject chunk;

	void BuildChunk() //청크가 드디어 만들어진다.
	{
		chunkData = new Block[World.chunkSize,World.chunkSize,World.chunkSize];
		

		for (int z = 0; z < World.chunkSize; z++)
			for(int y = 0; y < World.chunkSize; y++)
				for(int x = 0; x < World.chunkSize; x++)
				{
					Vector3 pos = new Vector3(x,y,z);
					int worldX = (int)(x + chunk.transform.position.x);
					int worldY = (int)(y + chunk.transform.position.y);
					int worldZ = (int)(z + chunk.transform.position.z);
					if(worldY <= Utils.GenerateHeight(worldX,worldZ)) // 생성한 높이값비교해서 청크의  그기대로 높이값을 정한다. 작거나 같으면  Dirt로 생성하고
                    //if(worldY <= HeightmapFromTexture.ApplyHeightmap(worldX, worldZ))
						chunkData[x,y,z] = new Block(Block.BlockType.DIRT, pos, chunk.gameObject, this);
						
					else   // 크면 이것은 공중이라 air 로 생성한다.
						chunkData[x,y,z] = new Block(Block.BlockType.AIR, pos, chunk.gameObject, this);
				}
	}

	public void DrawChunk() //청크를 청크월드의 크기를 정해서
	{
		for(int z = 0; z < World.chunkSize; z++)
			for(int y = 0; y < World.chunkSize; y++)
				for(int x = 0; x < World.chunkSize; x++)
				{
					chunkData[x,y,z].Draw(); //그린다.

					//UI parts: 생성된 chunkData 수를 카운트해보자.
					//Count(chunkData);

				}
		CombineQuads(); //그린 결과물을 콤바인한다.
	}

	// UI : count generated chunkData !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ing.............
	//public static int terrainAmountValue;

	//public void Count(Block[,,] chunkData)
   //   {
		
	//	GameObject[] Dirts = GameObject.FindGameObjectsWithTag("DirtCell");
	//	terrainAmountValue = Dirts.Length;
	//	//Debug.Log("Generated dirts:" + terrainAmountValue); // 생성된 dirts의 갯수를 카운트한다.
	//	TerrainCounter.AddCounter();

	//}


    // Use this for initialization 으로 재질과 포지선을 입력하여 고
    public Chunk (Vector3 position, Material c) {
		
		chunk = new GameObject(World.BuildChunkName(position)); //게임오브젝트를 청크에 넣고

		chunk.gameObject.tag = "DirtCell";/////////////<<<<<< 추가한 코드 !!!!!!!!!!!!!!!!!!!!


		chunk.transform.position = position; //포지션을 정하고
		cubeMaterial = c; //재질도 정하고 나서
		BuildChunk(); //청크를 만든다.
	}
	
	void CombineQuads() // 모든 메쉬를 연결하여 쿼드로 만들어낸다.
	{
		//1. Combine all children meshes
		MeshFilter[] meshFilters = chunk.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        //2. Create a new mesh on the parent object 
        MeshFilter mf = (MeshFilter) chunk.gameObject.AddComponent(typeof(MeshFilter));
        mf.mesh = new Mesh();

        //3. Add combined meshes on children as the parent's mesh
        mf.mesh.CombineMeshes(combine);

        //4. Create a renderer for the parent
		MeshRenderer renderer = chunk.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material = cubeMaterial;

		//5. Delete all uncombined children
		foreach (Transform quad in chunk.transform) {
     		GameObject.Destroy(quad.gameObject);
 		}

	}

}
