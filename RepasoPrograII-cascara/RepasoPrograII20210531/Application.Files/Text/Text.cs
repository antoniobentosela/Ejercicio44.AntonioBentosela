using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Text
{
    public class Text<T> : IFile<T>
    {
        public bool Read(string file, out T data)
        {
           throw new NotImplementedException();
        }

        public bool Save(string file, T data)
        {
            StreamWriter sw = new StreamWriter(file);
            if (data != null)
            {
                sw.Write(data);
                sw.Close();
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
