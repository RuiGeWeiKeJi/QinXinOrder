using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Utility
{
    /// <summary>
    /// 反射 工具类
    /// </summary>
    public class ReflectionUtil
    {
        /// <summary>
        /// 通过反射一个对象，拷贝到另一个对象
        /// </summary>
        /// <param name="sourObject"></param>
        /// <returns></returns>
        public static object ObjectMapToObject( object sourceObject )
        {
            try
            {
                //从源对象中获得类型转化的特性
                object[] converterAttributes = sourceObject.GetType().GetCustomAttributes(typeof(TypeConverterAttribute), true);
                if (converterAttributes == null || converterAttributes.Length < 1) return null;
                //获得源对象的所有属性
                PropertyInfo[] sourceProperties = sourceObject.GetType().GetProperties();
                if (sourceProperties == null || sourceProperties.Length < 1) return null;
                object mapObject = null;
                TypeConverterAttribute converterAttr = converterAttributes[0] as TypeConverterAttribute;
                //创建需要映射的对象实例
                mapObject = Activator.CreateInstance(Type.GetType(converterAttr.ConverterTypeName));
                //遍历所有的属性
                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    //从属性中获得显示特性
                    object[] propertyAttrs = sourceProperty.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    if (propertyAttrs == null || propertyAttrs.Length < 1) continue;
                    DisplayNameAttribute propertyAttr = propertyAttrs[0] as DisplayNameAttribute;
                    //从属性中获得需要映射的类型特性
                    //object[] propertyConvertAttrs = sourceProperty.GetCustomAttributes(typeof(TypeConverterAttribute), true);
                    //TypeConverterAttribute propertyConvertAttr = null;
                    //if (propertyConvertAttrs != null && propertyConvertAttrs.Length > 0)
                    //{
                    //    propertyConvertAttr = propertyAttrs[0] as TypeConverterAttribute;
                    //}

                    string displayNameStr = propertyAttr.DisplayName;
                    string[] displayNames = displayNameStr.Split(',');
                    if (displayNames == null || displayNames.Length < 1) continue;
                    foreach (string displayName in displayNames)
                    {          
                        //从映射对象中获得所有的属性
                        PropertyInfo[] mapProperties = mapObject.GetType().GetProperties();
                        if (mapProperties == null || mapProperties.Length < 1) continue;
                        foreach (PropertyInfo mapProperty in mapProperties)
                        {            
                            if (mapProperty.Name.Equals( displayName.Trim() , StringComparison.OrdinalIgnoreCase) == false)
                            {
                                continue;
                            }
                            if (mapProperty.PropertyType.IsGenericType)
                            {//判断是否是泛型类型
                                //获得源属性值
                                object sourceItem = sourceProperty.GetValue(sourceObject, null);
                                //获得源列表的元素数量
                                int count = (int)sourceItem.GetType().InvokeMember("get_Count", System.Reflection.BindingFlags.Instance
                                                  | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public, null, sourceItem, null);
                                //通过反射创建映射属性
                                object mapItems = Activator.CreateInstance(mapProperty.PropertyType, true);
                                //遍历源集合，赋值给目标对象
                                for (int i = 0; i < count; i++)
                                {
                                    object sourceItemObject = sourceItem.GetType().GetProperty("Item").GetValue(sourceItem, new object[] { i });
                                    Object mapChild = ObjectMapToObject(sourceItemObject);

                                    mapItems.GetType().InvokeMember("Add", System.Reflection.BindingFlags.Public |
                                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance
                                        , null, mapItems, new object[] { mapChild });
                                }
                                mapProperty.SetValue(mapObject, mapItems, null);
                                break;
                            }
                            else if (!mapProperty.PropertyType.IsValueType && mapProperty.PropertyType != typeof(string))
                            {//判断 映射的属性是否是自定义类型 
                                object sourceValue = sourceProperty.GetValue(sourceObject, null);
                                object mapValue = ObjectMapToObject(sourceValue);
                                mapProperty.SetValue(mapObject, mapValue, null);
                            }
                            else
                            {
                                mapProperty.SetValue(mapObject, sourceProperty.GetValue(sourceObject, null), null);
                                break;
                            }
                        }
                    }
                }

                return mapObject;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                return null;
            }
        }
    }
}
