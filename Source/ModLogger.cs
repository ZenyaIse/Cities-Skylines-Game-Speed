using System.Text;
using UnityEngine;

namespace GameSpeedMod
{
    public static class ModLogger
    {
        private static StringBuilder sb;

        private static void initStringBuilderIfNull()
        {
            if (sb == null)
            {
                sb = new StringBuilder("GameSpeedMod\n");
            }
        }

        public static void Add(string text)
        {
            initStringBuilderIfNull();
            sb.AppendLine(text);
        }

        public static void Add(string text, object value)
        {
            initStringBuilderIfNull();
            sb.AppendLine(string.Format("{0}: {1}", text, value));
        }

        public static void Add(string name, string fieldName, object beforeValue, object afterValue)
        {
            initStringBuilderIfNull();
            sb.AppendLine(string.Format("{0}: {1} {2} -> {3}", name, fieldName, beforeValue, afterValue));
        }

        public static void Add(string name, string fieldName, object beforeValue, object afterValue, object fieldName2, object beforeValue2, object afterValue2)
        {
            initStringBuilderIfNull();
            sb.AppendLine(string.Format("{0}: {1} {2} -> {3}, {4} {5} -> {6}", name, fieldName, beforeValue, afterValue, fieldName2, beforeValue2, afterValue2));
        }

        public static void Write()
        {
            if (sb != null)
            {
                Debug.Log(sb.ToString());
                sb = null;
            }
        }
    }
}
