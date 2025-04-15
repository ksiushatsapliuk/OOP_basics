using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7oop
{
    class Node
    {
        public double Value { get; set; }
        public Node Next { get; set; }

        public Node(double value)
        {
            Value = value;
            Next = null;
        }
    }
    class LinkedList
    {
        private Node head;
        private int count;

        public LinkedList()
        {
            head = null;
            count = 0;
        }

        public void Add(double value)
        {
            Node newNode = new Node(value);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        //індексатор
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Індекс поза межами списку.");
                }

                Node current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Value;
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Індекс поза межами списку.");
                }

                Node current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Value = value;
            }
        }

        public void PrintList()
        {
            if (head == null)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Node current = head;
            int index = 0;
            while (current != null)
            {
                Console.WriteLine($"[{index}] = {current.Value}");
                current = current.Next;
                index++;
            }
        }

        public double GetAverage()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Список порожній.");
            }

            double sum = 0;
            int count = 0;
            Node current = head;

            while (current != null)
            {
                sum += current.Value;
                count++;
                current = current.Next;
            }

            return sum / count;
        }

        public double FindFirstGreaterThanAverage()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Список порожній.");
            }

            double average = GetAverage();
            Node current = head;

            while (current != null)
            {
                if (current.Value > average)
                {
                    return current.Value;
                }
                current = current.Next;
            }

            throw new InvalidOperationException("Не знайдено елементів більших за середнє значення.");
        }

        public double SumOfElementsGreaterThan(double threshold)
        {
            if (head == null)
            {
                throw new InvalidOperationException("Список порожній.");
            }

            double sum = 0;
            bool found = false;
            Node current = head;

            while (current != null)
            {
                if (current.Value > threshold)
                {
                    sum += current.Value;
                    found = true;
                }
                current = current.Next;
            }
            if (!found)
            {
                throw new InvalidOperationException("Жоден елемент списку не перевищує задане значення.");
            }

            return sum;
        }

        public LinkedList GetNewListWithElementsLessThanAverage()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Список порожній");
            }

            double average = GetAverage();
            LinkedList newList = new LinkedList();
            Node current = head;

            while (current != null)
            {
                if (current.Value < average)
                {
                    newList.Add(current.Value);
                }
                current = current.Next;
            }

            return newList;
        }

        public void RemoveElementsAtEvenPositions()
        {
            if (head == null)
            {
                return;
            }

            //випадок для голови списку (позиція 0)
            if (head != null)
            {
                head = head.Next;
                count--;
            }

            Node current = head;
            int position = 1;

            while (current != null && current.Next != null)
            {
                if (position % 2 == 1) //якщо попередня позиція непарна, то наступна - парна
                {
                    current.Next = current.Next.Next;
                    count--;
                }
                else
                {
                    current = current.Next;
                }
                position++;
            }
        }

        public int Count
        {
            get { return count; }
        }
    }
}
