using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public GameObject fogPlane;
    public Transform player;
    public LayerMask fogLayer;
    public float maskRadius = 5f;
    public Vector3 offset = new Vector3(-6f, 5, -2f);


    private Mesh maskMesh;
    private Vector3[] m_vertices;
    private Color[] m_colors;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position + offset;
        Ray r = new Ray(transform.position, player.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < m_vertices.Length; i++)
            {
                Vector3 v = fogPlane.transform.TransformPoint(m_vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < maskRadius* maskRadius)
                {
                    float alpha = Mathf.Min(m_colors[i].a, dist / (maskRadius* maskRadius));
                    m_colors[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        maskMesh = fogPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = maskMesh.vertices;
        m_colors = new Color[m_vertices.Length];
        for (int i = 0; i < m_colors.Length; i++)
        {
            m_colors[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        maskMesh.colors = m_colors;
    }
}
