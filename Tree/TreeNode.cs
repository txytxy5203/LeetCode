using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    public class NTreeNode
    {
        public int val;
        public List<NTreeNode> childs;
        public NTreeNode(int val = 0)
        {
            this.val = val;
            childs = new List<NTreeNode>();
        }
    }
}
