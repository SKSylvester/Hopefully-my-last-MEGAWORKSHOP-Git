using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : MonoBehaviour
{
    public GameObject Cylinder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { //Always follow the evader
        MyVector3 Direction = MyVector3.SubtractVectors(new MyVector3(Cylinder.transform.position), new MyVector3(transform.position));

        float dot = MyVector3.DotProduct(Direction, Cylinder.GetComponent<NewEvader>().forwardDirection);
        float dot2 = MyVector3.DotProduct(Direction, Cylinder.GetComponent<NewEvader>().rightDirection);
        // Returns an angle between both the direction and normalizes it.

        if (dot > 0f) // (Greater Than) Checks if the direction is similar to the direction the evader is going. 
        {
            MyVector3 MovePos = MyVector3.AddVectors(new MyVector3(transform.position), Direction * Time.deltaTime * 1);

            transform.position = MovePos.ToUnityVector();
        }
    }

}
