﻿using System.Threading;
using System.Threading.Tasks;

namespace ServiceFabric.Azure.Messaging.EventHubs.Processor
{
    /// <summary>
    /// Interface for a checkpoint manager which persists Checkpoints.
    /// </summary>
    public interface ICheckpointManager
    {
        /// <summary>
        /// Does the checkpoint store exist?
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>True if it exists, false if not.</returns>
        Task<bool> CheckpointStoreExistsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Create the checkpoint store if it doesn't exist.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>True if it exists or was created OK, false if not.</returns>
        Task<bool> CreateCheckpointStoreIfNotExistsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Create an uninitialized checkpoint for the given partition.
        /// </summary>
        /// <param name="partitionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Checkpoint> CreateCheckpointIfNotExistsAsync(string partitionId, CancellationToken cancellationToken);

        /// <summary>
        /// Get the checkpoint for the given partition. Returns null if there is no checkpoint or if it is uninitialized.
        /// </summary>
        /// <param name="partitionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Checkpoint> GetCheckpointAsync(string partitionId, CancellationToken cancellationToken);

        /// <summary>
        /// Persist the Checkpoint for the given partition.
        /// </summary>
        /// <param name="partitionId"></param>
        /// <param name="checkpoint"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateCheckpointAsync(string partitionId, Checkpoint checkpoint, CancellationToken cancellationToken);

        Task<bool> AttemptMigration(string partitionId, CancellationToken cancellationToken);
    }
}
