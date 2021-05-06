using SimpleFileUpload.Domain.Enums;
using SimpleFileUpload.Domain.Interfaces;

namespace SimpleFileUpload.Domain.Entities
{
    public class UploadSetting : IEntity
    {
        public UploadSettingKeys Key { get; }
        public string Value { get; }

        private UploadSetting()
        {
        }

        public UploadSetting(UploadSettingKeys key, string value) : this()
        {
            Key = key;
            Value = value;
        }
    }
}