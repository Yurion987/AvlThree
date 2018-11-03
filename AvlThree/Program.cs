using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlThree
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0;i<10000 ; i++)
            {
                stopwatch.Start();
                var list = new List<int>(1000000);
                var a = new AVLTree<int>();

                var random = new Random(i);
                for (int j = 0; j < 1000; j++)
                {
   
                    int randomCis = random.Next(100000);
                    int randomAkcia = random.Next(100);
                    //insert
                    if (randomAkcia <= 50)
                    {
                        while (list.Contains(randomCis))
                        {
                            randomCis = random.Next(10000);
                        }
                       
                        list.Add(randomCis);
                        a.Add(randomCis);
                    }
                    //delete
                    else
                    {
                        if (list.Count > 0)
                        {
                            int c = random.Next(list.Count - 1);
                            //      Console.WriteLine("\n" + list[c] + "            \n" );
                        
                           
                            a.Delete(list[c]);
                            list.RemoveAt(c);
                        }
                    }
                    if (list.Count>0)
                    {
                        var listInOrder = a.InOrder();
                        list = list.OrderBy(x => x).ToList();
                        for (int k = 0; k < listInOrder.Count ; k++)
                        {
                            if (list[k] == listInOrder[k])
                            {
                           //     Console.Write("K");
                            }
                            else
                            {
                                throw new Exception("PRVKY SA NEZHODUJU");
                            }
                        }
                  //      Console.WriteLine("\n"); 
                   //     Console.WriteLine(j);
                        a.VyskaKontrola();
                    }
                }
               
                stopwatch.Stop();
            //    Console.WriteLine("\n \n "+stopwatch.Elapsed+ "\n \n ");
          //      Console.WriteLine();
            }
          
            Console.WriteLine();
          

        }
    }
}

