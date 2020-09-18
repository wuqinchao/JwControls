using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jw.Share
{
    public class EnumListItem
    {
        private bool _Valid = true;
        /// <summary>
        /// 是否为有效值子项,默认为true
        /// </summary>
        public bool Valid
        {
            get { return _Valid; }
            internal set { _Valid = value; }
        }

        private string _Text;
        /// <summary>
        /// 子项的文本
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        private string _Value;
        /// <summary>
        /// 子项的值
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private object _tag = null;
        /// <summary>
        /// 子项的关联对象,默认为null
        /// </summary>
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t">子项的文本</param>
        /// <param name="v">子项的值</param>
        /// <param name="valid">是否为有效值子项</param>
        public EnumListItem(string t, string v, bool valid)
        {
            _Text = t;
            _Value = v;
            _tag = null;
            _Valid = valid;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t">子项的文本</param>
        /// <param name="v">子项的值</param>
        /// <param name="o">关联对象</param>
        /// <param name="valid">是否为有效值子项</param>
        public EnumListItem(string t, string v, object o, bool valid)
        {
            _Text = t;
            _Value = v;
            _tag = o;
            _Valid = valid;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t">子项的文本</param>
        /// <param name="v">子项的值</param>
        public EnumListItem(string t, string v)
        {
            _Text = t;
            _Value = v;
            _tag = null;
            _Valid = true;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t">子项的文本</param>
        /// <param name="v">子项的值</param>
        /// <param name="o">关联对象</param>
        public EnumListItem(string t, string v, object o)
        {
            _Text = t;
            _Value = v;
            _tag = o;
            _Valid = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._Text;
        }
        /// <summary>
        /// 根据子项值判断是否为同一对象
        /// </summary>
        /// <param name="obj">子项值</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj.ToString().ToLower() == _Value.ToLower());
        }
        public override int GetHashCode()
        {
            return _Value.GetHashCode();
        }
        /// <summary>
        /// 根据子项文本判断是否为同一对象
        /// </summary>
        /// <param name="text">子项文本</param>
        /// <returns></returns>
        public bool EqualsText(string text)
        {
            return (_Text.ToLower().Equals(text.ToLower()));
        }
    }
    /// <summary>
    /// EnumList主要用于将枚举之类的数据组合成列表,可供界面绑定等操作
    /// 例如:可将枚举绑定到combobox
    /// </summary>
    [Serializable]
    public class EnumList
    {
        protected List<EnumListItem> _Items = new List<EnumListItem>();
        protected List<EnumListItem> _ValidItems = new List<EnumListItem>();
        /// <summary>
        /// 获取所有子项列表
        /// </summary>
        public List<EnumListItem> List
        {
            get { return _Items; }
        }
        /// <summary>
        /// 获取有效子项列表
        /// </summary>
        public List<EnumListItem> ValidList
        {
            get { return _ValidItems; }
        }
        /// <summary>
        /// 添加一个子项
        /// </summary>
        /// <param name="item">列表子项</param>
        public void AddItem(EnumListItem item)
        {
            _Items.Add(item);
            if (item.Valid)
            {
                _ValidItems.Add(item);
            }
        }
        /// <summary>
        /// 根据子项文本获取该子项的值
        /// </summary>
        /// <param name="text">子项文本</param>
        /// <returns>子项的值</returns>
        public string Value(string text)
        {
            string s = string.Empty;
            for (int n = 0; n < _Items.Count; n++)
            {
                if (_Items[n].EqualsText(text))
                {
                    s = _Items[n].Value;
                    break;
                }
            }
            return s;
        }
        /// <summary>
        /// 根据子项的值获取该子项文本
        /// </summary>
        /// <param name="value">子项的值</param>
        /// <returns>子项文本</returns>
        public string Text(string value)
        {
            string s = string.Empty;
            for (int n = 0; n < _Items.Count; n++)
            {
                if (_Items[n].Equals(value))
                {
                    s = _Items[n].Text;
                    break;
                }
            }
            return s;
        }
        /// <summary>
        /// 根据子项的值获取该子项
        /// </summary>
        /// <param name="value">子项的值</param>
        /// <returns>子项</returns>
        public EnumListItem getItem(string value)
        {
            EnumListItem item = null;
            for (int n = 0; n < _Items.Count; n++)
            {
                if (_Items[n].Equals(value))
                {
                    item = _Items[n];
                    break;
                }
            }
            if (item == null) item = _Items[0];
            return item;
        }
        /// <summary>
        /// 根据子项的值获取该子项在列表中的索引值
        /// </summary>
        /// <param name="value">子项的值</param>
        /// <returns>该子项在列表中的索引值</returns>
        public int getIndex(string value)
        {
            int Index = 0;
            for (int n = 0; n < _Items.Count; n++)
            {
                if (_Items[n].Equals(value))
                {
                    Index = n;
                    break;
                }
            }
            return Index;
        }
    }
}
