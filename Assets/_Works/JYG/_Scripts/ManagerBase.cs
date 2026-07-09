namespace JYG._Scripts
{
    public class ManagerBase : Description, IManager
    {
        public IManager Manager => this;
        protected ManagerInitializer managerInitializer;

        public virtual void Initialize(ManagerInitializer initializer)
        {
            managerInitializer = initializer;
        }
    }
}
