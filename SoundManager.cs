using NAudio.Wave;
using Petr_RP_CestaKMaturite.Properties;
using System.ComponentModel;

namespace Petr_RP_CestaKMaturite;

internal class SoundManager
{
    ComponentResourceManager rm = new(typeof(Resources));
    Stream stream;
    WaveOut waveOut;
    readonly string file;
    readonly float volume;
    public static bool bannedSound = true;

    public SoundManager(string _file, float _volume)
    {
        file = _file;
        volume = _volume;

        stream = (Stream)rm.GetObject(file);
        waveOut = new();
        waveOut.Init(new WaveFileReader(stream));
    }

    public void PlaySound()
    {
        waveOut.Stop();
        waveOut.Dispose();

        if (!bannedSound)
        {
            stream = (Stream)rm.GetObject(file);
            waveOut = new();
            waveOut.Init(new WaveFileReader(stream));
            waveOut.Volume = volume;

            waveOut.Play();
        }
    }
}
