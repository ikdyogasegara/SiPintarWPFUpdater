using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public abstract class ObservableObject
    {
        public static DataTable ToDataTable<T>(ObservableCollection<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }


            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
            return tb;
        }
        public static DataSet ToDataSet<T>(ObservableCollection<T> items)
        {
            var table = ToDataTable(items);
            table.TableName = "MainData";

            var ds = new DataSet();
            ds.Tables.Add(table);

            return ds;
        }

        public static DataSet ToDataSetArray(ObservableCollection<DataTable> data)
        {
            var ds = new DataSet();
            foreach (var item in data)
            {
                ds.Tables.Add(item);
            }
            return ds;
        }

        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        public static DataSet ToDataSet(DataTable data, string TbName = "MainData")
        {
            if (data is null)
            {
                return null;
            }
            data.TableName = TbName;
            var ds = new DataSet();
            ds.Tables.Add(data);
            return ds;
        }
    }
}
