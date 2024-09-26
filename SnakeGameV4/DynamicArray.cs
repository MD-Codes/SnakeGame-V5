using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameV4
{
    internal class DynamicArray<T> : IEnumerable<T>
    {
        private class Node
        {
            public T data;
            public Node next;
            public Node prev;

            public Node(T data)
            {
                this.data = data;
                this.next = null;
                this.prev = null;
            }
        }

        private Node head;
        private Node tail;
        private int size;

        public int Size
        {
            get { return size; }
        }

        public bool IsEmpty
        {
            get { return size == 0; }
        }

        public int Count { get; internal set; }

        public void Clear()
        {
            head = null;
            tail = null;
            size = 0;
        }

        public void Add(T data)
        {
            AddLast(data);
        }

        public void AddFirst(T data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.next = head;
                head.prev = newNode;
                head = newNode;
            }
            size++;
        }

        public void AddLast(T data)
        {
            Node newNode = new Node(data);
            if (tail == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.prev = tail;
                tail.next = newNode;
                tail = newNode;
            }
            size++;
        }

        public T PeekFirst()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The list is empty.");
            }
            return head.data;
        }

        public T PeekLast()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The list is empty.");
            }
            return tail.data;
        }

        public void RemoveFirst()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            if (head == tail)
            {
                Clear();
                return;
            }

            head = head.next;
            head.prev = null;
            size--;
        }

        public void RemoveLast()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            if (head == tail)
            {
                Clear();
                return;
            }

            tail = tail.prev;
            tail.next = null;
            size--;
        }

        private void Remove(Node node)
        {
            if (node.prev != null)
            {
                node.prev.next = node.next;
            }
            else
            {
                head = node.next;
            }

            if (node.next != null)
            {
                node.next.prev = node.prev;
            }
            else
            {
                tail = node.prev;
            }

            size--;
        }

        public void Remove(T data)
        {
            Node current = head;
            while (current != null)
            {
                if (current.data.Equals(data))
                {
                    Remove(current);
                    break;
                }
                current = current.next;
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index", "Index is out of range.");
            }

            if (index == 0)
            {
                RemoveFirst();
                return;
            }

            if (index == size - 1)
            {
                RemoveLast();
                return;
            }

            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.next;
            }

            Remove(current);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.data;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
