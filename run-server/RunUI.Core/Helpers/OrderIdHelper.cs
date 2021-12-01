﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    /// <summary>
    /// 序列号
    /// </summary>
    public static class OrderIdHelper
    {
        public static Func<string>? GetOrderIdCustom;

        /// <summary>
        /// 获取编号
        /// <para>DateTime.Now.ToString("yyyyMMddHHmmssfff") + count.ToString("0000");</para>
        /// </summary>
        /// <returns></returns>
        public static string GetOrderOrginal()
        {
            var str = GetOrderIdCustom?.Invoke();
            if (str.HasValue()) return str;

            var randomBytes = new byte[8];
            SequentialGuid.GetBytes(randomBytes);
            var now = DateTime.UtcNow;

            var msecsArray = BitConverter.GetBytes((long)(now.TimeOfDay.TotalSeconds / 3.333333));
            msecsArray = msecsArray.Take(2).ToArray();
            Array.Reverse(msecsArray);

            //return now.ToString("yyyy") + now.DayOfYear.ToString().PadLeft(3, '0') + msecsArray.Select(b => b.ToString("x").PadLeft(2, '0')).Join("") + randomBytes.Select(b => b.ToString("x").PadLeft(2, '0')).Join("");

            return now.ToString("yyyyMMdd") + msecsArray.Select(b => b.ToString("x").PadLeft(2, '0')).Join("") + randomBytes.Select(b => b.ToString("x").PadLeft(2, '0')).Join("");
        }

        public static string GuidString()
        {
            return SequentialGuid.Create(SequentialGuid.SequentialGuidType.SequentialAsString).ToString("N");
        }
    }
}