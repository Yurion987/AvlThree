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
            for (int i = 0; ; i++)
            {
                stopwatch.Start();
                var list = new List<int>(1000000);
                var a = new AVLTree<int>();

                var random = new Random(i);
                for (int j = 0; j < 1000; j++)
                {
   
                    int randomCis = random.Next(10000);
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
                            //   a.LevelOrderConsole();
                           
                            a.Delete(list[c]);
                            list.RemoveAt(c);
                        }
                    }
                    if (list.Count>0)
                    {
                        var listInOrder = a.InOrder();
                       
                        for (int k = 0; k < listInOrder.Count - 1; k++)
                        {
                            if (list.Contains(listInOrder[k]))
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
                if (i == 70) {
                    Console.WriteLine();
                }
                stopwatch.Stop();
            //    Console.WriteLine("\n \n "+stopwatch.Elapsed+ "\n \n ");
          //      Console.WriteLine();
            }
            /*  var list1 = a.InOrder();
              int c = 0;
              int x = 0;
              while (list1.Count != 0)
              {
                  x++;
                  if (i== 1) {
                      Console.WriteLine();
                  }
                  if (x == 7) {
                      Console.WriteLine();
                  }
                  c = random.Next(list1.Count-1);

                  Console.WriteLine("\n"+list1[c] + "            "+ x+"\n");
                  a.LevelOrderConsole();
                  a.Delete(list1[c]);

                  list1.RemoveAt(c);
              }
              Console.WriteLine(i);*/
            //  }
            /*   for (int i = 0; i < 10000; i++)
               {
                   int randomCis = random.Next(1000000);
                   while (list.Contains(randomCis)) {
                       randomCis = random.Next(1000000);
                   }
                   list.Add(randomCis);
                   a.Add(randomCis);
               }
               int pocetExist = 0;
               int pocetNeexist = 0;
               Console.WriteLine(a.Count);
               for (int i = 0; i < 10000; i++)
               {
                   int randomCis = random.Next(list.Count);
                   while (!list.Contains(randomCis))
                   {
                       randomCis = random.Next(list.Count);
                   }
                   Console.WriteLine(i);
                   if (i == 19) {
                       Console.WriteLine();

                   }

                   if (a.Contains(randomCis)) {
                       list.Remove(randomCis);
                       a.Delete(randomCis);
                       pocetExist++;
                       Console.WriteLine("Exisutje pred mazanim"); }

                   if (!a.Contains(randomCis)) {

                       pocetNeexist++;
                       Console.WriteLine("Neexistuje po mazani"); }
               }
             //  a.LevelOrderConsole();
               Console.WriteLine(a.Count);
               Console.WriteLine(pocetExist +" "+ pocetNeexist);
               stopwatch.Stop();
               Console.WriteLine(stopwatch.Elapsed);
               Console.ReadKey();*/
            /* var a = new AVLTree<string>();
             int x = 0;
             for (char i = 'A';  ; i++,x++)
             {
                 var asdf = new TNode<string> { Value = i.ToString() };
                 a.Add(asdf);
                 if (x == 7) break;
             }
             x = 0;
             for (char i = 'Z'; ; i--, x++)
             {
                 var asdf = new TNode<string> { Value = i.ToString() };
                 a.Add(asdf);
                 if (x == 7) break;
             }
             x = 0;
             for (char i = 'P'; ; i--, x++)
             {
                 var asdf = new TNode<string> { Value = i.ToString() };
                 a.Add(asdf);
                 if (x == 7) break;
             }
             a.LevelOrderConsole();

             a.Delete("K");
             a.LevelOrderConsole();*/
            /* TNode<string> d = new TNode<string> { Value = "O" };
             a.Add(d);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> c = new TNode<string> { Value = "D" };
             a.Add(c);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> e = new TNode<string> { Value = "B" };
             a.Add(e);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> g = new TNode<string> { Value = "U" };
             a.Add(g);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> i = new TNode<string> { Value = "V" };
             a.Add(i);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> j = new TNode<string> { Value = "T" };
             a.Add(j);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> k = new TNode<string> { Value = "X" };
             a.Add(k);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> l = new TNode<string> { Value = "Z" };
             a.Add(l);
             a.LevelOrderConsole();
             Console.WriteLine();
             TNode<string> o = new TNode<string> { Value = "Y" };
             a.Add(o);
             a.LevelOrderConsole();*/
            Console.WriteLine();
        }
    }
}

