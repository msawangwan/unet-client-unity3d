﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace Engine.pRNG {
    [TestFixture]
    public class pRNG_Test {
        private const long seed = 1482284596187742126;
        private const long maxl = 1 << 64;
        private const ulong maxul = 1 << 64;
        private const int maxi = 1 << 32;
        private const uint maxui = 1 << 32;
        private const ulong maxull = 1 << 128;

        private pRNG r;
        private int times;

        [SetUp]
        public void Init() {
            times = 30;
        }

        [Test]
        public void Generate() {
            Debug.Log("engine/prng: can we generate anything at all");
            r = new pRNG(seed);
            for (int i = 0; i < times; i++) {
                int a = r.Intn(50);
                float b = r.Float32();
                float c = r.InRangef(-10, 10);
            }
            Debug.Log("test complete");
        }

        [Test]
        public void Overflow() {
            Debug.Log("engine/prng: handle overflow when seed is initialised");
            r = new pRNG(seed);
            Assert.True(r.Seed >= 0, "engine/prng: seed value overflow: {0}", r.Seed);
            r = new pRNG(maxl);
            Assert.True(r.Seed >= 0, "engine/prng: seed value overflow: {0}", r.Seed);
            r = new pRNG(maxul);
            Assert.True(r.Seed >= 0, "engine/prng: seed value overflow: {0}", r.Seed);
            r = new pRNG(maxi);
            Assert.True(r.Seed >= 0, "engine/prng: seed value overflow: {0}", r.Seed);
            r = new pRNG(maxui);
            Assert.True(r.Seed >= 0, "engine/prng: seed value overflow: {0}", r.Seed);
            r = new pRNG(maxull);
            Assert.True(r.Seed >= 0, "engine/prng: seed value overflow: {0}", r.Seed);
        }

        [Test]
        public void ZeroValueInput() {
            Debug.Log("engine/prng: handle zero as an input parameter");
            r = new pRNG(seed);
            for (int i = 0; i < times; i++){
                try {
                    int a = r.Intn(0);
                    float b = r.InRangef(0, 0);
                } catch (System.DivideByZeroException dbze) {
                    Assert.Fail("engine/prng: caught divide by zero exception");
                }
            }
        }

        [Test]
        public void ValidateRanges() {
            Debug.Log("engine/prng: validate all values fall within expected bounds");
            r = new pRNG(seed);

            int max = 50;
            float minf = -50;
            float maxf = 50;

            for (int i = 0; i < times; i++) {
                int a = r.Intn(max);
                Assert.True(a >= 0 && a < max, "engine/prng: generated an out of range value: {0}", a);
                float b = r.Float32();
                Assert.True(b >= 0 && b < 1.0f, "engine/prng: generated an out of range value: {0}", b);
                float c = r.InRangef(minf, maxf);
                Assert.True(c >= minf && c < maxf, "engine/prng: generated an out of range value: {0}", c);
            }

            Debug.Log("test complete");
        }

    }
}
