
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace XML_Processing
{
  public class ObjectSerializer
  {
    public static void ObjectToXmlFile(object Data, string FileName)
    {
      FileStream stream = new FileStream(FileName, FileMode.Create);
      SoapFormatter formatter = new SoapFormatter();

      formatter.Serialize(stream, Data);

      stream.Close();
      stream.Dispose();
    }

    public static object XmlFileToObject(string FileName)
    {
      FileStream stream;
      SoapFormatter formatter = new SoapFormatter();
      object obj;

      stream = new FileStream(FileName, FileMode.Open);

      obj = formatter.Deserialize(stream);

      return obj;
    }
  }
}