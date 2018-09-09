using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace My.App.Settings
{
    /// <summary>
    /// Description of PropertyGroup.
    /// </summary>
    public class Properties
    {
        Dictionary<string, object> properties = new Dictionary<string, object>();

        public int Count
        {
            get
            {
                return properties.Count;
            }
        }

        public string[] Elements
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (KeyValuePair<string, object> property in properties)
                    ret.Add(property.Key);
                return ret.ToArray();
            }
        }

        public string this[string property]
        {
            get
            {
                return Convert.ToString(Get(property));
            }
            set
            {
                Set(property, value);
            }
        }

        public object Get(string property)
        {
            if (!properties.ContainsKey(property)) {
                return null;
            }
            return properties[property];
        }

        public T Get<T>(string property, T defaultValue)
        {
            if (!properties.ContainsKey(property)) {
                properties.Add(property, defaultValue);
                return defaultValue;
            }

            object o = properties[property];
            if (o is string && typeof(T) != typeof(string)) {                   // (1) string => 非string
                TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
                try {
                    o = c.ConvertFromInvariantString(o.ToString());
                } catch (Exception ex) {
                    MessageBox.Show("Error loading property '" + property + "': " + ex.Message);
                    o = defaultValue;
                }
                properties[property] = o; // store for future look up
            } else if (o is ArrayList && typeof(T).IsArray) {                   // (2) ArrayList => Array   (数组属性)
                ArrayList list = (ArrayList)o;
                Type elementType = typeof(T).GetElementType();
                Array arr = Array.CreateInstance(elementType, list.Count);
                TypeConverter c = TypeDescriptor.GetConverter(elementType);
                try {
                    for (int i = 0; i < arr.Length; ++i) {
                        if (list[i] != null) {
                            arr.SetValue(c.ConvertFromInvariantString(list[i].ToString()), i);
                        }
                    }
                    o = arr;
                } catch (Exception ex) {
                    MessageBox.Show("Error loading property '" + property + "': " + ex.Message);
                    o = defaultValue;
                }
                properties[property] = o; // store for future look up
            } else if (!(o is string) && typeof(T) == typeof(string)) {         // (3) 非string => string
                TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
                if (c.CanConvertTo(typeof(string))) {
                    o = c.ConvertToInvariantString(o);
                } else {
                    o = o.ToString();
                }
            }

            // 可以直接显示转换?
            if (o is T == false) {
                System.Diagnostics.Debug.Assert(false);
            }

            try {
                return (T)o;
            } catch (NullReferenceException) {
                // can happen when configuration is invalid -> o is null and a value type is expected
                return defaultValue;
            }
        }

        public void Set<T>(string property, T value)
        {
            T oldValue = default(T);
            if (!properties.ContainsKey(property)) {
                properties.Add(property, value);
            } else {
                oldValue = Get<T>(property, value);
                properties[property] = value;
            }
            OnPropertyChanged(new PropertyChangedEventArgs(this, property, oldValue, value));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Contains(string property)
        {
            return properties.ContainsKey(property);
        }

        public bool Remove(string property)
        {
            return properties.Remove(property);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Properties:{");
            foreach (KeyValuePair<string, object> entry in properties) {
                sb.Append(entry.Key);
                sb.Append("=");
                sb.Append(entry.Value);
                sb.Append(",");
            }
            sb.Append("}]");
            return sb.ToString();
        }

        public static Properties Load(string fileName)
        {
            if (!File.Exists(fileName)) {
                return null;
            }
            using (XmlTextReader reader = new XmlTextReader(fileName)) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        Properties properties = new Properties();
                        properties.ReadProperties(reader, reader.LocalName);
                        return properties;
                    }
                }
            }
            return null;
        }

        public static Properties ReadFromAttributes(XmlReader reader)
        {
            Properties properties = new Properties();
            if (reader.HasAttributes) {
                for (int i = 0; i < reader.AttributeCount; i++) {
                    reader.MoveToAttribute(i);
                    properties[reader.Name] = reader.Value;
                }
                reader.MoveToElement(); //Moves the reader back to the element node.
            }
            return properties;
        }

        public void ReadProperties(XmlReader reader, string endElement)
        {
            if (reader.IsEmptyElement) {
                return;
            }

            if (!reader.Read())
                return;

            var startElement = reader.LocalName;
            if (startElement != endElement)
                throw new Exception("没有找到属性: " + endElement);

            while (reader.Read()) {
                switch (reader.NodeType) {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == endElement) {
                            return;
                        }
                        break;
                    case XmlNodeType.Element:
                        string propertyName = reader.LocalName;
                        if (propertyName == "Properties") {  // (1) 子Properties
                            propertyName = reader.GetAttribute(0);
                            Properties p = new Properties();
                            p.ReadProperties(reader, "Properties");
                            properties[propertyName] = p;

                        } else if (propertyName == "Array") {  // (2) 基本类型1: 多值类型, ArrayList, name & string values
                            propertyName = reader.GetAttribute(0);
                            properties[propertyName] = ReadArray(reader);

                        } else {  // (3) 基本类型2: 单值类型, name & string value
                            properties[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
                        }
                        break;
                }
            }
        }

        ArrayList ReadArray(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return new ArrayList(0);

            ArrayList l = new ArrayList();
            while (reader.Read()) {
                switch (reader.NodeType) {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == "Array") {
                            return l;
                        }
                        break;
                    case XmlNodeType.Element:
                        l.Add(reader.HasAttributes ? reader.GetAttribute(0) : null);
                        break;
                }
            }
            return l;
        }

        public void WriteProperties(XmlWriter writer)
        {
            foreach (KeyValuePair<string, object> entry in properties) {
                object val = entry.Value;
                if (val is Properties) {
                    writer.WriteStartElement("Properties");
                    writer.WriteAttributeString("name", entry.Key);
                    ((Properties)val).WriteProperties(writer);
                    writer.WriteEndElement();
                } else if (val is Array || val is ArrayList) {
                    writer.WriteStartElement("Array");
                    writer.WriteAttributeString("name", entry.Key);
                    foreach (object o in (IEnumerable)val) {
                        writer.WriteStartElement("Element");
                        WriteValue(writer, o);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                } else {
                    writer.WriteStartElement(entry.Key);
                    WriteValue(writer, val);
                    writer.WriteEndElement();
                }
            }
        }

        void WriteValue(XmlWriter writer, object val)
        {
            if (val != null) {
                if (val is string) {
                    writer.WriteAttributeString("value", val.ToString());
                } else {
                    TypeConverter c = TypeDescriptor.GetConverter(val.GetType());
                    writer.WriteAttributeString("value", c.ConvertToInvariantString(val));
                }
            }
        }

        public void Save(string fileName)
        {
            using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8)) {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("Properties");
                WriteProperties(writer);
                writer.WriteEndElement();
            }
        }
    }



    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    public class PropertyChangedEventArgs : EventArgs
    {
        Properties properties;
        string key;
        object newValue;
        object oldValue;

        /// <returns>
        /// returns the changed property object
        /// </returns>
        public Properties Properties
        {
            get
            {
                return properties;
            }
        }

        /// <returns>
        /// The key of the changed property
        /// </returns>
        public string Key
        {
            get
            {
                return key;
            }
        }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object NewValue
        {
            get
            {
                return newValue;
            }
        }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object OldValue
        {
            get
            {
                return oldValue;
            }
        }

        public PropertyChangedEventArgs(Properties properties, string key, object oldValue, object newValue)
        {
            this.properties = properties;
            this.key = key;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }
}
