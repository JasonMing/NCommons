using System;
using System.Threading;
using System.Threading.Tasks;
using Nextension.Annotations;

namespace Nextension
{
	/// <summary>
	/// The extensions for <see cref="Task"/>.
	/// </summary>
	public static class Tasks
	{
		private static readonly Task CompletedTask = Task.FromResult(default(VoidResult));

		private static readonly Task CanceledTask;

		static Tasks()
		{
			var tcs = new TaskCompletionSource<VoidResult>();
			tcs.SetCanceled();
			CanceledTask = tcs.Task;
		}

		/// <summary>
		/// Gets a no-result completed by success <see cref="Task"/>.
		/// To get the <see cref="Task"/> with result, uses <see cref="Task.FromException{TResult}"/>
		/// </summary>
		public static Task Success
		{
			get { return CompletedTask; }
		}

		/// <summary>
		/// Gets a no-result completed by canceled <see cref="Task"/>.
		/// </summary>
		public static Task Canceled()
		{
			return CanceledTask;
		}

		/// <summary>
		/// Gets a completed by canceled <see cref="Task{T}"/>.
		/// </summary>
		/// <typeparam name="T">The result type.</typeparam>
		/// <returns>The canceled task.</returns>
		public static Task<T> Canceled<T>()
		{
			var tcs = new TaskCompletionSource<T>();
			tcs.SetCanceled();
			return tcs.Task;
		}

		/// <summary>
		/// Gets a no-result completed by faulted <see cref="Task"/>
		/// </summary>
		/// <param name="exception">The exception leads to fault.</param>
		/// <returns>The faulted task.</returns>
		public static Task Faulted([NotNull] Exception exception)
		{
			return Faulted<VoidResult>(exception);
		}

		/// <summary>
		/// Gets a completed by faulted <see cref="Task{T}"/>
		/// </summary>
		/// <typeparam name="T">The result type.</typeparam>
		/// <param name="exception">The exception leads to fault.</param>
		/// <returns>The faulted task.</returns>
		public static Task<T> Faulted<T>([NotNull] Exception exception)
		{
			Ensure.ArgumentNotNull(exception, "exception");

			var tcs = new TaskCompletionSource<T>();
			tcs.SetException(exception);
			return tcs.Task;
		}

		/// <summary>
		/// Gets a new non-disposed <see cref="CancellationToken"/> from the origin <paramref name="token"/>.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <returns>A new token.</returns>
		public static CancellationToken GetSafeToken(this CancellationToken token)
		{
			return GetSafeTokenSource(token).Token;
		}

		/// <summary>
		/// Gets a new non-disposed <see cref="CancellationTokenSource"/> from the origin <paramref name="token"/>.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <returns>A new token source.</returns>
		public static CancellationTokenSource GetSafeTokenSource(this CancellationToken token)
		{
			return CancellationTokenSource.CreateLinkedTokenSource(token, CancellationToken.None);
		}

		[Serializable]
		private struct VoidResult
		{
		}
	}
}
