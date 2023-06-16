namespace SecretsMocker.Helpers
{
    public static class MockHelpers
    {
        private const string SeedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Rand = new();
        
        public static string GenerateId()
        {
            var suffix = new string(Enumerable.Repeat(SeedChars, 6).Select(s => s[Rand.Next(s.Length)]).ToArray());
            return $"p-{suffix}";
        }
    }
}
