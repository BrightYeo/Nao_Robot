using UnityEngine;
using System.Collections;
using System.IO;


public class MyGUI : MonoBehaviour 
{
    public static float speedHR;

    public string stringScore = "0";
    public string stringWrong = "0";
    public string stringCompleted = "0";

    public static bool StartSimulatate;

    public static bool bAutomaticSimulate;
	public static int MinSpeed = 1;
	public static int MaxSpeed = 230;
	public static bool StartCount = false;

    bool bExit;
    bool bRestart;
    string textStartStop = "Start";
   
	// Use this for initialization
	void Start () 
    {
        bExit = false;
        bRestart = false;
       

        speedHR = 10;
       

        StartSimulatate = false;
        bAutomaticSimulate = true;
        Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (bExit)
        {
            Application.Quit();
        }
	}
   
    void OnGUI()
    {
        

        GUI.Box(new Rect(5,5,200,320),"");
        GUI.Label(new Rect(15, 25, 50, 40),"Speed");
        speedHR = GUI.HorizontalSlider(new Rect(80, 30, 100, 20), speedHR, MinSpeed, MaxSpeed);

        GUI.Label(new Rect(15, 50, 60, 40), "Period");
        if (StartSimulatate)
        {
            GUI.enabled = false;
        }
        stringScore = Main.period.ToString();
        stringScore = GUI.TextField(new Rect(80, 50, 80, 25), stringScore, 25);
        GUI.enabled = true;
      
       
      //  if (bRestart)
      //  {
      //      GUI.enabled = false;
      //  }
        if (GUI.Button(new Rect(15, 110, 55, 25), textStartStop))
       {

           if (!StartSimulatate)
           {
               StartSimulatate = true;
               bRestart = true;
               textStartStop = "Stop";
           }
            else
           {
                 StartSimulatate = false;
                 bRestart = false;
                 textStartStop = "Start";
           }
         
         
       }
     //  GUI.enabled = true;

       if (!bRestart)
      {
          GUI.enabled = false;
      }
       if (GUI.Button(new Rect(75, 110, 55, 25), "Restart"))
       {
           bRestart = true;
        

         
           int n = 9;
           int m = 5;
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
                                            for (int d = 0; d < Main.nd; d++)
                                            {
                                                Main.Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, d] = 0;
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

           Main.bReset = true;
           Main.period = 0;
       }

       GUI.enabled = true;

       if (GUI.Button(new Rect(135, 110, 55, 25), "Exit"))
       {
           bExit = true;
       }

       if (speedHR > 200)
       {

           GUI.Label(new Rect(15, 150, 120, 50), "Number of Collisions/10.000 periods");
           GUI.Label(new Rect(15, 220, 120, 50), "Number of Successes/10.000 periods");

           if (GUI.Button(new Rect(15, 280, 80, 25), "Start Count"))
           {
               Main.lstCollision.Clear();
               Main.lstCompleted.Clear();
               Main.listPeriod.Clear();
               StartCount = true;
           }
           if (GUI.Button(new Rect(125, 280, 80, 25), "Stop Count"))
           {
               // StartSimulatate = false;
               StartCount = false;

               System.IO.StreamWriter file = new System.IO.StreamWriter("Count.txt");

               int kk = Main.lstCollision.Count;
            
               for (int i = 0; i < kk; i++)
               {
                   file.WriteLine(Main.lstCollision[i] + " " + Main.lstCompleted[i] + " " + Main.listPeriod[i]);
               }

               file.Close();
           }
           stringWrong = Main.NumberCollisionObs.ToString();
           stringWrong = GUI.TextField(new Rect(140, 170, 60, 25), stringWrong, 25);

           stringCompleted = Main.NumberCompleted.ToString();
           stringCompleted = GUI.TextField(new Rect(140, 240, 60, 25), stringCompleted, 25);
       }
       else
       {
           StartCount = false;
       }

       const string fileName = "State.txt";
       if (GUI.Button(new Rect(15, 80, 80, 25), "Write State"))
        {
              
               using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
               {
                   writer.Write(Main.period);
                   int n = 9;
                   int m = 5;
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
                                                       for (int d = 0; d < Main.nd; d++)
                                                       {
                                                         
                                                           writer.Write(Main.Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, d]);
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

                 //  writer.Close();
               }
    
        }

       if (GUI.Button(new Rect(100, 80, 80, 25), "Read State"))
       {
           using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
           {
               Main.period = reader.ReadInt32();
               int n = 9;
               int m = 5;
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
                                                   for (int d = 0; d < Main.nd; d++)
                                                   {
                                                       
                                                       Main.Score[x1, y1, x2, y2, x3, y3, x4, y4, oi, d] = reader.ReadInt32();
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
           }
       }
    }
}
