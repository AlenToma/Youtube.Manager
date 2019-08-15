#if NETCOREAPP2_2
using System;
using System.IO;
using System.Linq;
using EntityWorker.Core.Interface;
using EntityWorker.Core.InterFace;

namespace Realm.Of.Y.Manager.Models.Container.DB_models.Rules
{
    public class CategoryRule : IDbRuleTrigger<VideoCategory>
    {
        public void AfterSave(IRepository repository, VideoCategory x, object objectId)
        {

        }

        public void BeforeSave(IRepository repository, VideoCategory x)
        {
            if (string.IsNullOrWhiteSpace(x.Name) && x.State != State.Removed)
                throw new Exception("Name cannot be empty");
            if (x.State != State.Removed && repository.Get<VideoCategory>().Where(a => a.EntityId != x.EntityId && x.User_Id == x.User_Id && x.Name == a.Name).ExecuteAny())
                throw new Exception("Name already exist");
            if (x.EntityId.HasValue)
            {
                var videos = repository.Get<VideoData>().Where(a => a.Category_Id == x.EntityId).Skip(0).Take(5).Execute();
                if (videos.Count() >= 1 && (videos.Count() <= 4 || string.IsNullOrWhiteSpace(x.Logo)))
                {
                    var user = repository.Get<User>().Where(u => u.EntityId == x.User_Id).ExecuteFirstOrDefault();
                    if (!string.IsNullOrEmpty(x.Logo) && File.Exists(Path.Combine(Actions.ImageRootPath, x.Logo)))
                        File.Delete(Path.Combine(Actions.ImageRootPath, x.Logo));
                    var document = new DirectoryManager(Path.Combine(Actions.ImageRootPath, Actions.GenerateUserFolderName(user.Email))).Create();
                    var fName = $"{Guid.NewGuid().ToString("N")}.png";
                    var path = Path.Combine(document.DirectoryPath, fName);

                    var file = Actions.CombineImages(videos.Select(a => a.ThumpUrl).ToArray());
                    File.WriteAllBytes(path, file);
                    x.Logo = Path.Combine(Actions.GenerateUserFolderName(user.Email), fName);

                }
            }

        }

        public void Delete(IRepository repository, VideoCategory x)
        {
            repository
                .GetSqlCommand("Delete Rating Where Category_Id=@CategoryId")
                .AddInnerParameter("CategoryId", x.EntityId)
                .ExecuteNonQuery();

            if (!string.IsNullOrEmpty(x.Logo) && File.Exists(Path.Combine(Actions.ImageRootPath, x.Logo)))
                File.Delete(Path.Combine(Actions.ImageRootPath, x.Logo));
        }
    }
}
#endif