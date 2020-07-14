using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace Jw.Winform.Ctrls.JwNavBar
{
    public class NavbarNodeCollection : IList, ICollection, IEnumerable
    {
		private JwNavBarNode owner;

		private int lastAccessedIndex = -1;

		private int fixedIndex = -1;

		internal int FixedIndex
		{
			get
			{
				return this.fixedIndex;
			}
			set
			{
				this.fixedIndex = value;
			}
		}

		public virtual JwNavBarNode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.owner.Nodes.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (JwNavBarNode)this.owner.Nodes[index];
			}
			set
			{
				if (index < 0 || index >= this.owner.Nodes.Count)
				{
					throw new ArgumentOutOfRangeException("index", "");
				}
				value.Parent = this.owner;
				value.index = index;
				this.owner.children[index] = value;
				value.Realize(false);
			}
		}

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is TreeNode)
				{
					this[index] = (TreeNode)value;
					return;
				}
				throw new ArgumentException(SR.GetString("TreeNodeCollectionBadTreeNode"), "value");
			}
		}

		public virtual TreeNode this[string key]
		{
			get
			{
				if (string.IsNullOrEmpty(key))
				{
					return null;
				}
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					return this[index];
				}
				return null;
			}
		}

		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.owner.childCount;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		internal TreeNodeCollection(TreeNode owner)
		{
			this.owner = owner;
		}

		public virtual TreeNode Add(string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Add(treeNode);
			return treeNode;
		}

		public virtual TreeNode Add(string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Add(treeNode);
			return treeNode;
		}

		public virtual TreeNode Add(string key, string text, int imageIndex)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageIndex = imageIndex;
			this.Add(treeNode);
			return treeNode;
		}

		public virtual TreeNode Add(string key, string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			this.Add(treeNode);
			return treeNode;
		}

		public virtual TreeNode Add(string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
			treeNode.Name = key;
			this.Add(treeNode);
			return treeNode;
		}

		public virtual TreeNode Add(string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			this.Add(treeNode);
			return treeNode;
		}

		public virtual void AddRange(TreeNode[] nodes)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			if (nodes.Length == 0)
			{
				return;
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && nodes.Length > 200)
			{
				treeView.BeginUpdate();
			}
			this.owner.Nodes.FixedIndex = this.owner.childCount;
			this.owner.EnsureCapacity(nodes.Length);
			for (int i = nodes.Length - 1; i >= 0; i--)
			{
				this.AddInternal(nodes[i], i);
			}
			this.owner.Nodes.FixedIndex = -1;
			if (treeView != null && nodes.Length > 200)
			{
				treeView.EndUpdate();
			}
		}

		public TreeNode[] Find(string key, bool searchAllChildren)
		{
			ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
			TreeNode[] array = new TreeNode[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		private ArrayList FindInternal(string key, bool searchAllChildren, TreeNodeCollection treeNodeCollectionToLookIn, ArrayList foundTreeNodes)
		{
			if (treeNodeCollectionToLookIn == null || foundTreeNodes == null)
			{
				return null;
			}
			for (int i = 0; i < treeNodeCollectionToLookIn.Count; i++)
			{
				if (treeNodeCollectionToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(treeNodeCollectionToLookIn[i].Name, key, true))
				{
					foundTreeNodes.Add(treeNodeCollectionToLookIn[i]);
				}
			}
			if (searchAllChildren)
			{
				for (int j = 0; j < treeNodeCollectionToLookIn.Count; j++)
				{
					if (treeNodeCollectionToLookIn[j] != null && treeNodeCollectionToLookIn[j].Nodes != null && treeNodeCollectionToLookIn[j].Nodes.Count > 0)
					{
						foundTreeNodes = this.FindInternal(key, searchAllChildren, treeNodeCollectionToLookIn[j].Nodes, foundTreeNodes);
					}
				}
			}
			return foundTreeNodes;
		}

		public virtual int Add(TreeNode node)
		{
			return this.AddInternal(node, 0);
		}

		private int AddInternal(TreeNode node, int delta)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.handle != IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
				{
					node.Text
				}), "node");
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && treeView.Sorted)
			{
				return this.owner.AddSorted(node);
			}
			node.parent = this.owner;
			int num = this.owner.Nodes.FixedIndex;
			if (num != -1)
			{
				node.index = num + delta;
			}
			else
			{
				this.owner.EnsureCapacity(1);
				node.index = this.owner.childCount;
			}
			this.owner.children[node.index] = node;
			this.owner.childCount++;
			node.Realize(false);
			if (treeView != null && node == treeView.selectedNode)
			{
				treeView.SelectedNode = node;
			}
			if (treeView != null && treeView.TreeViewNodeSorter != null)
			{
				treeView.Sort();
			}
			return node.index;
		}

		int IList.Add(object node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node is TreeNode)
			{
				return this.Add((TreeNode)node);
			}
			return this.Add(node.ToString()).index;
		}

		public bool Contains(TreeNode node)
		{
			return this.IndexOf(node) != -1;
		}

		public virtual bool ContainsKey(string key)
		{
			return this.IsValidIndex(this.IndexOfKey(key));
		}

		bool IList.Contains(object node)
		{
			return node is TreeNode && this.Contains((TreeNode)node);
		}

		public int IndexOf(TreeNode node)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i] == node)
				{
					return i;
				}
			}
			return -1;
		}

		int IList.IndexOf(object node)
		{
			if (node is TreeNode)
			{
				return this.IndexOf((TreeNode)node);
			}
			return -1;
		}

		public virtual int IndexOfKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return -1;
			}
			if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
			{
				return this.lastAccessedIndex;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
				{
					this.lastAccessedIndex = i;
					return i;
				}
			}
			this.lastAccessedIndex = -1;
			return -1;
		}

		public virtual void Insert(int index, TreeNode node)
		{
			if (node.handle != IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
				{
					node.Text
				}), "node");
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && treeView.Sorted)
			{
				this.owner.AddSorted(node);
				return;
			}
			if (index < 0)
			{
				index = 0;
			}
			if (index > this.owner.childCount)
			{
				index = this.owner.childCount;
			}
			this.owner.InsertNodeAt(index, node);
		}

		void IList.Insert(int index, object node)
		{
			if (node is TreeNode)
			{
				this.Insert(index, (TreeNode)node);
				return;
			}
			throw new ArgumentException(SR.GetString("TreeNodeCollectionBadTreeNode"), "node");
		}

		public virtual TreeNode Insert(int index, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Insert(index, treeNode);
			return treeNode;
		}

		public virtual TreeNode Insert(int index, string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		public virtual TreeNode Insert(int index, string key, string text, int imageIndex)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageIndex = imageIndex;
			this.Insert(index, treeNode);
			return treeNode;
		}

		public virtual TreeNode Insert(int index, string key, string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		public virtual TreeNode Insert(int index, string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		public virtual TreeNode Insert(int index, string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		private bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.Count;
		}

		public virtual void Clear()
		{
			this.owner.Clear();
		}

		public void CopyTo(Array dest, int index)
		{
			if (this.owner.childCount > 0)
			{
				Array.Copy(this.owner.children, 0, dest, index, this.owner.childCount);
			}
		}

		public void Remove(TreeNode node)
		{
			node.Remove();
		}

		void IList.Remove(object node)
		{
			if (node is TreeNode)
			{
				this.Remove((TreeNode)node);
			}
		}

		public virtual void RemoveAt(int index)
		{
			this[index].Remove();
		}

		public virtual void RemoveByKey(string key)
		{
			int index = this.IndexOfKey(key);
			if (this.IsValidIndex(index))
			{
				this.RemoveAt(index);
			}
		}

		public IEnumerator GetEnumerator()
		{
			return new WindowsFormsUtils.ArraySubsetEnumerator(this.owner.children, this.owner.childCount);
		}
	}
}
