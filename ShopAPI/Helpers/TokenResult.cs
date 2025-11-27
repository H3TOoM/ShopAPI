namespace ShopAPI.Helpers
{
    /// <summary>
    /// Simple value object that captures the generated JWT token and expiry time.
    /// </summary>
    /// <param name="Token">Serialized JWT string.</param>
    /// <param name="ExpiresAt">UTC expiration timestamp.</param>
    public record TokenResult(string Token, DateTime ExpiresAt);
}



