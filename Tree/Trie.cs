using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    /// <summary>
    /// 前缀树
    /// </summary>
    public class Trie
    {
        // https://leetcode.cn/problems/QC3q1f/description/
        public TrieNode root;
        /** Initialize your data structure here. */
        public Trie()
        {
            root = new TrieNode();
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            if (word == null) 
                return;

            TrieNode curr = root;
            curr.pass++;
            char[] chars = word.ToCharArray();
            int index = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                index = chars[i] - 'a';
                if (curr.nexts[index] == null)
                {
                    curr.nexts[index] = new TrieNode();
                }
                curr = curr.nexts[index];
                curr.pass++;
            }
            curr.end++;
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            // 这个函数也可以是之前word加入过几次
            if (word == null)
                return false;

            TrieNode curr = root;
            char[] chars = word.ToCharArray();
            int index = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                index = chars[i] - 'a';
                if (curr.nexts[index] == null)
                {
                    return false;
                }
                else
                { 
                    curr = curr.nexts[index];
                }
            }
            if (curr.end != 0)          
                return true;
            return false;          
        }
        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            if(prefix == null)
                return false;
            TrieNode curr = root;
            char[] chars = prefix.ToCharArray();
            int index = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                index = chars[i] - 'a';
                if (curr.nexts[index] == null)
                    return false;
                curr = curr.nexts[index];
            }
            return true;
        }
        public void Delete(string word)
        {
            // 左神添加的方法
            if (!Search(word))
                return;
            TrieNode curr = root;
            TrieNode last = null;
            curr.pass--;
            char[] chars = word.ToCharArray();
            int index = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                index = chars[i] - 'a';
                last = curr;
                curr = curr.nexts[index];
                curr.pass--;
                if(curr.pass == 0)
                    last.nexts[index] = null;
            }
            curr.end--;
        }
    }
    /// <summary>
    /// 单个前缀树节点
    /// </summary>
    public class TrieNode
    {
        public int pass;
        public int end;
        public TrieNode[] nexts;
        public TrieNode()
        {
            pass = 0;
            end = 0;
            nexts = new TrieNode[26];
        }
    }
}
