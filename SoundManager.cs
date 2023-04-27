using NAudio.Wave;
using Petr_RP_CestaKMaturite.Properties;
using System.ComponentModel;

namespace Petr_RP_CestaKMaturite;
internal class SoundManager {

    private static readonly ComponentResourceManager rm = new(typeof(Resources));
    private WaveOutEvent waveOut;
    private readonly string file;
    private WaveFileReader reader;
    public static bool bannedSound = false;

    public SoundManager(string _file) {
        file = _file;
        reader = new WaveFileReader((Stream)rm.GetObject(file));
        waveOut = new WaveOutEvent();
        waveOut.Init(reader);
    }

    public void PlaySound() {
        if (!bannedSound) {
            reader.Position = 0;
            waveOut.Play();
        }
    }
}