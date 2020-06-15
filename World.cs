using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode] // 변경사항 즉시 적용


public class World : MonoBehaviour {

	public Material textureAtlas;
	public static int columnHeight = 32;
	public static int chunkSize = 32;
	public static int worldSize = 32;
	public static Dictionary<string, Chunk> chunks;

	public static string BuildChunkName(Vector3 v)
	{
		return (int)v.x + "_" + 
			   (int)v.y + "_" + 
			   (int)v.z;
	}

	// 이하 코드는 사용하지 않는다.
	//IEnumerator BuildChunkColumn() 
	//{
	//	for(int i = 0; i < columnHeight; i++)
	//	{
	//		Vector3 chunkPosition = new Vector3(this.transform.position.x, 
	//											i*chunkSize, 
	//											this.transform.position.z);
	//		Chunk c = new Chunk(chunkPosition, textureAtlas);
	//		c.chunk.transform.parent = this.transform;
	//		chunks.Add(c.chunk.name, c);
	//	}

	//	foreach(KeyValuePair<string, Chunk> c in chunks)
	//	{
	//		c.Value.DrawChunk();
	//		yield return null;
	//	}

	//}

	public UIManager uIManager; // UIManager를 사용하겠다는 거로 선언을 우선하uIManager.Cal(); // UIManager의 cal()실행하여 블럭이 얼마나 생성되었는지 계산후 ui로 표시해준다.


	IEnumerator BuildWorld() //만들어진 청크를 가져와서 합쳐서 월드를 만든다.
	{
		for(int z = 0; z < worldSize; z++) 
			for(int x = 0; x < worldSize; x++)
				for(int y = 0; y < columnHeight; y++)
				{
					Vector3 chunkPosition = new Vector3(x*chunkSize, y*chunkSize, z*chunkSize); //청크포지션을 설정하고(여기서는 4x4x4)
					Chunk c = new Chunk(chunkPosition, textureAtlas); // 청크를 만들어서
					c.chunk.transform.parent = this.transform; //포지션을 정하고
					chunks.Add(c.chunk.name, c); //딕셔너리를 만들고
					
				}

		foreach(KeyValuePair<string, Chunk> c in chunks) //청크s의 딕셔녀리값 각각을 
		{
			c.Value.DrawChunk(); //청크를 그린다.
			yield return null;
			
		}

		uIManager.TotalVoxelCal(); // 블럭 생성이 끝나면, UIManager의 cal()실행하여 블럭이 얼마나 생성되었는지 계산후 ui로 표시해준다.
	}

	// Use this for initialization
	void Start () {
		chunks = new Dictionary<string, Chunk>(); // 딕셔너리로 청크s를 설정하고 
		this.transform.position = Vector3.zero; // 0,0,0으로 최소 포지선을 this로 설정하고
		this.transform.rotation = Quaternion.identity; // 회전값도 초기화 한다.
		StartCoroutine(BuildWorld()); // 이건 반복하여 빌드월드를 하겠다는 것으로 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
