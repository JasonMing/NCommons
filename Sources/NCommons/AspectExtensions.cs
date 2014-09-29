//using System;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;

//namespace NCommons
//{
//	public static class AspectExtensions
//	{

//		public static String ExtractKey<TSource, TReturn>(this TSource source, Expression<Func<TSource, TReturn>> call, out Expression<Func<TReturn>> invoker)
//		{
//			Ensure.NotNull(source, "source");
//			Ensure.NotNull(call, "call");

//			var methodCall = call.Body as MethodCallExpression;
//			if (methodCall == null || methodCall.Object != call.Parameters.Single())
//			{
//				throw new NotSupportedException(String.Format("Lambda表达式应仅包含对\"{0}\"的方法调用", typeof(TSource)));
//			}

//			// Resolve key.
//			Object[] arguments;
//			var key = ExtractMethodCallExpressionTrait(methodCall, out arguments);

//			var argumentTypes = methodCall.Method.GetParameters().Select(p => p.ParameterType).ToArray();

//			// Call the origin method by replacing the arguments with the expanded values.
//			invoker = Expression.Lambda<Func<TReturn>>(
//				Expression.Call(
//					Expression.Constant(source),
//					methodCall.Method,
//					arguments.Select((arg, i) => Expression.Constant(arg, argumentTypes[i]))));

//			return key;
//		}

//		public static String ExtractKey<TSource>(this TSource source, Expression<Action<TSource>> call, out Expression<Action> invoker)
//		{
//			Ensure.NotNull(source, "source");
//			Ensure.NotNull(call, "call");

//			var methodCall = call.Body as MethodCallExpression;
//			if (methodCall == null || methodCall.Object != call.Parameters.Single())
//			{
//				throw new NotSupportedException(String.Format("Lambda表达式应仅包含对\"{0}\"的方法调用", typeof(TSource)));
//			}

//			// Resolve key.
//			Object[] arguments;
//			var key = ExtractMethodCallExpressionTrait(methodCall, out arguments);

//			// Call the origin method by replacing the arguments with the expanded values.
//			invoker = Expression.Lambda<Action>(
//				Expression.Call(
//					Expression.Constant(source),
//					methodCall.Method,
//					arguments.Select(Expression.Constant)));

//			return key;
//		}

//		public static String ExtractKey<TSource, TReturn>(this TSource source, Expression<Func<TSource, TReturn>> call, out Func<TReturn> invoker)
//		{
//			Expression<Func<TReturn>> invokerExpression;
//			var key = ExtractKey(source, call, out invokerExpression);
//			invoker = invokerExpression.Compile();
//			return key;
//		}

//		public static String ExtractKey<TSource>(this TSource source, Expression<Action<TSource>> call, out Action invoker)
//		{
//			Expression<Action> invokerExpression;
//			var key = ExtractKey(source, call, out invokerExpression);
//			invoker = invokerExpression.Compile();
//			return key;
//		}

//		public static String ExtractKey<TSource, TReturn>(this TSource source, Expression<Func<TSource, TReturn>> call)
//		{
//			Expression<Func<TReturn>> invoker;
//			return ExtractKey(source, call, out invoker);
//		}

//		public static String ExtractKey<TSource>(this TSource source, Expression<Action<TSource>> call)
//		{
//			Expression<Action> invoker;
//			return ExtractKey(source, call, out invoker);
//		}

//		public static TReturn ExtractKeyAndInvoke<TSource, TReturn>(this TSource source, Expression<Func<TSource, TReturn>> call, out String key)
//		{
//			Func<TReturn> invoker;
//			key = ExtractKey(source, call, out invoker);
//			return invoker();
//		}

//		public static void ExtractKeyAndInvoke<TSource>(this TSource source, Expression<Action<TSource>> call, out String key)
//		{
//			Action invoker;
//			key = ExtractKey(source, call, out invoker);
//			invoker();
//		}

//		public static TReturn ExtractKeyAndInvoke<TSource, TReturn>(this TSource source, Expression<Func<TSource, TReturn>> call, Func<String, Func<TReturn>, TReturn> invoker)
//		{
//			String key;
//			return ExtractKeyAndInvoke(source, call, invoker, out key);
//		}

//		public static void ExtractKeyAndInvoke<TSource>(this TSource source, Expression<Action<TSource>> call, Action<String, Action> invoker)
//		{
//			String key;
//			ExtractKeyAndInvoke(source, call, invoker, out key);
//		}

//		public static TReturn ExtractKeyAndInvoke<TSource, TReturn>(this TSource source, Expression<Func<TSource, TReturn>> call, Func<String, Func<TReturn>, TReturn> invoker, out String key)
//		{
//			Func<TReturn> callInvoker;
//			key = ExtractKey(source, call, out callInvoker);
//			return invoker(key, callInvoker);
//		}

//		public static void ExtractKeyAndInvoke<TSource>(this TSource source, Expression<Action<TSource>> call, Action<String, Action> invoker, out String key)
//		{
//			Action callInvoker;
//			key = ExtractKey(source, call, out callInvoker);
//			invoker(key, callInvoker);
//		}


//		/// <summary>
//		/// Return the calling trait.
//		/// </summary>
//		/// <example>
//		/// Calling method:
//		///   <code>String Foo.Invoker(String arg1, Boolean arg2)</code>
//		/// With arguments:
//		///   <code>Invoker("foo", true);</code>
//		/// Resolve key is:
//		///   <code>"Foo.Invoker(String:foo, Boolean:true)"</code>
//		/// </example>
//		/// <param name="callExpression"></param>
//		/// <param name="evaluatedArguments"></param>
//		/// <returns></returns>
//		private static String ExtractMethodCallExpressionTrait(MethodCallExpression callExpression, out Object[] evaluatedArguments)
//		{
//			var argValueExprs = callExpression.Arguments;

//			// Expand (evaluate) argument expressions.
//			var newArrExpr = Expression.NewArrayInit(typeof(Object), argValueExprs
//				.Select(argExpr => argExpr.Type.IsValueType
//					? Expression.TypeAs(argExpr, typeof(Object))	// Boxing the ValueTypes.
//					: argExpr));
//			evaluatedArguments = Expression.Lambda<Func<Object[]>>(newArrExpr).Compile().Invoke();

//			// Resolve key.
//			var key = ResolveKey(callExpression.Method, evaluatedArguments);

//			return key;
//		}

//		private static String ResolveKey(MethodBase method, Object[] argValues)
//		{
//			var target = method.DeclaringType;
//			var name = method.Name;
//			var parameterTypes = method.GetParameters()
//				.Select(p => p.ParameterType.FullName)
//				.ToArray();
//			var parameterValues = argValues;

//			var parameters = String.Join(", ", Enumerable.Range(0, parameterTypes.Length).Select(i => String.Format("{0}:{1}", parameterTypes[i], parameterValues[i])));

//			return String.Format("{0}.{1}({2})", target, name, parameters);
//		}

//	}
//}
