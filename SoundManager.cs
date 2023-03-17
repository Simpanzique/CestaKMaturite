using NAudio.Wave;
using Petr_RP_CestaKMaturite.Properties;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Petr_RP_CestaKMaturite;

internal class SoundManager
{
    //private static readonly ComponentResourceManager rm = new(typeof(Resources));
    //private readonly string file;
    //public static bool bannedSound = true;

    //public SoundManager(string _file)
    //{
    //    file = _file;
    //}

    //public void PlaySound()
    //{
    //    if (!bannedSound)
    //    {
    //        using (var stream = (Stream)rm.GetObject(file))
    //        using (var waveOut = new WaveOut())
    //        {
    //            waveOut.Init(new WaveFileReader(stream));
    //            waveOut.Play();
    //        }
    //    }
    //}
    private static readonly ComponentResourceManager rm = new(typeof(Resources));
    Stream stream;
    WaveOut waveOut;
    readonly string file;
    public static bool bannedSound = true;

    public SoundManager(string _file)
    {
        file = _file;

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

            waveOut.Play();
        }
    }
}
