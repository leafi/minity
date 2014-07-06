using UnityEngine;
using System.Collections.Generic;

public class GoContainer {
  public GameObject gameObject;
  public bool xPlane;
  public bool yPlane;
  public bool zPlane;
  public float distance;

  public void UpdateDistance(Vector3 cameraPos) {
    if (xPlane) {
      distance = Mathf.Abs(cameraPos.x - gameObject.transform.position.x);
    } else if (yPlane) {
      distance = Mathf.Abs(cameraPos.y - gameObject.transform.position.y);
    } else if (zPlane) {
      distance = Mathf.Abs(cameraPos.z - gameObject.transform.position.z);
    }
  }
}

public class VoxelWorld : MonoBehaviour {

  List<GoContainer> planes = new List<GoContainer>();

  void createNewGo(string name, Vector3[] realVertices, Vector2[] meshUv, int[] meshTriangles, Color32[] colors, Vector3 pos) {

    GameObject go = new GameObject(name);
    go.AddComponent<MeshFilter>();
    go.AddComponent<MeshRenderer>();
    Mesh mesh = go.GetComponent<MeshFilter>().mesh;
    mesh.Clear();
    
    mesh.vertices = realVertices;
    mesh.uv = meshUv;
    mesh.triangles = meshTriangles;
    mesh.colors32 = colors;

    mesh.RecalculateBounds();
    mesh.RecalculateNormals();

    go.GetComponent<MeshFilter>().mesh = mesh;

    /*go.renderer.material = new Material(renderer.material.shader);
    go.renderer.material.SetTexture("_TextureAtlas", renderer.material.GetTexture("_TextureAtlas"));
    go.renderer.material.SetTexture("_Voxels", renderer.material.GetTexture("_Voxels"));*/
    go.renderer.material = renderer.material;

    go.transform.position = pos;
    go.transform.localScale = new Vector3(256f, 256f, 256f);

    var goc = new GoContainer();
    goc.xPlane = name.StartsWith("x");
    goc.yPlane = name.StartsWith("y");
    goc.zPlane = name.StartsWith("z");
    goc.gameObject = go;

    planes.Add(goc);
  }



