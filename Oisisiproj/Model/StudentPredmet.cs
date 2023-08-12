using Oisisiproj.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oisisiproj.Model
{
    public class StudentPredmet : ISerializable
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int PredmetId { get; set; }
       

        public StudentPredmet() { }

        public StudentPredmet(int studentId, int subjectId)
        {
            StudentId = studentId;
            PredmetId = subjectId;
            
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            StudentId = int.Parse(values[1]);
            PredmetId = int.Parse(values[2]);
            
        }

        public string[] ToCSV()
        {
            string[] csValues =
            {
                Id.ToString(),
                StudentId.ToString(),
                PredmetId.ToString(),
                
            };
            return csValues;
        }
    }
}
