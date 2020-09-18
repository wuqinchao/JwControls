using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace Jw.Winform.Ctrls
{
    public class JwIconfontManager
    {
        private static PrivateFontCollection FontCollection = new PrivateFontCollection();

        private static Dictionary<string, IconfontItem> _FontDict = new Dictionary<string, IconfontItem>();

        public const string DefaultFont = "Iconfont";

        static JwIconfontManager()
        {
            RegistFont(DefaultFont, Jw.Winform.Ctrls.Properties.Resources.iconfont, TypeDict, 1f);
        }

        public static string[] GetFonts()
        {
            return _FontDict.Keys.ToArray();
        }

        public static FontFamily GetFontFamily(string key)
        {
            var keyName = key;
            if(string.IsNullOrWhiteSpace(key) || !_FontDict.ContainsKey(key.ToUpper().Trim()))
            {
                keyName = DefaultFont;
            }
            return _FontDict[keyName.ToUpper().Trim()].Family;
        }

        public static IconfontItem GetFontInfo(string key)
        {
            return ContainsFont(key) ? _FontDict[key.ToUpper().Trim()] : null;
        }

        public static string GetFontIcon(string icon, string font)
        {
            return ContainsIcon(icon, font) ? _FontDict[font.ToUpper().Trim()].IconDict[icon] : string.Empty;
        }
            
        /// <summary>
        /// 注册图标字体
        /// </summary>
        /// <param name="name">字体KEY</param>
        /// <param name="fontStream">字体文件字节数组</param>
        /// <param name="dict">图标字典</param>
        /// <param name="pointZoom">图标转字体时的缩放比例</param>
        /// <remarks>字典项中的value必须为"\uxxxx"格式 </remarks>
        /// <returns>是否注册成功</returns>
        public static bool RegistFont(string name, byte[] fontStream, Dictionary<string, string> dict, float pointZoom = 1f)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            var realname = name.ToUpper().Trim();
            var index = 0;
            if (_FontDict.ContainsKey(realname)) return false;
            unsafe
            {
                fixed (byte* pFontData = fontStream)
                {
                    FontCollection.AddMemoryFont((System.IntPtr)pFontData, fontStream.Length);
                    index = FontCollection.Families.Length - 1;
                }
            }
            _FontDict.Add(realname, new IconfontItem() { Name = realname, Family = FontCollection.Families[index], IconDict = dict, PointZoom=pointZoom });
            return true;
        }
        /// <summary>
        /// 注册图标字体
        /// </summary>
        /// <param name="name">字体KEY</param>
        /// <param name="fontStream">字体文件路径</param>
        /// <param name="dict">图标字典</param>
        /// <param name="pointZoom">图标转字体时的缩放比例</param>
        /// <remarks>字典项中的value必须为"\uxxxx"格式 </remarks>
        /// <returns>是否注册成功</returns>
        public static bool RegistFont(string name, string fontPath, Dictionary<string, string> dict, float pointZoom = 1f)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            var realname = name.ToUpper().Trim();
            var index = 0;
            FontCollection.AddFontFile(fontPath);
            index = FontCollection.Families.Length - 1;
            _FontDict.Add(realname, new IconfontItem() { Name = realname, Family = FontCollection.Families[index], IconDict = dict, PointZoom = pointZoom });
            return true;
        }
        /// <summary>
        /// 指定的图标字体是否已注册
        /// </summary>
        /// <param name="key">字体KEY</param>
        /// <returns>是否已注册</returns>
        public static bool ContainsFont(string key)
        {
            return !string.IsNullOrWhiteSpace(key) 
                && _FontDict.ContainsKey(key.ToUpper().Trim());
        }
        /// <summary>
        /// 指定的图标是否存在
        /// </summary>
        /// <param name="icon">图标KEY</param>
        /// <param name="key">字体KEY</param>
        /// <returns>是否存在</returns>
        public static bool ContainsIcon(string icon, string key)
        {
            return !string.IsNullOrWhiteSpace(key) 
                && _FontDict.ContainsKey(key.ToUpper().Trim()) 
                && _FontDict[key.ToUpper().Trim()].IconDict.ContainsKey(icon);
        }
        public static Font GetIconFont(string fontkey, int insize)
        {
            var item = JwIconfontManager.GetFontInfo(fontkey);
            var size = insize * item.PointZoom;
            var font = new Font(item.Family, size, FontStyle.Regular, GraphicsUnit.Point);
            return font;
        }

        private static Dictionary<string, string> TypeDict = new Dictionary<string, string>()
        {
            {"device-usb","\ue62a"},
            {"icon-calendar-fill","\ue75c"},
            {"icon-safe-fill","\ue62c"},
            {"icon-safe","\ue62d"},
            {"icon-calendar","\ue630"},
            {"device-usb2","\ue631"},
            {"form-min","\ue6ba"},
            {"form-restore","\ue60d"},
            {"edit-copy","\ued2c"},
            {"edit-zoom-out-fill","\ue78f"},
            {"form-max","\ue6a6"},
            {"form-close","\ue66b"},
            {"edit-zoom-in","\ue7c5"},
            {"edit-background","\ue7cc"},
            {"edit-clear","\ue7cd"},
            {"edit-rss","\ue7d4"},
            {"arrow-left-circle","\ue64e"},
            {"arrow-down-circle","\ue64f"},
            {"arrow-right-circle","\ue650"},
            {"arrow-up-circle","\ue655"},
            {"arrow4-left","\ue666"},
            {"arrow4-down","\ue667"},
            {"arrow4-right","\ue669"},
            {"arrow4-up","\ue66a"},
            {"down","\ue607"},
            {"arrow1-up","\ue659"},
            {"arrow6-left","\ue65b"},
            {"arrow1-left","\ue65f"},
            {"arrow6-right","\ue661"},
            {"arrow1-down","\ue662"},
            {"arrow6-down","\ue663"},
            {"arrow1-right","\ue664"},
            {"arrow6-up","\ue665"},
            {"export","\ue61d"},
            {"home-fill","\ue621"},
            {"import","\ue642"},
            {"home","\ue629"},
            {"device-satellite","\ue6a4"},
            {"report2","\ue61c"},
            {"edit-layout","\ue645"},
            {"edit-div","\ue61f"},
            {"icon-4s","\ue627"},
            {"book","\ue61e"},
            {"liangdu","\ue61a"},
            {"icon-label-fill","\ue618"},
            {"icon-label","\ue724"},
            {"media-record-fill","\ue651"},
            {"media-music","\ue615"},
            {"device-database-fill","\ue633"},
            {"hit","\ue683"},
            {"device-database","\ue639"},
            {"cloud-l","\ue61b"},
            {"cloud-fill","\ue6a7"},
            {"device-mail","\ue9e5"},
            {"device-phone","\ue617"},
            {"bill-shopcar-fill","\ue610"},
            {"bill-shopcart","\ue619"},
            {"report1","\ue7cf"},
            {"bill-bag-fill","\ue611"},
            {"report","\ue641"},
            {"quxian2","\ue625"},
            {"bill-bag","\ue989"},
            {"bill-money","\ue60f"},
            {"bill-qianbao","\ue63c"},
            {"bill-scan-fill","\ue680"},
            {"bill-money-more","\ue602"},
            {"bill-barcode","\ue634"},
            {"bill-scan","\ue656"},
            {"report3","\ue60c"},
            {"bill-qianbao-fill","\ue6a5"},
            {"bill-qrqcode","\ue601"},
            {"time","\ue632"},
            {"time-fill","\ue91a"},
            {"device-lock-fill","\ue658"},
            {"device-iso-fill","\ue668"},
            {"device-iso","\ue66f"},
            {"device-lock","\ue6ce"},
            {"device-key","\ue60a"},
            {"device-boardcast","\ue600"},
            {"edit-folder-close","\ue60b"},
            {"lianjie","\ue622"},
            {"device-map-fill","\ue62b"},
            {"bill-card-fill","\ue609"},
            {"device-earth","\ue606"},
            {"edit-folder-open","\uea96"},
            {"edit-folder-close-fill","\ue6ed"},
            {"device-map","\ue614"},
            {"device-pc-fill","\ue654"},
            {"device-bluetooth-fill","\ue741"},
            {"edit-folder-open-fill","\ue671"},
            {"device-pc-l","\ue78c"},
            {"edit-delete","\ue65e"},
            {"device-servers","\ue628"},
            {"device-wifi","\ue640"},
            {"bill-card","\ue70d"},
            {"edit-cut","\ue620"},
            {"device-bluetooth","\ue748"},
            {"arrow2-left","\ue6bb"},
            {"arrow2-right","\ue6bc"},
            {"camera_icon","\ue657"},
            {"icon-gif","\ue90e"},
            {"media-play-fill","\ue653"},
            {"media-stop-fill","\ue612"},
            {"media-pause-fill","\ue67d"},
            {"media-pay","\ue624"},
            {"media-backward-fill","\ue7f3"},
            {"media-forword-fill","\ue7f4"},
            {"rotate-left","\ue65a"},
            {"rotate-right","\ue65d"},
            {"media-last-fill","\ue7ff"},
            {"media-first-fill","\ue800"},
            {"media-pause","\ue6c2"},
            {"media-stop","\ue62f"},
            {"poweroff-fill","\ue779"},
            {"task","\ue626"},
            {"arrow2-down","\ue694"},
            {"arrow2-up","\ue616"},
            {"icon-upload","\ue65c"},
            {"user-fill","\ue608"},
            {"media-micphone-fill","\ue660"},
            {"media-mic-fill","\ue603"},
            {"users","\ue604"},
            {"user-idcard-fill","\ue605"},
            {"user","\ue60e"},
            {"media-mic","\ue698"},
            {"hit1","\ue613"},
            {"hit2","\uebd9"},
            {"device-camera","\ue62e"},
            {"device-server","\ue726"},
            {"media-micphone","\ue6b2"},
            {"media-vol-min","\ue8b8"},
            {"media-vol-max","\ue8b9"},
            {"media-mic-some","\ue623"},
            {"confirm","\ue74c"},
            {"arrow5-down","\ue750"},
            {"time-history","\ue752"},
            {"icon-help","\ue753"},
            {"arrow5-left","\ue756"},
            {"icon-info","\ue75e"},
            {"op1-reeor","\ue75f"},
            {"op1-reduce","\ue760"},
            {"ring","\ue761"},
            {"arrow5-up","\ue762"},
            {"arrow5-right","\ue764"},
            {"op1-succ","\ue767"},
            {"poweroff","\ue768"},
            {"edit-upload","\ue76b"},
            {"icon-warning","\ue76f"},
            {"edit-refresh","\ue772"},
            {"op2-plus","\ue777"},
            {"device-mail-fill","\ue77e"},
            {"icon-help-fill","\ue783"},
            {"op2-reduce","\ue788"},
            {"op2-reeor","\ue789"},
            {"edit-pic-fill","\ue78a"},
            {"op2-succ","\ue78d"},
            {"star","\ue79d"},
            {"star-fill","\ue79e"},
            {"ring-fill","\ue7a2"},
            {"op-plus","\ue7b0"},
            {"op-minus","\ue7b1"},
            {"arrow3-down","\ue7b2"},
            {"comments","\ue7b5"},
            {"edit-paste","\ue7b6"},
            {"icon-info-fill","\ue730"},
            {"op1-plus","\ue742"},
            {"arrow3-up","\ue749"},
            {"edit-pic","\ue6ff"},
            {"arrow3-right","\ue743"},
            {"arrow3-left","\ue744"},
            {"edit-attachent","\ue745"},
            {"op-reeor","\ue747"},
            {"op-succ","\ue763"},
            {"edit-zoom-out","\ue76c"},
            {"set","\ue76d"},
            {"set-fill","\ue78e"},
            {"icon-warning-fill","\ue790"},
            {"edit-search","\ue7b3"},
        };
    }

    public class IconfontItem
    {
        private string name;
        private float pointZoom;
        private FontFamily family;
        private Dictionary<string, string> _dict;
        public string Name { get => name; set => name = value; }
        public FontFamily Family { get => family; set => family = value; }
        public Dictionary<string, string> IconDict { get => _dict; set => _dict = value; }
        public float PointZoom { get => pointZoom; set => pointZoom = value; }
    }
}
