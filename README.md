# AVEVA Data Hub Client Credentials DotNet Sample

**Version:** 1.1.3

[![Build Status](https://dev.azure.com/osieng/engineering/_apis/build/status/product-readiness/OCS/aveva.sample-adh-authentication_client_credentials_simple-dotnet?repoName=osisoft%2Fsample-adh-authentication_client_credentials_simple-dotnet&branchName=main)](https://dev.azure.com/osieng/engineering/_build/latest?definitionId=4393&repoName=osisoft%2Fsample-adh-authentication_client_credentials_simple-dotnet&branchName=main)

Developed against DotNet 6.0.

## Requirements

- DotNet 6.0
- Register a [Client-Credentials Client](https://datahub.connect.aveva.com/clients) in your AVEVA Data Hub tenant and create a client secret to use in the configuration of this sample. ([Video Walkthrough](https://www.youtube.com/watch?v=JPWy0ZX9niU))
  - __NOTE__: This sample only requires the `Tenant Member` role to run successfully 
    - see: ['Authorization Allowed for these roles' in the documentation](https://docs.osisoft.com/bundle/ocs/page/api-reference/tenant/tenant-tenants.html#get-tenant) 
  - It is strongly advised to not elevate the permissions of a client beyond what is necessary.

## About this sample

This sample is meant to be a very simple and straightforward to show how you can use common DotNet library calls to authenticate against ADH.  In a more complete application you should reuse the bearer token as appropriate and only reissue a new token when it is about to timeout.  

Steps:
1. Get needed variables
1. Get the authentication endpoint from the discovery URL
1. Use the client ID and Secret to get the bearer token from the authentication endpoint
1. Test it by calling the base tenant endpoint and making sure a valid response is returned

## Configuring the sample

The sample is configured using the file [appsettings.placeholder.json](appsettings.placeholder.json). Before editing, rename this file to `appsettings.json`. This repository's `.gitignore` rules should prevent the file from ever being checked in to any fork or branch, to ensure credentials are not compromised.

AVEVA Data Hub is secured by obtaining tokens from its identity endpoint. Client credentials clients provide a client application identifier and an associated secret (or key) that are authenticated against the token endpoint. You must replace the placeholders in your `appsettings.json` file with the authentication-related values from your tenant and a client-credentials client created in your ADH tenant.

```json
{
  "Resource": "https://uswe.datahub.connect.aveva.com",
  "ApiVersion": "v1",
  "TenantId": "PLACEHOLDER_REPLACE_WITH_TENANT_ID",
  "ClientId": "PLACEHOLDER_REPLACE_WITH_APPLICATION_IDENTIFIER",
  "ClientSecret": "PLACEHOLDER_REPLACE_WITH_APPLICATION_SECRET"
}
```

## Running the sample

To run this example from the command line once the `appsettings.json` is configured, run

```shell
cd AuthCC
dotnet restore
dotnet run
```

To test this example change directories to the test and run

```shell
cd AuthCCTest
dotnet restore
dotnet test
```

---

Tested against DotNet 6.0

For the main ADH Authentication samples page [ReadMe](https://github.com/osisoft/OSI-Samples-OCS/blob/main/docs/AUTHENTICATION.md)  
For the main ADH samples page [ReadMe](https://github.com/osisoft/OSI-Samples-OCS)  
For the main AVEVA samples page [ReadMe](https://github.com/osisoft/OSI-Samples)
