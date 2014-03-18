namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Storage
{
    internal class Storage
    {
        private static MutationStoragePerTypeStorage _instance;

        public static MutationStoragePerTypeStorage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MutationStoragePerTypeStorage();

                return _instance;
            }
        }

        public static void Clear()
        {
            Instance.Clear();
        }
    }
}