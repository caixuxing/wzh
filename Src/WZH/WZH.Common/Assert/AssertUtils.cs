namespace WZH.Common.Assert
{
    public static class AssertUtils
    {
        public static void IsObjNull(object? value, string msg)
        {
            if (null == value)
            {
                throw new CustomException(HttpStatusType.VERIFY, msg);
            }
        }
    }
}