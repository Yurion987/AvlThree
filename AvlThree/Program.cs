using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlThree
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new AVLTree<string>();
            TNode<string> d = new TNode<string> { Value = "O" };
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
            a.LevelOrderConsole();
            Console.WriteLine();
        }
    }
}

