namespace Core.Lib {
    public class Rand {
        /* algorithm constants for lcg() based on the SO post from this thread:
         *
         * http://stackoverflow.com/questions/15500621/c-c-algorithm-to-produce-same-pseudo-random-number-sequences-from-same-seed-on
         */

        private const long A = 8253729;
        private const long C = 2396403;
        private const long M = 32767;

        private long Seed;

        public Rand(long seed) {
            this.Seed = seed;
        }

        public float InRangef(int min, int max) {
            return (Int32n(max - min) + min) * Float32();
        }

        public float InRangef(float min, float max) {
            return (Int32n((int)(max - min)) + min) * Float32();
        }

        public int Int32n(int max) {
            return (int)lcg() % max;
        }

        public float Float32() {
            return (float)lcg() / (float)M;
        }

        private long lcg() {
            Seed = (A  * Seed + C) % M;
            return Seed;
        }
    }
}
