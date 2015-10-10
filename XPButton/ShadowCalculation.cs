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
        static List<List<int>> ShadowAlphaValueList = new List<List<int>>()
        {
            new List<int>() { 40 },
            new List<int>() { 70, 3 },
            new List<int>() { 65, 22, 3 },
            new List<int>() { 50, 36, 4, 1 },
            new List<int>() { 55, 24, 15, 5, 0 },
            new List<int>() { 35, 22, 15, 10, 6, 3 },
            new List<int>() { 40 },
            new List<int>() { 40 },
            new List<int>() { 40 },
            new List<int>() { 40 },
            new List<int>() { 40 },
        };

        public static List<int> GetShadowValues(int shadowCount)
        {
            return ShadowAlphaValueList[shadowCount-1];
        }
    }
}
