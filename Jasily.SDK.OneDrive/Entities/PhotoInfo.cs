using System;
using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class PhotoInfo : OneDriveObject
    {
        [DataMember(Name = "cameraMake")]
        public string CameraMake { get; set; }

        [DataMember(Name = "cameraModel")]
        public string CameraModel { get; set; }

        [DataMember(Name = "exposureDenominator")]
        public double ExposureDenominator { get; set; }

        [DataMember(Name = "exposureNumerator")]
        public double ExposureNumerator { get; set; }

        [DataMember(Name = "focalLength")]
        public double FocalLength { get; set; }

        [DataMember(Name = "fNumber")]
        public double FNumber { get; set; }

        [DataMember(Name = "takenDateTime")]
        private string TakenDateTime;

        [IgnoreDataMember]
        public DateTime Taken
        {
            get { return this.GetISODateTime(this.TakenDateTime); }
        }
    }
}