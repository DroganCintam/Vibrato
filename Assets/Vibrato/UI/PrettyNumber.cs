namespace Vibrato.UI
{
    public static class PrettyNumber
    {
        public static string Format(int value)
        {
            return value switch
            {
                < 10_000 => value.ToString("N0"),
                < 1_000_000 => $"{value / 1_000f:0.#}K",
                _ => $"{value / 1_000_000f:0.#}M"
            };
        }
    }
}