#region License
//   Copyright 2015 Brook Shi
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;

namespace XP
{
    public class ShadowCalculation
    {
        public static List<int> GetShadowValues(int shadowCount)
        {
            return GetShadowColors(shadowCount);
        }

        static List<int> GetShadowColors(int count)
        {
            if (count == 1)
                return new List<int> { 255 - GetMin(1) };

            List<int> shadowColors = new List<int>();
            double gapIncrease = GetGapIncrease(count);
            int max = GetMax(count);
            int curr = max;

            for (int i = 0; i < count; i++)
            {
                curr = curr - (int)(gapIncrease * i);
                shadowColors.Add(255 - curr);
            }
            shadowColors.Reverse();
            return shadowColors;
        }

        static int GetMin(int count)
        {
            var max = 180;
            var min = 140;
            var curr = max;
            for (int i = 1; i < count; i++)
            {
                curr = curr - Math.Max(2, (9 - i) * 2);
            }
            return Math.Max(min, curr);
        }

        static int GetMax(int count)
        {
            if (count == 1)
                return GetMin(1);
            if (count == 2)
                return 221;
            else if (count == 3)
                return 236;
            else if (count == 4)
                return 247;
            else
                return Math.Min(253, 245 + count);
        }

        static double GetGapIncrease(int count)
        {
            var len = GetMax(count) - GetMin(count);
            return (len * 2.0) / ((count - 1) * count);
        }
    }
}
