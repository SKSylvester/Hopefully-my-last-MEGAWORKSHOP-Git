using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MatrixObject : MonoBehaviour
{
    public GameObject Player;
    MyVector3 up = new MyVector3(0, 1, 0);
    Vector3[] ModelSpaceVertices;
    public float yawAngle;
    public float pitchAngle;
    public float rollAngle;
    public Vector3 position;



    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<GameObject>();

        //Mesh filter stores the information about the current mesh
        MeshFilter MF = GetComponent<MeshFilter>();

        //Get a copy pof all thje vertices (Not effeicent)
        ModelSpaceVertices = (MF.mesh.vertices);

    }

    // Update is called once per frame
    void Update()
    {
        // MyVector3 F = new MyVector3(transform.position - Player.transform.position);

        // MyVector3 R = MyVector3.CrossProduct(up, F);

        // MyVector3 U = MyVector3.CrossProduct(F, R);

        //To-DO: Use R and U to re-calcuate position of vertices or anything for that matter

        yawAngle += Time.deltaTime;
        pitchAngle += Time.deltaTime;
        rollAngle += Time.deltaTime;
       
        //Define a new array witht the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        //Create our scale matrix
        MyMatrix4x4 scaleMatrix = new MyMatrix4x4(new MyVector3(4, 0, 0), new MyVector3(0, 1, 0), new MyVector3(0, 0, 1), new MyVector3(0, 0, 0));

        //Create our rotation matrix
        MyMatrix4x4 rotationMatrix = new MyMatrix4x4(
           new MyVector3(Mathf.Cos(yawAngle), 0, -Mathf.Sin(yawAngle)),
           new MyVector3(0, 1, 0),
           new MyVector3(Mathf.Sin(yawAngle), 0, Mathf.Cos(yawAngle)),
           new MyVector3(0, 0, 0));

        MyMatrix4x4 pitchMatrix = new MyMatrix4x4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, Mathf.Cos(pitchAngle), -Mathf.Sin(pitchAngle)),
            new MyVector3(0, Mathf.Sin(pitchAngle), Mathf.Cos(pitchAngle)),
            new MyVector3(0, 0, 0));

        MyMatrix4x4 rollMatrix = new MyMatrix4x4(
            new MyVector3(Mathf.Cos(rollAngle), -Mathf.Sin(rollAngle), 0),
            new MyVector3(Mathf.Sin(rollAngle), Mathf.Cos(rollAngle), 0),
            new MyVector3(0, 0, 1),
            new MyVector3(0, 0, 0));

        MyMatrix4x4 translationMatrix = new MyMatrix4x4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, 1, 0),
            new MyVector3(0, 0, 1),
            new MyVector3(position.x, position.y, position.z));

        MyMatrix4x4 R = rotationMatrix * (pitchMatrix * rollMatrix);



        for (int i = 0; i < TransformedVertices.Length; i++)
        {

            TransformedVertices[i] = R * ModelSpaceVertices[i];

            //Forcing w axis to 1 because my vector3 only contains 3 vectors and was making the w axis 0.
            Vector4 v = ModelSpaceVertices[i];
            v.w = 1;
            TransformedVertices[i] = translationMatrix * v;
        }

            //Stores information about the current mesh
            MeshFilter MF = GetComponent<MeshFilter>();

        //Assign our new vertices
         MF.mesh.vertices = TransformedVertices;

        // to make the mesh look correct
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();
    }

}
