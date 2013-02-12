// -----------------------------------------------------------------------
// <copyright file="PartFile.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a loaded part file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartFile>))]
    public class PartFile : StorableObject
    {
        private string name, title, author, manufacturer, description, vesselType, module, mesh, category, subCategory, dragModelType;
        private int rescaleFactor, cost, angularDrag, crashTolerance, breakingForce, breakingTorque, maxTemp, explosionPotential;
        private decimal mass, maxDrag, minDrag;
        private IList<PartModule> modules;
        private IList<PartResource> resources;

        /// <summary>
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name", name);
                DisplayName = name;
            }
        }

        /// <summary>
        /// Gets or sets the title. - File Property: title
        /// </summary>
        [JsonProperty("title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title", title);
            }
        }

        /// <summary>
        /// Gets or sets the author. - File Property: author
        /// </summary>
        [JsonProperty("author")]
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged("Author", author);
            }
        }

        /// <summary>
        /// Gets or sets the manufacturer. - File Property: manufacturer
        /// </summary>
        [JsonProperty("manufacturer")]
        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                manufacturer = value;
                OnPropertyChanged("Manufacturer", manufacturer);
            }
        }

        /// <summary>
        /// Gets or sets the description. - File Property: description
        /// </summary>
        [JsonProperty("description")]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description", description);
            }
        }

        /// <summary>
        /// Gets or sets the vessel type. - File Property: vesselType
        /// </summary>
        [JsonProperty("vesselType")]
        public string VesselType
        {
            get
            {
                return vesselType;
            }
            set
            {
                vesselType = value;
                OnPropertyChanged("VesselType", name);
            }
        }

        /// <summary>
        /// Gets or sets the module. - File Property: module
        /// </summary>
        [JsonProperty("module")]
        public string Module
        {
            get
            {
                return module;
            }
            set
            {
                module = value;
                OnPropertyChanged("Module", module);
            }
        }

        /// <summary>
        /// Gets or sets the mesh file name. - File Property: mesh
        /// </summary>
        [JsonProperty("mesh")]
        public string Mesh
        {
            get
            {
                return mesh;
            }
            set
            {
                mesh = value;
                OnPropertyChanged("Mesh", mesh);
            }
        }

        /// <summary>
        /// Gets or sets the rescale factor. - File Property: rescaleFactor
        /// </summary>
        [JsonProperty("rescaleFactor")]
        public int RescaleFactor
        {
            get
            {
                return rescaleFactor;
            }
            set
            {
                rescaleFactor = value;
                OnPropertyChanged("RescaleFactor", rescaleFactor);
            }
        }

        /// <summary>
        /// Gets or sets the cost. - File Property: cost
        /// </summary>
        [JsonProperty("cost")]
        public int Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
                OnPropertyChanged("Cost", cost);
            }
        }

        /// <summary>
        /// Gets or sets the category. - File Property: category
        /// </summary>
        [JsonProperty("category")]
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                OnPropertyChanged("Category", category);
            }
        }

        /// <summary>
        /// Gets or sets the sub category. - File Property: subcategory
        /// </summary>
        [JsonProperty("subcategory")]
        public string SubCategory
        {
            get
            {
                return subCategory;
            }
            set
            {
                subCategory = value;
                OnPropertyChanged("SubCategory", subCategory);
            }
        }

        // Standard Part Parameters
        /// <summary>
        /// Gets or sets the mass. - File Property: mass
        /// </summary>
        [JsonProperty("mass")]
        public decimal Mass
        {
            get
            {
                return mass;
            }
            set
            {
                mass = value;
                OnPropertyChanged("Mass", mass);
            }
        }

        /// <summary>
        /// Gets or sets the drag model type. - File Property: dragModelType
        /// </summary>
        [JsonProperty("dragModelType")]
        public string DragModelType
        {
            get
            {
                return dragModelType;
            }
            set
            {
                dragModelType = value;
                OnPropertyChanged("DragModelType", dragModelType);
            }
        }

        /// <summary>
        /// Gets or sets maximum drag. - File Property: maximum_drag
        /// </summary>
        [JsonProperty("maximum_drag")]
        public decimal MaxDrag
        {
            get
            {
                return maxDrag;
            }
            set
            {
                maxDrag = value;
                OnPropertyChanged("MaxDrag", maxDrag);
            }
        }

        /// <summary>
        /// Gets or set minimum drag. - File Property: minimum_drag
        /// </summary>
        [JsonProperty("minimum_drag")]
        public decimal MinDrag
        {
            get
            {
                return minDrag;
            }
            set
            {
                minDrag = value;
                OnPropertyChanged("MinDrag", minDrag);
            }
        }

        /// <summary>
        /// Gets or sets angular drag. - File Property: angularDrag
        /// </summary>
        [JsonProperty("angularDrag")]
        public int AngularDrag
        {
            get
            {
                return angularDrag;
            }
            set
            {
                angularDrag = value;
                OnPropertyChanged("AngularDrag", angularDrag);
            }
        }

        /// <summary>
        /// Gets or sets the crash tolerance. - File Property: crashTolerance
        /// </summary>
        [JsonProperty("crashTolerance")]
        public int CrashTolerance
        {
            get
            {
                return crashTolerance;
            }
            set
            {
                crashTolerance = value;
                OnPropertyChanged("crashTolerance", crashTolerance);
            }
        }

        /// <summary>
        /// Gets or sets the breaking force. - File Property: breakingForce
        /// </summary>
        [JsonProperty("breakingForce")]
        public int BreakingForce
        {
            get
            {
                return breakingForce;
            }
            set
            {
                breakingForce = value;
                OnPropertyChanged("BreakingForce", breakingForce);
            }
        }

        /// <summary>
        /// Gets or sets the breaking torque. - File Property: breakingTorque
        /// </summary>
        [JsonProperty("breakingTorque")]
        public int BreakingTorque
        {
            get
            {
                return breakingTorque;
            }
            set
            {
                breakingTorque = value;
                OnPropertyChanged("BreakingTorque", breakingTorque);
            }
        }

        /// <summary>
        /// Gets or sets the maximum temp. - File Property: maxTemp
        /// </summary>
        [JsonProperty("maxTemp")]
        public int MaxTemp
        {
            get
            {
                return maxTemp;
            }
            set
            {
                maxTemp = value;
                OnPropertyChanged("MaxTemp", maxTemp);
            }
        }

        /// <summary>
        /// Gets or sets the explosion potential. - File Property: explosionPotential
        /// </summary>
        [JsonProperty("explosionPotential")]
        public int ExplosionPotential
        {
            get
            {
                return explosionPotential;
            }
            set
            {
                explosionPotential = value;
                OnPropertyChanged("ExplosionPotential", explosionPotential);
            }
        }       

        /// <summary>
        /// Gets or sets the module collection. - File Property: MODULE
        /// </summary>
        [JsonProperty("MODULE")]
        public IList<PartModule> Modules
        {
            get
            {
                return modules;
            }
            set
            {
                modules = value;
                OnPropertyChanged("Modules", modules);
            }
        }

        /// <summary>
        /// Gets or sets the resource collection. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources
        {
            get
            {
                return resources;
            }
            set
            {
                resources = value;
                OnPropertyChanged("Resources", resources);
            }
        }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: Implementation
        }
    }
}
