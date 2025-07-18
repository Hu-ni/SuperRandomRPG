using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spartdungeon.Services
{
    public class XmlSerializerHelper
    {
        /// <summary>
        /// 파일로부터 XML 역직렬화해서 객체로 로드하기
        /// </summary>
        public static T Deserialize<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), "");


            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(fs);
            }
        }

        /// <summary>
        /// 객체를 XML로 직렬화해서 파일에 저장하기
        /// </summary>
        public static void Serialize<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), "");


            // 1. 경로에서 폴더 추출
            string dirPath = Path.GetDirectoryName(filePath);

            // 2. 폴더가 없다면 생성
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// XML 문자열로부터 객체 생성
        /// (예: 리소스를 Resources에 TextAsset으로 넣고 쓸 때)
        /// </summary>
        public static T DeserializeFromString<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), "");
            using (StringReader reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// 객체를 XML 문자열로 직렬화
        /// (디버깅용이나 네트워크 전송용)
        /// </summary>
        public static string SerializeToString<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), "");
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}
