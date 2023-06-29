namespace SecretsMocker.Helpers
{
    public static class MockHelpers
    {
        private const string LowerSeedChars = "abcdefghijklmnopqrstuvwxyz";
        private const string SeedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly Random Rand = new();
        
        public static string GenerateId()
        {
            var suffixOne = new string(Enumerable.Repeat(LowerSeedChars, 5).Select(s => s[Rand.Next(s.Length)]).ToArray());
            var suffixTwo = new string(Enumerable.Repeat(SeedChars, 27).Select(s => s[Rand.Next(s.Length)]).ToArray());
            return $"tmp.{suffixOne}_{suffixTwo}";
        }
    }
}
