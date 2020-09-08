namespace Trains
{
    // These operations have been benchmarked to find a fast implementation
    // See the Benchmarks project
    public static class Optimizations
    {
        public static int Ceiling(int value, int divisor)
        {
            return (value + divisor - 1) / divisor;
        }
    }
}