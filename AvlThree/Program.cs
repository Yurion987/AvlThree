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
            var a = new BTree<double>();
            //    TNode<double> b = new TNode<double> { Value = 10 };
            //    a.Add(b);
            TNode<double> d = new TNode<double> { Value = 10 };
            a.Add(d);
            TNode<double> c = new TNode<double> { Value = 8 };
            a.Add(c);
            TNode<double> e = new TNode<double> { Value = 6 };
            a.Add(e);
            TNode<double> g = new TNode<double> { Value = 5 };
            a.Add(g);
            TNode<double> i = new TNode<double> { Value = 7 };
            a.Add(i);
            //       TNode<double> j = new TNode<double> { Value = 8.5 };
            //     a.Add(j);
            //       TNode<double> k = new TNode<double> { Value = 11 };
            //      a.Add(k);
            a.InOrder(a.Root);
            Console.WriteLine();
            a.PravaRotacia(c);
            a.InOrder(a.Root);
            Console.WriteLine();
            a.LavaRotacia(e);
            a.InOrder(a.Root);
        }
    }
    }
}
