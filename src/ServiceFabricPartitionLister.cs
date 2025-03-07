﻿using System;
using System.Fabric;
using System.Fabric.Query;
using System.Threading.Tasks;

namespace ServiceFabric.Azure.Messaging.EventHubs.Processor
{
    class ServiceFabricPartitionLister : IFabricPartitionLister
    {
        private ServicePartitionList partitionList = null;

        public async Task<int> GetServiceFabricPartitionCount(Uri serviceFabricServiceName)
        {
            using (FabricClient fabricClient = new FabricClient())
            {
                this.partitionList = await fabricClient.QueryManager.GetPartitionListAsync(serviceFabricServiceName).ConfigureAwait(false);
            }
            return this.partitionList.Count;
        }

        public Task<int> GetServiceFabricPartitionOrdinal(Guid serviceFabricPartitionId)
        {
            int ordinal = -1;
            for (int a = 0; a < partitionList.Count; a++)
            {
                if (this.partitionList[a].PartitionInformation.Id == serviceFabricPartitionId)
                {
                    ordinal = a;
                    break;
                }
            }
            return Task.FromResult<int>(ordinal);
        }
    }
}
