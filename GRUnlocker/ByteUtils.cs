using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRUnlocker {
    public class ByteUtils {

        ////// Byte Utils//////
        public static readonly int[] Empty = new int[0];

        public static void ReplaceByteRange(ref byte[] bytes, byte[] newRange, string fromString, string toString, int fromIndex = 0) {

            List<byte> lst = bytes.ToList();
            int[] start_anchorPoints = Locate(lst.ToArray(), Encoding.UTF8.GetBytes(fromString), fromIndex);
            int[] end_anchorPoints = Locate(lst.ToArray(), Encoding.UTF8.GetBytes(toString), fromIndex);

            if(start_anchorPoints.Length == 0 || end_anchorPoints.Length == 0) return;

            for(int i = 0; i < start_anchorPoints.Length; i++) {
                int endAnchor = end_anchorPoints.Where(x => x > start_anchorPoints[i]).First();

                lst.RemoveRange(start_anchorPoints[i], endAnchor - start_anchorPoints[i]);
                lst.InsertRange(start_anchorPoints[i], newRange);
            }

            bytes = lst.ToArray();
        }

        public static int[] Locate(byte[] self, byte[] candidate, int fromIndex = 0) {
            if(IsEmptyLocate(self, candidate)) return Empty;

            var list = new List<int>();

            for(int i = fromIndex; i < self.Length; i++) {
                if(!IsMatch(self, i, candidate))
                    continue;

                list.Add(i);
            }
            return list.Count == 0 ? Empty : list.ToArray();
        }

        public static bool IsMatch(byte[] array, int position, byte[] candidate) {
            if(candidate.Length > (array.Length - position)) return false;

            for(int i = 0; i < candidate.Length; i++)
                if(array[position + i] != candidate[i])
                    return false;

            return true;
        }

        public static bool IsEmptyLocate(byte[] array, byte[] candidate) {
            return array == null
                || candidate == null
                || array.Length == 0
                || candidate.Length == 0
                || candidate.Length > array.Length;
        }
    }
}

