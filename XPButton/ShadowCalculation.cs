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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XP
{
    public class ShadowCalculation
    {
        //179
        //154, 221 (67)
        //140, 198, 236 (38)
        //133, 182, 218, 243 ( 25)
        //128, 170, 214, 230, 247 (17)
        //124, 161, 193, 218, 136, 249 (13)
        //122, 155, 184, 208, 226, 241, 250 (9)
        //119, 149, 176, 199, 217, 232, 244, 251 (7)
        //102, 119, 149, 176, 199, 217, 232, 244, 251 (7)
        //102, 118, 145, 170, 191, 209, 224, 237, 246, 252 (16, 27, 25, 21, 18, 15, 13, 9, 4)
        static List<List<int>> ShadowAlphaValueList = new List<List<int>>()
        {
            //new List<int>() { 44 },
            //new List<int>() { 41, 3 },
            //new List<int>() { 38, 32, 3 },
            //new List<int>() { 34, 31, 14, 4 },
            //new List<int>() { 30, 30, 17, 10, 5 },
            //new List<int>() { 25, 29, 18, 13, 9, 4 },

            //new List<int>() { 40 },
            //new List<int>() { 70, 3 },
            //new List<int>() { 65, 22, 3 },
            //new List<int>() { 50, 36, 4, 1 },
            //new List<int>() { 55, 24, 15, 5, 0 },
            //new List<int>() { 35, 22, 15, 10, 6, 3 },

            new List<int>() { 76 },
            new List<int>() { 41, 3 },
            new List<int>() { 38, 32, 3 },
            new List<int>() { 34, 31, 14, 4 },
            new List<int>() { 127, 85, 41, 25, 3 },
            new List<int>() { 25, 29, 18, 13, 9, 4 },
            new List<int>() { 40 },
            new List<int>() { 40 },
            new List<int>() { 40 },
            new List<int>() { 40 },
            new List<int>() { 40 },
        };

        static int GetMinGap(int count)
        {
            if (count == 1)
                return 0;
            else if (count == 2)
                return 67;
            else
            {
                var curr = 50;
                for(int i=3;i<= count;i++)
                {
                    curr = curr - 2 * Math.Abs(9 - i);
                }
                return Math.Max(curr, 2);
            }
        }

        static int GetMax(int count)
        {
            var max = 180;
            var min = 100;
            var curr = max;
            for (int i = 1; i < count; i++)
            {
                curr = curr - 2 * Math.Abs(10 - (count-1) * 2);
            }
            return Math.Max(min, curr);
        }

        static int GetMin(int count)
        {
            if (count == 1)
                throw new ArgumentException();

            if (count == 2)
                return 221;
            else if (count == 3)
                return 236;
            else if (count == 4)
                return 247;
            else
                return Math.Max(GetMin(count - 1), 253);
        }

        static int GetGapIncrease(int count)
        {
            var len = GetMax(count) - GetMin(count);
            return (len - (count - 1) * GetMinGap(count)) * 2 / (count - 1) * (count - 2);
        }

        static List<int> GetShadowColors(int count)
        {
            List<int> colors = new List<int>();
            int minGap = GetMinGap(count);
            int gapIncrease = GetGapIncrease(count);
            int min = GetMin(count);
            int last = 0;
            for(int i=0;i<count;i++)
            {
                colors.Add(min - gapIncrease * count);
            }
            return colors;
        }

        public static List<int> GetShadowValues(int shadowCount)
        {
            return ShadowAlphaValueList[shadowCount-1];
        }
    }
}
