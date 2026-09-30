 IBAS Support WebApp:
Blazor-webapp til IBAS' kundeservice. Kunder kan oprette supporthenvendelser,
og medarbejdere kan se en samlet liste over alle henvendelser. Data gemmes i
Azure CosmosDB.

Til at opsætte cosmosDB har bruget dette:

export RESGRP="IBasSupportRG"
export DBACCOUNT="ibas-db-account-22933"
export DATABASE="IBasSupportDB"
export CONTAINER="ibassupport"

az group create --name $RESGRP --location westeurope

az provider register --namespace Microsoft.DocumentDB --wait

az cosmosdb create --name $DBACCOUNT --resource-group $RESGRP \
--enable-free-tier true \
--locations regionName=francecentral failoverPriority=0 isZoneRedundant=False

az cosmosdb sql database create --account-name $DBACCOUNT \
--resource-group $RESGRP --name $DATABASE

az cosmosdb sql container create --account-name $DBACCOUNT \
--resource-group $RESGRP --database-name $DATABASE \
--name $CONTAINER --partition-key-path "/category"


Jeg nåede det at lave det hele 