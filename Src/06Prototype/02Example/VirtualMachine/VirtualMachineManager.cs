namespace _06Prototype._02Example.VirtualMachine
{
    // 虚拟机管理器
    public class VirtualMachineManager
    {
        private Dictionary<string, VirtualMachine> virtualMachines = new Dictionary<string, VirtualMachine>();

        public void RegisterVirtualMachine(string name, VirtualMachine prototype)
        {
            virtualMachines[name] = prototype;
        }

        public VirtualMachine CreateVirtualMachine(string name)
        {
            if (virtualMachines.TryGetValue(name, out var prototype))
            {
                return prototype.Clone();
            }

            throw new InvalidOperationException("未找到虚拟机的原型");
        }
    }
}
