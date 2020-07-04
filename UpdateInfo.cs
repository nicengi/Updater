using System;

namespace Nicengi.Update
{
    [Serializable]
    public sealed class UpdateInfo
    {
        #region Properties
        public string SoftwareGuid { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        #endregion

        #region Methods
        public bool NeedToBeUpdated(Version version)
        {
            return new Version(Version) > version;
        }

        public bool NeedToBeUpdated(string version)
        {
            return NeedToBeUpdated(new Version(version));
        }
        #endregion
    }
}
