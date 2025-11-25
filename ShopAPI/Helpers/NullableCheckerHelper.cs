namespace ShopAPI.Helpers
{
    public static class NullableCheckerHelper
    {
        // Check Nullable || Check Properties
        public static T CheckNullable<T>(this T obj) where T : class
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value is null)
                    throw new ArgumentNullException($"Property {property.Name} is null!");
            }

            return obj;
        }

        // Check Not Found
        public static T CheckNotFound<T>(this T obj) where T : class
        {
            if (obj is null)
                throw new KeyNotFoundException("Entity not found!");
            return obj;
        }
    }
}
