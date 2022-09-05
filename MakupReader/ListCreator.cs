using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MarkupReader.FileReader;

namespace MarkupReader
{
    internal class ListCreator<T>  where T : AbstractMarkupObject<T>, new()
    {

        private Stack<T> stack;
        private List<T> list;
        private IFileConverter fileConverter;
        private FileInfo file;
        private IConfigurationListCreator config;

        public ListCreator(IFileConverter fileConverter, FileInfo file, IConfigurationListCreator configurationListCreator)
        {
            stack = new Stack<T>();
            list = new List<T>();
            this.fileConverter = fileConverter;
            this.file = file;
            this.config = configurationListCreator;
        }

        public List<T> createMarkupObj()
        {

            return GetObjectsAsync();
        }

        private List<T> GetObjectsAsync()
        {

            List<string> Nodes = fileConverter.ConvertToList(file).Result;


            foreach (string Node in Nodes)
            {

                if (Node.Contains(config.MarkupStart))
                {

                    T Obj = Get_Object(Node);
                    if (Node.Contains(config.MarkupEnd) || Node.Contains(config.MarkupEndStart))
                    {
                        if (Node.Contains(config.MarkupEnd))
                        {
                            AddNode(Obj);
                        }
                        else
                        {
                            AddNodeExt();
                        }

                    }
                    else
                    {
                        if (Node.Contains(config.MarkupStartEnd))
                        {
                            stack.Push(Obj);
                        }
                    }
                }
                else
                {
                    var test = stack.Peek();
                    test.Text = Node;
                }
            }
            return (list);
        }
        
        private int CheckPos(string name)
        {
            return (name.Contains(" ") ? name.IndexOf(" ") : name.Length - 1);

        }
        private void AddNode(T xmlObj)
        {
            if (stack.Count > 0)
            {

                var parent = stack.Peek();
                if (parent != null)
                {
                    parent.Children.Add(xmlObj);
                }
            }
            else
            {
                list.Add(xmlObj);
            }
        }
        private void AddNodeExt()
        {
            var node = stack.Pop();
            if (stack.Count > 0)
            {

                var parent = stack.Peek();
                if (parent != null)
                {
                    parent.Children.Add(node);
                }
            }
            else
            {
                list.Add(node);
            }
        }
        private T Get_Object(string Node)
        {
                T Obj = new T();
                  Obj.Name = GetNameNode(Node);
              
                  GetAttibutes(Node, Obj);
                return Obj;
        }
        private string GetNameNode(string name)
        {
            return name.Substring(1, CheckPos(name)).Replace("/", "").Replace(">", "");
        }
        private void GetAttibutes(string Node,T Obj)
        {

            if (String.IsNullOrWhiteSpace(Node.Substring(CheckPos(Node)).Replace("/", "").Replace(">", "")))
            {
                return;
            }
            else
            {
                string Atti = GetDictionaryField(Obj.GetType().GetRuntimeFields());
                if (String.IsNullOrEmpty(Atti))
                {
                    return;
                }
                else
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    string CleanString = Node.Substring(CheckPos(Node)).Replace("/", "").Replace(">", "").Trim();
                    string[] parts = CleanString.Split(' ');
                    foreach (string part in parts)
                    {
                        var AttiSplit = part.Split('=');
                        if (AttiSplit.Length > 1)
                        {
                            dict.Add(AttiSplit[0], AttiSplit[1].Replace("\"", ""));
                        }
                    }
                    if(dict.Count > 0)
                    {
                        Obj.GetType().GetRuntimeField(Atti).SetValue(Obj, dict);
                    } 
                }
            }
        }
        private string GetDictionaryField(IEnumerable<FieldInfo> fields)
        {
            foreach (FieldInfo prop in fields)
            {
                if (prop.FieldType != typeof(string) && typeof(IDictionary<string, string>).IsAssignableFrom(prop.FieldType))
                {
                    return prop.Name;
                }
            }
            return string.Empty;
        }
    }
}
