﻿namespace Library.DataContext.JWT
{
    public class JWTSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresInMinutes { get; set; }
    }
}
