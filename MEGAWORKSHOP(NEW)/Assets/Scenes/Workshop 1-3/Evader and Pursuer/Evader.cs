using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evader : MonoBehaviour
{
    public MyVector3 Direction;
    // Start is called before the first frame update
    void Start()
    {
        Direction = new MyVector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //When the Key is pressed
        {

            Direction = new MyVector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)); //Random Direction when button pressed

        }
        MyVector3 MovePos = MyVector3.AddVectors(new MyVector3(transform.position), Direction * Time.deltaTime * 2); //Move the object over time.
        transform.position = MovePos.ToUnityVector();

    }
}
