using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Beeffective.Extensions
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
