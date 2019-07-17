using EntityWorker.Core.Attributes;
namespace Youtube.Manager.Models.Container.DB_models
{
    public abstract class Base_Entity
    {
        [PrimaryKey]
        public long? EntityId { get; set; }

        [ExcludeFromAbstract]
        public State State { get; set; }
    }
}
