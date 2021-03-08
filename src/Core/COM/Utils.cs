using System;

namespace Core.COM
{
    /// <summary>
    ///     Various utilities that assist in working with COM Objects.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        ///     Retrieve a service instance from the Immersive Shell with a specific GUID.
        /// </summary>
        /// <param name="service">The specific service instance requested.</param>
        /// <typeparam name="TService">The interface of the requested service.</typeparam>
        /// <returns>The instance of <typeparamref name="TService" /> related to <paramref name="service" />.</returns>
        /// <exception cref="InvalidOperationException">The Immersive Shell could not be created.</exception>
        public static TService FromShell<TService>(Guid service)
        {
            var shell = CreateInstance<IServiceProvider>(CLSID.ImmersiveShell);
            if (shell != null)
            {
                shell.QueryService(service, typeof(TService).GUID, out var instance);
                return (TService) instance;
            }

            throw new InvalidOperationException("Could not create shell.");
        }

        /// <summary>
        ///     Retrieve a service instance from the Immersive Shell.
        /// </summary>
        /// <typeparam name="TService">The interface of the requested service.</typeparam>
        /// <returns>An instance of <typeparamref name="TService" />.</returns>
        /// <exception cref="InvalidOperationException">The Immersive Shell could not be created.</exception>
        public static TService FromShell<TService>()
        {
            return FromShell<TService>(typeof(TService).GUID);
        }

        /// <summary>
        ///     Create an instance of <typeparamref name="TInstance" />.
        /// </summary>
        /// <param name="guid">The identifier for the implementation of <typeparamref name="TInstance" />.</param>
        /// <typeparam name="TInstance">The interface of the requested instance.</typeparam>
        /// <returns>An instance of type <typeparamref name="TInstance" />.</returns>
        public static TInstance CreateInstance<TInstance>(Guid guid)
        {
            var vdmType = Type.GetTypeFromCLSID(guid);
            var instance = Activator.CreateInstance(vdmType ?? throw new InvalidOperationException(
                $"Could not get type of {guid}"));
            return (TInstance) instance;
        }

        /// <summary>
        ///     An extension method which allows IObjectArrays to be more naturally indexed.
        /// </summary>
        /// <param name="arr">The Object array to be indexed.</param>
        /// <param name="i">The index of the requested item.</param>
        /// <typeparam name="TElement">The type of the requested item.</typeparam>
        /// <returns>The element at index <paramref name="i" /> of <paramref name="arr" />.</returns>
        public static TElement Get<TElement>(this IObjectArray arr, int i)
        {
            arr.GetAt(
                (uint) i,
                typeof(IVirtualDesktop).GUID,
                out var desktop);
            return (TElement) desktop;
        }
    }
}