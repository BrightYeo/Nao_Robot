using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    public const int n = 9;
    public const int m = 5;
    public static bool bReset;
    public static float speed = 100;

    public static int NumberCollisionObs = 0;
    public static int NumberCompleted = 0;

    public static List<int> lstCollision = new List<int>(10000);
    public static List<int> lstCompleted = new List<int>(10000);
    public static List<int> listPeriod = new List<int>(10000);

    Vector3 posObstacle;
    Vector3 posBall;
    Vector3 posRobot;
    Vector3 posHuman;

    Vector2 posObstacleLocal;
    Vector2 posBallLocal;

    Vector2 posGrabBall;
    Vector2 posGrabBallLocal;

    Vector2 posRobotLocal;
    Vector2 posHumanLocal;

    public static Vector3 directionRobot;
    public static Vector3 directionObstacle;
    public static int period;

    bool bStart;


    Vector2 prevPosRobotLocal;
    int indexDirection;

    public const int nd = 5;
    int[] arrtemp = new int[nd];
    public static int[, , , , , , , , ,] Score = new int[n, m, n, m, n, m, n, m, 2, nd];//  pos Human,Pos Robot,posObstacle,posBall, bring Object i or not, direction
    public static bool bBring;

    // Use this for initialization
    int CountRnd = 0;
    int OldRnd = -1;

    List<Vector2> list = new List<Vector2>(5);

    public GameObject human;
    public GameObject robot;
    public GameObject obstacle;
    public GameObject ball;

    public GameObject RightHandObject;
    public GameObject RHand;
    public GameObject LHandOfHuman;
    public GameObject LArmOfHuman;


    Vector2 prevPosObstacleLocal;

    bool bMovetoGrab = false;
    bool bMove180 = false;
    Vector3 posAverage;
    Vector3 posOldGrab;
    int randBall;
    bool bWaitObstacle = false;
    Quaternion qRobot;
    Quaternion qHuman;


    void RandomPosition()
    {
        list.Clear();
        list.Add(Basic.posBed1);
        list.Add(Basic.posBed2);
        list.Add(Basic.posBed3);
        list.Add(Basic.posBed4);

        list.Add(Basic.posSofa1);
        list.Add(Basic.posSofa2);
        list.Add(Basic.posSofa3);

        list.Add(Basic.posStove1);
        list.Add(Basic.posStove2);

        list.Add(Basic.posTable1);
        list.Add(Basic.posTable2);

        list.Add(Basic.posChair);

        //Human

        bool bCheck = true;
         posHumanLocal = new Vector2(0,2);
         list.Add(posHumanLocal);
        
         posBallLocal = new Vector2(8,0);
         posGrabBallLocal = new Vector2(7, 0);
          posBall = Basic.lstSpecBallPos[9];

        list.Add(posBallLocal);
         randBall = 2; //specific place

        //Obstacle
           posObstacleLocal = new Vector2(4, 0);
           list.Add(posObstacleLocal);

        //Robot
          posRobotLocal = new Vector2(2,1);
           list.Add(posRobotLocal);

        if (MyGUI.speedHR < 200)
        {
            posHuman = Basic.ConvertLocalToWorld(posHumanLocal);
            posHuman.y = 0;
            posRobot = Basic.ConvertLocalToWorld(posRobotLocal);
            posObstacle = Basic.ConvertLocalToWorld(posObstacleLocal);
            if (randBall != 2)// not a specific place
            {
                posBall = Basic.ConvertLocalToWorld(posBallLocal);
            }

            robot.transform.position = posRobot;
            obstacle.transform.position = posObstacle;
            ball.transform.position = posBall;

            Vector3 directHumanRobot = robot.transform.position - posHuman;
            directHumanRobot.y = 0;
            directHumanRobot = directHumanRobot.normalized;
            if (directHumanRobot != new Vector3(0, 0, 0))
            {
                human.transform.rotation = Quaternion.LookRotation(directHumanRobot);
                human.transform.position = posHuman - 1.5f * directHumanRobot;
            }
        }

    }

    void Start()
    {
        qRobot = RightHandObject.transform.localRotation;
        qHuman = LArmOfHuman.transform.localRotation;

        bMovetoGrab = false;
        Basic.InitSpecialBallPosition();

        bReset = false;
        period = 0;

        prevPosRobotLocal = new Vector2(0, 0);
        bStart = false;
        CountRnd = 0;
        OldRnd = -1;


        bBring = false;


        for (int x1 = 0; x1 < n; x1++)
        {
            for (int y1 = 0; y1 < m; y1++)
            {
                for (int x2 = 0; x2 < n; x2++)
                {
                    for (int y2 = 0; y2 < m; y2++)
                    {
                        for (int x3 = 0; x3 < n; x3++)
                        {
                            for (int y3 = 0; y3 < m; y3++)
                            {
                                for (int x4 = 0; x4 < n; x4++)
                                {
                                    for (int y4 = 0; y4 < m; y4++)
                                    {
                                        for (int oi = 0; oi < 2; oi++)
                                        {
                                            for (int d = 0; d < nd; d++)
                                            {
                                                Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, d] = 0;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
        RandomPosition();

    }

    void FindActionObstacle()
    {
        if (!bMovetoGrab) // when pause
        {

            int iObstacleDirection = Random.Range(0, 4);
            prevPosObstacleLocal = posObstacleLocal;
            switch (iObstacleDirection)
            {
                case 0: //Right
                    if (posObstacleLocal.x < 8) // from 7 to 0, can still be right
                    {
                        if (posObstacleLocal == new Vector2(5, 2) || posObstacleLocal == new Vector2(5, 3)) // place wall
                        {

                        }
                        else
                        {
                            posObstacleLocal += new Vector2(1, 0);
                            for (int i = 0; i < 14; i++)
                            {
                                if (posObstacleLocal == list[i])
                                {
                                    posObstacleLocal = prevPosObstacleLocal;
                                    break;
                                }
                            }
                        }
                    }


                    break;
                case 1://Left
                    if (posObstacleLocal.x > 0)
                    {
                        if (posObstacleLocal == new Vector2(6, 2) || posObstacleLocal == new Vector2(6, 3))//wall
                        {

                        }
                        else
                        {
                            posObstacleLocal -= new Vector2(1, 0);
                            for (int i = 0; i < 14; i++)
                            {
                                if (posObstacleLocal == list[i])
                                {
                                    posObstacleLocal = prevPosObstacleLocal;
                                    break;
                                }
                            }
                        }
                    }

                    break;
                case 2://Up
                    if (posObstacleLocal.y > 0)
                    {

                        posObstacleLocal -= new Vector2(0, 1);
                        for (int i = 0; i < 14; i++)
                        {
                            if (posObstacleLocal == list[i])
                            {
                                posObstacleLocal = prevPosObstacleLocal;
                                break;
                            }
                        }
                    }


                    break;
                case 3://Down
                    if (posObstacleLocal.y < 4)
                    {

                        posObstacleLocal += new Vector2(0, 1);
                        for (int i = 0; i < 14; i++)
                        {
                            if (posObstacleLocal == list[i])
                            {
                                posObstacleLocal = prevPosObstacleLocal;
                                break;
                            }
                        }
                    }

                    break;
            }

            // posObstacleLocal = prevPosObstacleLocal;
            if (speed < 200)
            {
                obstacle.transform.position = Basic.ConvertLocalToWorld(prevPosObstacleLocal);

                posObstacle = Basic.ConvertLocalToWorld(posObstacleLocal);

                if (posObstacleLocal != prevPosObstacleLocal)
                {
                    directionObstacle = posObstacle - obstacle.transform.position;
                    directionObstacle = directionObstacle.normalized;
                }
                else
                {
                    directionObstacle = new Vector3(0, 0, 0);
                }


            }
        }
    }

    void FindAction()
    {
        bStart = true;

        if (!bMovetoGrab)
        {

            int x1 = (int)posHumanLocal.x;
            int y1 = (int)posHumanLocal.y;


            int x2 = (int)posRobotLocal.x;
            int y2 = (int)posRobotLocal.y;


            int x3 = (int)prevPosObstacleLocal.x;
            int y3 = (int)prevPosObstacleLocal.y;

            int x4 = (int)posBallLocal.x;
            int y4 = (int)posBallLocal.y;



            int oi;// indexObj+1;
            if (bBring)
            {
                oi = 1;
            }
            else
            {
                oi = 0;
            }

            //find the best direction
            float maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0];
            int rnd = 0;
            for (int i = 1; i < nd; i++)
            {
                if (maxd < Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i])
                {
                    maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i];
                    rnd = i;
                }
            }


            // if all direction are equal, then randomly..
            if (Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0] == Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 1] && Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 1] == Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 2] && Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 2] == Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 3] && Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 3] == Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 4])//Can not find max
            {
                rnd = Random.Range(0, nd);
            }
            else
            {
                for (int i = 0; i < nd; i++)
                {
                    arrtemp[i] = -1; //exception
                }
                int k = 0;
                for (int i = 0; i < nd; i++)
                {
                    if (maxd == Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i])
                    {
                        arrtemp[k] = i;
                        k++;
                    }
                }

                if (k > 1)
                {
                    bool finished = false;
                    while (!finished)
                    {
                        rnd = Random.Range(0, nd);
                        for (int i = 0; i < nd; i++)
                        {
                            if (arrtemp[i] == rnd)
                            {
                                finished = true;
                                break;
                            }
                        }
                    }
                }

            }

            indexDirection = rnd;// current direction has been selected
            
            prevPosRobotLocal = posRobotLocal; // new addition
            switch (rnd)
            {
                case 0://Right
                    if (posRobotLocal.x < 8)
                    {
                        //currentPosition = rigidbody.position + new Vector3(2, 0, 0);
                        if (posRobotLocal == new Vector2(5, 2) || posRobotLocal == new Vector2(5, 3))
                        {
                            Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0] = -1000000;
                        }
                        else
                        {
                            posRobotLocal += new Vector2(1, 0);
                            for (int i = 0; i < 12; i++)
                            {
                                if (posRobotLocal == list[i])
                                {
                                    posRobotLocal = prevPosRobotLocal;
                                    Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0] = -1000000;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0] = -1000000;
                    }

                    break;
                case 1://Left
                    if (posRobotLocal.x > 0)
                    {
                        //currentPosition = rigidbody.position + new Vector3(-2, 0, 0);
                        if (posRobotLocal == new Vector2(6, 2) || posRobotLocal == new Vector2(6, 3))
                        {
                            Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0] = -1000000;
                        }
                        else
                        {
                            posRobotLocal -= new Vector2(1, 0);
                            for (int i = 0; i < 12; i++)
                            {
                                if (posRobotLocal == list[i])
                                {
                                    posRobotLocal = prevPosRobotLocal;
                                    Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 1] = -1000000;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 1] = -1000000;
                    }
                    break;
                case 2://Up
                    if (posRobotLocal.y > 0)
                    {
                        //currentPosition = rigidbody.position + new Vector3(0, 0, 2);
                        posRobotLocal -= new Vector2(0, 1);
                        for (int i = 0; i < 12; i++)
                        {
                            if (posRobotLocal == list[i])
                            {
                                posRobotLocal = prevPosRobotLocal;
                                Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 2] = -1000000;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 2] = -1000000;
                    }

                    break;
                case 3://Down
                    if (posRobotLocal.y < 4)
                    {
                        //currentPosition = rigidbody.position + new Vector3(0, 0, -2);
                        posRobotLocal += new Vector2(0, 1);
                        for (int i = 0; i < 12; i++)
                        {
                            if (posRobotLocal == list[i])
                            {
                                posRobotLocal = prevPosRobotLocal;
                                Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 3] = -1000000;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 3] = -1000000;
                    }
                    break;

                case 4://Stop
                    break;
            }

            if (speed < 200)
            {


                robot.transform.position = Basic.ConvertLocalToWorld(prevPosRobotLocal);
                posRobot = Basic.ConvertLocalToWorld(posRobotLocal);

                if (posRobotLocal != prevPosRobotLocal)
                {
                    if (posRobotLocal == posHumanLocal)
                    {
                        directionRobot = posRobot - robot.transform.position;
                        directionRobot = directionRobot.normalized;
                        posRobot = robot.transform.position + 2 * directionRobot; // avoid the case when the robot collides human
                    }
                    else
                    {
                        directionRobot = posRobot - robot.transform.position;
                        directionRobot = directionRobot.normalized;
                    }

                }
                else
                {
                    directionRobot = new Vector3(0, 0, 0);
                }



                posHuman = Basic.ConvertLocalToWorld(posHumanLocal);
                posHuman.y = 0;
                if (randBall != 2)
                {
                    posBall = Basic.ConvertLocalToWorld(posBallLocal);
                }

                //human.transform.position = posHuman;
                ball.transform.position = posBall;

                Vector3 directHumanRobot = robot.transform.position - posHuman;
                directHumanRobot.y = 0;
                directHumanRobot = directHumanRobot.normalized;
                if (directHumanRobot != new Vector3(0, 0, 0))
                {
                    human.transform.rotation = Quaternion.LookRotation(directHumanRobot);
                    human.transform.position = posHuman - 1.5f * directHumanRobot;
                }
            }
        }
        else//Only move for grapping
        {
            if (speed < 200)
            {


                posRobot = Basic.ConvertLocalToWorld(posRobotLocal);
                robot.transform.position = posRobot;

                posAverage = (posRobot + new Vector3(posBall.x, posRobot.y, posBall.z)) / 2;

                posOldGrab = posRobot;
                directionRobot = posAverage - posRobot;
                directionRobot = directionRobot.normalized;

                posHuman = Basic.ConvertLocalToWorld(posHumanLocal);
                posHuman.y = 0;

               // human.transform.position = posHuman;
                ball.transform.position = posBall;

                Vector3 directHumanRobot = robot.transform.position - posHuman;
                directHumanRobot.y = 0;
                directHumanRobot = directHumanRobot.normalized;
                if (directHumanRobot != new Vector3(0, 0, 0))
                {
                    human.transform.rotation = Quaternion.LookRotation(directHumanRobot);
                    human.transform.position = posHuman - 1.5f * directHumanRobot;
                }
            }
            else
            {
                bMovetoGrab = false;
                bBring = true;
            }
        }


    }


    int iResetCase = -1;
    bool bDelay = false;
    float delayTime = 0;
    void UpdateScore()
    {
        int reward = 0;
        if (posRobotLocal == posHumanLocal)//Need to reset the Scene
        {
            if (bBring)
            {
                if (speed < 200)
                {
                    posBall = LHandOfHuman.transform.position;

                    bDelay = true;
                    delayTime = 0;
                }

                reward = 100;
                iResetCase = 0;//completed mission
            }
            else
            {
                reward = -200;
            }

            bReset = true;
        }
        else if (posRobotLocal == posObstacleLocal)
        {
            iResetCase = 1;//collision with obstacle
            reward = -200;
            bReset = true;
        }
        else if (posObstacleLocal == prevPosRobotLocal && posRobotLocal == prevPosObstacleLocal)//swap each other
        {
            iResetCase = 1;//collision with obstacle
            reward = -200;
            bReset = true;
        }



        int x1 = (int)posHumanLocal.x;
        int y1 = (int)posHumanLocal.y;

        int x2 = (int)posRobotLocal.x;
        int y2 = (int)posRobotLocal.y;

        int x3 = (int)posObstacleLocal.x;
        int y3 = (int)posObstacleLocal.y;

        int x4 = (int)posBallLocal.x;
        int y4 = (int)posBallLocal.y;



        int oi;//= indexObj + 1;
        if (bBring)
        {
            oi = 1;
        }
        else
        {
            oi = 0;
        }

        int xPre = (int)prevPosRobotLocal.x;
        int yPre = (int)prevPosRobotLocal.y;

        int xObPre = (int)prevPosObstacleLocal.x;
        int yObPre = (int)prevPosObstacleLocal.y;

        int d = indexDirection;



        float maxd;

        int NewScore;

        if (posRobotLocal == posBallLocal)
        {

            if (!bBring)
            {
                bBring = true;
                if (MyGUI.speedHR < 200)
                {
                    RightHandObject.transform.localRotation = Quaternion.AngleAxis(-90, new Vector3(1, 0, 0));
                }

                oi = 1;
                maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0];
                for (int i = 1; i < nd; i++)
                {
                    if (maxd < Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i])
                    {
                        maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i];
                    }
                }
                NewScore = (int)(0.9f * maxd + reward);

                Score[x1, y1, xPre, yPre, xObPre, yObPre, x4, y4, 0, d] = NewScore;

            }

        }
        else if (posRobotLocal == posGrabBallLocal)
        {
            if (!bBring)
            {
                //  bBring = true;
                bMovetoGrab = true;
                if (MyGUI.speedHR < 200)
                {
                    RightHandObject.transform.localRotation = Quaternion.AngleAxis(-90, new Vector3(1, 0, 0));
                }

                oi = 1;
                maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0];
                for (int i = 1; i < nd; i++)
                {
                    if (maxd < Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i])
                    {
                        maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i];
                    }
                }
                NewScore = (int)(0.9f * maxd + reward);

                Score[x1, y1, xPre, yPre, xObPre, yObPre, x4, y4, 0, d] = NewScore;

            }
        }
        else
        {
            maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, 0];
            for (int i = 1; i < nd; i++)
            {
                if (maxd < Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i])
                {
                    maxd = Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, i];
                }
            }

            NewScore = (int)(0.9f * maxd + reward);
            Score[x1, y1, xPre, yPre, xObPre, yObPre, x4, y4, oi, d] = NewScore;
        }


    }


    // int countPeriods = 0;

    void Update()
    {
        speed = MyGUI.speedHR;



        if (MyGUI.StartSimulatate)
        {

            if (speed > 200)
            {
                if (MyGUI.StartCount)
                {
                    NumberCollisionObs = 0;
                    NumberCompleted = 0;
                    // countPeriods = 0;
                }


                int dem = 0;
                while (dem < 10000)
                {
                    if (!bStart)
                    {
                        FindActionObstacle();
                        FindAction();
                    }
                    else
                    {
                        UpdateScore();
                        bStart = false;
                    }

                    if (bReset)
                    {
                        bMovetoGrab = false;
                        bBring = false;
                        prevPosRobotLocal = new Vector3(0, 0);

                        bStart = false;

                        CountRnd = 0;
                        OldRnd = -1;


                        //======
                        if (MyGUI.StartCount)
                        {

                            if (iResetCase == 0)
                            {
                                NumberCompleted++;
                            }
                            else if (iResetCase == 1)
                            {
                                NumberCollisionObs++;
                            }

                        }

                        //======
                        dem++;

                        period++;
                        if (MyGUI.StartCount)
                        {
                            if (dem == 10000)
                            {
                                lstCollision.Add(NumberCollisionObs);
                                lstCompleted.Add(NumberCompleted);
                                listPeriod.Add(period);
                            }
                        }

                        RandomPosition();
                        bReset = false;
                    }
                }

            }
            else
            {

                if (bDelay)
                {

                    Vector3 directHumanRobot = robot.transform.position - posHuman;
                    directHumanRobot.y = 0;
                    directHumanRobot = directHumanRobot.normalized;
                    if (directHumanRobot != new Vector3(0, 0, 0))
                    {
                        human.transform.rotation = Quaternion.LookRotation(directHumanRobot);
                        human.transform.position = posHuman - 1.5f * directHumanRobot;
                    }


                    robot.transform.position = posRobot;
                    ball.transform.position = posBall;
                    obstacle.transform.position = posObstacle;


                    delayTime += Time.deltaTime;
                    if (delayTime > 3)
                    {

                        bDelay = false;
                    }

                    return;
                }
                else if (bReset)
                {

                    RightHandObject.transform.localRotation = qRobot;
                    LArmOfHuman.transform.localRotation = qHuman;

                    bMovetoGrab = false;
                    bBring = false;
                    prevPosRobotLocal = new Vector3(0, 0);

                    bStart = false;

                    CountRnd = 0;
                    OldRnd = -1;

                    period++;
                    bReset = false;

                    RandomPosition();
                }
                // Debug.Log(bStart);
                if (!bStart)
                {
                    FindActionObstacle();
                    FindAction();
                }
                else
                {
                    if (bMovetoGrab) // just for move to grab the ball
                    {
                        controlAnimation.walk = true;

                        if (bMove180 == false)
                        {
                            Vector3 direction1 = posAverage - robot.transform.position;
                            direction1 = direction1.normalized;

                            Vector3 totalV = direction1 + directionRobot;
                            if (totalV.magnitude > 1.8)
                            {
                                robot.transform.position += directionRobot * speed * Time.deltaTime;
                                robot.transform.rotation = Quaternion.LookRotation(directionRobot);

                            }
                            else
                            {
                                directionRobot = -directionRobot;
                                bMove180 = true;
                                bBring = true;
                            }
                        }
                        else
                        {
                            Vector3 direction1 = posOldGrab - robot.transform.position;
                            direction1 = direction1.normalized;

                            Vector3 totalV = direction1 + directionRobot;
                            if (totalV.magnitude > 1.8)
                            {
                                robot.transform.position += directionRobot * speed * Time.deltaTime;
                                robot.transform.rotation = Quaternion.LookRotation(directionRobot);

                                if (bBring)
                                {
                                    posBall = RHand.transform.position;
                                    ball.transform.position = posBall;
                                }

                            }
                            else
                            {
                                bMove180 = false;
                                bMovetoGrab = false;
                            }
                        }

                    }
                    else //normal motion when the ball is not in the specific position
                    {
                        if (directionObstacle != new Vector3(0, 0, 0))
                        {
                            Vector3 direction2 = posObstacle - obstacle.transform.position;
                            direction2 = direction2.normalized;

                            Vector3 totalV2 = direction2 + directionObstacle;
                            if (totalV2.magnitude > 1.8)
                            {
                                obstacle.transform.position += directionObstacle * speed * Time.deltaTime;
                                bWaitObstacle = true;
                            }
                            else
                            {
                                bWaitObstacle = false;
                                obstacle.transform.position = posObstacle;
                            }
                        }
                        else
                        {
                            bWaitObstacle = false;
                        }


                        if (directionRobot != new Vector3(0, 0, 0))
                        {
                            Vector3 direction1 = posRobot - robot.transform.position;
                            direction1 = direction1.normalized;

                            Vector3 totalV = direction1 + directionRobot;
                            if (totalV.magnitude > 1.8)
                            {
                                robot.transform.position += directionRobot * speed * Time.deltaTime;
                                robot.transform.rotation = Quaternion.LookRotation(directionRobot);
                                if (bBring)
                                {
                                    posBall = RHand.transform.position;
                                    ball.transform.position = posBall;
                                }

                                controlAnimation.walk = true;
                            }
                            else
                            {

                                controlAnimation.walk = false;

                                obstacle.transform.position = posObstacle;
                                if (bBring)
                                {
                                    posBall = RHand.transform.position;
                                    ball.transform.position = posBall;
                                }
                                robot.transform.position = posRobot;
                                UpdateScore();

                                bStart = false;
                            }
                        }
                        else
                        {
                            if (bBring)
                            {
                                posBall = RHand.transform.position;
                                ball.transform.position = posBall;
                            }
                            robot.transform.position = posRobot;

                            if (bWaitObstacle == false)
                            {
                                UpdateScore();

                                bStart = false;
                            }
                        }
                    }

                    Vector3 directHumanRobot = robot.transform.position - posHuman;
                    directHumanRobot.y = 0;
                    directHumanRobot = directHumanRobot.normalized;
                    if (directHumanRobot != new Vector3(0, 0, 0))
                    {
                        human.transform.rotation = Quaternion.LookRotation(directHumanRobot);
                        human.transform.position = posHuman - 1.5f * directHumanRobot;
                    }


                }

                //
            }
        }
        else
        {
            controlAnimation.walk = false;
        }

    }
}
