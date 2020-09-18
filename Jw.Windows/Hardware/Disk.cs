using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace vPowerManager.Share
{
    public class Disk
    {
        private string _DeviceID;

        public string DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }
        private string _VolumeName = "";

        public string VolumeName
        {
            get { return _VolumeName; }
            set { _VolumeName = value; }
        }
        private DriveType _DiskType = DriveType.Unknown;

        public DriveType DiskType
        {
            get { return _DiskType; }
            set { _DiskType = value; }
        }
        private string _FileSystem = "";

        public string FileSystem
        {
            get { return _FileSystem; }
            set { _FileSystem = value; }
        }
        private ulong _Size = 0;
        /// <summary>
        /// Size, units ("bytes")
        /// </summary>
        public ulong Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
        private ulong _FreeSpace = 0;
        /// <summary>
        /// FreeSpace, units ("bytes")
        /// </summary>
        public ulong FreeSpace
        {
            get { return _FreeSpace; }
            set { _FreeSpace = value; }
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}{1,15}{2,10}{3,20}{4,20}/{5}", DeviceID, DiskType, FileSystem, VolumeName, Size, FreeSpace);
        //}

        public static Disk[] QueryInfo()
        {
            List<Disk> disks = null;
            ManagementScope namespaceScope = new ManagementScope("\\\\.\\ROOT\\CIMV2");
            //ObjectQuery diskQuery = new ObjectQuery("SELECT * FROM Win32_LogicalDisk WHERE DriveType = " + HARD_DISK + "");
            ObjectQuery diskQuery = new ObjectQuery("SELECT * FROM Win32_LogicalDisk");
            ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(namespaceScope, diskQuery);
            ManagementObjectCollection colDisks = mgmtObjSearcher.Get();
            disks = new List<Disk>();
            foreach (ManagementObject objDisk in colDisks)
            {
                Disk disk = new Disk()
                {
                    DeviceID = objDisk["DeviceID"].ToString(),
                    DiskType = (DriveType)int.Parse(objDisk["DriveType"].ToString()),
                };
                if (objDisk["FileSystem"] != null)
                {
                    disk.FileSystem = objDisk["FileSystem"].ToString();
                }
                if (objDisk["VolumeName"] != null)
                {
                    disk.VolumeName = objDisk["VolumeName"].ToString();
                }
                if (objDisk["Size"] != null)
                {
                    disk.Size = ulong.Parse(objDisk["Size"].ToString()) / 1024 / 1024;
                }
                if (objDisk["FreeSpace"] != null)
                {
                    disk.FreeSpace = ulong.Parse(objDisk["FreeSpace"].ToString()) / 1024 / 1024;
                }
                disks.Add(disk);
            }
            if (disks == null)
            {
                return null;
            }
            else
            {
                return disks.ToArray();
            }
        }
    }

    public enum DriveType
    {
        Unknown = 0,
        NoRootDirectory = 1,
        Removable = 2,
        Local = 3,
        Network = 4,
        CompactDisc = 5,
        Ram = 6
    }
}
