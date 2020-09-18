using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace ConfigSystem
{
    public class Ini
    {
        /// <summary>
        /// Copies a string into the specified section of an initialization file.
        /// </summary>
        /// <returns>
        /// If the function successfully copies the string to the initialization file, the return value is nonzero.
        /// If the function fails, or if it flushes the cached version of the most recently accessed initialization file, the return value is zero. 
        /// </returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filePath);

        /// <summary>
        /// Retrieves a string from the specified section in an initialization file.
        /// </summary>
        /// <returns>
        /// The return value is the number of characters copied to the buffer, not including the terminating null character.
        /// </returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);

        /// <summary>
        /// Retrieves all the keys and values for the specified section of an initialization file.
        /// </summary>
        /// <returns>
        /// The return value is the number of characters copied to the buffer, not including the terminating null character.
        /// </returns>
        //[DllImport("kernel32.dll")]
        //private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        private const int SIZE = 255;

        public string FileName { get; private set; }

        public Ini(string fileName)
        {
            this.SetFileName(fileName);
        }

        private void SetFileName(string fileName)
        {
            this.FileName = fileName;

            if (!Path.IsPathRooted(this.FileName))
            {
                var basePath = System.IO.Directory.GetCurrentDirectory();
                this.FileName = Path.Combine(basePath, this.FileName);
            }
        }

        public bool FileExsits()
        {
            return File.Exists(this.FileName);
        }

        public bool CreateFile()
        {
            if (!FileExsits())
            {
                try
                {
                    using (var fs = File.Create(this.FileName))
                    { }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public void DeleteKey(string section, string key)
        {
            WritePrivateProfileString(Encoding.UTF8.GetBytes(section), Encoding.UTF8.GetBytes(key), null, this.FileName);
        }

        //public void DeleteSection(string section)
        //{
        //    WritePrivateProfileString(section, null, null, this.FileName);
        //}

        public string ReadString(string section, string key)
        {
            var buffer = new byte[SIZE];
            var i = GetPrivateProfileString(Encoding.UTF8.GetBytes(section), Encoding.UTF8.GetBytes(key), null, buffer, SIZE, this.FileName);
            if (i > 0)
            {
                return Encoding.UTF8.GetString(buffer).TrimEnd('\0');
            }
            else
            {
                return string.Empty;
            }
        }

        public string ReadString(string section, string key, string defaultValue)
        {
            var buffer = new byte[SIZE];
            var i = GetPrivateProfileString(Encoding.UTF8.GetBytes(section), Encoding.UTF8.GetBytes(key), null, buffer, SIZE, this.FileName);
            if (i > 0)
            {
                return Encoding.UTF8.GetString(buffer).TrimEnd('\0');
            }
            else
            {
                return defaultValue;
            }
        }

        public int ReadInt(string section, string key, int defaultValue)
        {
            var r = 0;
            var buffer = new byte[SIZE];
            var i = GetPrivateProfileString(Encoding.UTF8.GetBytes(section), Encoding.UTF8.GetBytes(key), null, buffer, SIZE, this.FileName);
            if (i > 0)
            {
                var rs = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                if (string.IsNullOrEmpty(rs))
                {
                    return defaultValue;
                }
                else
                {
                    if (!int.TryParse(rs, out r))
                    {
                        return defaultValue;
                    }
                    else
                    {
                        return r;
                    }
                }
            }
            else
            {
                return defaultValue;
            }
        }

        public bool WriteString(string section, string key, string value)
        {
            var l = WritePrivateProfileString(Encoding.UTF8.GetBytes(section), Encoding.UTF8.GetBytes(key), string.IsNullOrEmpty(value)?null:Encoding.UTF8.GetBytes(value), this.FileName);
            return l > 0;
        }

        //public bool SectionExists(string section)
        //{
        //    int i = GetPrivateProfileString(section, null, null, new StringBuilder(SIZE), SIZE, this.FileName);
        //    return i > 0;
        //}

        //public bool KeyExists(string section, string key)
        //{
        //    int i = GetPrivateProfileString(Encoding.UTF8.GetBytes(section), Encoding.UTF8.GetBytes(key), null, new StringBuilder(SIZE), SIZE, this.FileName);
        //    return i > 0;
        //}

        //public IDictionary<string, string> ReadSection(string section)
        //{
        //    var buffer = new byte[2048];
        //    GetPrivateProfileSection(section, buffer, 2048, this.FileName);
        //    var tmp = Encoding.ASCII.GetString(buffer).Trim('\0').Split('\0');
        //    var result = new Dictionary<string, string>();

        //    foreach (var entry in tmp)
        //    {
        //        var s = entry.Split(new string[] { "=" }, 2, StringSplitOptions.None);
        //        result.Add(s[0], s[1]);
        //    }
        //    return result;
        //}
    }
}
