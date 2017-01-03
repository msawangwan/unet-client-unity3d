namespace UnityLib {
    [System.Serializable]
    public class Key {
        public string bareFormat;
        public string redisFormat;

        public Key() {}

        public Key(string bareFormat) {
            this.bareFormat = bareFormat;
        }

        public Key(string bareFormat, string redisFormat) {
            this.bareFormat = bareFormat;
            this.redisFormat = redisFormat;
        }
    }
}
