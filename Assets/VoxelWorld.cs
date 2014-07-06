using UnityEngine;
using System.Collections.Generic;

public class VoxelWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
    //gameObject.AddComponent("MeshFilter");
    //gameObject.AddComponent("MeshRenderer");
    Mesh mesh = GetComponent<MeshFilter>().mesh;
    /*mesh.Clear();
    mesh.vertices = new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
    mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
    var g = new Color32(0, 255, 0, 255);
    mesh.colors32 = new Color32[] { new Color32(255, 0, 0, 255), g, new Color32(0, 0, 255, 255) };
    mesh.triangles = new int[] {0, 1, 2};*/

    mesh.Clear();
    List<Vector3> meshVertices = new List<Vector3>();
    List<Vector2> meshUv = new List<Vector2>();
    List<int> meshTriangles = new List<int>();

    const bool backface = true;
    //const float the_diff = -0.99f / 256.0f;
    const float the_diff = 0f;

    // X planes
    for (int x = 0; x < 256; x++) {
      int v_i = meshVertices.Count;
      float ecks = -0.5f + (x / 256.0f);
      meshVertices.Add(new Vector3(ecks, -0.5f, 0.5f));
      meshUv.Add(new Vector2(0.0f, 0.0f));
      meshVertices.Add(new Vector3(ecks, 0.5f, 0.5f));
      meshUv.Add(new Vector2(0.0f, 1.0f));
      meshVertices.Add(new Vector3(ecks, 0.5f, -0.5f));
      meshUv.Add(new Vector2(1.0f, 1.0f));
      meshVertices.Add(new Vector3(ecks, -0.5f, -0.5f));
      meshUv.Add(new Vector2(1.0f, 0.0f));

      meshTriangles.AddRange(new int[] { v_i + 2, v_i + 1, v_i, v_i, v_i + 3, v_i + 2 });


      if (backface) {
        v_i += 4;
        ecks -= the_diff;
        meshVertices.Add(new Vector3(ecks, -0.5f, 0.5f));
        meshUv.Add(new Vector2(0.0f, 0.0f));
        meshVertices.Add(new Vector3(ecks, 0.5f, 0.5f));
        meshUv.Add(new Vector2(0.0f, 1.0f));
        meshVertices.Add(new Vector3(ecks, 0.5f, -0.5f));
        meshUv.Add(new Vector2(1.0f, 1.0f));
        meshVertices.Add(new Vector3(ecks, -0.5f, -0.5f));
        meshUv.Add(new Vector2(1.0f, 0.0f));
        meshTriangles.AddRange(new int[] { v_i, v_i + 1, v_i + 2, v_i + 2, v_i + 3, v_i });

        // inside
        //meshTriangles.AddRange(new int[] { v_i + 2, v_i + 1, v_i, v_i, v_i + 3, v_i + 2 });
      }
    }

    // Y planes
    for (int y = 0; y < 256; y++) {
      int v_i = meshVertices.Count;
      float ecks = -0.5f + (y / 256.0f);
      meshVertices.Add(new Vector3(-0.5f, ecks, -0.5f));
      meshUv.Add(new Vector2(0.0f, 0.0f));
      meshVertices.Add(new Vector3(-0.5f, ecks, 0.5f));
      meshUv.Add(new Vector2(0.0f, 1.0f));
      meshVertices.Add(new Vector3(0.5f, ecks, 0.5f));
      meshUv.Add(new Vector2(1.0f, 1.0f));
      meshVertices.Add(new Vector3(0.5f, ecks, -0.5f));
      meshUv.Add(new Vector2(1.0f, 0.0f));

      meshTriangles.AddRange(new int[] { v_i, v_i + 1, v_i + 2, v_i + 2, v_i + 3, v_i });

      if (backface) {
        v_i += 4;
        ecks -= the_diff;
        meshVertices.Add(new Vector3(-0.5f, ecks, -0.5f));
        meshUv.Add(new Vector2(0.0f, 0.0f));
        meshVertices.Add(new Vector3(-0.5f, ecks, 0.5f));
        meshUv.Add(new Vector2(0.0f, 1.0f));
        meshVertices.Add(new Vector3(0.5f, ecks, 0.5f));
        meshUv.Add(new Vector2(1.0f, 1.0f));
        meshVertices.Add(new Vector3(0.5f, ecks, -0.5f));
        meshUv.Add(new Vector2(1.0f, 0.0f));
        //meshTriangles.AddRange(new int[] { v_i, v_i + 1, v_i + 2, v_i + 2, v_i + 3, v_i });
        
        // inside
        meshTriangles.AddRange(new int[] { v_i + 2, v_i + 1, v_i, v_i, v_i + 3, v_i + 2 });
      }
    }

    // Z planes
    for (int y = 0; y < 256; y++) {
      int v_i = meshVertices.Count;
      float ecks = -0.5f + (y / 256.0f);
      meshVertices.Add(new Vector3(-0.5f, -0.5f, ecks));
      meshUv.Add(new Vector2(0.0f, 0.0f));
      meshVertices.Add(new Vector3(-0.5f, 0.5f, ecks));
      meshUv.Add(new Vector2(0.0f, 1.0f));
      meshVertices.Add(new Vector3(0.5f, 0.5f, ecks));
      meshUv.Add(new Vector2(1.0f, 1.0f));
      meshVertices.Add(new Vector3(0.5f, -0.5f, ecks));
      meshUv.Add(new Vector2(1.0f, 0.0f));

      meshTriangles.AddRange(new int[] { v_i + 2, v_i + 1, v_i, v_i, v_i + 3, v_i + 2 });

      if (backface) {
        v_i += 4;
        ecks -= the_diff;
        meshVertices.Add(new Vector3(-0.5f, -0.5f, ecks));
        meshUv.Add(new Vector2(0.0f, 0.0f));
        meshVertices.Add(new Vector3(-0.5f, 0.5f, ecks));
        meshUv.Add(new Vector2(0.0f, 1.0f));
        meshVertices.Add(new Vector3(0.5f, 0.5f, ecks));
        meshUv.Add(new Vector2(1.0f, 1.0f));
        meshVertices.Add(new Vector3(0.5f, -0.5f, ecks));
        meshUv.Add(new Vector2(1.0f, 0.0f));
        meshTriangles.AddRange(new int[] { v_i, v_i + 1, v_i + 2, v_i + 2, v_i + 3, v_i });
        
        // inside
        //meshTriangles.AddRange(new int[] { v_i + 2, v_i + 1, v_i, v_i, v_i + 3, v_i + 2 });
      }
    }

    mesh.vertices = meshVertices.ToArray();
    mesh.uv = meshUv.ToArray();
    mesh.triangles = meshTriangles.ToArray();




    var colors = new List<Color32>();
    for (int i = 0; i < mesh.vertices.Length; i++) {
      var v = mesh.vertices[i];
      var vv = new Vector3(v.x, v.y, v.z);

      if (backface && (i % 8 > 3)) {
        vv.x += 1.0f / 256.0f;
        vv.y += 1.0f / 256.0f;
        vv.z += 1.0f / 256.0f;
      }

      // each vertex coord must be in range [-0.5,0.5]
      colors.Add(new Color32((byte)(255 * (vv.x + 0.5)),
        (byte)(255 * (vv.y + 0.5)),
        (byte)(255 * (vv.z + 0.5)),
        (byte)254));
    }
    mesh.colors32 = colors.ToArray();

    mesh.RecalculateBounds();
    mesh.RecalculateNormals();


    //const int VOXEL_TEX_LENGTH = 2048;
    const int WORLD_LENGTH = 256;


    // test voxel texture
    /* TextureFormat.Alpha8 ? */
    /* or one of the numerous 16-bit formats... */
    //#if (!UNITY_PRO_LICENSE)
    //throw new Exception("3D textures don't work in free Unity. You need to 
    var voxels = new Texture3D(WORLD_LENGTH, WORLD_LENGTH, WORLD_LENGTH, TextureFormat.RGBA32, false);
    voxels.filterMode = FilterMode.Point;
    var vox = new Color[WORLD_LENGTH * WORLD_LENGTH * WORLD_LENGTH];
    /*for (int i = 0; i < vox.Length; i++) {
      //vox[i] = new Color32(15 * 16 + 1, 15 * 16 + 0, 14 * 16, 14 * 16 + 1);
      vox[i] = new Color32(1, 1, 1, 1);
    }*/

    for (int z = 0; z < WORLD_LENGTH; z++) {
      for (int y = 0; y < WORLD_LENGTH; y++) {
        for (int x = 0; x < WORLD_LENGTH; x++) {
          vox[z * WORLD_LENGTH * WORLD_LENGTH + (y * WORLD_LENGTH) + x] = (
             Random.value > 0.9f ? new Color(y / 256.0f, y / 256.0f, y / 256.0f, y / 256.0f) : new Color(0f, 0f, 0f, 0f));
        }
      }
    }
    voxels.SetPixels(vox);
    voxels.Apply();

    renderer.material.SetTexture("_Voxels", voxels);


    //GetComponent<MeshRenderer>().material.SetTexture("UEOAOU", lookupTex);

	}
	
	// Update is called once per frame
	void Update () {
    //renderer.transform.Rotate(0f, 20f * Time.deltaTime, 0f);
	}
}
