using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlThree
{
    class AVLTree<T> where T : IComparable<T>
    {
        public TNode<T> Root { get; set; }
        public void Add(TNode<T> newone)
        {
            if (Root == null)
            {
                Root = newone;
                return;
            }
            var tmpVrchol = Root;
           
            while (true)
            {
                if (newone.Value.CompareTo(tmpVrchol.Value) == 0)
                {
                    Console.WriteLine("Vrchol ma rovnake ID ako nachadzany");
                    return;
                }
                //newone je mensi ako tmpVrchol
                if (newone.Value.CompareTo(tmpVrchol.Value) < 0)
                {
                    if (tmpVrchol.Left == null)
                    {
                        newone.Parent = tmpVrchol;
                        tmpVrchol.Left = newone;
                        Zrotuj(newone);
                        return;
                    }
                    tmpVrchol = tmpVrchol.Left;
                }
                // newone je vecsi ako tmpVrchol
                if (newone.Value.CompareTo(tmpVrchol.Value) > 0)
                {
                    if (tmpVrchol.Right == null)
                    {
                        newone.Parent = tmpVrchol;
                        tmpVrchol.Right = newone;
                        Zrotuj(newone);
                        return;
                    }
                    tmpVrchol = tmpVrchol.Right;
                }

            }
        }
        public void Zrotuj(TNode<T> inserted) {
            string rotacia = "";
            if (inserted == Root) return ;
            while(inserted!= Root)
            {
                rotacia += inserted.Parent.Left == inserted ? "L" : "R";
                inserted = inserted.Parent;
                inserted.Vyska = Math.Max(inserted.Left == null ? 0 : inserted.Left.Vyska + 1, inserted.Right == null ? 0 : inserted.Right.Vyska + 1);
                var vyskaPraveho = inserted.Right == null ? 0 : inserted.Right.Vyska+1;
                var vyskaLaveho = inserted.Left == null ? 0 : inserted.Left.Vyska+1;
                if (Math.Abs(vyskaLaveho - vyskaPraveho) >= 2) {
                    var tmp = rotacia.Substring(rotacia.Length - 2);
                    if (tmp == "RR")
                    {
                        LavaRotacia(inserted);
                        return;
                    } else if (tmp == "LL") {
                        PravaRotacia(inserted);
                        return;
                    }
                    //RL rotacia opacne ako string
                    else if (tmp == "LR")
                    {
                        PravaRotacia(inserted.Right);
                        LavaRotacia(inserted);
                        return;
                    }
                    //LR rotacia opacne ako string
                    else if (tmp == "RL")
                    {
                        LavaRotacia(inserted.Left);
                        PravaRotacia(inserted);
                        return;
                    }
                }
            }


        }
        public void Delete(T Node)
        {

        }
        public void PravaRotacia(TNode<T> rotovanyVrch)
        {
            //ma laveho syna 
            if (rotovanyVrch.Left != null)
            {
                var tmpVrchol = rotovanyVrch.Left;

                // syn ma praveho syna
                if (rotovanyVrch.Left.Right != null)
                {
                    rotovanyVrch.Left = tmpVrchol.Right;
                    tmpVrchol.Right = rotovanyVrch;
                    rotovanyVrch.Left.Parent = rotovanyVrch;
                }
                //syn nema praveho syna
                else
                {
                    rotovanyVrch.Left = null;
                    tmpVrchol.Right = rotovanyVrch;

                }

                //rotovany vrchol ma parenta
                if (rotovanyVrch.Parent != null)
                {
                    // som lavy syn parenta
                    if (rotovanyVrch.Parent.Left == rotovanyVrch)
                    {
                        rotovanyVrch.Parent.Left = tmpVrchol;
                    }
                    else
                    {
                        rotovanyVrch.Parent.Right = tmpVrchol;
                    }

                    tmpVrchol.Parent = rotovanyVrch.Parent;
                    rotovanyVrch.Parent = tmpVrchol;

                }
                else
                {
                    tmpVrchol.Parent = null;
                    Root = tmpVrchol;
                    rotovanyVrch.Parent = tmpVrchol;
                }
                
                rotovanyVrch.Vyska = Math.Max(rotovanyVrch.Left == null ? 0 :rotovanyVrch.Left.Vyska +1, rotovanyVrch.Right == null ? 0 : rotovanyVrch.Right.Vyska +1);   
                tmpVrchol.Vyska = Math.Max(tmpVrchol.Left == null ? 0 : tmpVrchol.Left.Vyska + 1, tmpVrchol.Right == null ? 0 : tmpVrchol.Right.Vyska + 1);
            }
        }

        public void LavaRotacia(TNode<T> rotovanyVrch)
        {
            //ma praveho syna 
            if (rotovanyVrch.Right != null)
            {
                var tmpVrchol = rotovanyVrch.Right;

                // syn ma laveho syna
                if (rotovanyVrch.Right.Left != null)
                {
                    rotovanyVrch.Right = tmpVrchol.Left;
                    tmpVrchol.Left = rotovanyVrch;
                    rotovanyVrch.Right.Parent = rotovanyVrch;
                }
                //syn nema laveho syna
                else
                {
                    rotovanyVrch.Right = null;
                    tmpVrchol.Left = rotovanyVrch;

                }

                //rotovany vrchol ma parenta
                if (rotovanyVrch.Parent != null)
                {
                    // som pravy syn parenta
                    if (rotovanyVrch.Parent.Right == rotovanyVrch)
                    {
                        rotovanyVrch.Parent.Right = tmpVrchol;
                    }
                    else
                    {
                        rotovanyVrch.Parent.Left = tmpVrchol;
                    }

                    tmpVrchol.Parent = rotovanyVrch.Parent;
                    rotovanyVrch.Parent = tmpVrchol;

                }
                else
                {
                    tmpVrchol.Parent = null;
                    Root = tmpVrchol;
                    rotovanyVrch.Parent = tmpVrchol;
                }
                rotovanyVrch.Vyska = Math.Max(rotovanyVrch.Right == null ? 0 : rotovanyVrch.Right.Vyska + 1, rotovanyVrch.Left == null ? 0 : rotovanyVrch.Left.Vyska + 1);
                tmpVrchol.Vyska = Math.Max(tmpVrchol.Right == null ? 0 : tmpVrchol.Right.Vyska + 1, tmpVrchol.Left == null ? 0 : tmpVrchol.Left.Vyska + 1);
            }
        }
        public void InOrder(TNode<T> vrchol)
        {
            if (vrchol.Left != null)
            {
                InOrder(vrchol.Left);
            }

            Console.Write(vrchol.Value);
            if (vrchol.Right != null)
            {
                InOrder(vrchol.Right);
            }
        }
        public void LevelOrderConsole()
        {
            Queue<TNode<T>> stack = new Queue<TNode<T>>();
            stack.Enqueue(Root);
            List<TNode<T>> tree = new List<TNode<T>>();
            while (stack.Count > 0 && stack.Count < Math.Pow(2, Root.Vyska + 2))
            {
                var actual = stack.Dequeue();
                tree.Add(actual);
                if (actual == null)
                {
                    stack.Enqueue(null);
                    stack.Enqueue(null);
                }
                else
                {
                    if (actual.Left != null) stack.Enqueue(actual.Left);
                    else stack.Enqueue(null);
                    if (actual.Right != null) stack.Enqueue(actual.Right);
                    else stack.Enqueue(null);
                }
            }

            int height = Root.Vyska;
            int deep = 0;
            string indent = new string(' ', (int)(Math.Pow(2, Root.Vyska) - 1));
            string spaces = new string(' ', (int)(Math.Pow(2, Root.Vyska + 1) - 1));
            int size = (int)Math.Pow(2, Root.Vyska + 1) ;
            int count = 1;
            int index = 0;
            for (int i = 0; i < size; i += (int)Math.Pow(2, deep))
            {
                Console.Write(indent);
                for (int j = index; j < count; j++)
                {
                    if (tree.ToArray()[j] != null) Console.Write(tree.ToArray()[j].Value );
                    else Console.Write(" ");
                    Console.Write(spaces);
                    index++;
                }

                Console.WriteLine();

                deep++;
                indent = new string(' ', (int)(Math.Pow(2, (height - deep)) - 1));
                spaces = new string(' ', (int)(Math.Pow(2, (height - deep) + 1) - 1));
                count += (int)Math.Pow(2, deep);

            }
        }
    }

    public class TNode<T> where T : IComparable<T>
    {

        public T Value { get; set; }
        public TNode<T> Left { get; set; }
        public TNode<T> Right { get; set; }
        public TNode<T> Parent { get; set; }
        public int Vyska { get; set; }


    }


}

