using System;

namespace CSharpNovel2
{
    public static class Utils
    {
        public static T Max<T>(params T[] nums) where T : IComparable
        {
            if (nums.Length == 0) return default(T)!;
            var max = nums[0];
            for (var i = 1; i < nums.Length; i++)
            {
                max = max.CompareTo(nums[i]) > 0 ? max : nums[i];
            }
            return max;
        }
    }
}