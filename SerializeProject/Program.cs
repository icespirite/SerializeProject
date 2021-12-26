using System;
using System.IO;
using System.Text;

namespace SerializeProject
{
  public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random; // произвольный элемент внутри списка
        public string Data;
       

        public ListNode(string data)
        {
            Data = data;
            
        }
    }
  public class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        private int[] masIdRandomElement;

        public void Add(string data)
        {
            ListNode node = new ListNode(data);

            if (Head == null)
                Head = node;
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }
            Tail = node;
            Count++;
        }
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }
        public void SetRandom()
        {
            Random rand = new Random();
            masIdRandomElement = new int[Count];
            for (int i = 0; i < Count; i++)
            {               
                masIdRandomElement[i] = rand.Next(0, Count);                
                Get(i).Random = Get(masIdRandomElement[i]);
               
            }
        }
        public ListNode Get(int index)
        {
            ListNode currentNode = Head;
            if (Count <= index) throw new ArgumentException();
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;                
            }
            return currentNode;

        }
        public void Serialize(Stream s)
        {
            ListNode currentNode;
           
            s.Write(BitConverter.GetBytes(Count));
            for (int i = 0; i < Count; i++)
            {
                currentNode = Get(i);
                byte[] bytes = Encoding.UTF8.GetBytes(currentNode.Data);
                s.Write(BitConverter.GetBytes(bytes.Length));
                s.Write(bytes);
                s.Write(BitConverter.GetBytes(masIdRandomElement[i])); 
                
            }            
           
        }
        public void Deserialize(Stream s)
        {
            byte[] buff = new byte[4];
            s.Read(buff);
            int count = BitConverter.ToInt32(buff);
            int[] masIdRandomElements = new int[count];
            Clear();
            for (int i = 0; i < count; i++)
            {
                s.Read(buff);
                int stringSize = BitConverter.ToInt32(buff);
                byte[] stringByte = new byte[stringSize];
                s.Read(stringByte);
                Add(Encoding.UTF8.GetString(stringByte));
                s.Read(buff);
                
                masIdRandomElements[i] = BitConverter.ToInt32(buff);
            }
            for (int i = 0; i < count; i++)
            {
                Get(i).Random = Get(masIdRandomElements[i]);
                
            }
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
             
        }
    }
}
