# HaSpMan

HaSpMan is a software solution to managing members and accounting/bookkeeping of financial records.

## Running locally

**Prerequisites**:

- .NET 6.0 SDK
- Docker

Start the included [`docker-compose.dev.yaml`](./docker-compose.dev.yaml) using the following command (run it in the root of this repository)

```powershell
docker compose -f docker-compose.dev.yaml up -d
```

This will start a local MSSQL database container, as well as a Keycloak container.
The Keycloak container must be configured for use the first time you run it:

1. Go to [http://localhost:5202/auth](http://localhost:5202/auth)
2. Log in with admin:admin
3. Go to the `DevRealm` by selecting it from the realm drop down in the upper left area, then go to **Clients**
4. Select the `haspman` client, then go to the **Credentials** tab
5. Copy the guid noted under **Secret** to your clipboard
6. Run the following command in the [Web](./Web) subfolder of this repository:
   ```powershell
   dotnet user-secrets set "Oidc:ClientSecret" "<guid>"
   ```

You have now configured everything necessary to run the project locally.

Run `dotnet watch run` in the [Web](./Web) subfolder to start debugging the project. The `dotnet watch` command will watch for any changed files, and rebuild/reload the web application right away.
