using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Attendance_Master
{
   public class FingerCapture
    {
        private static ReaderCollection _readers;
        public static Reader _reader;
        private static string ReaderName = string.Empty;
       public static string ReturnListofFingerCapture()
        {
            _readers = ReaderCollection.GetReaders();
            foreach (Reader Reader in _readers)
            {
                _reader = Reader;
               ReaderName= Reader.Description.SerialNumber;
            }
            return ReaderName;
        }
    }
    public class getRecords
    {
    }
}
