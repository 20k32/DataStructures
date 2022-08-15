namespace Set
{
    public class GenSet<T>
    {
        public event Action<string> print;
        protected List<T> Data = new List<T>();
        public int Size => Data.Count;
        public GenSet() { }
        public GenSet(T el)
        {
            Add(el);
        }
        public void Add(T el)
        {
            if (Data.Contains(el)) return;
            Data.Add(el);
        }
        public void Remove(int index)
        {
            if (index > Data.Count || index < 0) return;
            Data.RemoveAt(index);
        }
        public T Get(int index)
        {
            if (index > Data.Count || index < 0) return default(T);
            return Data[index];
        }
        public void Print()
        {
            foreach (var item in Data)
            {
                print?.Invoke(item?.ToString() ?? throw new Exception("No such element found in list"));
            }
        }
        public static List<T> Union(GenSet<T> set1, GenSet<T> set2)
        {
            List<T> reslist = new List<T>();
            for (int i = 0; i < set1.Size; i++)
                reslist.Add(set1.Get(i));
            for (int i = 0; i < set2.Size; i++)
                reslist.Add(set2.Get(i));
            return reslist.Distinct().ToList();
        }
        public GenSet<T> Union(GenSet<T> set2)
        {
            GenSet<T> ResSet = new GenSet<T>();
            foreach (var item in Data)
            {
                ResSet.Add(item);
            }
            foreach (var item in set2.Data)
            {
                ResSet.Add(item);
            }
            return ResSet;
        }
        public static List<T> Difference(GenSet<T> set1, GenSet<T> set2)
        {
            List<T> reslist = new List<T>();
            for (int i = 0; i < set1.Size; i++)
            {
                reslist.Add(set1.Data[i]);
            }
            for (int j = 0; j < set2.Size; j++)
            {
                if (set1.Data.Contains(set2.Data[j]))
                {
                    reslist.Remove(set2.Data[j]);
                }
            }
            return reslist;
        }
        public GenSet<T> Difference(GenSet<T> set2)
        {
            GenSet<T> ResSet = new GenSet<T>();
            for (int i = 0; i < Size; i++)
            {
                ResSet.Add(Data[i]);
            }
            for (int j = 0; j < set2.Size; j++)
            {
                if (Data.Contains(set2.Data[j]))
                {
                    ResSet.Data.Remove(set2.Data[j]);
                }
            }
            return ResSet;
        }
        public static List<T> SymDifference(GenSet<T> set1, GenSet<T> set2)
        {
            List<T> reslist1 = Difference(set1, set2);
            List<T> reslist2 = Difference(set2, set1);
            foreach (var item in reslist2)
            {
                if (reslist1.Contains(item)) continue;
                reslist1.Add(item);
            }
            return reslist1;
        }
        public GenSet<T> SymDifference(GenSet<T> set2)
        {
            GenSet<T> ResSet1 = Difference(set2);
            GenSet<T> ResSet2 = set2.Difference(this);
            foreach (var item in ResSet2.Data)
            {
                ResSet1.Add(item);
            }
            return ResSet1;
        }
        public static List<T> Intersection(GenSet<T> set1, GenSet<T> set2)
        {
            List<T> reslist = new List<T>();

            for (int i = 0; i < set2.Data.Count; i++)
            {
                if (set1.Data.Contains(set2.Data[i]))
                {
                    reslist.Add(set2.Data[i]);
                }
            }
            return reslist;
        }
        public GenSet<T> Intersection(GenSet<T> set2)
        {
            GenSet<T> ResSet = new GenSet<T>();

            for (int i = 0; i < set2.Data.Count; i++)
            {
                if (Data.Contains(set2.Data[i]))
                {
                    ResSet.Add(set2.Data[i]);
                }
            }
            return ResSet;
        }
        public bool IsSubset(GenSet<T> set2)
        {
            if (set2.Data.Count >= Data.Count)
                return false;
            bool t = true;
            foreach (var item in set2.Data)
            {
                if (!Data.Contains(item))
                    t = false;
            }
            return t;
        }
        public override string ToString()
        {
            string s = "";
            foreach (var item in Data)
            {
                s += item + "\n";
            }
            return s;
        }
    }
}
