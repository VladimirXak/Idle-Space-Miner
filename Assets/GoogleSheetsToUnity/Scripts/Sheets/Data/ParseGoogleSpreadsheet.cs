using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HakoLibrary.GoogleSheets
{
    public static class ParseGoogleSpreadsheet
    {
        private const string _nullValue = "{NULL}";

        public static string Parse(List<string[]> data)
        {
            if (data == null)
                return null;

            Dictionary<string, List<string>> listUniqueClasses = new Dictionary<string, List<string>>();
            StringBuilder sbData = new StringBuilder();
            sbData.Append("{\n");

            for (int row = 0; row < data.Count; row++)
            {
                if (data[row].Length == 0)
                    continue;

                if (GetTypeTitle(data[row][0]) == TypeTitle.Class)
                {
                    string nameClass = data[row][0].Replace("{Class_", "").Replace("}", "");
                    string textClass = ParseClass(data, row);

                    if (!listUniqueClasses.ContainsKey(nameClass))
                        listUniqueClasses.Add(nameClass, new List<string>());

                    listUniqueClasses[nameClass].Add(textClass);
                }
                else if (GetTypeTitle(data[row][0]) == TypeTitle.List)
                {
                    string textList = ParseList(data, row, 0);
                    sbData.Append(textList);
                }
            }

            foreach (var listClasses in listUniqueClasses)
            {
                if (listClasses.Value.Count == 1)
                {
                    sbData.Append($"\"{listClasses.Key}\":\n");
                    sbData.Append(listClasses.Value[0]);
                }
                else
                {
                    sbData.Append($"\"List{listClasses.Key}\":[\n");

                    foreach (var textClass in listClasses.Value)
                        sbData.Append(textClass);

                    sbData.Remove(sbData.Length - 2, 1);
                    sbData.Append("],\n");
                }
            }

            sbData.Remove(sbData.Length - 2, 1);
            sbData.Append("}\n");

            return sbData.ToString();
        }

        private static string ParseClass(List<string[]> data, int row)
        {
            Dictionary<string, List<string>> listUniqueClasses = new Dictionary<string, List<string>>();
            StringBuilder sbJsonClass = new StringBuilder();
            sbJsonClass.Append("{\n");

            data[row][0] = _nullValue;

            for (int cell = 1; cell < data[row].Length; cell++)
            {
                string value = (data[row + 1].Length <= cell) ? "" : data[row + 1][cell];
                sbJsonClass.Append($"\"{data[row][cell].Replace("_", "")}\":\"{value}\",\n");
            }

            for (row += 1; row < data.Count; row++)
            {
                if (data[row].Length == 0)
                    continue;

                TypeTitle typeTitle = GetTypeTitle(data[row][0]);

                if (typeTitle == TypeTitle.ClassIn)
                {
                    string nameClass = data[row][0].Replace("{ClassIn_", "").Replace("}", "");
                    string textClass = ParseClass(data, row);

                    if (!listUniqueClasses.ContainsKey(nameClass))
                        listUniqueClasses.Add(nameClass, new List<string>());

                    listUniqueClasses[nameClass].Add(textClass);
                }
                if (typeTitle == TypeTitle.List)
                {
                    sbJsonClass.Append(ParseList(data, row, 0));
                }
                else if (typeTitle == TypeTitle.End || typeTitle == TypeTitle.Class)
                {
                    if (typeTitle == TypeTitle.End)
                        data[row][0] = _nullValue;

                    break;
                }
            }

            foreach (var listClasses in listUniqueClasses)
            {
                if (listClasses.Value.Count == 1)
                {
                    sbJsonClass.Append($"\"{listClasses.Key}\":\n");
                    sbJsonClass.Append(listClasses.Value[0]);
                }
                else
                {
                    sbJsonClass.Append($"\"List{listClasses.Key}\":[\n");

                    foreach (var textClass in listClasses.Value)
                        sbJsonClass.Append(textClass);

                    sbJsonClass.Remove(sbJsonClass.Length - 2, 1);
                    sbJsonClass.Append("],\n");
                }
            }

            sbJsonClass.Remove(sbJsonClass.Length - 2, 1);
            sbJsonClass.Append("},\n");

            return sbJsonClass.ToString();
        }

        private static string ParseList(List<string[]> data, int row, int cell)
        {
            StringBuilder sb = new StringBuilder();
            string nameList = data[row][0].Replace("{", "").Replace("}", "").Replace("_", "");
            sb.Append($"\"{nameList}\":");
            sb.Append("[\n");

            data[row][0] = _nullValue;

            cell++;

            List<string> listFields = new List<string>();

            while (cell != data[row].Length && !string.IsNullOrEmpty(data[row][cell]))
            {
                listFields.Add(data[row][cell].Replace("_", ""));
                cell++;
            }

            bool islistEmpty = true;

            for (row += 1; row < data.Count; row++)
            {
                if (data[row].Length == 0)
                    continue;

                sb.Append("{\n");

                for (cell = 0; cell <= listFields.Count; cell++)
                {
                    if (cell == 0)
                    {
                        TypeTitle typeTitle = GetTypeTitle(data[row][cell]);

                        if (typeTitle == TypeTitle.End || typeTitle == TypeTitle.Class || typeTitle == TypeTitle.List || typeTitle == TypeTitle.ClassIn)
                        {
                            if (islistEmpty)
                                sb.Remove(sb.Length - 2, 2);
                            else
                                sb.Remove(sb.Length - 4, 4);

                            sb.Append("],\n");

                            return sb.ToString();
                        }

                        islistEmpty = false;
                        continue;
                    }

                    string value = "";

                    if (data[row].Length > cell)
                        value = data[row][cell];

                    sb.Append($"\"{listFields[cell - 1]}\":\"{value}\",\n");
                }

                islistEmpty = false;

                sb.Remove(sb.Length - 2, 1);
                sb.Append("},\n");
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append("],\n");

            return sb.ToString();
        }

        private static TypeTitle GetTypeTitle(string data)
        {
            if (data.ToUpper().Contains("{CLASS_"))
                return TypeTitle.Class;

            if (data.ToUpper().Contains("CLASSIN_"))
                return TypeTitle.ClassIn;

            if (data.ToUpper().Contains("{END}"))
                return TypeTitle.End;

            if (data.ToUpper().Contains("{LIST_"))
                return TypeTitle.List;

            if (data.ToUpper().Contains(_nullValue))
                return TypeTitle.NullValue;

            return TypeTitle.NULL;
        }

        private enum TypeTitle
        {
            NULL,
            Class,
            ClassIn,
            List,
            End,
            NullValue,
        }
    }
}
