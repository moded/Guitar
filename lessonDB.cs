using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Guitar
{
    public class lessonDB
    {
        public Dictionary<int, singleLesson> lessonDataBase;

        public lessonDB() {
            lessonDataBase = new Dictionary<int, singleLesson>();
            return;
        }

        public void addLesson(int lessonNum, singleLesson lesson)
        {
            lessonDataBase.Add(lessonNum, lesson);
            return;
        }

        public singleLesson getLesson(int lessonNum)
        {
            return lessonDataBase[lessonNum];
        }
        
    }
        
}
