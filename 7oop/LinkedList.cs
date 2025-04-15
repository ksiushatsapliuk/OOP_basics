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

        // Додавання елемента в кінець списку
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

        // Функція індексації - отримання елемента за індексом
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Індекс поза межами списку");
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
                    throw new IndexOutOfRangeException("Індекс поза межами списку");
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
                Console.WriteLine("Список порожній");
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

        // Знаходження середнього значення елементів списку
        public double GetAverage()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Список порожній");
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

        // 1. Знайти перший елемент більший за середнє значення
        public double FindFirstGreaterThanAverage()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Список порожній");
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

            throw new InvalidOperationException("Не знайдено елементів більших за середнє значення");
        }

        // 2. Знайти суму елементів, значення яких більше за задане
        public double SumOfElementsGreaterThan(double threshold)
        {
            if (head == null)
            {
                throw new InvalidOperationException("Список порожній");
            }

            double sum = 0;
            Node current = head;

            while (current != null)
            {
                if (current.Value > threshold)
                {
                    sum += current.Value;
                }
                current = current.Next;
            }

            return sum;
        }

        // 3. Отримати новий список зі значень елементів менших за середнє значення
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

        // 4. Видалити елементи, що розташовані на парних позиціях (нумерація починається з 0)
        public void RemoveElementsAtEvenPositions()
        {
            if (head == null)
            {
                return;
            }

            // Особливий випадок для голови списку (позиція 0)
            if (head != null)
            {
                head = head.Next;
                count--;
            }

            // Видаляємо решту елементів на парних позиціях
            Node current = head;
            int position = 1;

            while (current != null && current.Next != null)
            {
                if (position % 2 == 1) // Якщо попередня позиція непарна, то наступна - парна
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

        // Отримання кількості елементів у списку
        public int Count
        {
            get { return count; }
        }
    }
}
