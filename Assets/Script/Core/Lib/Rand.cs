// http://stackoverflow.com/questions/15500621/c-c-algorithm-to-produce-same-pseudo-random-number-sequences-from-same-seed-on

namespace Core.Lib {
    public class Rand {
        private int Seed;

        public Rand(int seed) {
            this.Seed = seed;
        }

        public int pRNG() {
            this.Seed = (8253729 * Seed + 2396403);
            return Seed % 32767;
        }

        public int pRNGrange(int bound) {
            this.Seed = (8253729 * Seed + 2396403);
            int n = Seed % 32767;
            return n % bound;
        }
    }
}
