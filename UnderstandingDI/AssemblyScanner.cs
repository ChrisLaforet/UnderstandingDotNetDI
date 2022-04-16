using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnderstandingDI
{
	public class AssemblyScanner
	{
		public static IEnumerable<Type> GetAllClassesInCurrentExecutingAssembly()
		{
			return from t in Assembly.GetExecutingAssembly().GetTypes()
				where t.IsClass
				select t;
		} 
		
		public static ISet<Type> GetAllServiceClassesInCurrentExecutingAssembly()
		{
			var serviceClasses = new HashSet<Type>();
			foreach (var possibleServiceClass in GetAllClassesInCurrentExecutingAssembly())
			{
				// is class annotated with [Service]?
				if (possibleServiceClass.GetCustomAttributes(typeof(ServiceAttribute), false).Length != 0)
				{
					serviceClasses.Add(possibleServiceClass);
				}
			}

			return serviceClasses;
		} 
	}
}