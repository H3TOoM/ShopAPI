namespace ShopAPI.Helpers
{
    public static class NullableCheckerHelper
    {
        // Check Nullable || Check Properties
        public static bool IsNullEntity<T>(this T obj) where T : class
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value is null)
                    return true;
            }

            return false;
        }

        // Check Not Found
        public static bool IsNotFound<T>(this T obj) where T : class
        {
            if (obj is null)
                return true;
            return false;
        }
    }
}
