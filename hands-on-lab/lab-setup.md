# CloudCore MBA lab setup guide

This document provides detailed, step-by-step instructions for setting up the required lab resources.

> **IMPORTANT**: For workshop attendees, these steps are meant to be informational. The lab environment will be set up for you prior to the workshop.

- [CloudCore MBA lab setup guide](#cloudcore-mba-lab-setup-guide)
  - [Required resources](#required-resources)
  - [Provision Azure SQL Database](#provision-azure-sql-database)
    - [Configure failover group](#configure-failover-group)
  - [Create Azure Storage account](#create-azure-storage-account)
  - [Provision Key Vault](#provision-key-vault)
  - [Contoso website Web App setup](#contoso-website-web-app-setup)
    - [Set up managed identity for Web App](#set-up-managed-identity-for-web-app)
    - [Configure Web App](#configure-web-app)
    - [Publish Web App](#publish-web-app)
  - [Contoso Call Center admin website setup](#contoso-call-center-admin-website-setup)
    - [Set up managed identity for Admin Web App](#set-up-managed-identity-for-admin-web-app)
    - [Configure Admin Web App](#configure-admin-web-app)
    - [Publish Admin Web App](#publish-admin-web-app)
  - [Payment Gateway API setup](#payment-gateway-api-setup)
    - [Publish Payment Gateway API](#publish-payment-gateway-api)
  - [Offers Web API setup](#offers-web-api-setup)
    - [Set up managed identity for Offers API](#set-up-managed-identity-for-offers-api)
    - [Configure Offers API](#configure-offers-api)
    - [Publish Offers Web API](#publish-offers-web-api)
  - [Update Web App configuration for Contoso website](#update-web-app-configuration-for-contoso-website)

## Required resources

- Application Insights
  - Attach to web app
- App Services
  - Web App for Contoso website
    - Standard (S1)
  - Web App for Call Center admin website
    - Standard (S1) - use same App Service Plan as above.
  - API App for Payment Gateway
    - Standard (S1) - use same App Service Plan as above.
  - API App for Offers Web API
    - Standard (S1) - use same App Service Plan as above.
- Azure SQL Database
  - P1 instance (scale down to S0 when not in use)
  - Read scale-out enabled (required Premium tier)
- Azure Storage account
  - Standard
  - LRS
- Key Vault
  - Use to highlight how secrets can be stored in Key Vault, and then accessed via Web App configuration without any code changes (@Microsoft.KeyVault(SecretUri=...)).

## Provision Azure SQL Database

TODO: Add steps for copying and updating the connection string

### Configure failover group

TODO: Move this down, as the web app creates the database, so need to run the app first, and then create a failover group...

TODO: Figure out if this is really something we want to do, given the fact that lab resources will be shared amongst all lab attendees. Won't really be able to failover without impacting everyone, and only one team would be able to do it.

TODO: Add steps for setting up the failover group.

## Create Azure Storage account

TODO: Add steps for retrieving the connection string

## Provision Key Vault

TODO: Create secrets

- `AzureQueueConnectionString`
  - Paste in the storage account connection string
- `ContosoSportsLeague`
  - Paste in the SQL database connection string

## Contoso website Web App setup

### Set up managed identity for Web App

TODO: Add steps for setting up a managed identity and then add a Key Vault access policy.

### Configure Web App

1. In the Azure portal, navigate to the Web App resource.
2. Select **Configuration** in the left-hand menu.
3. Select **+ New application setting**.
4. In the New application settings dialog enter the following:
   - **Name**: Enter `AzureQueueConnectionString`
   - **Value**: Enter `@Microsoft.KeyVault(SecretUri=<your-key-vault-azure-queue-connection-string-secret-uri>)`, replacing `<your-key-vault-azure-queue-connection-string-secret-uri>` with the Key Vault secret location you copied above.
5. Select **OK**.
6. Next, scroll down to the Connection string section, and select **+New connection string**.
7. In the New connection string dialog enter the following:
   - **Name**: Enter `ContosoSportsLeague`
   - **Value**: Enter `@Microsoft.KeyVault(SecretUri=<your-key-vault-contoso-sports-league-secret-uri>)`, replacing `<your-key-vault-contoso-sports-league-secret-uri>` with the Key Vault secret location you copied above.
   - **Type**: Select **SQLAzure**.

TODO: Turn off FTP and set site to Always On.
TODO: Set to https only on TSL/SSL settings blade.

### Publish Web App

1. Open Visual Studio.
2. Open the `Contoso.App.SportsLeague` solution in Visual Studio.
3. Right-click the `Contoso.Apps.SportsLeague.Web` project, and select **Publish** from the context menu.
4. Select the existing Web App that was created above, and create a new publishing profile.
5. Select **Publish** to deploy the web site to Azure.

## Contoso Call Center admin website setup

### Set up managed identity for Admin Web App

TODO: Add steps for setting up a managed identity and then add a Key Vault access policy.

### Configure Admin Web App

1. In the Azure portal, navigate to the Web App resource.
2. Select **Configuration** in the left-hand menu.
3. Scroll down to the Connection string section, and select **+New connection string**.
4. In the New connection string dialog enter the following:
   - **Name**: Enter `ContosoSportsLeague`
   - **Value**: Enter `@Microsoft.KeyVault(SecretUri=<your-key-vault-contoso-sports-league-secret-uri>)`, replacing `<your-key-vault-contoso-sports-league-secret-uri>` with the Key Vault secret location you copied above.
   - **Type**: Select **SQLAzure**.

TODO: Turn off FTP and set site to Always On.
TODO: Set to https only on TSL/SSL settings blade.

### Publish Admin Web App

1. Open Visual Studio.
2. Open the `Contoso.App.SportsLeague` solution in Visual Studio.
3. Right-click the `Contoso.Apps.SportsLeague.Admin` project, and select **Publish** from the context menu.
4. Select the existing Web App that was created above, and create a new publishing profile.
5. Select **Publish** to deploy the web site to Azure.

## Payment Gateway API setup

TODO: Create API App

### Publish Payment Gateway API

1. Open Visual Studio.
2. Open the `Contoso.App.SportsLeague` solution in Visual Studio.
3. Right-click the `Contoso.Apps.PaymentGateway` project, and select **Publish** from the context menu.
4. Select the existing API App that was created above, and create a new publishing profile.
5. Select **Publish** to deploy the API to Azure.

## Offers Web API setup

TODO: Create API App

### Set up managed identity for Offers API

TODO: Add steps for setting up a managed identity and then add a Key Vault access policy.

### Configure Offers API

1. In the Azure portal, navigate to the API App resource.
2. Select **Configuration** in the left-hand menu.
3. Scroll down to the Connection string section, and select **+New connection string**.
4. In the New connection string dialog enter the following:
   - **Name**: Enter `ContosoSportsLeague`
   - **Value**: Enter `@Microsoft.KeyVault(SecretUri=<your-key-vault-contoso-sports-league-secret-uri>)`, replacing `<your-key-vault-contoso-sports-league-secret-uri>` with the Key Vault secret location you copied above.
   - **Type**: Select **SQLAzure**.

TODO: Set CORS policy
TODO: Turn off FTP and set site to Always On.
TODO: Set to https only on TSL/SSL settings blade.

### Publish Offers Web API

1. Open Visual Studio.
2. Open the `Contoso.App.SportsLeague` solution in Visual Studio.
3. Right-click the `Contoso.Apps.SportsLeague.Offers` project, and select **Publish** from the context menu.
4. Select the existing API App that was created above, and create a new publishing profile.
5. Select **Publish** to deploy the API to Azure.

## Update Web App configuration for Contoso website

In this step, you will add configuration values for the deployed APIs to the main Contoso Sports web application. This configuration values allow the website to interact with the APIs deployed into Azure.

1. Open the main Contoso Sports Web App in the Azure portal.
2. Select **Configuration**.
3. Add a new **Application Setting** with the following values:
   - **Name**: `paymentsAPIUrl`
   - **Value**: Enter the **HTTPS** URL for the Payments API App with `/api/nvp` appended to the end.

    > **Example**: `https://contososportspayments.azurewebsites.net/api/nvp`

4. Add another **Application Setting** with the following values:
   - **Name**: `offersAPIUrl`
   - **Value**: Enter the **HTTPS** URL for the Offers API App with `/api/get` appended to the end

    > **Example**: `https://contososportsoffers.azurewebsites.net/api/get`
