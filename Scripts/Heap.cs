using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    //parent (n-1)/2
    //child left 2n+1
    //child left 2n+2

    T[] items;
    int currentItemCount;

    public Heap(int maxSize)
    {
        items = new T[maxSize];
    }

    public void Add (T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public bool Contains (T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    public int Count
    {
        get { return currentItemCount; }
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    void SortDown (T item)
    {
        while (true)
        {
            int childOnLeft = item.HeapIndex * 2 + 1;
            int childOnRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childOnLeft < currentItemCount)
            {
                swapIndex = childOnLeft;

                if (childOnRight < currentItemCount)
                {
                    if (items[childOnLeft].CompareTo(items[childOnRight]) < 0)
                    {
                        swapIndex = childOnRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                } else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void SortUp (T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            } else
            {
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap (T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}
