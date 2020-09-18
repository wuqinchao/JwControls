using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Jw.Share
{
    public class Shortcut
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            uint dwLowDateTime;
            uint dwHighDateTime;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATA
        {
            public const int MAX_PATH = 260;

            uint dwFileAttributes;
            FILETIME ftCreationTime;
            FILETIME ftLastAccessTime;
            FILETIME ftLastWriteTime;
            uint nFileSizeHight;
            uint nFileSizeLow;
            uint dwOID;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            string cFileName;
        }

        [ComImport]
        [Guid("0000010c-0000-0000-c000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersist
        {
            [PreserveSig]
            void GetClassID(out Guid pClassID);
        }

        [ComImport]
        [Guid("0000010b-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistFile
            : IPersist
        {
            new void GetClassID(out Guid pClassID);

            [PreserveSig]
            int IsDirty();

            [PreserveSig]
            void Load(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                uint dwMode);

            [PreserveSig]
            void Save(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                [MarshalAs(UnmanagedType.Bool)] bool fRemember);

            [PreserveSig]
            void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

            [PreserveSig]
            void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] string ppszFileName);
        }

        [ComImport]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IShellLink
        {
            [PreserveSig]
            void GetPath(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszFile,
                int cch,
                ref WIN32_FIND_DATA pfd,
                uint fFlags);

            [PreserveSig]
            void GetIDList(out IntPtr ppidl);

            [PreserveSig]
            void SetIDList(IntPtr ppidl);

            [PreserveSig]
            void GetDescription(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszName,
                int cch);

            [PreserveSig]
            void SetDescription(
                [MarshalAs(UnmanagedType.LPWStr)] string pszName);

            [PreserveSig]
            void GetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszDir,
                int cch);

            [PreserveSig]
            void SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr)] string pszDir);

            [PreserveSig]
            void GetArguments(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszArgs,
                int cch);

            [PreserveSig]
            void SetArguments(
                [MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

            [PreserveSig]
            void GetHotkey(out ushort pwHotkey);

            [PreserveSig]
            void SetHotkey(ushort wHotkey);

            [PreserveSig]
            void GetShowCmd(out int piShowCmd);

            [PreserveSig]
            void SetShowCmd(int iShowCmd);

            [PreserveSig]
            void GetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszIconPath,
                int cch,
                out int piIcon);

            [PreserveSig]
            void SetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
                int iIcon);

            [PreserveSig]
            void SetRelativePath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
                uint dwReserved);

            [PreserveSig]
            void Resolve(
                IntPtr hwnd,
                uint fFlags);

            [PreserveSig]
            void SetPath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [GuidAttribute("00021401-0000-0000-C000-000000000046")]
        [ClassInterfaceAttribute(ClassInterfaceType.None)]
        [ComImportAttribute()]
        public class CShellLink
        {
        }

        public const int SW_SHOWNORMAL = 1;
        /// <summary>
        /// 创建快捷方式。
        /// </summary>
        /// <param name="shortcutPath">快捷方式路径。</param>
        /// <param name="targetPath">目标路径。</param>
        /// <param name="workingDirectory">工作路径。</param>
        /// <param name="description">快捷键描述。</param>
        public static bool CreateShortcut(string shortcutPath, string targetPath, string workingDirectory, string description, string iconLocation = null)
        {
            try
            {
                CShellLink cShellLink = new CShellLink();
                IShellLink iShellLink = (IShellLink)cShellLink;
                iShellLink.SetDescription(description);
                iShellLink.SetShowCmd(SW_SHOWNORMAL);
                iShellLink.SetPath(targetPath);

                iShellLink.SetWorkingDirectory(workingDirectory);

                if (!iconLocation.IsNullOrEmpty())
                {
                    iShellLink.SetIconLocation(iconLocation, 0);
                }

                IPersistFile iPersistFile = (IPersistFile)iShellLink;
                iPersistFile.Save(shortcutPath, false);
                Marshal.ReleaseComObject(iPersistFile);
                iPersistFile = null;
                Marshal.ReleaseComObject(iShellLink);
                iShellLink = null;
                Marshal.ReleaseComObject(cShellLink);
                cShellLink = null;
                return true;
            }
            catch //(System.Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 创建快捷方式。
        /// </summary>
        /// <param name="shortcutPath">快捷方式路径。</param>
        /// <param name="targetPath">目标路径。</param>
        /// <param name="parameter"></param>
        /// <param name="workingDirectory">工作路径。</param>
        /// <param name="description">快捷键描述。</param>

        public static bool CreateShortcut(string shortcutPath, string targetPath, string parameter, string workingDirectory, string description, string iconLocation = null)
        {
            try
            {
                CShellLink cShellLink = new CShellLink();
                IShellLink iShellLink = (IShellLink)cShellLink;
                iShellLink.SetDescription(description);
                iShellLink.SetShowCmd(SW_SHOWNORMAL);
                iShellLink.SetPath(targetPath);
                iShellLink.SetArguments(parameter);
                iShellLink.SetWorkingDirectory(workingDirectory);

                if (!iconLocation.IsNullOrEmpty())
                {
                    iShellLink.SetIconLocation(iconLocation, 0);
                }

                IPersistFile iPersistFile = (IPersistFile)iShellLink;
                iPersistFile.Save(shortcutPath, false);
                Marshal.ReleaseComObject(iPersistFile);
                iPersistFile = null;
                Marshal.ReleaseComObject(iShellLink);
                iShellLink = null;
                Marshal.ReleaseComObject(cShellLink);
                cShellLink = null;
                return true;
            }
            catch //(System.Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        /// <param name="targetPath">可执行文件路径</param>
        /// <param name="description">快捷方式名称</param>
        /// <param name="iconLocation">快捷方式图标路径</param>
        /// <param name="workingDirectory">工作路径</param>
        /// <returns></returns>
        public static bool CreateDesktopShortcut(string targetPath, string description, string iconLocation = null, string workingDirectory = null)
        {
            if (workingDirectory.IsNullOrEmpty())
            {
                workingDirectory = Shortcut.GetDeskDir();
            }
            return Shortcut.CreateShortcut(Shortcut.GetDeskDir() + "\\" + description + ".lnk", targetPath, workingDirectory, description, iconLocation);
        }

        /// <summary>
        /// 创建程序菜单快捷方式
        /// </summary>
        /// <param name="targetPath">可执行文件路径</param>
        /// <param name="description">快捷方式名称</param>
        /// <param name="menuName">程序菜单中子菜单名称，为空则不创建子菜单</param>
        /// <param name="iconLocation">快捷方式图标路径</param>
        /// <param name="workingDirectory">工作路径</param>
        /// <returns></returns>
        public static bool CreateProgramsShortcut(string targetPath, string description, string menuName, string iconLocation = null, string workingDirectory = null)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                workingDirectory = Shortcut.GetProgramsDir();
            }
            string shortcutPath = Shortcut.GetProgramsDir();
            if (!menuName.IsNullOrEmpty())
            {
                shortcutPath += "\\" + menuName;
                if (!System.IO.Directory.Exists(shortcutPath))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(shortcutPath);
                    }
                    catch //(System.Exception ex)
                    {
                        return false;
                    }
                }
            }
            shortcutPath += "\\" + description + ".lnk";
            return Shortcut.CreateShortcut(shortcutPath, targetPath, workingDirectory, description, iconLocation);
        }

        public static bool AddFavorites(string url, string description, string folderName, string iconLocation = null, string workingDirectory = null)
        {
            if (workingDirectory.IsNullOrEmpty())
            {
                workingDirectory = Shortcut.GetProgramsDir();
            }
            string shortcutPath = Shortcut.GetFavoriteDir();
            if (!folderName.IsNullOrEmpty())
            {
                shortcutPath += "\\" + folderName;
                if (!System.IO.Directory.Exists(shortcutPath))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(shortcutPath);
                    }
                    catch //(System.Exception ex)
                    {
                        return false;
                    }
                }
            }
            shortcutPath += "\\" + description + ".lnk";
            return Shortcut.CreateShortcut(shortcutPath, url, workingDirectory, description, iconLocation);
        }

        internal const uint SHGFP_TYPE_CURRENT = 0;
        internal const int MAX_PATH = 260;
        internal const uint CSIDL_COMMON_STARTMENU = 0x0016;              // All Users\Start Menu
        internal const uint CSIDL_COMMON_PROGRAMS = 0x0017;               // All Users\Start Menu\Programs
        internal const uint CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019;       // All Users\Desktop
        internal const uint CSIDL_PROGRAM_FILES = 0x0026;                 // C:\Program Files
        internal const uint CSIDL_FLAG_CREATE = 0x8000;                   // new for Win2K, or this in to force creation of folder
        internal const uint CSIDL_COMMON_FAVORITES = 0x001f;              // All Users Favorites

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void SHGetFolderPathW(
            IntPtr hwndOwner,
            int nFolder,
            IntPtr hToken,
            uint dwFlags,
            IntPtr pszPath);

        internal static string SHGetFolderPath(int nFolder)
        {
            string pszPath = new string(' ', MAX_PATH);
            IntPtr bstr = Marshal.StringToBSTR(pszPath);
            SHGetFolderPathW(IntPtr.Zero, nFolder, IntPtr.Zero, SHGFP_TYPE_CURRENT, bstr);
            string path = Marshal.PtrToStringBSTR(bstr);
            int index = path.IndexOf('\0');
            string path2 = path.Substring(0, index);
            Marshal.FreeBSTR(bstr);
            return path2;
        }


        public static string GetSpecialFolderPath(uint csidl)
        {
            return SHGetFolderPath((int)(csidl | CSIDL_FLAG_CREATE));
        }

        public static string GetDeskDir()
        {
            return GetSpecialFolderPath(CSIDL_COMMON_DESKTOPDIRECTORY);
        }

        public static string GetProgramsDir()
        {
            return GetSpecialFolderPath(CSIDL_COMMON_PROGRAMS);
        }

        public static string GetFavoriteDir()
        {
            return GetSpecialFolderPath(CSIDL_COMMON_FAVORITES);
        }
    }
}
