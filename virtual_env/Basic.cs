using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Basic 
{
    public static Vector2 posBed1 = new Vector2(0, 3);
    public static Vector2 posBed2 = new Vector2(1, 3);
    public static Vector2 posBed3 = new Vector2(0, 4);
    public static Vector2 posBed4 = new Vector2(1, 4);


    public static Vector2 posSofa1 = new Vector2(2, 2);
    public static Vector2 posSofa2 = new Vector2(2, 3);
    public static Vector2 posSofa3 = new Vector2(2, 4);

    public static Vector2 posStove1 = new Vector2(8, 0);
    public static Vector2 posStove2 = new Vector2(8, 1);

    public static Vector2 posTable1 = new Vector2(8, 3);
    public static Vector2 posTable2 = new Vector2(8, 4);

    public static Vector2 posChair = new Vector2(6, 4);


    public static List<Vector3> lstSpecBallPos = new List<Vector3>(20);
    public static List<Vector2> lstSpecGrabPosLocal = new List<Vector2>(20);
    public static List<Vector2> lstSpecBallPosLocal = new List<Vector2>(20);

    public static void InitSpecialBallPosition()//10 diem
    {
        lstSpecBallPos.Clear();
        lstSpecGrabPosLocal.Clear();
        lstSpecBallPosLocal.Clear();


        lstSpecBallPos.Add(new Vector3(-12,2.2f,-2));//1
        lstSpecGrabPosLocal.Add(new Vector2(0,2));
        lstSpecBallPosLocal.Add(new Vector2(0,3));

        lstSpecBallPos.Add(new Vector3(-9, 2.2f, -2));//2
        lstSpecGrabPosLocal.Add(new Vector2(1, 2));
        lstSpecBallPosLocal.Add(new Vector2(1, 3));

       // lstSpecBallPos.Add(new Vector3(-6, 3.5f, 1.5f));
     //  lstSpecGrabPosLocal.Add(new Vector2(2, 1));
        //lstSpecBallPosLocal.Add(new Vector2(2, 2));

        lstSpecBallPos.Add(new Vector3(-5, 2.5f, 0));//3
        lstSpecGrabPosLocal.Add(new Vector2(3, 2));
        lstSpecBallPosLocal.Add(new Vector2(2, 2));
       

        lstSpecBallPos.Add(new Vector3(-5, 2.5f, -3));//4
        lstSpecGrabPosLocal.Add(new Vector2(3, 3));
        lstSpecBallPosLocal.Add(new Vector2(2, 3));

        lstSpecBallPos.Add(new Vector3(-5, 2.5f, -6));//5
        lstSpecGrabPosLocal.Add(new Vector2(3, 4));
        lstSpecBallPosLocal.Add(new Vector2(2, 4));


        lstSpecBallPos.Add(new Vector3(6, 2, -5.5f));//6
        lstSpecGrabPosLocal.Add(new Vector2(6, 3));
        lstSpecBallPosLocal.Add(new Vector2(6, 4));


        lstSpecBallPos.Add(new Vector3(11, 4.5f, -6));//7
        lstSpecGrabPosLocal.Add(new Vector2(7, 4));
        lstSpecBallPosLocal.Add(new Vector2(8, 4));

        lstSpecBallPos.Add(new Vector3(11, 4.5f, -3));//8
        lstSpecGrabPosLocal.Add(new Vector2(7, 3));
        lstSpecBallPosLocal.Add(new Vector2(8, 3));


       // lstSpecBallPos.Add(new Vector3(12, 4.5f, -2.5f));
        //lstSpecGrabPosLocal.Add(new Vector2(8, 2));

        lstSpecBallPos.Add(new Vector3(11.5f, 4.5f, 3));//9
        lstSpecGrabPosLocal.Add(new Vector2(7, 1));
        lstSpecBallPosLocal.Add(new Vector2(8, 1));

        lstSpecBallPos.Add(new Vector3(11.5f, 4.5f, 6));//10
        lstSpecGrabPosLocal.Add(new Vector2(7, 0));
        lstSpecBallPosLocal.Add(new Vector2(8, 0));
    }
    public static Vector3 ConvertLocalToWorld(Vector2 posLocal)
    {
        Vector3 result = new Vector3(0,3,0);
        result.x = -12 + posLocal.x * 3;
        result.z = -6 + (4 - posLocal.y) *3;

        return result;
    }

    public static Vector2 ConvertWorldToLocal(Vector3 posWorld)
    {
        Vector2 result = new Vector2(0, 0);//x,z
        int x=0;
        int y=0;
        for (int i = 1; i < 9; i++ )
        {
            if (posWorld.x < -13.5 + i*3 )
            {
                x = i-1;
                for (int j = 1; j < 5; j++)
                {
                    if (posWorld.z < -7.5 + j * 3)
                    {
                        y = 4 -(j-1);
                       
                       // print(x.ToString() +","+y.ToString());
                        break;
                    }
                }
                break;
            }
           
        }
        result.x = x;
        result.y = y;

        return result;
    }
	// Use this for initialization
	
}