	// Use this for initialization
	void Start () {
    //gameObject.AddComponent("MeshFilter");
    //gameObject.AddComponent("MeshRenderer");
    /*mesh.Clear();
    mesh.vertices = new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
    mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
    var g = new Color32(0, 255, 0, 255);
    mesh.colors32 = new Color32[] { new Color32(255, 0, 0, 255), g, new Color32(0, 0, 255, 255) };
    mesh.triangles = new int[] {0, 1, 2};*/

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

    // this renderer's material is copied to new objects
    renderer.material.SetTexture("_Voxels", voxels);






    const bool backface = true;
    //const float the_diff = -0.99f / 256.0f;
    const float the_diff = 0f;

    List<Vector3> xRealVertices = new List<Vector3>();
    xRealVertices.Add(new Vector3(0, -0.5f, 0.5f));
    xRealVertices.Add(new Vector3(0, 0.5f, 0.5f));
    xRealVertices.Add(new Vector3(0, 0.5f, -0.5f));
    xRealVertices.Add(new Vector3(0, -0.5f, -0.5f));

    // If we have backfaces then we must add a new set of backface verts, because they need different
    //  colours so we can shove them back into the block rather than showing the next block over.

    if (backface) {
      xRealVertices.Add(new Vector3(0, -0.5f, 0.5f));
      xRealVertices.Add(new Vector3(0, 0.5f, 0.5f));
      xRealVertices.Add(new Vector3(0, 0.5f, -0.5f));
      xRealVertices.Add(new Vector3(0, -0.5f, -0.5f));
    }
    List<Vector2> meshUv = new List<Vector2>();
    meshUv.Add(new Vector2(0.0f, 0.0f));
    meshUv.Add(new Vector2(0.0f, 1.0f));
    meshUv.Add(new Vector2(1.0f, 1.0f));
    meshUv.Add(new Vector2(1.0f, 0.0f));
    if (backface) {
      meshUv.Add(new Vector2(0.0f, 0.0f));
      meshUv.Add(new Vector2(0.0f, 1.0f));
      meshUv.Add(new Vector2(1.0f, 1.0f));
      meshUv.Add(new Vector2(1.0f, 0.0f));
    }
    List<int> meshTriangles = new List<int>();
    int bf = backface ? 4 : 0;
    meshTriangles.AddRange(new int[] { 2, 1, 0, 0, 3, 2, 0 + bf, 1 + bf, 2 + bf, 2 + bf, 3 + bf, 0 + bf });
    List<int> reverseMeshTriangles = new List<int>();
    reverseMeshTriangles.AddRange(new int[] { 0, 1, 2, 2, 3, 0, 2 + bf, 1 + bf, 0 + bf, 0 + bf, 3 + bf, 2 + bf });

    // X planes
    for (int x = 0; x < 256; x++) {
      float ecks = -0.5f + (x / 256.0f);

      var colors = new List<Color32>();
      for (int i = 0; i < xRealVertices.Count; i++) {
        var v = xRealVertices[i];
        var vv = new Vector3(v.x, v.y, v.z);
        float backness = 0f;

        if (backface && (i % 8 > 3)) {
          backness = 1.0f / 256.0f;
        }

        // each vertex coord must be in range [-0.5,0.5]
        colors.Add(new Color32((byte)(255 * (ecks + backness + 0.5)),
          (byte)(255 * (vv.y + backness + 0.5)),
          (byte)(255 * (vv.z + backness + 0.5)),
          (byte)254));
      }

      createNewGo("x" + x.ToString(), xRealVertices.ToArray(), meshUv.ToArray(), meshTriangles.ToArray(),
                  colors.ToArray(), new Vector3(ecks * 256f, 0f, 0f));
    }

    List<Vector3> yRealVertices = new List<Vector3>();
    yRealVertices.Add(new Vector3(-0.5f, 0f, -0.5f));
    yRealVertices.Add(new Vector3(-0.5f, 0f, 0.5f));
    yRealVertices.Add(new Vector3(0.5f, 0f, 0.5f));
    yRealVertices.Add(new Vector3(0.5f, 0f, -0.5f));
    if (backface) {
      yRealVertices.Add(new Vector3(-0.5f, 0f, -0.5f));
      yRealVertices.Add(new Vector3(-0.5f, 0f, 0.5f));
      yRealVertices.Add(new Vector3(0.5f, 0f, 0.5f));
      yRealVertices.Add(new Vector3(0.5f, 0f, -0.5f));
    }

    // Y planes
    for (int y = 0; y < 256; y++) {
      float why = -0.5f + (y / 256.0f);

      var colors = new List<Color32>();
      for (int i = 0; i < yRealVertices.Count; i++) {
        var v = yRealVertices[i];
        var vv = new Vector3(v.x, v.y, v.z);
        float backness = 0f;

        if (backface && (i % 8 > 3)) {
          backness = 1.0f / 256.0f;
        }

        // each vertex coord must be in range [-0.5,0.5]
        colors.Add(new Color32((byte)(255 * (vv.x + backness + 0.5)),
          (byte)(255 * (why + backness + 0.5)),
          (byte)(255 * (vv.z + backness + 0.5)),
          (byte)254));
      }

      createNewGo("y" + y.ToString(), yRealVertices.ToArray(), meshUv.ToArray(), reverseMeshTriangles.ToArray(),
        colors.ToArray(), new Vector3(0f, why * 256f, 0f));
    }

    List<Vector3> zRealVertices = new List<Vector3>();
    zRealVertices.Add(new Vector3(-0.5f, -0.5f, 0f));
    zRealVertices.Add(new Vector3(-0.5f, 0.5f, 0f));
    zRealVertices.Add(new Vector3(0.5f, 0.5f, 0f));
    zRealVertices.Add(new Vector3(0.5f, -0.5f, 0f));
    if (backface) {
      zRealVertices.Add(new Vector3(-0.5f, -0.5f, 0f));
      zRealVertices.Add(new Vector3(-0.5f, 0.5f, 0f));
      zRealVertices.Add(new Vector3(0.5f, 0.5f, 0f));
      zRealVertices.Add(new Vector3(0.5f, -0.5f, 0f));
    }

    // Z planes
    for (int z = 0; z < 256; z++) {
      float zee = -0.5f + (z / 256.0f);

      var colors = new List<Color32>();
      for (int i = 0; i < zRealVertices.Count; i++) {
        var v = zRealVertices[i];
        var vv = new Vector3(v.x, v.y, v.z);
        float backness = 0f;

        if (backface && (i % 8 > 3)) {
          backness = 1.0f / 256.0f;
        }

        // each vertex coord must be in range [-0.5,0.5]
        colors.Add(new Color32((byte)(255 * (vv.x + backness + 0.5)),
          (byte)(255 * (vv.y + backness + 0.5)),
          (byte)(255 * (zee + backness + 0.5)),
          (byte)254));
      }

      createNewGo("z" + z.ToString(), zRealVertices.ToArray(), meshUv.ToArray(), meshTriangles.ToArray(),
        colors.ToArray(), new Vector3(0f, 0f, zee * 256f));
    }

	}
	
	// Update is called once per frame
	void Update () {
    //renderer.transform.Rotate(0f, 20f * Time.deltaTime, 0f);

    /*for (int i = 0; i < planes.Count; i++) {
      planes[i].UpdateDistance(Camera.main.transform.position);
    }

    planes.Sort((firstGo, secondGo) => {
      return firstGo.distance.CompareTo(secondGo.distance);
    });

    for (int i = 0; i < planes.Count; i++) {
      planes[i].gameObject.renderer.material.renderQueue = 1000 + i;
    }*/
	}
}
