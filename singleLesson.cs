using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Guitar
{
    public class singleLesson
    {
        private String type;
        private List<Chord> chordsList = new List<Chord>();
        private List<double> delayList = new List<double>();
        private string songName;

        public singleLesson(String type, String name , List<Chord> lessons, List<double> delays= null)
        {
            this.type = type;
            this.chordsList = lessons;
            this.delayList = delays;
            this.songName = name;
        }

        public singleLesson(String type, List<Chord> list)
        {
            this.type = type;
            this.chordsList = list;
        }

        public String getType()
        {
            return this.type;
        }

        public List<Chord> getchordsList()
        {
            return this.chordsList;
        }

        public List<double> getDelaysList()
        {
            return this.delayList;
        }
    }
}
