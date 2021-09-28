using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_STANDALONE

using System.Diagnostics;
public class CmdHelper
{
    public static void ProcessExcuteDoc()
    {
        string path = "keytool -exportcert -alias {0} -storepass {1} -keypass {2} -keystore {3} | openssl sha1 -binary | openssl base64";

        path = string.Format(path, "user", "123456", "123456", "E:/UnityProject/Cx/TugOfWar/Z_KeyStore/user.keystore");

        StartCmd(path);

    }


    private static string StartCmd(string Command)
    {
        Process proc = new Process(); 
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.FileName = "cmd.exe";
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardError = true;
        proc.StartInfo.RedirectStandardInput = true;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start();
        proc.StandardInput.WriteLine(Command);
        proc.StandardInput.WriteLine("exit");
        string outStr = proc.StandardOutput.ReadToEnd();
        proc.Close();
        return outStr;
    }
    
}
#endif
