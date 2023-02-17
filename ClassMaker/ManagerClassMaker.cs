using System;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;

namespace ClassMaker
{
    public class ManagerClassMaker
    {
        

        public Type CreateTypeFromTable(IEnumerable<string> FieldsName)
        {
            var aName = new AssemblyName("InternalMapperAssembly");
            var ab = AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            var mb = ab.DefineDynamicModule(aName.Name);
            var typeBuilder = mb.DefineType("TableType", TypeAttributes.Public, typeof(TableBase));

            foreach (var field in FieldsName)
            {
                FieldBuilder FieldBldr=  typeBuilder.DefineField("_"+field, typeof(string), FieldAttributes.Public);
                PropertyBuilder PropBldr =typeBuilder.DefineProperty(field, PropertyAttributes.HasDefault, typeof(string), null);

                MethodAttributes getSetAttr =MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
                MethodBuilder GetPropMthdBldr = typeBuilder.DefineMethod("get_"+field,
                                       getSetAttr,
                                       typeof(string),
                                       Type.EmptyTypes);
                ILGenerator GetIL = GetPropMthdBldr.GetILGenerator();

                GetIL.Emit(OpCodes.Ldarg_0);
                GetIL.Emit(OpCodes.Ldfld, FieldBldr);
                GetIL.Emit(OpCodes.Ret);


                MethodBuilder SetPropMthdBldr = typeBuilder.DefineMethod("set_" + field,
                                       getSetAttr,
                                       null,
                                       new Type[] { typeof(string) });

                ILGenerator SetIL = SetPropMthdBldr.GetILGenerator();

                SetIL.Emit(OpCodes.Ldarg_0);
                SetIL.Emit(OpCodes.Ldarg_1);
                SetIL.Emit(OpCodes.Stfld, FieldBldr);
                SetIL.Emit(OpCodes.Ret);
                PropBldr.SetGetMethod(GetPropMthdBldr);
                PropBldr.SetSetMethod(SetPropMthdBldr);

            }

            return typeBuilder.CreateType();
            
        }
        public object? AddDatatoFields(Type type, string[] fieldsValue)
        {
            if (fieldsValue== null || type==null)
            {
                return null;
            }
            object obj = Activator.CreateInstance(type);
            int i = 0;
            foreach (var item in type.GetFields())
            {
                item.SetValue(obj, fieldsValue[i]);
                i++;
            }
            return obj;
        }

        public List<object>? CreateList(Type type)
        {
            return Activator.CreateInstance(typeof(List<>).MakeGenericType(type)) as List<object>;
        }
        public void AddTolist(object ListType, object Element)
        {
            if (IsList(ListType))
            {
                Type type = ListType.GetType();
                var MethodAdd = type.GetMethod("Add");
                if(MethodAdd != null)
                {
                    MethodAdd.Invoke(ListType, new object[] { Element });
                }  
            }
        }
        private bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
    }
}