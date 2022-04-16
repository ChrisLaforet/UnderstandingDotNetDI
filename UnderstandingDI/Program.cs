using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnderstandingDI
{
	public class UnderstandingDI
	{
		static void Main(string[] args)
		{
			var serviceClasses = new HashSet<Type>();
			serviceClasses.Add(typeof(ServiceA));
			serviceClasses.Add(typeof(ServiceB));
			
			var serviceA = createServiceA(serviceClasses);
			
			Console.WriteLine(serviceA.jobA());
		}

		private static ServiceA createServiceA(ISet<Type> serviceClasses)
		{
			// step 1: create an instance of each service class
			var serviceInstances = new HashSet<object>();
			foreach (var serviceClass in serviceClasses)
			{
				// https://stackoverflow.com/questions/178645/how-does-wcf-deserialization-instantiate-objects-without-calling-a-constructor#179486
				// https://stackoverflow.com/questions/390578/creating-instance-of-type-without-default-constructor-in-c-sharp-using-reflectio
				serviceInstances.Add(FormatterServices.GetUninitializedObject(serviceClass));
			}
			
			// step 2: wire them together
			foreach (var serviceInstance in serviceInstances)
			{
				foreach (var property in serviceInstance.GetType().GetProperties())
				{
					var propertyType = property.PropertyType;
					
					foreach (var matchPartner in serviceInstances)
					{
						// https://stackoverflow.com/questions/708205/c-sharp-object-type-comparison
						var matchPartnerType = matchPartner.GetType();
						
						// find a suitable matching service instance
						if (IsAssignableTo(propertyType, matchPartnerType))
//						if (matchPartnerType.IsAssignableFrom(propertyType))
						{
							property.SetValue(serviceInstance, matchPartner);
							break;
						}
					}
				}
			}
			
			// step 3: from all our service instances, find ServiceA
			foreach (var serviceInstance in serviceInstances)
			{
				if (serviceInstance is ServiceA)
				{
					return serviceInstance as ServiceA;
				}
			}

			return null;
		}

		public static bool IsAssignableTo(Type propertyType, Type instanceType)
		{
			if (propertyType == instanceType)
			{
				return true; // Either both are null or they are the same type
			}

			if (propertyType == null || instanceType == null)
			{
				return false;
			}

			if (propertyType.IsAssignableFrom(instanceType) ||
			    instanceType.IsSubclassOf(propertyType))
			{
				return true;
			}

			return propertyType.BaseType == instanceType.BaseType; // They have the same immediate parent
		}
	}
}