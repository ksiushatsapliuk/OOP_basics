using System.Collections;

namespace LinkedListLib
{
        public class LinkedList : IEnumerable<double>
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
                    newNode.Next = head;
                    head = newNode;
                }
                count++;
            }

            public double this[int index]
            {
                get
                {
                    if (index < 0 || index >= count)
                    {
                        throw new IndexOutOfRangeException("Index out of range.");
                    }

                    Node current = head;
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                    return current.Value;
                }
            }

            public double GetAverage()
            {
                if (head == null)
                {
                    throw new InvalidOperationException("List is empty.");
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
                    throw new InvalidOperationException("List is empty.");
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

                throw new InvalidOperationException("No elements found greater than the average value.");
            }

            public double SumOfElementsGreater(double threshold)
            {
                if (head == null)
                {
                    throw new InvalidOperationException("List is empty.");
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
                    throw new InvalidOperationException("No element in the list exceeds the given value.");
                }

                return sum;
            }

            public LinkedList GetNewWithElementsLessThanAverage()
            {
                if (head == null)
                {
                    throw new InvalidOperationException("List is empty");
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

            public void RemoveAtEvenPositions()
            {
                if (head == null)
                {
                    return;
                }

                if (head != null)
                {
                    head = head.Next;
                    count--;
                }

                Node current = head;
                int position = 1;

                while (current != null && current.Next != null)
                {
                    if (position % 2 == 1)
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
            public void RemoveAt(int index)
            {
                if (head == null)
                {
                    throw new InvalidOperationException("List is empty.");
                }

                if (index < 0 || index >= count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
                }

                if (index == 0)
                {
                    head = head.Next;
                }
                else
                {
                    Node current = head;
                    for (int i = 0; i < index - 1; i++)
                    {
                        current = current.Next;
                    }

                    current.Next = current.Next?.Next;
                }

                count--;
            }

            public int Count
            {
                get { return count; }
            }
            public IEnumerator<double> GetEnumerator()
            {
                Node current = head;
                while (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }
}