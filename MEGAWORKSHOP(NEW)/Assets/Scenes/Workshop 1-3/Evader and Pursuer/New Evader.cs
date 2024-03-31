using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEvader : MonoBehaviour
{

    public MyVector3 Rotation;
    MyVector3 mouseRotation = new MyVector3(0, 0, 0);
    public MyVector3 forwardDirection = new MyVector3(0, 0, 0);
    public MyVector3 rightDirection = new MyVector3(0, 0, 0);
    MyVector3 lastMousePosition = new MyVector3(0, 0, 0);
    MyVector3 MouseDelta = new MyVector3(0, 0, 0);
    public MyVector3 eulerRotation;
    public MyVector3 eulerAngle;
    MyVector3 up = new MyVector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {

    }
    public MyVector3 CalculateEuler()
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        Rotation = new MyVector3(transform.eulerAngles);


        rv = Rotation / (180f / Mathf.PI);


        return rv;
    }

    // Update is called once per frame
    void Update()
    {

        MouseDelta = new MyVector3(Input.mousePosition) - lastMousePosition;
        lastMousePosition = new MyVector3(Input.mousePosition);
        float MouseRotation = MathsLib.VectorToRadians(MouseDelta);

        Debug.Log(transform.eulerAngles);
        Debug.Log(MouseDelta);
        Debug.Log(eulerRotation);


        eulerRotation = CalculateEuler();


        eulerAngle = Rotation + MouseDelta;

        transform.eulerAngles = eulerAngle.ToUnityVector();

        forwardDirection = MyVector3.EulerAnglesToDirection(eulerRotation);
        rightDirection = MyVector3.CrossProduct(forwardDirection, up);

        // Calculate mouse delta
        MouseDelta = new MyVector3(Input.mousePosition) - lastMousePosition;
        lastMousePosition = new MyVector3(Input.mousePosition);

        // Convert mouse delta to rotation
        mouseRotation = new MyVector3(-MouseDelta.y, MouseDelta.x, 0);

        // Apply rotation to the object's Euler angles
        transform.eulerAngles += mouseRotation.ToUnityVector();

        // Correct Euler angles to stay within range
        MyVector3 currentEulerAngles = eulerAngle;
        currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 0, 360f);
        //currentEulerAngles.z = Mathf.Clamp(currentEulerAngles.z, -90f, 90f);
        //currentEulerAngles.y = Mathf.Clamp(currentEulerAngles.y, -90f, 90f);
        transform.eulerAngles = currentEulerAngles.ToUnityVector();

        // Calculate forward and right directions
        forwardDirection = MyVector3.EulerAnglesToDirection(CalculateEuler());

        rightDirection = MyVector3.CrossProduct(up, forwardDirection);

        //Debug.Log(currentEulerAngles);
        Debug.Log(forwardDirection);
        //Debug.Log(rightDirection);

        if (Input.GetKey(KeyCode.W))
        {
            MyVector3 MovePos = MyVector3.AddVectors(new MyVector3(transform.position), forwardDirection * Time.deltaTime * 2);

            transform.position = MovePos.ToUnityVector();
        }


    }
}
//CODE I USED TO DO THIS WORKSHOP 3 (BENS CODE)
//public class NewBehaviourScript : MonoBehaviour
//{

//    Start is called before the first frame update
//    void Start()
//    {

//    }
//    public Vector3 Rotation;
//    Vector3 mouseRotation = new Vector3();
//    Vector3 forwardDirection = new Vector3();
//    Vector3 rightDirection = new Vector3();

//    Vector3 lastMousePosition = new Vector3();
//    Vector3 MouseDelta = new Vector3();



//    public Vector3 CalculateEuler()
//    {
//        Vector3 rv = new Vector3();

//        Rotation = transform.eulerAngles;


//        rv = Rotation / (180f / Mathf.PI);



//        return rv;
//    }

//    void Update()
//    {
//        /*

//        MouseDelta = (Input.mousePosition - lastMousePosition);

//        lastMousePosition = Input.mousePosition;

//        float MouseRotation = MathsLib.VectortoRadians(MouseDelta);



//        Debug.Log(transform.eulerAngles);
//        Debug.Log(MouseDelta);
//        Debug.Log(eulerRotation);
//        eulerRotation = CalculateEuler();


//        transform.eulerAngles = Rotation + MouseDelta;


//        transform.eulerAngles = transform.eulerAngles + MouseDelta;



//        fowardDirection = MathsLib.EulerangleToDirection(eulerRotation);

//        rightDirection = MathsLib.VectorCrossProduct(fowardDirection,Vector3.up);

//        */


//        Calculate mouse delta
//       MouseDelta = Input.mousePosition - lastMousePosition;
//        lastMousePosition = Input.mousePosition;

//        MouseDelta /= 5;
//        Convert mouse delta to rotation
//       mouseRotation = new Vector3(-MouseDelta.y, MouseDelta.x, 0);

//        Apply rotation to the object's Euler angles
//        transform.eulerAngles += mouseRotation;

//        Correct Euler angles to stay within range
//       Vector3 currentEulerAngles = transform.eulerAngles;
//        currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 0, 360f);
//        currentEulerAngles.z = Mathf.Clamp(currentEulerAngles.z, -90f, 90f);
//        currentEulerAngles.y = Mathf.Clamp(currentEulerAngles.y, -90f, 90f);
//        transform.eulerAngles = currentEulerAngles;

//        Calculate forward and right directions
//       forwardDirection = MathsLib.EulerangleToDirection(CalculateEuler());

//        rightDirection = MathsLib.VectorCrossProduct(Vector3.up, forwardDirection);

//        Debug.Log(currentEulerAngles);
//        Debug.Log(forwardDirection);
//        Debug.Log(rightDirection);

//        if (Input.GetKey(KeyCode.W))
//        {
//            transform.position += forwardDirection * Time.deltaTime;
//        }


//        if (Input.GetKey(KeyCode.D))
//        {
//            transform.position += rightDirection * Time.deltaTime;
//        }

//        if (Input.GetKey(KeyCode.S))
//        {
//            transform.position += -1 * forwardDirection * Time.deltaTime;
//        }


//        if (Input.GetKey(KeyCode.A))
//        {
//            transform.position += -1 * rightDirection * Time.deltaTime;
//        }





//    }
