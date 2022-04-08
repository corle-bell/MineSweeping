
#if UNITY_EDITOR
using UnityEngine;
using System.IO;
using ExcelDataReader;
using LitJson;
using BmFramework.Core;
using UnityEditor;
public class Excel2Json
{
    public static void Convert(string _In_Path, string _out_Path)
    {
        var stream = File.Open(_In_Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            var result = reader.AsDataSet();
            if (result.Tables.Count > 0)
            {

                for (int i = 0; i < result.Tables.Count; i++)
                {
                    var json = new JsonData();
                    json.SetJsonType(JsonType.Array);

                    string sheedName = result.Tables[i].ToString();

                    BmDebug.Info("开始导出: " + sheedName);
                    double time = EditorApplication.timeSinceStartup;

                    int len = result.Tables[i].Rows[0].ItemArray.Length;
                    string[] keyName = new string[len];
                    string[] valueTypeName = new string[len];
                    string[] valueDescName = new string[len];

                    //解析前三行 字段名  类型  描述
                    for (int q = 0; q < result.Tables[i].Rows[0].ItemArray.Length; q++)
                    {
                        keyName[q] = result.Tables[i].Rows[0].ItemArray[q].ToString();
                        valueTypeName[q] = result.Tables[i].Rows[1].ItemArray[q].ToString();
                        valueDescName[q] = result.Tables[i].Rows[2].ItemArray[q].ToString();
                    }


                    for (int j = 0; j < result.Tables[i].Rows.Count - 3; j++)
                    {
                        int index = j + 3;
                        var item = new JsonData();
                        for (int q = 0; q < result.Tables[i].Rows[index].ItemArray.Length; q++)
                        {
                            var valueString = result.Tables[i].Rows[index].ItemArray[q].ToString();

                            switch (valueTypeName[q])
                            {
                                case "string":
                                    item[keyName[q]] = valueString;
                                    break;
                                case "int":
                                    item[keyName[q]] = int.Parse(valueString);
                                    break;
                                case "float":
                                    item[keyName[q]] = float.Parse(valueString);
                                    break;
                                case "float[]":
                                    {
                                        var tt = new JsonData();
                                        tt.SetJsonType(JsonType.Array);
                                        float[] fArr = FrameworkTools.stringToFloatArray(valueString);
                                        for (int z = 0; z < fArr.Length; z++)
                                        {
                                            tt.Add(fArr[z]);
                                        }
                                        item[keyName[q]] = tt;
                                    }
                                    break;
                                case "int[]":
                                    {
                                        var tt = new JsonData();
                                        tt.SetJsonType(JsonType.Array);
                                        int[] fArr = FrameworkTools.stringToIntArray(valueString);
                                        for (int z = 0; z < fArr.Length; z++)
                                        {
                                            tt.Add(fArr[z]);
                                        }
                                        item[keyName[q]] = tt;
                                    }
                                    break;
                                case "string[]":
                                    {
                                        var tt = new JsonData();
                                        tt.SetJsonType(JsonType.Array);
                                        string[] fArr = FrameworkTools.Split(valueString, ",");
                                        for (int z = 0; z < fArr.Length; z++)
                                        {
                                            tt.Add(fArr[z]);
                                        }
                                        item[keyName[q]] = tt;
                                    }
                                    break;
                            }
                        }
                        json.Add(item);
                    }

                    File.WriteAllText(string.Format(_out_Path + "/{0}.json", sheedName), json.ToJson());


                    double ret = EditorApplication.timeSinceStartup - time;
                    BmDebug.Info(string.Format("导出结束: {0}  消耗:{1} ms", sheedName, ret.ToString("0.000000")));
                }

            }

            reader.Dispose();
            reader.Close();
        }
        stream.Dispose();
        stream.Close();
    }
}
#endif
