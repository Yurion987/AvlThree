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
                    //newone je mensi ako tmpVrchol
                    if (newone.Value.CompareTo(tmpVrchol.Value) < 0)
                    {
                        if (tmpVrchol.Left == null)
                        {
                            newone.Parent = tmpVrchol;
                            tmpVrchol.Left = newone;
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
                            return;
                        }
                        tmpVrchol = tmpVrchol.Right;
                    }
                    if (newone.Value.CompareTo(tmpVrchol.Value) == 0)
                    {
                        Console.WriteLine("Vrchol ma rovnake ID ako nachadzany");
                        return;
                    }
                }
            }

            public void Deete(T todel)
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

        }

        public class TNode<T> where T : IComparable<T>
        {

            public T Value { get; set; }
            public TNode<T> Left { get; set; }
            public TNode<T> Right { get; set; }
            public TNode<T> Parent { get; set; }
            public int BalanceFactor { get; set; }


        }


    }

