using lab2.vehicles;
using lab2.SerializableVehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Serializers
{
    public class TextSerializer : ISerializer
    {
        public void Serialize(string filename, List<Vehicle> vehicles)
        {
            var models = Converter.VehiclesToSerializable(vehicles);
            StringBuilder result = new StringBuilder("[");
            foreach (var model in models)
            {
                SerializeObject(result, model);
            }
            result.Append("]");
            File.WriteAllText(filename, result.ToString());
        }

        private void SerializeObject(StringBuilder str, object obj)
        {
            str.Append(obj.GetType().Name + "{");
            var properties = obj.GetType().GetProperties().Where(x => x.CanWrite && x.CanRead);
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj);
                if (propertyValue != null)
                {
                    str.Append("\"" + property.Name + "\":");
                    var propertyType = property.PropertyType;
                    if (propertyType == typeof(string))
                    {
                        str.Append("\"" + ((string)propertyValue).Replace("\"", "\"\"") + "\"");
                    }
                    else if (propertyType.IsPrimitive || propertyType.IsEnum)
                    {
                        str.Append("\"" + propertyValue.ToString() + "\"");
                    }
                    else
                    {
                        SerializeObject(str, propertyValue);
                    }
                    str.Append(';');
                }
            }
            str.Append("}");
        }

        public List<Vehicle> Deserialize(string filename)
        {
            var models = new List<VehicleSerializableModel>();
            using var reader = new StreamReader(filename);
            if (reader.Read() != '[')
            {
                throw new FileFormatException();
            }
            while (reader.Peek() != ']')
            {
                models.Add((VehicleSerializableModel)DeserializeObject(reader));
            }
            return Converter.SerializableToVehicles(models);
        }

        private string ReadQuotes(StreamReader reader)
        {
            if (reader.Read() != '"')
            {
                throw new FileFormatException();
            }
            StringBuilder stringBuilder = new StringBuilder();
            int c;
            while ((c = reader.Read()) != '"' || reader.Peek() == '"')
            {
                if (c == -1) 
                {
                    throw new FileFormatException();
                }
                if (c == '"')
                {
                    reader.Read();
                }
                stringBuilder.Append((char)c);
            }
            return stringBuilder.ToString();
        }

        private string ReadUntil(char stop, StreamReader reader)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int c;
            while ((c = reader.Peek()) != stop && c != -1)
            {
                stringBuilder.Append((char)reader.Read());
            }
            return stringBuilder.ToString();
        }

        private object DeserializeObject(StreamReader reader)
        {
            string className = ReadUntil('{', reader);
            var type = Type.GetType("lab2.SerializableVehicles." + className) ?? throw new FileFormatException();
            var model = Activator.CreateInstance(type) ?? throw new FileFormatException();
            if (reader.Read() != '{')
            {
                throw new FileFormatException();
            }
            while (reader.Peek() != '}')
            {
                string propertyName = ReadQuotes(reader);
                var property = type.GetProperty(propertyName) ?? throw new FileFormatException();
                if (reader.Read() != ':')
                {
                    throw new FileFormatException();
                }
                object propertyValue = reader.Peek() == '"' ? ReadQuotes(reader) : DeserializeObject(reader);
                SetPropertyValue(model, property, propertyValue);
                if (reader.Read() != ';')
                {
                    throw new FileFormatException();
                }
            }
            reader.Read();
            return model;
        }

        private void SetPropertyValue(object obj, PropertyInfo property, object value)
        {
            if (property.PropertyType == typeof(int))
            {
                property.SetValue(obj, Convert.ToInt32(value as string));
            }
            else if (property.PropertyType == typeof(bool))
            {
                property.SetValue(obj, Convert.ToBoolean(value as string));
            }
            else if (property.PropertyType.IsEnum)
            {
                property.SetValue(obj, Enum.Parse(property.PropertyType, (string)value));
            }
            else
            {
                property.SetValue(obj, value);
            }

        }
    }
}
