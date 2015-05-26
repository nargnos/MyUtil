// 用随机的内容填充对象, 可设置填充方式
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fill
{
	public sealed class TypeCreator
	{
		private static readonly Type stringType = typeof (string);
		private readonly Dictionary<Type, object[]> createParams = new Dictionary<Type, object[]>();

		public TypeCreator()
		{
			DefaultArrayLength = 10;
		}

		public uint DefaultArrayLength { get; set; }

		public object[] this[Type type]
		{
			set { SetTypeCtorParam(type, value); }
		}

		internal static void CheckType(Type type)
		{
			if (type.IsAbstract || type.IsInterface)
			{
				throw new NotSupportedException(type.Name + "not support");
			}
		}

		public void SetTypeCtorParam(Type type, params object[] args)
		{
			if (args == null || args.Length == 0)
			{
				RemoveCtorParam(type);
				return;
			}
			if (createParams.ContainsKey(type))
			{
				createParams[type] = args;
			}
			else
			{
				createParams.Add(type, args);
			}
		}

		public void RemoveCtorParam(Type type)
		{
			createParams.Remove(type);
		}

		public object CreateInstance(Type type)
		{
			CheckType(type);
			if (type == stringType)
			{
				// 特殊照顾
				return String.Empty;
			}
			if (type.IsArray)
			{
				var elemType = type.GetElementType();
				var rank = new int[type.GetArrayRank()];
				for (var i = 0; i < rank.Length; i++)
				{
					rank[i] = (int) DefaultArrayLength;
				}
				return Array.CreateInstance(elemType, rank);
			}
			if (type.IsGenericType)
			{
				if (type.GenericTypeArguments.Length == 0)
				{
					throw new ArgumentException(type.Name + " T is null");
				}
			}
			return Activator.CreateInstance(type, createParams.ContainsKey(type) ? createParams[type] : null);
		}
	}

	public sealed class RandomSetter
	{
        // FIX: 这里传参方式有问题
		public delegate void Setter(ref object instance);
		private readonly Dictionary<Type, Setter> randomSetters = new Dictionary<Type, Setter>();

		internal RandomSetter()
		{
		}

		public Setter this[Type type]
		{
			set { SetRandomSetter(type, value); }
		}
		public void SetRandomSetter(Type type, Setter setter)
		{
			if (setter == null)
			{
				RemoveSetter(type);
			}
			if (randomSetters.ContainsKey(type))
			{
				randomSetters[type] = setter;
			}
			else
			{
				randomSetters.Add(type, setter);
			}
		}

		public void RemoveSetter(Type type)
		{
			randomSetters.Remove(type);
		}

		internal bool SetRandomValue(ref object instance)
		{
			var tmpType = instance.GetType();
		    if (!randomSetters.ContainsKey(tmpType))
		    {
		        return false;
		    }
		    randomSetters[tmpType](ref instance);
		    return true;
		}
	}

	public sealed class RandomEngine
	{
		private readonly RandomSetter defaultSetter = new RandomSetter();
		private readonly Random random = new Random();

		public RandomEngine()
		{
			Creator = new TypeCreator();
			Setter = new RandomSetter();

			// 设置默认生成器
			defaultSetter[typeof(string)] = (ref object instance) =>
			{
				var tmpBuffer = new byte[8];
				random.NextBytes(tmpBuffer);
				instance = Convert.ToBase64String(tmpBuffer).TrimEnd('='); // 让别人看不出是base64...
			};
			defaultSetter[typeof(byte)] = 
			defaultSetter[typeof(SByte)] = 
				(ref object instance) => instance = (byte)random.Next();

            defaultSetter[typeof(char)] = (ref object instance) => instance = (char)random.Next(0x20,0x7f);

			defaultSetter[typeof(Int64)] =
			defaultSetter[typeof(Int32)] =
			defaultSetter[typeof(Int16)] =
			defaultSetter[typeof(UInt16)] =
			defaultSetter[typeof(UInt32)] =
			defaultSetter[typeof(UInt64)] =
				(ref object instance) => instance = Convert.ChangeType(random.Next(), instance.GetType());
			
			defaultSetter[typeof(bool)] = (ref object instance) => instance = random.Next(2) == 0;

			defaultSetter[typeof(decimal)] =
			defaultSetter[typeof(float)] =
			defaultSetter[typeof(double)] =
				(ref object instance) => instance = Convert.ChangeType(random.NextDouble(), instance.GetType());
            
		}

		public RandomSetter Setter { get; private set; }
		public TypeCreator Creator { get; private set; }

		private void SetUnregistInstance(ref object instance)
		{
			var type = instance.GetType();
			TypeCreator.CheckType(type); // 如果有设置器就按设置器的来, 否则检查失败的类型不能进入这个函数
			if (type.IsValueType)
			{
				if (type.IsGenericType)
				{
					// 泛型结构体, KeyValuePair也来这里,但是KeyValuePair改不了,放行...
					// return;
				}
				// 数字已在设置器中处理
				// 枚举
				if (type.IsEnum)
				{
					var vals = type.GetEnumValues();
					instance = vals.GetValue(random.Next(vals.Length));
					return;
				}
				// 结构体
				SetPropAndFields(instance, type);

				return;
			}
            if (type.IsArray)
            {
                // 数组
                var array = (Array)instance;
                Debug.Assert(array.Length != 0);
                var elemType = type.GetElementType();
                var rank = array.Rank;
                // 取每个维长度
                var length = new int[rank];
                var tmpIndexs = new int[rank];
                length[rank - 1] = 1;
                for (var i = rank - 1; i > 0; i--)
                {
                    length[i - 1] = array.GetLength(i) * length[i];
                }
                for (var i = 0; i < array.Length; i++)
                {
                    var tmpCount = i;
                    for (var j = 0; j < rank; j++)
                    {
                        tmpIndexs[j] = (tmpCount / length[j]);
                        tmpCount = tmpCount % length[j];
                    }
                    array.SetValue(CreateRandomInstance(elemType), tmpIndexs);
                }
                return;
            }
			if (type.IsClass)
			{
				if (type.IsGenericType)
				{
					// 泛型
					if (instance is ICollection)
					{
						// 集合
						var countProp = type.GetProperty("Count");
						var count = (int) countProp.GetValue(instance);
						if (count == 0)
						{
							// 空集合
						    count = (int) Creator.DefaultArrayLength;
						}
						else
						{
							// 有内容,清空再设置
							var clearFunc = type.GetMethod("Clear");
							clearFunc.Invoke(instance, null);
						}
                        AddRandomGenericItems(instance, type, count);
						return;
					}
                    // 元组不能改
				}

				// 类
				SetPropAndFields(instance, type);

				return;
			}

			throw new NotSupportedException(type.Name + " not support");
		}

		private void SetPropAndFields(object instance, Type type)
		{
			foreach (
				var item in
					from prop in type.GetProperties() 
					where prop.CanWrite && prop.SetMethod.GetParameters().Length == 1 
					select prop
					)
			{
				item.SetValue(instance, CreateRandomInstance(item.PropertyType));
			}
            
			foreach (var item in type.GetFields())
			{
				item.SetValue(instance, CreateRandomInstance(item.FieldType));
			}
		}

		private void AddRandomGenericItems(object instance, Type type, int count)
		{
			var addFunc = type.GetMethod("Add");
			for (var i = 0; i < count;i++)
			{
				var param = CreateGenericParams(type);
                try
                {
                    addFunc.Invoke(instance, param);
                }
                catch 
                {
                    continue;
                }
			}
		}

		private object[] CreateGenericParams(Type type)
		{
			var param = new object[type.GenericTypeArguments.Length];
			for (var j = 0; j < type.GenericTypeArguments.Length; j++)
			{
				param[j] = CreateRandomInstance(type.GenericTypeArguments[j]);
			}
			return param;
		}

		public void SetRandomValue(ref object instance)
		{
		    if (Setter.SetRandomValue(ref instance))
		    {
		        return;
		    }
		    // 未注册类型，使用默认随机器设置
		    if (defaultSetter.SetRandomValue(ref instance))
		    {
		        return;
		    }
		    // 未注册类型且非基本类型，可能为结构体、枚举、类、数组等
		    SetUnregistInstance(ref instance);
		}

	    public object CreateRandomInstance(Type type)
		{
			var result = Creator.CreateInstance(type);
			SetRandomValue(ref result);
			return result;
		}
        public object CreateRandomInstance<T>()
        {
            return CreateRandomInstance(typeof(T));
        }
	}

	public static class TestHelper
	{
		public static object CreateTestInstance(this Type type, RandomEngine engine)
		{
			return engine.CreateRandomInstance(type);
		}
        
		public static object CreateInstance(this Type type, TypeCreator creator)
		{
			return creator.CreateInstance(type);
		}
	}
}