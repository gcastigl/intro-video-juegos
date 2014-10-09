using UnityEngine;
using System.Collections;

using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;

/**
 * This is a hack to rotate unity terrains. 
 * 
 * It export the terrain to obj at runtime and then places it inside the scene
 * 
 * This class is PURE BLACK MAGIC. Go on if you dare...
 */
using UnityEditor;

enum SaveFormat { Triangles, Quads }
enum SaveResolution { Full=0, Half, Quarter, Eighth, Sixteenth }

public class TerrainToObj {

	private SaveFormat saveFormat = SaveFormat.Triangles;
	private SaveResolution saveResolution = SaveResolution.Half;

	public GameObject terrainGO;

	private TerrainData terrain;
	private Vector3 terrainPos;
	private int tCount;
	private int counter;

	public Mesh execute() {
		terrainPos = terrainGO.transform.position;
		Terrain TerrainComponent = terrainGO.GetComponent<Terrain> ();
		terrain = TerrainComponent.terrainData;
		Export ();
		ObjImporter impoter = new ObjImporter ();
		return impoter.ImportFile ("Assets/resources/exteported-terrain.obj");
		// objReaderCSharpV4 loader;
		// loader.LoadFile("Assets/resources/exteported-terrain.obj");
	}

	void Export()
	{
		string fileName = "Assets/resources/exteported-terrain.obj";
		int w = terrain.heightmapWidth;
		int h = terrain.heightmapHeight;
		Vector3 meshScale = terrain.size;
		int tRes = (int) Mathf.Pow(2, (int) saveResolution);
		meshScale = new Vector3(meshScale.x / (w - 1) * tRes, meshScale.y, meshScale.z / (h - 1) * tRes);
		Vector2 uvScale = new Vector2(1.0f / (w - 1), 1.0f / (h - 1));
		float[,] tData = terrain.GetHeights(0, 0, w, h);
		
		w = (w - 1) / tRes + 1;
		h = (h - 1) / tRes + 1;
		Vector3[] tVertices = new Vector3[w * h];
		Vector2[] tUV = new Vector2[w * h];
		
		int[] tPolys;
		
		if (saveFormat == SaveFormat.Triangles)
		{
			tPolys = new int[(w - 1) * (h - 1) * 6];
		}
		else
		{
			tPolys = new int[(w - 1) * (h - 1) * 4];
		}
		
		// Build vertices and UVs
		for (int y = 0; y < h; y++)
		{
			for (int x = 0; x < w; x++)
			{
				tVertices[y * w + x] = Vector3.Scale(meshScale, new Vector3(-y, tData[x * tRes, y * tRes], x)) + terrainPos;
				tUV[y * w + x] = Vector2.Scale( new Vector2(x * tRes, y * tRes), uvScale);
			}
		}
		
		int  index = 0;
		if (saveFormat == SaveFormat.Triangles)
		{
			// Build triangle indices: 3 indices into vertex array for each triangle
			for (int y = 0; y < h - 1; y++)
			{
				for (int x = 0; x < w - 1; x++)
				{
					// For each grid cell output two triangles
					tPolys[index++] = (y * w) + x;
					tPolys[index++] = ((y + 1) * w) + x;
					tPolys[index++] = (y * w) + x + 1;
					
					tPolys[index++] = ((y + 1) * w) + x;
					tPolys[index++] = ((y + 1) * w) + x + 1;
					tPolys[index++] = (y * w) + x + 1;
				}
			}
		}
		else
		{
			// Build quad indices: 4 indices into vertex array for each quad
			for (int y = 0; y < h - 1; y++)
			{
				for (int x = 0; x < w - 1; x++)
				{
					// For each grid cell output one quad
					tPolys[index++] = (y * w) + x;
					tPolys[index++] = ((y + 1) * w) + x;
					tPolys[index++] = ((y + 1) * w) + x + 1;
					tPolys[index++] = (y * w) + x + 1;
				}
			}
		}
		
		// Export to .obj
		StreamWriter sw = new StreamWriter(fileName);
		try
		{
			
			sw.WriteLine("# Unity terrain OBJ File");
			
			// Write vertices
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			counter = tCount = 0;
			for (int i = 0; i < tVertices.Length; i++)
			{
				StringBuilder sb = new StringBuilder("v ", 20);
				// StringBuilder stuff is done this way because it's faster than using the "{0} {1} {2}"etc. format
				// Which is important when you're exporting huge terrains.
				sb.Append(tVertices[i].x.ToString()).Append(" ").
					Append(tVertices[i].y.ToString()).Append(" ").
						Append(tVertices[i].z.ToString());
				sw.WriteLine(sb);
			}
			// Write UVs
			for (int i = 0; i < tUV.Length; i++)
			{
				StringBuilder sb = new StringBuilder("vt ", 22);
				sb.Append(tUV[i].x.ToString()).Append(" ").
					Append(tUV[i].y.ToString());
				sw.WriteLine(sb);
			}
			if (saveFormat == SaveFormat.Triangles)
			{
				// Write triangles
				for (int i = 0; i < tPolys.Length; i += 3)
				{
					StringBuilder sb = new StringBuilder("f ", 43);
					sb.Append(tPolys[i] + 1).Append("/").Append(tPolys[i] + 1).Append(" ").
						Append(tPolys[i + 1] + 1).Append("/").Append(tPolys[i + 1] + 1).Append(" ").
							Append(tPolys[i + 2] + 1).Append("/").Append(tPolys[i + 2] + 1);
					sw.WriteLine(sb);
				}
			}
			else
			{
				// Write quads
				for (int i = 0; i < tPolys.Length; i += 4)
				{
					StringBuilder sb = new StringBuilder("f ", 57);
					sb.Append(tPolys[i] + 1).Append("/").Append(tPolys[i] + 1).Append(" ").
						Append(tPolys[i + 1] + 1).Append("/").Append(tPolys[i + 1] + 1).Append(" ").
							Append(tPolys[i + 2] + 1).Append("/").Append(tPolys[i + 2] + 1).Append(" ").
							Append(tPolys[i + 3] + 1).Append("/").Append(tPolys[i + 3] + 1);
					sw.WriteLine(sb);
				}
			}
		}
		catch(Exception err)
		{
			Debug.Log("Error saving file: " + err.Message);
		}
		sw.Close();
	}
}