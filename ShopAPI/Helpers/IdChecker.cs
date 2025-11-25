namespace ShopAPI.Helpers
{
    public static class IdChecker
    {
        public static int CheckId(this int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID!");
            return id;
        }
    }
}
