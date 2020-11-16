using GRUnlocker.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRUnlocker {
    class SwordData {
        public static readonly List<string> SwordNames = new List<string> {
            {"Seisouken (default)"},
            {"TsuruGR-74"},
            {"Sentinel"},
            {"Twilight Seisouken"},
            {"Yama"},
            {"Kinteki Seisouken"},
            {"Venom TsuruGR"},
            {"Vintage Sentinel"},
            {"TsuruGR Nini-chan"},
            {"Modified Carbon Yama"},
            {"Izanami"},
            {"Void Dragon TsuruGR"},
            {"Izanami Unchained"},
            {"Dharma Street Yama"},
            {"Chrome Izanami"}
        };

        public static byte[] GetSwordData(int id) {
            if(id < 0 && id > 14) return null;
            List<Byte> data = Resources.SwordsData.ToList();
            int anchor = ByteUtils.Locate(data.ToArray(), Encoding.UTF8.GetBytes("-"))[0];
            if(id == 0) { // default sword
                data.RemoveRange(anchor, data.Count - anchor);
                return data.ToArray();
            } else {
                data.RemoveRange(0, anchor + 1);
                data[data.Count - 5] = (byte)id;
                return data.ToArray();
            }
        }

        public static string DisplaySwordNames() {
            string str = "";
            for(int i = 0; i < SwordNames.Count; i++) {
                str+= $"{i} - {SwordNames[i]}\n";
            }
            return str;
        }
    }
}
