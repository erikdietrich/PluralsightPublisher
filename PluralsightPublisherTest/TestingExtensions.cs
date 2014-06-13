using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisherTest
{
    public static class TestingExtensions
    {
        public static IList<T> AsList<T>(this T target)
        {
            return new List<T> { target };
        }

        public static IList<T> AsList<T>(this T target, int count)
        {
            return Enumerable.Repeat(target, count).ToList();
        }
    }
}
