using Data.Context;

namespace Business
{
    public class ManagerBase
    {
        private Db _db;
        public ManagerBase() { _db = new Db(); }
        public Db Db { get { return (_db == null) ? new Db() : _db; } }
    }
}