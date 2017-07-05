using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mapping
{
    /// <summary>  
    /// <remarks>Xml序列化与反序列化</remarks>  
    /// <creator>zhangdapeng</creator>  
    /// </summary>  
    public class XmlSerializeUtil
    {
        #region 反序列化  
        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="xml">XML字符串</param>  
        /// <returns></returns>  
        public static object DeserializeString(Type type, string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }
        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="xml">路径</param>  
        /// <returns></returns>  
        public static object Deserialize(Type type, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            return DeserializeString(type, xmlDoc.OuterXml);
        }
        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <param name="type"></param>  
        /// <param name="xml"></param>  
        /// <returns></returns>  
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化  
        /// <summary>  
        /// 序列化并且写到文件  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="obj">对象</param>  
        /// <param name="path">文件保存路径</param>  
        /// <returns></returns>  
        public static void Serializer(Type type, object obj, string path)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, obj);
            writer.Close();
        }
        /// <summary>  
        /// 序列化 返回文本  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="obj">对象</param>   
        /// <returns></returns>  
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            //序列化对象  
            xml.Serialize(Stream, obj);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }
        #endregion
    }
}
