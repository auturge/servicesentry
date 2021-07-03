<div id="top" align="right"><a href="https://github.com/auturge/servicesentry#top">(home)</a></div>

<h1 align="center">RESTful API</h1>

> [![Work In Progress][WIP-badge]](#top)
>
> This draft is a work in progress and is being provided for information purposes only.
>
> Because it is a work in progress, there are parts that are either missing or will be revised; the content will change. 
>
> Please, do not cite without the author’s permission.

## Table of Contents ##
- [HTTP Review](#http-review)
- [REST Conventions](#rest-conventions)
- [Security](#security)
- [Agent](#agent)
  - [Agent Resources](#agent-resources)
  - [Agent API](#agent-api)
- [Developer Links](#developer-links)

<a href="#top">(go to top)</a>

----

## HTTP Review ##

### HTTP Methods ###

First, a few definitions:
- **safety**: HTTP methods are considered "safe" when they do not alter the server state.
- **idempotence**: HTTP methods are considered "idempotent" when multiple identical requests will have the same outcome, so that it does not matter if the same request is sent once or a hundred times.

| Mathod | Purpose | Safe? | Idempotent |
| --- | --- | --- | --- |
| **GET** | Retrieve a resource | yes | yes |
| **POST** | Create a resource | NO | NO |
| **PUT** | Update (or create, if necessary) a resource | NO | yes |
| **DELETE** | delete a resource | NO | yes |

### HTTP Error Codes ###

For results, HTTP has return codes. These are the basic ones your services should return:
  - 200: Done, it was okay. Generally, your GETs return this code.
  - 201: “Done, and created.” Generally, your POSTs return this code.
  - 204: “Done, and no body.” Generally, your DELETEs return this code.
  - 400: “Client sent me junk, and I’m not going to mess with it.”
  - 401: “Unauthorized, the client should authenticate first.”
  - 403: “Not allowed. You can’t have it because you logged in but don’t have permission to this thing or to delete this thing.”
  - 404: “Can’t find it.”
  - 410: “Marked as deleted.”
  - 451: “The government made me not show it.”

<a href="#top">(go to top)</a>

----

## REST Conventions ##

Remember, the "s" and "t" of "REST" stand for "state transfer".

**REST != CRUD**

The resources used by ServiceSentry are STATES.  states like "starting/started" or "stopping/stopped".

The REST API simply transfers states between client and server.

<a href="#top">(go to top)</a>

----

## Security ##

The API must be secure, and require authentication, since it will be accepting requests from an arbitrary web source.

Each call must be authenticated, but should only be authenticated once.

We don't want the specific service name to be visible to the API user (consider this "sensitive information"). As such, the ID used to reference a service will be either a number or a guid.

<a href="#top">(go to top)</a>

----

## Agent ##

The [Agent][agent] is a long-running Windows service that monitors services, and handles restarting, emailing, etc. The agent uses a subscription-based model where, on startup, it subscribes to a configured set of services, and allows the [Client][client] to see and manage those services.

### Agent Resources ###

#### Minimal State Transfer Object (MSTO) ####

ID and state "should" be all that's needed to MANAGE a service. That makes the "minimal state transfer object" (MSTO) look like this:
```json
{
	id: "2c39aa47-45a8-48e2-8adb-4ef036174da2",
	state: "stopped"
}
```

#### Display State Transfer Object (DSTO) ####

ID, display name, and state "should" be all that's needed to DISPLAY a service:

```json
{
	id: "2c39aa47-45a8-48e2-8adb-4ef036174da2",
	displayName: "World Wide Web Publishing Service"
	state: "stopped"
}
```
#### Group Subscription Objects (GSOs) ####

##### Minimal Group Subscription Object (MinGSO) #####

In order to form a group of services, the [Agent][agent] will need, at a minimum:

```json
{
	displayName: "IIS Services"
}
```

We call the above a "minimal group subscription object" (MinGSO).

##### Maximal Group Subscription Object (MaxGSO) #####

The MinGSO may be expanded into a more robust entity, up to the "maximal group subscription object (MaxGSO)":

```json
{
	displayName: "IIS Services",
	services: [
		"38ba8906-22bd-4ad8-8e96-ab6f5fc53f58", // IISADMIN
		"9a661aa6-d41b-47c0-8142-71289ec62c3b", // MSFTPSVC
		"2c39aa47-45a8-48e2-8adb-4ef036174da2", // W3SVC
	]
}
```

**Note** that the Agent must already be subscribed to each service being added to a group in this way.

#### Service Subscription Objects (SSOs) ####

##### Minimal Service Subscription Object (MinSSO) #####

In order to subscribe to a service, the [Agent][agent] will need, at a minimum:

```json
{
	serviceName: "W3SVC"
}
```

##### Maximal Service Subscription Object (MaxSSO) #####

The MinSSO may be expanded into a more robust entity, up to the "maximal service subscription object (MaxSSO)":

```json
{
	displayName: "IIS",
	group: "IIS Services",
	serviceName: "W3SVC",
	logs: [ "%SystemDrive%\inetpub\logs\LogFiles\*.*" ],
	configs: [ 
		"%WinDir%\System32\Inetsrv\Config\ApplicationHost.config",
		"%WinDir%\System32\Inetsrv\Config\Administration.config",
		"%WinDir%\System32\Inetsrv\Config\Redirection.config"
	]
}
```

<a href="#top">(go to top)</a>

----

### Agent API ###

#### DELETE ####

- DELETE https://&lt;authority&gt;/servicesentry/groups/&lt;id&gt;
  - Dispose of the specified service group, ending monitoring of any included services
  
- DELETE https://&lt;authority&gt;/servicesentry/services/&lt;id&gt;
  - Stop monitoring the given service

#### GET ####

- GET https://&lt;authority&gt;/servicesentry/groups
  - Returns a collection of [MSTO][msto]s representing the service groups

- GET https://&lt;authority&gt;/servicesentry/groups/&lt;id&gt;
  - Returns the [MSTO][msto]s for the specified group

- GET https://&lt;authority&gt;/servicesentry/services
  - Returns a collection of [MSTO][msto]s representing the services

- GET https://&lt;authority&gt;/servicesentry/services/&lt;id&gt;
  - Returns the [MSTO][msto] for the given service

**Note**: If a given call returns one or more [MSTO][msto](s) by default, then appending the `?$expand=display` [OData][odata] System Query Option will cause the call to return the corresponding [DSTO][dsto](s) instead.

- GET https://&lt;authority&gt;/servicesentry/services/&lt;id&gt;/configs
  - Returns a zip of the config files for the given service

- GET https://&lt;authority&gt;/servicesentry/services/&lt;id&gt;/files
  - Returns a zip of the config files _and_ the log files for the given service
    
- GET https://&lt;authority&gt;/servicesentry/services/&lt;id&gt;/logs
  - Returns a zip of the log files for the given service

#### HEAD ####

- HEAD https://&lt;authority&gt;/servicesentry/*   _(i.e., any endpoint)_
  - Returns the headers as if the URL was called with GET, but does not return content

#### OPTIONS ####

- OPTIONS https://&lt;authority&gt;/servicesentry/*   _(i.e., any endpoint)_
  - Return available HTTP methods and other options

#### PATCH ####

- PATCH https://&lt;authority&gt;/servicesentry/groups/&lt;id&gt;
  - BODY: One [MSTO][msto] with the ID of a service group
  - Attempts to toggle the state of the specified group of services

- PATCH https://&lt;authority&gt;/servicesentry/services/&lt;id&gt;
  - BODY: One [MSTO][msto] with the ID of a service
  - Attempts to toggle the state of the specified service

#### POST ####

- POST https://&lt;authority&gt;/servicesentry/groups
  - BODY: One or more [GSO][gso](s)
  - Create one or more service group(s)

- POST https://&lt;authority&gt;/servicesentry/services
  - BODY: One or more [SSO][sso](s)
  - Start monitoring the given service(s)

#### PUT ####

- PUT https://&lt;authority&gt;/servicesentry/groups/&lt;id&gt;
  - BODY: One or more [GSO][gso](s)
  - Replace a service group
  
- PUT https://&lt;authority&gt;/servicesentry/services/&lt;id&gt;
  - BODY: One or more [SSO][sso](s)
  - Replace the subscription information for the given service, and adjust monitoring if necessary




<a href="#top">(go to top)</a>

----

## OData Support ##

- $expand
- $count
- $top
- $skip

<a href="#top">(go to top)</a>

----

## Developer Links ##

- HTTP
  - [MDN Glossary: REST](https://developer.mozilla.org/en-US/docs/Glossary/REST)
  - [restapitutorial.com](https://www.restapitutorial.com/)
  - [restcookbook.com](https://restcookbook.com/)
- ODATA
  - [odata](https://www.odata.org/documentation/)
  - [Create an odata v4 endpoint](https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint)

<a href="#top">(go to top)</a>

----

[WIP-badge]: https://img.shields.io/static/v1?label=WIP:&message=Work-in-Progress&color=blueviolet "This draft is a work in progress and is being provided for information purposes only.&#10;&#10;Because it is a work in progress, there are parts that are either missing or will be revised; the content will change. &#10;&#10;Please, do not cite without the author’s permission."

[agent]: design-overview.md#agent "ServiceSentry Agent"
[client]: design-overview.md#client "ServiceSentry Client"
[$expand]: http://docs.oasis-open.org/odata/odata/v4.01/odata-v4.01-part2-url-conventions.html#sec_SystemQueryOptionexpand
[dsto]: #display-state-transfer-object-DSTO "Display State Transfer Object"
[gso]: #group-subscription-objects-GSOs "Group Subscription Object"
[msto]: #minimal-state-transfer-object-MSTO "Minimal State Transfer Object"
[odata]: https://www.odata.org/documentation/
[sso]: #service-subscription-objects-SSOs "Service Subscription Object"
[warden]: design-overview.md#warden "ServiceSentry Warden"
[wdsto]: #warden-display-state-transfer-object-WDSTO "Warden Display State Transfer Object"
[wsto]: #warden-state-transfer-object-WSTO "Warden State Transfer Object"
