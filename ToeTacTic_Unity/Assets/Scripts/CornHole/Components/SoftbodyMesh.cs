using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftbodyMesh : MonoBehaviour
{
    public float flexibility = 1f;
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;
    private Mesh originalMesh, meshClone;
    private MeshRenderer meshRenderer;
    private SoftbodyVertex[] sbVertices;
    private Vector3[] vertexArray;

    // Start is called before the first frame update
    void Start()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = meshClone;
        meshRenderer = GetComponent<MeshRenderer>();
        sbVertices = new SoftbodyVertex[meshClone.vertices.Length];
        for (int i = 0; i < meshClone.vertices.Length; i++)
		{
            sbVertices[i] = new SoftbodyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;
        for (int i = 0; i < sbVertices.Length; i++)
		{
            Vector3 target = transform.TransformPoint(vertexArray[sbVertices[i].ID]);
            float intensity = (1 - (meshRenderer.bounds.max.y - target.y) / meshRenderer.bounds.size.y) * flexibility;
            sbVertices[i].Shake(target, mass, stiffness, damping);
            target = transform.InverseTransformPoint(sbVertices[i].position);
            vertexArray[sbVertices[i].ID] = Vector3.Lerp(vertexArray[sbVertices[i].ID], target, intensity);
        }
        meshClone.vertices = vertexArray;
    }

    public class SoftbodyVertex
	{
        public int ID;
        public Vector3 position;
        public Vector3 velocity, force;

        public SoftbodyVertex(int id, Vector3 pos)
		{
            this.ID = id;
            this.position = pos;
		}

        public void Shake(Vector3 target, float mass, float stiffness, float dampening)
		{
            force = (target - position) * stiffness;
            velocity = (velocity + force / mass) * dampening;
            position += velocity;
            if ((velocity + force + force / mass).magnitude < 0.001f)
			{
                position = target;
			}
		}
	}
}
