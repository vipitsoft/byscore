using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NPOI.XWPF.UserModel;

namespace BYSCORE.UI
{
    public static class NpoiHeplper
    {
        /// <summary>
        /// 输出模板docx文档(使用字典)
        /// </summary>
        /// <param name="tempFilePath">docx文件路径</param>
        /// <param name="outPath">输出文件路径</param>
        /// <param name="data">字典数据源</param>
        public static void Export(string tempFilePath, string outPath, Dictionary<string, string> data)
        {
            using (FileStream stream = File.OpenRead(tempFilePath))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                //遍历段落                  
                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKey(para, data);
                }
                //遍历表格      
                foreach (var table in doc.Tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                ReplaceKey(para, data);
                            }
                        }
                    }
                }
                //写文件
                FileStream outFile = new FileStream(outPath, FileMode.Create);
                doc.Write(outFile);
                outFile.Close();
            }
        }

        private static void ReplaceKey(XWPFParagraph para, Dictionary<string, string> data)
        {
            string text = "";
            foreach (var run in para.Runs)
            {
                text = run.ToString();
                foreach (var key in data.Keys)
                {
                    //$$模板中数据占位符为$KEY$
                    if (text.Contains($"${key}$"))
                    {
                        text = text.Replace($"${key}$", data[key]);
                    }
                }
                run.SetText(text, 0);
            }
        }


        /// <summary>
        /// 输出模板docx文档(使用反射)
        /// </summary>
        /// <param name="tempFilePath">docx文件路径</param>
        /// <param name="outPath">输出文件路径</param>
        /// <param name="data">对象数据源</param>
        public static void ExportObjet(string tempFilePath, string outPath, object data)
        {
            using (FileStream stream = File.OpenRead(tempFilePath))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                //遍历段落                  
                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKeyObjet(para, data);
                }
                //遍历表格      
                foreach (var table in doc.Tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                ReplaceKeyObjet(para, data);
                            }
                        }
                    }
                }
                //写文件
                FileStream outFile = new FileStream(outPath, FileMode.Create);
                doc.Write(outFile);
                outFile.Close();
            }
        }
        private static void ReplaceKeyObjet(XWPFParagraph para, object model)
        {
            string text = "";
            Type t = model.GetType();
            PropertyInfo[] pi = t.GetProperties();
            foreach (var run in para.Runs)
            {
                text = run.ToString();
                foreach (PropertyInfo p in pi)
                {
                    //$$模板中数据占位符为$KEY$
                    string key = $"${p.Name}$";
                    if (text.Contains(key))
                    {
                        try
                        {
                            text = text.Replace(key, p.GetValue(model, null).ToString());
                        }
                        catch (Exception)
                        {
                            //可能有空指针异常
                            text = text.Replace(key, "");
                        }
                    }
                }
                run.SetText(text, 0);
            }
        }

        /// <summary>
        /// 实体输出模板docx文档(使用反射)
        /// </summary>
        /// <param name="tempFilePath">docx文件路径</param>
        /// <param name="data">对象数据源</param>
        public static MemoryStream ExportModel(string tempFilePath, object data)
        {
            using (FileStream stream = File.OpenRead(tempFilePath))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                //遍历段落                  
                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKeyModel(para, data);
                }
                XWPFParagraph xWPFParagraph = doc.CreateParagraph();
                XWPFRun xWPFRun = xWPFParagraph.CreateRun();
                xWPFRun.SetText("aaaa");
                //写文件
                //FileStream outFile = new FileStream(outPath, FileMode.Create);
                MemoryStream memoryStream = new MemoryStream();
                doc.Write(memoryStream);
                //outFile.Close();
                return memoryStream;
            }
        }
        private static void ReplaceKeyModel(XWPFParagraph para, object model)
        {
            string text = "";
            Type t = model.GetType();
            PropertyInfo[] pi = t.GetProperties();
            foreach (var run in para.Runs)
            {
                text = run.ToString();
                foreach (PropertyInfo p in pi)
                {
                    //$$模板中数据占位符为$KEY$
                    string key = $"${p.Name}$";
                    if (text.Contains(key))
                    {
                        try
                        {
                            text = text.Replace(key, p.GetValue(model, null).ToString());
                        }
                        catch (Exception)
                        {
                            //可能有空指针异常
                            text = text.Replace(key, "");
                        }
                    }
                }
                run.SetText(text, 0);
            }
        }
    }
}
