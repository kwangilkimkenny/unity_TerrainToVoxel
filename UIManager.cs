using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text DirtAmount;
    public int terrainAmountValue;


    public void TotalVoxelCal()
    {

        //Chunk 데이터 생성이 모두 끝났으면 다음 코드를 실행하면 됨

        GameObject[] Dirts = GameObject.FindGameObjectsWithTag("DirtCell");
        //var Dirt = GameObject.FindGameObjectsWithTag("DirtCell");
        terrainAmountValue = Dirts.Length;

        DirtAmount.text = terrainAmountValue.ToString();

        Debug.Log("Generated dirts:" + DirtAmount.text); // 생성된 dirts의 갯수를 카운트한다.


    }
}
