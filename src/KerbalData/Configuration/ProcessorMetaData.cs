// -----------------------------------------------------------------------
// <copyright file="ProcessorMetaData.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    internal class ProcessorMetaData : ProcessorConfig
    {
        private Type model;

        public ProcessorMetaData()
        {
            Init();
        }

        public ProcessorMetaData(object config)
        {
            if (config is ProcessorConfig)
            {
                Init((ProcessorConfig)config);
            }
            else
            {
                throw new KerbalDataException("Invaild configuration object");
            }
        }

        public ProcessorMetaData(ProcessorConfig config)
        {
            Init(config);
        }

        public Type Model
        {
            get
            {
                if (model == null && !String.IsNullOrEmpty(ModelType))
                {
                    model = Type.GetType(ModelType);
                }

                return model;
            }
            private set
            {
                model = value;
            }
        }

        private void Init(ProcessorConfig config = null)
        {
            if (config != null)
            {
                Converter = config.Converter;
                Index = config.Index;
                ModelType = config.ModelType;
                Name = config.Name;
                Serializer = config.Serializer;

                Model = Type.GetType(ModelType);
            }
        }
    }
}
