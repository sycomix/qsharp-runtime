// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Quantum.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Blob details.
    /// </summary>
    public partial class BlobDetails
    {
        /// <summary>
        /// Initializes a new instance of the BlobDetails class.
        /// </summary>
        public BlobDetails()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the BlobDetails class.
        /// </summary>
        /// <param name="containerName">The container name.</param>
        /// <param name="blobName">The blob name.</param>
        public BlobDetails(string containerName, string blobName = default(string))
        {
            ContainerName = containerName;
            BlobName = blobName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the container name.
        /// </summary>
        [JsonProperty(PropertyName = "containerName")]
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the blob name.
        /// </summary>
        [JsonProperty(PropertyName = "blobName")]
        public string BlobName { get; set; }

    }
}
