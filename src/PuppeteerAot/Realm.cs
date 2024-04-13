using System.Text.Json;
using System.Threading.Tasks;

using PuppeteerAot.Input;

namespace PuppeteerAot
{
    public abstract class Realm
    {
        public Realm(TimeoutSettings timeoutSettings)
        {
            TimeoutSettings = timeoutSettings;
        }

        public TaskManager TaskManager { get; set; } = new();

        public TimeoutSettings TimeoutSettings { get; }

        public abstract IEnvironment Environment { get; }

        public abstract Task<IJSHandle> AdoptHandleAsync(IJSHandle handle);

        public abstract Task<IElementHandle> AdoptBackendNodeAsync(object backendNodeId);

        public abstract Task<IJSHandle> TransferHandleAsync(IJSHandle handle);

        public abstract Task<IJSHandle> EvaluateExpressionHandleAsync(string script);

        public abstract Task<IJSHandle> EvaluateFunctionHandleAsync(string script, params object[] args);

        public abstract Task<T> EvaluateExpressionAsync<T>(string script);

        public abstract Task<JsonElement> EvaluateExpressionAsync(string script);

        public abstract Task<T> EvaluateFunctionAsync<T>(string script, params object[] args);

        public abstract Task<JsonElement> EvaluateFunctionAsync(string script, params object[] args);

        public async Task<IJSHandle> WaitForFunctionAsync(string script, WaitForFunctionOptions options, params object[] args)
        {
            using var waitTask = new WaitTask(
                this,
                script,
                false,
                options.Polling,
                options.PollingInterval,
                options.Timeout ?? TimeoutSettings.Timeout,
                options.Root,
                args);

            return await waitTask
                .Task
                .ConfigureAwait(false);
        }

        public async Task<IJSHandle> WaitForExpressionAsync(string script, WaitForFunctionOptions options)
        {
            using var waitTask = new WaitTask(
                this,
                script,
                true,
                options.Polling,
                options.PollingInterval,
                options.Timeout ?? TimeoutSettings.Timeout,
                null, // Root
                null); // args

            return await waitTask
                .Task
                .ConfigureAwait(false);
        }
    }
}
