
namespace Spotify.Database
{
    public static class StringExtentions
    {
        public static int LevenshteinDistance(this string @this, string comparedstring)
        {
            var n = @this.Length;
            var m = comparedstring.Length;

            var matrix = new int[n + 1, m + 1];

            if (n == 0)
                return m;
            if (m == 0)
                return n;

            for(int i = 0; i <= n; matrix[i, 0] = i++) { }
            for(int i = 0; i <= m; matrix[0, i] = i++) { }

            for(int i = 1; i <= n; i++)
            {
                for(int j = 1; j <= m; j++)
                {
                    int cost = (@this[i - 1] == comparedstring[j - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[n, m];
        }

        /// <summary>
        /// Checks if one string is similiar to another using levenshtein algorithm
        /// </summary>
        public static bool IsSimiliar(this string @this, string comparedString)
            => @this.LevenshteinDistance(comparedString) <= 5;
    }
}
