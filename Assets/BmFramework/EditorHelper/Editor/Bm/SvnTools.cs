﻿
/**
 *  本类是在Unity中直接调用SVN命令做相应的操作
 *  使用方法：右击project面板中的物体
 *  如果操作没有执行，检查计算机环境有没有配置svn路径
 *  选择svn 命令 相应的选项
 */

using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.IO;
using System.Threading;
public class SvnTools : Editor
{

    static class SVNCmd
    {
        public const string COMMIT = "commit";
        public const string UPTATE = "update";
    }

    /// <summary>
    /// exe的应用名称。
    /// </summary>
    public const string SVN_APP_NAME = "TortoiseProc.exe";

    private static string cmdFormat = null;
    private static string GetCmdFormatString()
    {
        if (string.IsNullOrEmpty(cmdFormat))
        {
            cmdFormat = SVN_APP_NAME + " " + "/command:{0} /path:{1} /closeonend:0";
        }
        return cmdFormat;
    }

    /// <summary>
    /// SVN 提交数据
    /// </summary>
    [MenuItem("Assets/SVN Operation/SVN Commit...")]
    public static void Commit()
    {
        var go = GetCurSelectionObj();
        if (go == null) { return; }
        var dir_path = GetObjPath(go);
        string cmd = string.Format(GetCmdFormatString(), SVNCmd.COMMIT, dir_path);
        Debug.Log(cmd);
        InvokeCmd(cmd);
    }

    /// <summary>
    /// SVN 更新数据
    /// </summary>
    [MenuItem("Assets/SVN Operation/SVN Update...")]
    public static void Update()
    {
        var go = GetCurSelectionObj();
        if (go == null) { return; }
        var dir_path = GetObjPath(go);
        string cmd = string.Format(GetCmdFormatString(), SVNCmd.UPTATE, dir_path);
        Debug.Log(cmd);
        InvokeCmd(cmd);
    }

    /// <summary>
    /// SVN 更新全部数据
    /// </summary>
    [MenuItem("Assets/SVN Operation/SVN UpdateALL...")]
    public static void UpdateALL()
    {
        var dir_path = Application.dataPath;
        string cmd = string.Format(GetCmdFormatString(), SVNCmd.UPTATE, dir_path);
        InvokeCmd(cmd);
    }


    /// <summary>
    /// 获取当前选择到的物体
    /// </summary>
    private static UnityEngine.Object GetCurSelectionObj()
    {
        if (Selection.objects.Length != 1)
        {
            return null;
        }
        var go = Selection.objects[0];

        return go;
    }

    /// <summary>
    /// 根据预设名称获取路径
    /// </summary>
    private static string GetObjPath(UnityEngine.Object go)
    {
        string str = Application.dataPath.Replace("Assets", "");
        string path = AssetDatabase.GetAssetPath(go);
        string dir_path = System.IO.Path.GetFullPath(str + path);
        return dir_path;
    }


    /// <summary>
    /// 调用CMD 命令
    /// </summary>
    private static void InvokeCmd(string cmd)
    {
        //UnityEngine.Debug.Log(cmd);
        AssetDatabase.Refresh();
        new Thread(new ThreadStart(() =>
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/c " + cmd + "&exit";    //确定程式命令行  /c 执行字符串指定的命令然后终断 详细查询CMD命令:CMD /?
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start(); //启动程序
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        })).Start();
    }

}