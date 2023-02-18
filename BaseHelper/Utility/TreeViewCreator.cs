using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reflection;

namespace BaseHelper.Utility
{
    public class TreeViewCreator 
    {

        public List<TreeViewItem> AddItems<T>(IEnumerable<T> ts) where T : class
        {
             List<TreeViewItem> itemCollection = new List<TreeViewItem>();
            
            foreach (T t in ts)
            {
                Type type = t.GetType();
                string? ChildrenCollection = GetNestedField<T>(type.GetRuntimeFields());
                if (ChildrenCollection != null)
                {
                   List<T> Childen= (List<T>) type.GetRuntimeField(ChildrenCollection).GetValue(t);
                   if(Childen.Count() != 0)
                   {
                        TreeViewItem treeViewItem = new TreeViewItem();
                        treeViewItem.Header = GetHeader(t);
                        treeViewItem.ItemsSource=AddItems(Childen);
                        itemCollection.Add(treeViewItem);
                    }
                   else
                    {
                        if (t != null)
                        { 
                            TreeViewItem  treeViewItem = new TreeViewItem();
                            treeViewItem.Header = GetHeader(t);
                            itemCollection.Add(treeViewItem); 
                        }
                    }
                }
            }
             ;
            return itemCollection;
        }
        private string GetNestedField<T>(IEnumerable<FieldInfo> fields)
        {
            foreach (FieldInfo prop in fields)
            {
                if (prop.FieldType != typeof(string) && typeof(IEnumerable<T>).IsAssignableFrom(prop.FieldType))
                {
                   return prop.Name;
                }
            }
            return string.Empty;
        }
        private string GetDictionaryField(IEnumerable<FieldInfo> fields)
        {
            foreach (FieldInfo prop in fields)
            {
                if (prop.FieldType != typeof(string) && typeof(IDictionary<string,string>).IsAssignableFrom(prop.FieldType))
                {
                    return prop.Name;
                }
            }
            return string.Empty;
        }
        private string? GetHeader<T>(T t)
        {
            string header = string.Empty;
            Type type = t.GetType();
            header = type.GetProperty("Name").GetValue(t).ToString();
            var test = type.GetProperties();
            if(type.GetProperty("Text").GetValue(t)!=null)
            {
                header = header + " value=\"" + type.GetProperty("Text").GetValue(t).ToString() + "\"";
            }
            string Atti = GetDictionaryField(type.GetRuntimeFields());
            if (!String.IsNullOrEmpty(Atti))
            {
                Dictionary<string,string> keyValues = (Dictionary<string,string>)type.GetRuntimeField(Atti).GetValue(t);
                if(keyValues!=null)
                {
                    foreach (var item in keyValues)
                    {
                        header = header + " " + item.Key + "=\"" + item.Value+"\"\b";
                    }
                    
                }

            }
            return header;
        }

    }
}
