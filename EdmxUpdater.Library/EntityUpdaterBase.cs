using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using EdmxUpdater.Schemas;
using System.IO;

namespace EdmxUpdater.Library
{
    public abstract class EntityUpdaterBase
    {
        public void ProcessFile(string edmxFileName)
        {
            var serializer = new XmlSerializer(typeof(TEdmx));

            using (var fs = new FileStream(edmxFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                var edmx = (TEdmx) serializer.Deserialize(fs);

                UpdateEdmx(edmx);

                fs.Position = 0;

                serializer.Serialize(fs, edmx);

                fs.Close();
            }
        }

        protected abstract void UpdateEdmx(TEdmx edmx);
    }
}
