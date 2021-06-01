using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application.Files.Xml
{
    public class Xml<T> : IFile<T> where T : class, new()
    {
        //TODO usar aca excepciones (try, catch) para validar el serialize y deserializa. Encerrando los using completos
        public bool Read(string file, out T data)
        {                    
            using (XmlTextReader xmlTextReader = new XmlTextReader(file))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                data = (T)serializer.Deserialize(xmlTextReader);
                return serializer.CanDeserialize(xmlTextReader);       
            }
        }

        public bool Save(string file, T data)
        {
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(file, Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));               
                if (data != null)
                {
                    serializer.Serialize(xmlTextWriter, data);
                    return true;
                }
                else 
                {
                    return false;
                }        
            }
        }
    }
}
