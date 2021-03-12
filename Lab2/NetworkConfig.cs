namespace Lab2
{
    public class NetworkConfig
    {
        // 1 - black color
        //-1 - white color
        public readonly double[] inputVector = {
            1, 1, 1, 1,
            1, -1, -1, 1,
            1, 1, 1, 1,
            1, 1, -1, -1
        };

        private readonly double[] firstSampleVector = {
            1, -1, -1, -1,
            1, -1, -1, -1,
            1, -1, -1, -1,
            1, 1, 1, 1
        };
        
        private readonly double[] secondSampleVector = {
            1, 1, 1, 1,
            1, -1, -1, 1,
            1, 1, 1, 1,
            1, -1, -1, -1
        };

        public readonly double[][] sampleVectors;
        
        public const int VectorLength = 16;
        
        public const double E_max = 0.01;

        public NetworkConfig()
        {
            sampleVectors = new double[][] {firstSampleVector, secondSampleVector};
        }
    }
}