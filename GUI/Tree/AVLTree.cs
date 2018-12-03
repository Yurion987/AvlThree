using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class AVLTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public TNode<T> Root { get; set; }
        public int Count { get; set; }
        public bool Add(T newData)
        {
            var newone = new TNode<T>() { Value = newData };
            if (Root == null)
            {
                Root = newone;
                Count++;
                return true;
            }
            var tmpVrchol = Root;

            while (true)
            {
                if (newone.Value.CompareTo(tmpVrchol.Value) == 0)
                {
                    return false;
                }
                //newone je mensi ako tmpVrchol
                if (newone.Value.CompareTo(tmpVrchol.Value) < 0)
                {
                    if (tmpVrchol.Left == null)
                    {
                        newone.Parent = tmpVrchol;
                        tmpVrchol.Left = newone;
                        Zrotuj(newone);
                        Count++;
                        return true;
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
                        Count++;
                        return true;
                    }
                    tmpVrchol = tmpVrchol.Right;
                }

            }
        }
        private void Zrotuj(TNode<T> inserted)
        {
            string rotacia = "";
            if (inserted == Root) return;
            while (inserted != Root)
            {
                rotacia += inserted.Parent.Left == inserted ? "L" : "R";
                inserted = inserted.Parent;
                inserted.Vyska = Math.Max(inserted.Left == null ? 0 : inserted.Left.Vyska + 1, inserted.Right == null ? 0 : inserted.Right.Vyska + 1);
                var vyskaPraveho = inserted.Right == null ? 0 : inserted.Right.Vyska + 1;
                var vyskaLaveho = inserted.Left == null ? 0 : inserted.Left.Vyska + 1;
                if (Math.Abs(vyskaLaveho - vyskaPraveho) >= 2)
                {
                    var tmp = rotacia.Substring(rotacia.Length - 2);
                    if (tmp == "RR")
                    {
                        LavaRotacia(inserted);
                        return;
                    }
                    else if (tmp == "LL")
                    {
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
        private TNode<T> FindNode(T data)
        {
            var tmpVrch = Root;
            while (true)
            {
                if (tmpVrch.Value.CompareTo(data) > 0)
                {
                    if (tmpVrch.Left == null)
                    {

                        throw new Exception("Nenajdeny prvok");
                    }
                    tmpVrch = tmpVrch.Left;
                }
                else
                if (tmpVrch.Value.CompareTo(data) < 0)
                {
                    if (tmpVrch.Right == null)
                    {

                        throw new Exception("Nenajdeny prvok");
                    }
                    tmpVrch = tmpVrch.Right;
                }
                else if (tmpVrch.Value.CompareTo(data) == 0)
                {
                    return tmpVrch;
                }
                else if (tmpVrch == null)
                {

                    throw new Exception("Nenajdeny prvok");
                }

            }
        }
        public T Find(T data) {
            var tmpVrch = Root;
            if (tmpVrch == null) return default(T); 
            while (true)
            {
                if (tmpVrch.Value.CompareTo(data) > 0)
                {
                    if (tmpVrch.Left == null)
                    {
                        return default(T);
                    }
                    tmpVrch = tmpVrch.Left;
                }
                else
                if (tmpVrch.Value.CompareTo(data) < 0)
                {
                    if (tmpVrch.Right == null)
                    {

                        return default(T);
                    }
                    tmpVrch = tmpVrch.Right;
                }
                else if (tmpVrch.Value.CompareTo(data) == 0)
                {
                    return tmpVrch.Value;
                }
                else if (tmpVrch == null)
                {

                    return default(T);
                }

            }
        }
        public void Delete(T data)
        {
            if (Root == null) throw new Exception("Mazanie v prazdnom strome");
            var hladany = FindNode(data);
            Count = Count - 1;
            if (hladany == Root && Root.Vyska == 0)
            {
                Root = null;
                return;
            }
            var otecNahradneho = new TNode<T>();
            var tmp = hladany;
            
            // ma praveho nasledovnika
            if (tmp.Right != null)
            {
                tmp = tmp.Right;
                // nasledovnik ma lavych synov
                if (tmp.Left != null)
                {
                    while (true)
                    {
                        if (tmp.Left != null)
                        {
                            tmp = tmp.Left;
                        }
                        else
                        {
                            Swap(ref hladany, ref tmp);
                            otecNahradneho = tmp.Parent;
                            if (tmp.Right != null)
                            {
                                tmp.Right.Parent = tmp.Parent;
                                tmp.Parent.Left = tmp.Right;
                            }
                            else
                            {
                                tmp.Parent.Left = null;
                            }
                            
                            Balance(otecNahradneho);
                            break;
                        }
                    }

                }
                // nasledovnik nema lavych synov
                else
                {
                    // nasledovnik nema ziadneho syna
                    tmp.Parent = hladany.Parent;
                    if (hladany != Root)
                    {
                        if (hladany.Parent.Right == hladany)
                        {
                            hladany.Parent.Right = tmp;
                        }
                        else
                        {
                            hladany.Parent.Left = tmp;
                        }
                    }
                    else
                    {
                        Root = tmp;
                    }
                    if (hladany.Left != null)
                    {
                        tmp.Left = hladany.Left;
                        tmp.Left.Parent = tmp;
                    }
                    
                    Balance(tmp);
                }
            }
            else
            {
                if (hladany.Left != null)
                {
                    //tento if doplneny
                    if (hladany == Root)
                    {
                        Root = hladany.Left;
                        hladany.Left.Parent = null;
                    }
                    else
                    {
                        hladany.Left.Parent = hladany.Parent;
                        if (hladany.Parent.Left == hladany)
                        {
                            hladany.Parent.Left = hladany.Left;
                        }
                        else
                        {
                            hladany.Parent.Right = hladany.Left;
                        }
                    }
                }
                else
                {
                    if (hladany.Parent.Left == hladany)
                    {
                        hladany.Parent.Left = null;
                    }
                    else
                    {
                        hladany.Parent.Right = null;
                    }
                }
                Balance(hladany.Parent);
               
                if (hladany == Root) tmp = Root;
            }
        }
        private void Balance(TNode<T> node)
        {


            while (node != null)
            {
                var vyskaPraveho = node.Right == null ? 0 : node.Right.Vyska + 1;
                var vyskaLaveho = node.Left == null ? 0 : node.Left.Vyska + 1;

                node.Vyska = Math.Max(vyskaPraveho, vyskaLaveho);

                if (Math.Abs(vyskaLaveho - vyskaPraveho) >= 2)
                {
                    node = DeleteRotacie(node);
                }
                else
                {
                    node = node.Parent;
                }

            }
            if (Root != null)
            {
                Root.Vyska = Math.Max(Root.Left == null ? 0 : Root.Left.Vyska + 1, Root.Right == null ? 0 : Root.Right.Vyska + 1);
            }
        }
        private TNode<T> DeleteRotacie(TNode<T> node)
        {
            string rotacia = "";
            var tmp = node;
            while (rotacia.Length < 2 && node != null)
            {
                if (rotacia.Length == 1 && node.Right != null && node.Left != null)
                {
                    if (node.Right.Vyska > node.Left.Vyska)
                    {
                        rotacia += "R";
                    }
                    else if (node.Left.Vyska > node.Right.Vyska)
                    {
                        rotacia += "L";
                    }
                    else
                    {
                        rotacia += rotacia;

                    }
                }
                else
                {
                    if ((node.Right == null ? 0 : node.Right.Vyska + 1) >= (node.Left == null ? 0 : node.Left.Vyska + 1))
                    {
                        rotacia += "R";
                        node = node.Right;
                    }
                    else
                    {

                        rotacia += "L";
                        node = node.Left;
                    }
                }
                if (rotacia.Length >= 2)
                {
                    if (rotacia == "RR")
                    {
                        LavaRotacia(tmp);

                    }
                    else if (rotacia == "LL")
                    {
                        PravaRotacia(tmp);

                    }
                    else if (rotacia == "RL")
                    {
                        PravaRotacia(tmp.Right);
                        LavaRotacia(tmp);
                    }
                    else if (rotacia == "LR")
                    {
                        LavaRotacia(tmp.Left);
                        PravaRotacia(tmp);
                    }
                }
            }
            return tmp;
        }
        private void Swap(ref TNode<T> deletovany, ref TNode<T> nahradny)
        {
            var tmpData = deletovany.Value;
            deletovany.Value = nahradny.Value;
            nahradny.Value = tmpData;

        }
        private void PravaRotacia(TNode<T> rotovanyVrch)
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

                rotovanyVrch.Vyska = Math.Max(rotovanyVrch.Left == null ? 0 : rotovanyVrch.Left.Vyska + 1, rotovanyVrch.Right == null ? 0 : rotovanyVrch.Right.Vyska + 1);
                tmpVrchol.Vyska = Math.Max(tmpVrchol.Left == null ? 0 : tmpVrchol.Left.Vyska + 1, tmpVrchol.Right == null ? 0 : tmpVrchol.Right.Vyska + 1);
            }
        }
        private void LavaRotacia(TNode<T> rotovanyVrch)
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
        public List<T> InOrder()
        {
            var list = new List<T>();

            if (Root == null) return null;
            Stack<TNode<T>> stack = new Stack<TNode<T>>();
            TNode<T> tmp_actual = Root;
            TNode<T> tmp_popped;

            stack.Push(Root);
            while (stack.Count > 0 || tmp_actual != null)
            {
                while (tmp_actual != null)
                {
                    if (tmp_actual != Root) stack.Push(tmp_actual);
                    tmp_actual = tmp_actual.Left;
                }

                tmp_popped = stack.Pop();
                list.Add(tmp_popped.Value);
                tmp_actual = tmp_popped.Right;

            }

            return list;

        }
        public void VyskaKontrola()
        {



            Stack<TNode<T>> stack = new Stack<TNode<T>>();
            TNode<T> tmp_actual = Root;
            TNode<T> tmp_popped;

            stack.Push(Root);
            while (stack.Count > 0 || tmp_actual != null)
            {
                while (tmp_actual != null)
                {
                    if (tmp_actual != Root) stack.Push(tmp_actual);
                    tmp_actual = tmp_actual.Left;
                }

                tmp_popped = stack.Pop();
                if (tmp_popped.Left == null && tmp_popped.Right == null)
                {
                    if (tmp_popped.Vyska == 0)
                    {
                        //          Console.Write("");
                    }
                    else
                    {
                        throw new Exception("ZLA VYSKA");
                    }
                }
                else if (tmp_popped.Left != null && tmp_popped.Right == null)
                {
                    if (tmp_popped.Left.Vyska + 1 == tmp_popped.Vyska)
                    {
                        //           Console.Write("");
                    }
                    else
                    {
                        throw new Exception("ZLA VYSKA");
                    }
                }
                else if (tmp_popped.Left == null && tmp_popped.Right != null)
                {
                    if (tmp_popped.Right.Vyska + 1 == tmp_popped.Vyska)
                    {
                        //            Console.Write("");
                    }
                    else
                    {
                        throw new Exception("ZLA VYSKA");
                    }
                }
                else if (tmp_popped.Left != null && tmp_popped.Right != null)
                {
                    var max = Math.Max(tmp_popped.Right.Vyska + 1, tmp_popped.Left.Vyska + 1);
                    if (max == tmp_popped.Vyska)
                    {
                        //          Console.Write("");
                    }
                    else
                    {
                        throw new Exception("ZLA VYSKA");
                    }
                }

                tmp_actual = tmp_popped.Right;

            }

            //    Console.WriteLine();





        }
        public bool Contains(T data) {
            try
            {
                return Find(data) != null;
            }
            catch (Exception)
            {
                return false;
            }
              
        }
        public T Max()
        {
            if (Root!= null) {
                var tmpNode = Root;
                var tmpValue = Root.Value;
                while (true)
                {
                    if (tmpNode.Right != null)
                    {
                        tmpNode = tmpNode.Right;
                        tmpValue = tmpNode.Value;
                    }
                    else
                    {
                        break;
                    }
                }
                return tmpValue;
            }
           return default(T);
            
        }
        public void Clear() {
            Root = null;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new MojEnumerator<T>(Root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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


    class MojEnumerator<T> : IEnumerator<T> where T : IComparable<T>
    {
        
        private T _result;
        Stack<TNode<T>> _stack = new Stack<TNode<T>>();
        private TNode<T> _current;
      
        public MojEnumerator(TNode<T> node)
        {
            _current = node;
        }

        public void Dispose()
        {
            _current = null;
        }

        public bool MoveNext()
        {
            while (_stack.Count > 0 || _current != null)
            {
                if (_current != null)
                {
                    _stack.Push(_current);
                    _current = _current.Left;
                }
                else
                {
                    _current = _stack.Pop();
                    _result = _current.Value;
                    _current = _current.Right;
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public T Current
        {
            get { return _result; }
        }

        object IEnumerator.Current => Current;
    }

}

