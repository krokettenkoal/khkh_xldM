using parsePAX_v2.Attributes;
using parsePAX_v2.Extensions;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace parsePAX_v2.Utils {
    class BinaryMappingLite {
        public static T ReadObject<T>(ArraySegment<byte> bytes, int baseOffset = 0) where T : class
            => ReadObject<T>(bytes, new StreamingOffset(baseOffset));

        public static T ReadObject<T>(ArraySegment<byte> bytes, StreamingOffset offset) where T : class
            => (T)ReadObject(bytes, typeof(T), offset);

        public static object ReadObject(ArraySegment<byte> bytes, Type type, StreamingOffset offset) {
            var instance = type.InvokeMember("", BindingFlags.CreateInstance, null, null, new object[0]);
            foreach (var prop in type.GetProperties()) {
                var dataAtt = prop.GetCustomAttribute<DataAttribute>();
                if (dataAtt != null) {
                    prop.SetValue(instance, ReadObjectOf(bytes, prop.PropertyType, dataAtt, offset));
                }
            }
            return instance;
        }

        private static object ReadObjectOf(ArraySegment<byte> bytes, Type propertyType, DataAttribute dataAtt, StreamingOffset offset) {
            if (propertyType.IsValueType) {
                if (false) { }
                else if (propertyType == typeof(float)) {
                    return bytes.ReadSingle(offset);
                }
                else if (propertyType == typeof(int)) {
                    return bytes.ReadInt32(offset);
                }
                else if (propertyType == typeof(uint)) {
                    return (uint)bytes.ReadInt32(offset);
                }
                else if (propertyType == typeof(short)) {
                    return bytes.ReadInt16(offset);
                }
                else if (propertyType == typeof(ushort)) {
                    return (ushort)bytes.ReadInt16(offset);
                }
                else if (propertyType == typeof(sbyte)) {
                    return (sbyte)bytes.ReadByte(offset);
                }
                else if (propertyType == typeof(byte)) {
                    return bytes.ReadByte(offset);
                }
                else if (propertyType == typeof(Vector4)) {
                    var a = bytes.ReadSingle(offset);
                    var b = bytes.ReadSingle(offset);
                    var c = bytes.ReadSingle(offset);
                    var d = bytes.ReadSingle(offset);
                    return new Vector4(a, b, c, d);
                }
                else {
                    throw new NotSupportedException(propertyType.FullName);
                }
            }
            else if (propertyType.IsArray) {
                var cx = dataAtt.Count;
                if (cx < 0) {
                    throw new ArgumentOutOfRangeException();
                }
                var elementType = propertyType.GetElementType();
                var array = (System.Collections.IList)Array.CreateInstance(elementType, cx);
                for (int x = 0; x < cx; x++) {
                    array[x] = ReadObject(bytes, elementType, offset);
                }
                return array;
            }
            else {

            }
            throw new NotSupportedException(propertyType.FullName);
        }

        public static int GetUnitSize<T>() where T : class {
            var bytes = 0;
            foreach (var prop in typeof(T).GetProperties()) {
                var dataAtt = prop.GetCustomAttribute<DataAttribute>();
                if (dataAtt != null) {
                    bytes += Marshal.SizeOf(prop.PropertyType);
                }
            }
            return bytes;
        }
    }
}
