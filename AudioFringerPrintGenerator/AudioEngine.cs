using System;

namespace AudioFringerPrintGenerator
{
    internal class AudioEngine
    {
        public AudioEngine()
        {
        }

        internal void Close()
        {
            throw new NotImplementedException();
        }

        internal AudioSamples ReadMonoFromFile(string filename, int sampleRate, int startPositionInMS, int toReadInMS)
        {
            throw new NotImplementedException();
        }

        internal FingerprintSignature CreateFingerprint(AudioSamples samples, SpectrogramConfig spectrogramConfig)
        {
            throw new NotImplementedException();
        }
    }
}