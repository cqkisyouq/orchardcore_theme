namespace FlyingRat.Braksn
{
    public static class ConstString
    {
        public static string Date_Format { get;private set; } = "yyyy-MM-dd";
        public static string DefaultImage { get; private set; } = "video-01.jpg";

        #region 图片 key
        public static string Image_Template_Grid { get; } = "template-grid";
        public static string Image_Template_Left { get; } = "template-left";
        public static string Image_Template_Right { get; } = "template-right";
        public static string Image_Template_Table_Left { get; } = "template-table-left";
        public static string Image_Template_Table_Right { get; } = "template-table-right";
        public static string Image_Sidebar_Hot { get; } = "sidebar-hot";
        public static string Image_Sidebar_Banner { get; } = "sidebar-banner";
        public static string Image_List { get; } = "list-img";
        #endregion

        public static string Default_Ellipsis { get; } = "...";
    }
}
