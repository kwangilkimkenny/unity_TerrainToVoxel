using System.IO;
using UnityEditor;
using UnityEngine;
public static class HeightmapFromTexture
{
    [MenuItem("Terrain/Heightmap From Texture")]

    public static void ApplyHeightmap()
    //public static void ApplyHeightmap(float x, float z) /// 이렇게 바꿨고. loat x, float z 이 두 값을 적용해야 한다.
    {
        //string heightmapPath = EditorUtility.OpenFilePanel("Texture", GetFolderPath(SpecialFolder.Desktop), ".png");


        Texture2D heightmap = Selection.activeObject as Texture2D; //  빈 텍스쳐를 생성하여 활성화 한다.

        if (heightmap == null)
        {
            EditorUtility.DisplayDialog("No texture selected", "Please select a texture.", "Cancel");
            return;
        }
        //가져온  2D 이미지 넓이, 높이
        int w = heightmap.width;
        int h = heightmap.height;

        //적용하기 위한 테레인 데이터
        var terrain = Terrain.activeTerrain.terrainData;
        int w2 = terrain.heightmapWidth; // 넓이
        Debug.Log(w2);
  
        float[,] heightmapData = terrain.GetHeights(0, 0, w2, w2); // index x, y의 첫번째 높이맵 샘플의 배열을 얻어온다.
        Debug.Log(heightmapData);

        Color[] mapColors = heightmap.GetPixels(); // pixel의 값을 추출하여 컬러 리스트에 담는다.
        Debug.Log(mapColors);

        Color[] map = new Color[w2 * w2]; //
        Debug.Log(map);

        if (w2 != w || h != w) // 이미지와 터레인의 크기가 일치하지 않으면 보정을 한다.
        {
            // Resize using nearest-neighbor scaling if texture has no filtering
            if (heightmap.filterMode == FilterMode.Point)// 필터모드가 heightmap과 point의 그것과 같으면
            {
                float dx = (float)w / (float)w2;  // 터레인의 넓이값으로 가져온 이미지의 넓이값을 나눠서 dx를 만든다.
                float dy = (float)h / (float)w2;

                for (int y = 0; y < w2; y++)
                {
                    if (y % 20 == 0) // 20으로 나눈 나머지값이 0이면 
                    {
                        EditorUtility.DisplayProgressBar("Resize", "Calculating texture", Mathf.InverseLerp(0.0f, w2, y)); //  터레인의 크기와 이미지 사이의 값으로 보정한다.( 보간)
                    }
                    int thisY = Mathf.FloorToInt(dy * y) * w;// y값을 계산해서 thisY 로하고
                    int yw = y * w2;

                    for (int x = 0; x < w2; x++)
                    {
                        map[yw + x] = mapColors[Mathf.FloorToInt(thisY + dx * x)];

                         
                    }
                }
            }
            // Otherwise resize using bilinear filtering 일치하면 bilinear로 필터링을 한다.
            else
            {
                float ratioX = (1.0f / ((float)w2 / (w - 1)));
                float ratioY = (1.0f / ((float)w2 / (h - 1)));
                for (int y = 0; y < w2; y++)
                {
                    if (y % 20 == 0)
                    {
                        EditorUtility.DisplayProgressBar("Resize", "Calculating texture", Mathf.InverseLerp(0.0f, w2, y));
                    }
                    int yy = Mathf.FloorToInt(y * ratioY);
                    int y1 = yy * w;
                    int y2 = (yy + 1) * w;
                    int yw = y * w2;
                    for (int x = 0; x < w2; x++)
                    {
                        int xx = Mathf.FloorToInt(x * ratioX);
                        Color bl = mapColors[y1 + xx];
                        Color br = mapColors[y1 + xx + 1];
                        Color tl = mapColors[y2 + xx];
                        Color tr = mapColors[y2 + xx + 1];
                        float xLerp = x * ratioX - xx;
                        map[yw + x] = Color.Lerp(Color.Lerp(bl, br, xLerp), Color.Lerp(tl, tr, xLerp), y * ratioY - (float)yy);
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }
        else // 정확하게 터레인과 이미지가 일치하면 그냥 컬러럴 추출해서 맵에 담는다.
        {
            // Use original if no resize is needed
            map = mapColors;
        }


        // Assign texture data to heightmap 그리고나서 텍스춰데이터를 하이트맵에 넣는다.
        for (int y = 0; y < w2; y++)
        {
            for (int x = 0; x < w2; x++)
            {
                heightmapData[y, x] = map[y * w2 + x].grayscale;
            }
        }
        terrain.SetHeights(0, 0, heightmapData); // 인덱스의 x(0), y(0)의 heightmapData의 배열을 설정한다.(만든다?)

        Debug.Log("heightmapdata : " + heightmapData);
    }
}