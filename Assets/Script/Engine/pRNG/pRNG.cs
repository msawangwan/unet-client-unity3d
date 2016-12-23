namespace Engine.pRNG {
    public class pRNG {
        /* algorithm constants for lcg() based on the SO post from this thread:
         *
         * http://stackoverflow.com/questions/15500621/c-c-algorithm-to-produce-same-pseudo-random-number-sequences-from-same-seed-on
         */

        private const long A = 8253729;
        private const long C = 2396403;
        private const long M = 32767;

        public ulong Seed {
            get {
                return seed;
            }
        }

        private ulong seed;

        public pRNG(ulong seed) {
            ulong p = (A * seed + C);

            if (seed <= 0) { // (seed == 0) should not be an accepted seed, only the server can generate a valid seed value
                this.seed = (ulong)System.Environment.TickCount;
            } else if (p >= ulong.MaxValue){
                this.seed = seed / 2;
            } else {
                this.seed = seed;
            }
        }

        public float InRangef(int min, int max) {
            return (Intn(max - min) + min) * Float32();
        }

        public float InRangef(float min, float max) {
            return (Intn((int)(max - min)) + min) * Float32();
        }

        public int Intn(int max) {
            if (max == 0) {
                return 0;
            }
            return (int)lcg() % max;
        }

        public float Float32() {
            return (float)lcg() / (float)M;
        }

        private ulong lcg() {
            seed = (A  * seed + C) % M;
            return seed;
        }
    }
}
