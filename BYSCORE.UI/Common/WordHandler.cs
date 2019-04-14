using System;
using System.ComponentModel;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace BYSCORE.UI
{
    public static class WordHandler
    {
        public static MemoryStream ExportWordByFields<T>(T mod, string TempleteFilePath, string ExpFilePath)
        {
            if (mod == null)
            {
                throw new Exception("模型为空！");
            }

            System.Reflection.PropertyInfo[] properties = mod.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("模型属性为空！");
            }

            if (!File.Exists(TempleteFilePath))
            {
                throw new Exception("指定路径的模板文件不存在！");
            }


            Document doc = new Document();
            doc.LoadFromFile(TempleteFilePath);

            foreach (var item in properties)
            {
                string name = item.Name; //属性名称
                string value = item.GetValue(mod, null).ToString();  //属性值
                doc.Replace(name, value, true, true);
            }

            //doc.SaveToFile(ExpFilePath, FileFormat.Docx);
            MemoryStream memoryStream = new MemoryStream();
            doc.SaveToStream(memoryStream, FileFormat.Docx);
            memoryStream.Position = 0;
            doc.Close();
            return memoryStream;
        }
    }
}
