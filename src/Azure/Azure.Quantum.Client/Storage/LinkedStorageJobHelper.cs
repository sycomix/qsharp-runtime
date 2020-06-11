﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Azure.Quantum.Storage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Bond;
    using global::Azure.Storage.Blobs;
    using Microsoft.Azure.Quantum.Utility;
    using Microsoft.Quantum;
    using Microsoft.Quantum.Models;

    public abstract class LinkedStorageJobHelper : JobStorageHelperBase
    {
        private readonly IStorageOperations storageOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedStorageJobHelper"/> class.
        /// </summary>
        /// <param name="storageOperations">The jobs client.</param>
        public LinkedStorageJobHelper(IStorageOperations storageOperations)
        {
            this.storageOperations = storageOperations;
        }

        /// <summary>
        /// Uploads the job input.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <param name="input">The input.</param>
        /// <param name="protocol">Serialization protocol of the input to upload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Container uri + Input uri without SAS.
        /// </returns>
        public override async Task<(string containerUri, string inputUri)> UploadJobInputAsync(
            string jobId,
            Stream input,
            ProtocolType protocol = ProtocolType.COMPACT_PROTOCOL,
            CancellationToken cancellationToken = default)
        {
            string containerName = GetContainerName(jobId);

            BlobContainerClient containerClient = await this.GetContainerClient(containerName);

            await this.StorageHelper.UploadBlobAsync(
                containerClient,
                Constants.Storage.InputBlobName,
                input,
                protocol,
                cancellationToken);

            Uri inputUri = containerClient
                .GetBlobClient(Constants.Storage.InputBlobName)
                .Uri;

            return (containerClient.Uri.ToString(), inputUri.ToString());
        }

        /// <summary>
        /// Uploads the job program output mapping.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <param name="mapping">The job program output mapping.</param>
        /// <param name="protocol">Serialization protocol of the mapping to upload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Container uri + Mapping uri without SAS.</returns>
        public override async Task<(string containerUri, string mappingUri)> UploadJobMappingAsync(
            string jobId,
            Stream mapping,
            ProtocolType protocol = ProtocolType.COMPACT_PROTOCOL,
            CancellationToken cancellationToken = default)
        {
            string containerName = GetContainerName(jobId);
            BlobContainerClient containerClient = await this.GetContainerClient(containerName);

            await this.StorageHelper.UploadBlobAsync(
                containerClient,
                Constants.Storage.MappingBlobName,
                mapping,
                protocol,
                cancellationToken);

            Uri mappingUri = containerClient
                .GetBlobClient(Constants.Storage.MappingBlobName)
                .Uri;

            return (containerClient.Uri.ToString(), mappingUri.ToString());
        }

        protected override async Task<BlobContainerClient> GetContainerClient(string containerName)
        {
            // Calls the service to get a container SAS Uri
            var containerUri = await storageOperations.SasUriAsync(new BlobDetails { ContainerName = containerName });

            return new BlobContainerClient(new Uri(containerUri.SasUri));
        }
    }
}
