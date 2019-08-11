using EntityWorker.Core.Attributes;
namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
    public abstract class Base_Entity
    {
        [PrimaryKey]
        public long? EntityId { get; set; }

        [ExcludeFromAbstract]
        public State State { get; set; }
    }
}
