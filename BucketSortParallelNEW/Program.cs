using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSortParallelNEW
{
    class Program
    {
        private static int hash(int value, int bucketRange, int bucketCount)
        {
            int bucketNumber = value / bucketRange;
            if (bucketNumber == bucketCount)
                bucketNumber--;
            else if (bucketNumber > bucketCount)
            {
                bucketNumber = bucketCount--;
            }
            return bucketNumber;
        }

        public static void BucketSort(int[] data, int bucketCount)
        {
            var buckets = new List<int>[bucketCount];

            var min = int.MaxValue;
            var max = -int.MaxValue;

            for (int i = 0; i < data.Length; i++)
            {
                min = Math.Min(min, data[i]);
                max = Math.Max(max, data[i]);
            }

            int range = max - min;
            int bucketRange = (int)Math.Ceiling((double)range / bucketCount);

            Parallel.For(0, bucketCount, i => buckets[i] = new List<int>());

            Parallel.For(0, data.Length, i =>
            {
                buckets[hash(data[i], bucketRange, bucketCount)].Add(data[i]);
            });

            Parallel.For(0, bucketCount, i => buckets[i].Sort());

            var index = 0;
            for (var i = 0; i < bucketCount; i++)
                for (var j = 0; j < buckets[i].Count; j++)
                    data[index++] = buckets[i][j];
        }
        static void Main(string[] args)
        {
            int[] data = { 3, 77, 33, 44, 2, 22, 35, 747, 333, 442, 21, 24 };

            BucketSort(data, 10);

            Console.WriteLine("Sorted  Array \n");
            foreach (var i in data)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }
}
