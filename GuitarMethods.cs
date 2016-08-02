using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar
{
    public abstract class GuitarMethods
    {
        /*Set Guitar mode.
         *@param p_mode -mode to set from Enum Mode
         *return value - true if chmod succeeded otherwise false.
        */
        public abstract bool SetMode(Mode p_mode);

        public abstract bool LightLED(Led[] p_led);

        public abstract bool PlayChord(Chord p_chord);

        public  void Stream(Chord[] p_chords,double[] p_msDelay,bool p_wait) { }

    }

    public enum Mode
    {
        FullControl,
        Chords,
        Stream,
    }

    public enum Chord
    {
        A,B,C,D,E,F,G,Am,Bm,Cm,Dm,Em,Fm,Gm
    }
}
