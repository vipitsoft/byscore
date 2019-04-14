using System;
using System.Collections.Generic;

namespace BYSCORE.UI
{
    public class TreeViewModel
    {
        public TreeViewModel() { }
        public TreeViewModel(int id, string str, List<TreeViewModel> node)
        {
            NodeId = id;
            Text = str;
            Nodes = node;
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public int NodeId { get; set; }    //树的节点Id，区别于数据库中保存的数据Id。若要存储数据库数据的Id，添加新的Id属性；若想为节点设置路径，类中添加Path属性
        public string Text { get; set; }   //节点名称
        public List<TreeViewModel> Nodes { get; set; }
        public State State { get; set; }
        public List<string> Tags { get; set; }
    }

    public class State
    {
        public bool Checked { get; set; } = false;
    }
}
