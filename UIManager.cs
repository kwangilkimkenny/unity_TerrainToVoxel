using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //public Text TerrainAmount;
    public Text AirAmount;
    public Text DirtAmount;

    //public int terrainAmountValue;
    public int airAmountValue;
    public int dirtAmountValue;

    public Chunk chunk;


    public void TotalVoxelCal()
    {

        //Chunk 데이터 생성이 모두 끝났으면 다음 코드를 실행하면 됨
        /*
        GameObject[] Dirts = GameObject.FindGameObjectsWithTag("TotalBlocks");
        terrainAmountValue = Dirts.Length;
        DirtAmount.text = terrainAmountValue.ToString();
        //Debug.Log("Generated dirts:" + DirtAmount.text); // 생성된 dirts의 갯수를 카운트한다.
        

        //airBlocks count and display UI
        GameObject[] AirBlocks = GameObject.FindGameObjectsWithTag("AirBlocks");
        airAmountValue = AirBlocks.Length;
        AirAmount.text = airAmountValue.ToString();

        //DirtBlocks count and display UI
        GameObject[] DirtBlocks = GameObject.FindGameObjectsWithTag("DirtBlocks");
        dirtAmountValue = DirtBlocks.Length;
        DirtAmount.text = dirtAmountValue.ToString();
        */

        //GameObject[] DirtBlocks = GameObject.FindGameObjectsWithTag("DirtBlocks");
        //dirtAmountValue = DirtBlocks.Length;
        //DirtAmount.text = chunk.CountDirts.ToString();


    }
}
