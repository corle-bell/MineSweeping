using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BmFramework.Core
{
    public class SceneHelper
    {
        public static void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static void LoadScene(string _name)
        {
            SceneManager.LoadScene(_name, LoadSceneMode.Single);
        }

        public static void LoadScene(int _id)
        {
            SceneManager.LoadScene(_id, LoadSceneMode.Single);
        }

        public static void AddScene(string _name)
        {
            SceneManager.LoadScene(_name, LoadSceneMode.Additive);
        }

        public static void AddScene(int _id)
        {
            SceneManager.LoadScene(_id, LoadSceneMode.Additive);
        }

        public static void LoadSceneAsync(string _name, System.Action<AsyncOperation> callback)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_name, LoadSceneMode.Single);
            TaskLogic.Instance.AddAsyncOperation(op, callback);
        }
        public static void LoadSceneAsync(int _id, System.Action<AsyncOperation> callback)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_id, LoadSceneMode.Single);
            TaskLogic.Instance.AddAsyncOperation(op, callback);
        }

        public static void AddSceneAsync(string _name, System.Action<AsyncOperation> callback)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_name, LoadSceneMode.Additive);
            TaskLogic.Instance.AddAsyncOperation(op, callback);
        }

        public static void AddSceneAsync(int _id, System.Action<AsyncOperation> callback)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_id, LoadSceneMode.Additive);
            TaskLogic.Instance.AddAsyncOperation(op, callback);
        }
    }
}

