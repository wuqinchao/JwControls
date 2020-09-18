using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Share.File
{
    public enum DataSizeType
    {
        b, B, K, M, G, T
    }
    public class FileSize
    {
        private const decimal SizeBase = 1024m;
        private const decimal SizeBase2 = 1024m * 1024m;
        private const decimal SizeBase3 = 1024m * 1024m * 1024m;
        private const decimal SizeBase4 = 1024m * 1024m * 1024m * 1024m;
        private const decimal SizeBase5 = 1024m * 1024m * 1024m * 1024m * 1024m;

        public static decimal GetSizeString(object source, DataSizeType sourceType, DataSizeType targetType)
        {
            //string s = string.Empty;
            decimal sv = Convert.ToDecimal(source);
            decimal st = 0;
            switch (sourceType)
            {
                case DataSizeType.b:
                    switch (targetType)
                    {
                        case DataSizeType.b:
                            st = sv;
                            break;
                        case DataSizeType.B:
                            st = sv / SizeBase;
                            break;
                        case DataSizeType.K:
                            st = sv / SizeBase2;
                            break;
                        case DataSizeType.M:
                            st = sv / SizeBase3;
                            break;
                        case DataSizeType.G:
                            st = sv / SizeBase4;
                            break;
                        case DataSizeType.T:
                            st = sv / SizeBase5;
                            break;
                    }
                    break;
                case DataSizeType.B:
                    switch (targetType)
                    {
                        case DataSizeType.b:
                            st = sv * SizeBase;
                            break;
                        case DataSizeType.B:
                            st = sv;
                            break;
                        case DataSizeType.K:
                            st = sv / SizeBase;
                            break;
                        case DataSizeType.M:
                            st = sv / SizeBase2;
                            break;
                        case DataSizeType.G:
                            st = sv / SizeBase3;
                            break;
                        case DataSizeType.T:
                            st = sv / SizeBase4;
                            break;
                    }
                    break;
                case DataSizeType.K:
                    switch (targetType)
                    {
                        case DataSizeType.b:
                            st = sv * SizeBase2;
                            break;
                        case DataSizeType.B:
                            st = sv * SizeBase;
                            break;
                        case DataSizeType.K:
                            st = sv;
                            break;
                        case DataSizeType.M:
                            st = sv / SizeBase;
                            break;
                        case DataSizeType.G:
                            st = sv / SizeBase2;
                            break;
                        case DataSizeType.T:
                            st = sv / SizeBase3;
                            break;
                    }
                    break;
                case DataSizeType.M:
                    switch (targetType)
                    {
                        case DataSizeType.b:
                            st = sv * SizeBase3;
                            break;
                        case DataSizeType.B:
                            st = sv * SizeBase2;
                            break;
                        case DataSizeType.K:
                            st = sv * SizeBase;
                            break;
                        case DataSizeType.M:
                            st = sv;
                            break;
                        case DataSizeType.G:
                            st = sv / SizeBase;
                            break;
                        case DataSizeType.T:
                            st = sv / SizeBase2;
                            break;
                    }
                    break;
                case DataSizeType.G:
                    switch (targetType)
                    {
                        case DataSizeType.b:
                            st = sv * SizeBase4;
                            break;
                        case DataSizeType.B:
                            st = sv * SizeBase3;
                            break;
                        case DataSizeType.K:
                            st = sv * SizeBase2;
                            break;
                        case DataSizeType.M:
                            st = sv * SizeBase;
                            break;
                        case DataSizeType.G:
                            st = sv;
                            break;
                        case DataSizeType.T:
                            st = sv / SizeBase;
                            break;
                    }
                    break;
                case DataSizeType.T:
                    switch (targetType)
                    {
                        case DataSizeType.b:
                            st = sv * SizeBase5;
                            break;
                        case DataSizeType.B:
                            st = sv * SizeBase4;
                            break;
                        case DataSizeType.K:
                            st = sv * SizeBase3;
                            break;
                        case DataSizeType.M:
                            st = sv * SizeBase2;
                            break;
                        case DataSizeType.G:
                            st = sv * SizeBase;
                            break;
                        case DataSizeType.T:
                            st = sv;
                            break;
                    }
                    break;
            }
            //s = st.ToString();
            return st;
        }

        private static DataSizeType UpgradeSizeType(DataSizeType type)
        {
            switch (type)
            {
                case DataSizeType.b:
                    return DataSizeType.B;
                case DataSizeType.B:
                    return DataSizeType.K;
                case DataSizeType.K:
                    return DataSizeType.M;
                case DataSizeType.M:
                    return DataSizeType.G;
                case DataSizeType.G:
                    return DataSizeType.T;
                default:
                    return DataSizeType.T;
            }
        }
        private static DataSizeType DegradingSizeType(DataSizeType type)
        {
            switch (type)
            {
                case DataSizeType.T:
                    return DataSizeType.G;
                case DataSizeType.G:
                    return DataSizeType.M;
                case DataSizeType.M:
                    return DataSizeType.K;
                case DataSizeType.K:
                    return DataSizeType.B;
                case DataSizeType.B:
                    return DataSizeType.b;
                default:
                    return DataSizeType.b;
            }
        }

        public static string GetAutoSizeString(object source, DataSizeType soucetype = DataSizeType.B)
        {
            string s = source.ToString();
            decimal d = Convert.ToDecimal(source);
            DataSizeType type = soucetype;
            while (d > 1000m && type != DataSizeType.T)
            {
                type = UpgradeSizeType(type);
                d = GetSizeString(source, soucetype, type);
            }
            s = d.ToString("f2") + type.ToString();
            return s;
        }
    }
}
