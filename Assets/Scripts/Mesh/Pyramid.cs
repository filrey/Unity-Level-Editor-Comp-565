using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Pyramid : MonoBehaviour {

    public bool sharedVertices = false;

    Vector3[] vertices;

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if(vertices!=null)
        {
            float size = 0.1f;
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(vertices[i], size);
                Gizmos.color = Color.white;
                Handles.Label(new Vector3(vertices[i].x,vertices[i].y,vertices[i].z), i.ToString());
            }
        }
#endif
    }

    public void Rebuild()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter not found!");
            return;
        }

        Vector3 p0 = new Vector3(0, 0, 0);
        Vector3 p1 = new Vector3(0, 0, 1);
        Vector3 p2 = new Vector3(1, 0, 1);
        Vector3 p3 = new Vector3(1, 0, 0);
        Vector3 p4 = new Vector3(0.5f, 0.5f, 0.5f);

        vertices = new Vector3[5];
        vertices[0] = p0;
        vertices[1] = p1;
        vertices[2] = p2;
        vertices[3] = p3;
        vertices[4] = p4;

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            meshFilter.mesh = new Mesh();
            mesh = meshFilter.sharedMesh;
        }
        mesh.Clear();
        if (sharedVertices)
        {
            //mesh.vertices = vertices; //new Vector3[] { p0, p1, p2, p3, p4 }; //, p5 };
            //mesh.triangles = new int[]{
            //    0,1,2,
            //    0,2,3,
            //    2,1,3,
            //    0,3,1
            //    //0,1,2,
            //    //3,2,1
            //};

            //// basically just assigns a corner of the texture to each vertex
            //mesh.uv = new Vector2[]{
            //    new Vector2(0,0),
            //    new Vector2(1,0),
            //    new Vector2(0,1),
            //    new Vector2(1,1),
            //};
        }
        else
        {
            mesh.vertices = new Vector3[]{
                p0,p1,p2,       // base 1
                p3,p1,p2//,       // base 2 
                //p0,p1,p4       // side-1
                //p2,p3,p4,
                //p3,p2,p4,
                //p2,p0,p4
            };
            mesh.triangles = new int[]{
                0,2,1,          // facing up
                1,3,2           
                //1,2,3
                //2,1,0,          // facing down
                //3,2,0           // facing down
                //12,13,14,
                //15,16,17
            };

            Vector2 uv0 = new Vector2(0, 0);
            Vector2 uv1 = new Vector2(1, 0);
            Vector2 uv2 = new Vector2(0.5f, 1);

            mesh.uv = new Vector2[]{
                uv0,uv1,uv2,
                uv0,uv1,uv2//,
                //uv0,uv1,uv2//,
                //uv0,uv1,uv2,
                //uv0,uv1,uv2,
                //uv0,uv1,uv2
            };

        }

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        //mesh.Optimize();
    }

    // Use this for initialization
    void Start()
    {
        Rebuild();

        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices = mesh.vertices;

        //foreach (var v in vertices)
        //{
        //    Debug.Log(string.Format("{0},{1},{2}", v.x, v.y, v.z));
        //}

        //// create new colors array where the colors will be created.
        //Color[] colors = new Color[vertices.Length];

        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    //colors[i] = Color.Lerp(Color.red, Color.blue, vertices[i].y);
        //    colors[i] = Color.Lerp(new Color(vertices[i].x, vertices[i].y, vertices[i].z), new Color(vertices[i].x, vertices[i].y, vertices[i].z), vertices[i].z);
        //}
        //// assign the array of colors to the Mesh.
        //mesh.colors = colors;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
