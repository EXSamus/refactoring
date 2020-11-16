using System;
using System.Numerics;
using circuit;
using System.Threading;
using voiture;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections; 
using affichage;
namespace refactoring
{
    class Program
    {

        public static Affichage affichage = new Affichage();
        public static readonly int[] listVoiture = { 30, 16, 15, 7, 3, 40, 56, 22, 29, 10 };
        //doWorkCallback callback = new doWorkCallback(dataCallBack);

        public static Thread[] thr = new Thread[listVoiture.Length];
        //public static int[] classement = new int[listVoiture.Length][];
        public static Mutex race;
        static void Main(string[] args)
        {
            race = new Mutex ();
            int[][] classement = new int[3][];

            //List<int[]> classement = new List<int[]>();
            initClassement(classement);
            initMultiThread(classement, race);      
            startMultiThread();
            readMemory(classement, race);
            affichage.affichage(classement);
        }
        public static void initClassement(int[][] classement)
        {
            for (int i = 0; i < 3; i++)
            {
                classement[i] = new int[] { i, 0, 0, 0, 0, 2, 0, 0 };
            }
        }
        public static void initMultiThread(int[][] classement, Mutex  sem)
        {
            Circuit circuit = new Circuit();

            for (int i = 0; i < 3; i++)
            {
                int temp = i;
                thr[temp] = new Thread(() => circuit.simu(temp, classement, race));
                thr[temp].Name = "Voiture " + temp;
            }
        }
        public static void startMultiThread()
        {
            for (int i = 0; i < 3; i++)
            {
                thr[i].Start();
            }
            Thread.Sleep(500);

        }


        /**********************************  fonctions auxiliaires  ******************************************/
        public static bool readMemory(int[][] classement, Mutex sem)
        {
            
            bool check = true;
            while (check)
            {
                int counter = 0;
                for (int i = 0; i < classement.Length; i++)
                {
                    if (classement[i][5] == 0)
                    {
                        counter++;
                    }
                    //à Modifier
                    if (counter == 3)
                    {
                        return false;
                    }
                }
                //Console.Clear();
                sem.WaitOne();
                affichage.affichage(classement);
                sem.ReleaseMutex();
                Thread.Sleep(2500);  
            };
            return check;
        }
    }

}