﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DMIPSpaceInvaders.Game.Utils
{
    public static class Extensions
    {
        public static List<T> Copy<T>(this List<T> self) where T : class
        {
            T[] array = new T[self.Count + 1];
            Array.Copy(self.ToArray(), array, self.Count);
            return array.ToList();
        }

        public static string Repeat(this char self, int amount)
        {
            string txt = "";
            for (int i = 0; i < amount; i++)
                txt += self;
            return txt;
        }
    }
}