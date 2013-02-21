// -----------------------------------------------------------------------
// <copyright file="ProcessorMetaData.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Configuration
{
    using System;

    /// <summary>
    /// Wrapper used for data in the <see cref="ProcessorRegistry"/>
    /// </summary>
    internal class ProcessorMetaData : ProcessorConfig
    {
        private Type model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorMetaData" /> class.
        /// </summary>
        public ProcessorMetaData()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorMetaData" /> class.
        /// </summary>
        /// <param name="config"></param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorMetaData" /> class.
        /// </summary>
        /// <param name="config"></param>
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
            if (config == null)
            {
                return;
            }

            Converter = config.Converter;
            Index = config.Index;
            ModelType = config.ModelType;
            Serializer = config.Serializer;

            Model = Type.GetType(ModelType);  
        }
    }
}
