using System;
using System.Collections.Generic;

/* 前言
 * 题目：
 * 有一个若干正整数组成的数列
 * 每个数不超过32
 * 已知数列中存在重复的数字
 * 找出数列中所有重复出现的数
 * 期待：O(n)时间复杂度
 * 状态：完成
 */

namespace find_repeat_number
{
    class Program
    {
        static List<int> find_repeat_number(List<int> input, int max)
        {
            int[] temp = new int[max + 1];
            for (int i = 0; i < input.Count; i++)
            {
                temp[input[i]]++;
            }
            List<int> result = new List<int>();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] >= 2)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            List<int> input = new List<int> { 1, 32, 30, 19, 18, 3, 2, 2, 2, 2, 2, 2, 0, 32, 31, 17, 18 };
            List<int> result = find_repeat_number(input, 32);
            Console.WriteLine("重复出现的数有：");
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
            }
            Console.ReadKey();
        }
    }
}
