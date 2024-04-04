using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quat
{
    public float w, x, y, z;
    public MyVector3 v;
    public Quat()
    {
        w = 0.0f;
        v = new MyVector3(0, 0, 0);
    }
    

    public Quat(float Angle, MyVector3 Axis)
    {

        float halfAngle = Angle / 2;
        w = Mathf.Cos(halfAngle);
        x = Axis.x * Mathf.Sin(halfAngle);
        y = Axis.y * Mathf.Sin(halfAngle);
        z = Axis.z * Mathf.Sin(halfAngle);

        return;
    }
    public Quat(MyVector3 Position)
    {
        w = 0.0f;
        v = new MyVector3(Position.x, Position.y, Position.z);
    }

    public static Quat operator*(Quat lhs, Quat rhs)
    {
        Quat rv = new Quat();

        //Scaler Formula
        rv.w = lhs.w * rhs.w - (lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z);

        //Vector Forumla
        MyVector3 lhsVector = new MyVector3(lhs.x, lhs.y, lhs.z); //Get left hand side Quaternions
        MyVector3 rhsVector = new MyVector3(rhs.x, rhs.y, rhs.z); //Get Right hand side Quaternions
        MyVector3 VectorCross = MyVector3.CrossProduct(lhsVector, rhsVector); //Calculate the cross product between the lhs and rhs vectors

        // Adds the Cross product and scales quaternions on each axis.
        rv.x = lhs.w * rhs.x + lhs.x * rhs.w + VectorCross.x; 
        rv.y = lhs.w * rhs.y + lhs.y * rhs.w + VectorCross.y;
        rv.z = lhs.w * rhs.z + lhs.z * rhs.w + VectorCross.z;

 
        return rv; 
    }
    // Getter and setter for axis

    public MyVector3 Axis
    {
        get
        {
            return new MyVector3(x, y, z);
        }
        set
        {
            x = value.x;
            y = value.y;
            z = value.z;
        }
    }

    // Set axis method
    public void SetAxis(MyVector3 axis)
    {
        x = axis.x;
        y = axis.y;
        z = axis.z;
    }

    // Get axis method
    public MyVector3 GetAxis()
    {
        return new MyVector3(x, y, z);
    }

  
    // Inverse method
    public Quat Inverse()
    {
        Quat rv = new Quat();
        rv.w = w;

        // Set the axis to the negative of the current axis
        rv.SetAxis(new MyVector3(0, 0, 0) - GetAxis()); //- Get axis from nothing that way it becomes a negative
        return rv;
    }
    public Vector4 GetAxisAngle()
    {
        Vector4 rv = new Vector4();

        //Inverse cosine to get half angle back
        float halfAngle = Mathf.Acos(w);
        rv.w = halfAngle * 2;

        rv.x = x / Mathf.Sin(halfAngle);
        rv.y = y / Mathf.Sin(halfAngle);
        rv.z = z / Mathf.Sin(halfAngle);

        return rv;
    }

    public static Quat SLERP(Quat q, Quat r, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);

        Quat d = r * q.Inverse();
        Vector4 AxisAngle = d.GetAxisAngle();
        Quat dT = new Quat(AxisAngle.w * t, new MyVector3(AxisAngle.x, AxisAngle.y, AxisAngle.z));

        return dT * q;
    }
}
