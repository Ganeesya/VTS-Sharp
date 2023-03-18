// using System.Diagnostics;

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace VTS.Networking.Impl{
    public class TokenStorageImpl : ITokenStorage
    {
        private static readonly UTF8Encoding ENCODER = new UTF8Encoding();
        private string _fileName = "token.json";
        private string _path = "";

        public TokenStorageImpl(string dirPath)
        {
            this._path = Path.Combine(dirPath, this._fileName);
            // Application.OpenURL(Application.LocalUserAppDataPath);
        }
        public string LoadToken()
        {
            if(File.Exists(this._path)){
                return File.ReadAllText(this._path);
            }
            return null;
        }

        public void SaveToken(string token)
        {
            string dirPath = Path.GetDirectoryName(_path);
            if (!File.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            File.WriteAllText(this._path, token, ENCODER);
        }

        public void DeleteToken()
        {
            if(File.Exists(this._path)){
                File.Delete(this._path);
            }
        }
    }
}
