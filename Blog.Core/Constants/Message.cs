namespace Blog.Core.Constants;

public static class Message
{
    public static class Error
    {
        public static string MinLength = "{0} không được ít hơn {1} ký tự";
        public static string MaxLength = "{0} không được vượt quá {1} ký tự";
        public static string Required = "{0} không được để trống";
        public static string NotFound = "{0} không tồn tại";
        public static string Invalid = "{0} không hợp lệ";
        public static string Exists = "{0} đã tồn tại";
    }
}