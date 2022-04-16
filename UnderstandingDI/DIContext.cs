using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace UnderstandingDI
{
	public class DIContext
	{
		// the Dependency Injection Container in formal terms
		// Spring refers to this as the ApplicationContext

		private readonly ISet<object> serviceInstances = new HashSet<object>();

		public DIContext(IEnumerable<Type> serviceClasses)
		{
			// step 1: create an instance of each service class
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
						{
							property.SetValue(serviceInstance, matchPartner);
							break;
						}
					}
				}
			}
		}

		public T GetServiceInstance<T>()
		{
			Type desiredType = typeof(T);
			foreach (var serviceInstance in serviceInstances)
			{
				if (serviceInstance.GetType().Equals(desiredType))
				{
					return (T)serviceInstance;
				}
			}
			
			foreach (var serviceInstance in serviceInstances)
			{
				if (desiredType.IsAssignableFrom(serviceInstance.GetType()))
				{
					return (T)serviceInstance;
				}
			}

			throw new ClassImplementationNotFoundException(desiredType);
		}
		
		private bool IsAssignableTo(Type propertyType, Type instanceType)
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

	public class ClassImplementationNotFoundException: Exception
	{
		public ClassImplementationNotFoundException(Type type) : 
			base("Cannot locate an instance of type " + type.FullName)
		{
		}
	}
}