namespace PuppeteerAot.Cdp.Messaging
{
    public class DeviceAccessDeviceRequestPromptedResponse
    {
        public string Id { get; set; }

        public DeviceAccessDevice[] Devices { get; set; } = [];

        public class DeviceAccessDevice
        {
            public string Name { get; set; }

            public string Id { get; set; }
        }
    }
}
