using Java_Corruptor.UI;
using RTCV.Common;
using RTCV.NetCore;

namespace Java_Corruptor
{
    internal class PluginConnectorEMU : IRoutable
    {
        Java_Corruptor plugin;

        public PluginConnectorEMU(Java_Corruptor _plugin)
        {
            plugin = _plugin;
            LocalNetCoreRouter.registerEndpoint(this, Ep.EMU_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;
            switch (e.message.Type)
            {
                case Commands.SHOW_WINDOW:
                    try
                    {
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            if (S.GET<PluginForm>().IsDisposed)
                            {
                                Java_Corruptor.PluginForm = new(plugin);
                                S.SET(Java_Corruptor.PluginForm);
                            }
                            S.GET<PluginForm>().Show();
                            S.GET<PluginForm>().Activate();
                        });
                        break;
                    }
                    catch
                    {
                        Logging.GlobalLogger.Error($"{nameof(PluginConnectorEMU)}: SHOW_WINDOW failed Reason:\r\n{e}");
                        break;
                    }
                //case Commands.BYTESGET:
                //    try
                //    {
                //        Tuple<long, long, string> objectValue = (Tuple<long, long, string>)message.objectValue;
                //        e.setReturnValue((object)this.GetByteArr(objectValue.Item1, objectValue.Item2, objectValue.Item3));
                //        break;
                //    }
                //    catch (Exception ex)
                //    {
                //        if (!(ex is ArgumentNullException))
                //        {
                //            NullReferenceException referenceException = ex as NullReferenceException;
                //            break;
                //        }
                //        break;
                //    }
            }
            return e.returnMessage;
        }

        //private byte[] GetByteArr(long start, long end, string domain)
        //{
        //    MemoryInterface memoryInterface = MemoryDomains.GetInterface(domain);
        //    return end >= memoryInterface.get_Size() ? (byte[])null : memoryInterface.PeekBytes(start, end, true);
        //}
    }
}