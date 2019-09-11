﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioFringerPrintGenerator
{
    public class AudioFringerPrintMaker : IDisposable
    {
        private bool alreadyDisposed;
        private AudioEngine audioEngine;
        private static Random random = new Random();

        public AudioFringerPrintMaker()
        {
            alreadyDisposed = false;
            audioEngine = new AudioEngine();
        }

        ~AudioFringerPrintMaker()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            alreadyDisposed = true;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!alreadyDisposed)
            {
                if (audioEngine != null)
                {
                    audioEngine.Close();
                    audioEngine = null;
                }
            }
        }

        public void Close()
        {
            Dispose(false);
        }

        public FingerprintSignature CreateAudioFingerprint(string key, string filename)
        {
            return CreateAudioFingerprint(key, filename, 0, -1);
        }

        public FingerprintSignature CreateAudioFingerprint(string key, string filename, int startPositionInMS, int toReadInMS)
        {
            SpectrogramConfig spectrogramConfig = new DefaultSpectrogramConfig();

            AudioSamples samples = null;
            try
            {
                // First read audio file and downsample it to mono 5512hz
                samples = audioEngine.ReadMonoFromFile(filename, spectrogramConfig.SampleRate, startPositionInMS, toReadInMS);
            }
            catch
            {
                return null;
            }

            // No slice the audio is chunks seperated by 11,6 ms (5512hz 11,6ms = 64 samples!)
            // An with length of 371ms (5512kHz 371ms = 2048 samples [rounded])

            FingerprintSignature fingerprint = audioEngine.CreateFingerprint(samples, spectrogramConfig);
            if (fingerprint != null)
            {
                fingerprint.Reference = key;
            }
            return fingerprint;
        }

    }
}
