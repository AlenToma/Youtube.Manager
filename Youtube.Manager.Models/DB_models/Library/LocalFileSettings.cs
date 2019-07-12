using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Youtube.Manager.Models.Container
{
    public class LocalFileSettings
    {
        private string _settingsPath;
        private bool _prevSave;

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual string Image { get; set; }

        public virtual bool PermissionCheck { get; set; }


        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_prevSave)
                SaveChanges();
        }

        public void SaveChanges()
        {
            var str = JsonConvert.SerializeObject(this);
            File.WriteAllText(_settingsPath, str);
        }

        public LocalFileSettings ReadSettings(string settingsPath)
        {
            _prevSave = true;
            _settingsPath = settingsPath;
            if (!File.Exists(settingsPath))
                File.Create(settingsPath).Close();
            var file = File.ReadAllText(settingsPath, Encoding.UTF8);
            if (!string.IsNullOrEmpty(file))
            {
                var localSettings = JsonConvert.DeserializeObject<LocalFileSettings>(file);
                UserName = localSettings.UserName;
                Image = localSettings.Image;
                Password = localSettings.Password;
            }
            _prevSave = false;

            return this;
        }
    }
}
