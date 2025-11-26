namespace ShopAPI.Helpers
{
    public static class IdChecker
    {
        public static bool IsInValidId(this int id)
        {
            if (id <= 0)
                return true;
            return false;
        }
    }
}
