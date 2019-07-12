using EntityWorker.Core.Interface;
using EntityWorker.Core.InterFace;
using System;

namespace Youtube.Manager.Models.Container.DB_models.Rules
{
    public class VideoDataRule : IDbRuleTrigger<VideoData>
    {
        public void AfterSave(IRepository repository, VideoData itemDbEntity, object objectId)
        {

        }

        public void BeforeSave(IRepository repository, VideoData itemDbEntity)
        {

            if (string.IsNullOrWhiteSpace(itemDbEntity.Video_Id))
                throw new Exception("Video_Id cant be empty");
        }

        public void Delete(IRepository repository, VideoData itemDbEntity)
        {
            repository.GetSqlCommand("Delete Rating Where VideoData_Id=@VideoDataId")
                       .AddInnerParameter("VideoDataId", itemDbEntity.EntityId)
                       .ExecuteNonQuery();
        }
    }
}
