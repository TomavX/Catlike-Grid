using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {
        [SerializeField]
        private int xSize, ySize;
//        [SerializeField]
//        private int ySize;

    private Vector3[] vertices;
    private Mesh mesh;

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(2.25f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i++] = tangent;
//                yield return wait;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;
        int[] triangles = new int[xSize * ySize * 6];
        for(int iy = 0, i = 0; iy < ySize; iy++)
            for(int ix = 0; ix < xSize; ix++)
            {
                triangles[i++] = iy * (xSize + 1) + ix + 1;
                triangles[i++] = iy * (xSize + 1) + ix;
                triangles[i++] = (iy + 1) * (xSize + 1) + ix;
                triangles[i++] = iy * (xSize + 1) + ix + 1;
                triangles[i++] = (iy + 1) * (xSize + 1) + ix;
                triangles[i++] = (iy + 1) * (xSize + 1) + 1 + ix;
//                mesh.triangles = triangles;
//                yield return wait;
            }
        mesh.triangles = triangles;
        yield return wait;
        mesh.RecalculateNormals();
    }
    
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private void OnDrawGizmos()
    {
//        if (vertices == null)
            return;
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
