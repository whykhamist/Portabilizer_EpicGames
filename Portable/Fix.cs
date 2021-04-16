using Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Portable
{
    public class Fix : IFix
    {
        /// <summary>
        /// This method will be used in starting an application instead of the built in method of the Portabilizer.
        /// Throw a NotImplementedExemption to use the default method.
        /// </summary>
        /// <param name="executable"></param>
        /// <param name="args"></param>
        public void ExecuteApplication(string executable, string[] args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function will be executed BEFORE creating Symlinks and applying registry values.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public Task Pre(IProgress<FixProgress> progress = null, CancellationToken cancelToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function will be executed AFTER creating Symlinks and applying registry values.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public Task Post(IProgress<FixProgress> progress = null, CancellationToken cancelToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
