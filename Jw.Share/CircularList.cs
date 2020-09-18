using System;
using System.Collections;
using System.Collections.Generic;

namespace Jw.Share
{
    /// <summary>
    /// 环形列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularList<T> : IEnumerable<T>, IEnumerator<T>
    {
        /// <summary>
        /// 元素列表
        /// </summary>
        protected T[] Items;
        /// <summary>
        /// 当前索引
        /// </summary>
        protected int Index;
        /// <summary>
        /// 是否已产生覆盖,队列用完时,从头开始覆盖
        /// </summary>
        protected bool Loaded;
        /// <summary>
        /// 当前遍历索引
        /// </summary>
        protected int EnumIdx;

        /// <summary>
        /// 环形队列构造,固定长度
        /// </summary>
        public CircularList(int numItems)
        {
            if (numItems <= 0)
            {
                throw new ArgumentOutOfRangeException("队列长度不能小于等于0");
            }

            Items = new T[numItems];
            Index = 0;
            Loaded = false;
            EnumIdx = -1;
        }

        /// <summary>
        /// 获取当前值
        /// </summary>
        public T Value
        {
            get => Items[Index];
            set => Items[Index] = value;
        }

        public bool HasLoaded => Loaded;

        /// <summary>
        /// 获取当前队列中的元素总数
        /// </summary>
        public int Count
        {
            get { return Loaded ? Items.Length : Index; }
        }

        /// <summary>
        /// 获取当前队列最长的长度
        /// </summary>
        public int Length
        {
            get { return Items.Length; }
        }

        /// <summary>
        /// 获取/设置指定索引的值
        /// </summary>
        public T this[int index]
        {
            get
            {
                RangeCheck(index);
                return Items[index];
            }
            set
            {
                RangeCheck(index);
                Items[index] = value;
            }
        }

        /// <summary>
        /// 移动索引到下一个元素
        /// </summary>
        public void Next()
        {
            if (++Index != Items.Length) return;
            Index = 0;
            Loaded = true;
        }

        /// <summary>
        /// 重置列表
        /// 清空元素,当前索引将归0
        /// </summary>
        public void Clear()
        {
            Index = 0;
            Items.Initialize();
            Loaded = false;
        }

        /// <summary>
        /// 设置所有元素的值,并将索引归0
        /// </summary>
        public void SetAll(T val)
        {
            Index = 0;
            Loaded = true;

            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = val;
            }
        }

        /// <summary>
        /// 检测给定的索引是否有效
        /// </summary>
        protected void RangeCheck(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(
                   "Indexer cannot be less than 0.");
            }

            if (index >= Items.Length)
            {
                throw new ArgumentOutOfRangeException(
                   "Indexer cannot be greater than or equal to the number of  items in the collection.");
            }
        }

        // Interface implementations:

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public T Current => this[EnumIdx];

        public void Dispose()
        {
        }

        object IEnumerator.Current => this[EnumIdx];

        public bool MoveNext()
        {
            ++EnumIdx;
            var ret = EnumIdx < Count;

            if (!ret)
            {
                Reset();
            }

            return ret;
        }

        public void Reset()
        {
            EnumIdx = -1;
        }
    }
}
